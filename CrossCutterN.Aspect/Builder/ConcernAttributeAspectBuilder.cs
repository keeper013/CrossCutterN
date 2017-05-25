/**
 * Description: concern attribute aspect builder
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Builder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Concern;
    using Advice.Concern;

    internal sealed class ConcernAttributeAspectBuilder : SwitchableAspectBuilderWithDefaultOptions, IWriteOnlyConcernAttributeAspectBuilder
    {
        private bool _pointCutAtEntry;
        private bool _pointCutAtException;
        private bool _pointCutAtExit;

        public Type ClassConcernAttributeType { get; private set; }
        public Type MethodConcernAttributeType { get; private set; }
        public Type PropertyConcernAttributeType { get; private set; }
        public Type NoConcernAttributeType { get; private set; }

        public bool PointCutAtEntry
        {
            private get
            {
                return _pointCutAtEntry;
            }
            set
            {
                ReadOnly.Assert(false);
                _pointCutAtEntry = value;
            }
        }

        public bool PointCutAtException
        {
            private get
            {
                return _pointCutAtException;
            }
            set
            {
                ReadOnly.Assert(false);
                _pointCutAtException = value;
            }
        }

        public bool PointCutAtExit
        {
            private get
            {
                return _pointCutAtExit;
            }
            set
            {
                ReadOnly.Assert(false);
                _pointCutAtExit = value;
            }
        }

        public ConcernAttributeAspectBuilder(Type classConcernAttributeType, Type methodConcernAttributeType, 
            Type propertyConcernAttributeType, Type noConcernAttributeType)
        {
            if(classConcernAttributeType!= null && !classConcernAttributeType.IsSubclassOf(typeof(ClassConcernAttribute))) {
                throw new ArgumentException(
                    string.Format("Class concern attribute type must be a sub class of {0}", typeof(ClassConcernAttribute).FullName), 
                    "classConcernAttributeType");
            }
            if (methodConcernAttributeType != null && !methodConcernAttributeType.IsSubclassOf(typeof(MethodConcernAttribute)))
            {
                throw new ArgumentException(
                    string.Format("Method concern attribute type must be a sub class of {0}", typeof(MethodConcernAttribute).FullName),
                    "methodConcernAttributeType");
            }
            if (propertyConcernAttributeType != null && !propertyConcernAttributeType.IsSubclassOf(typeof(PropertyConcernAttribute)))
            {
                throw new ArgumentException(
                    string.Format("Property concern attribute type must be a sub class of {0}", typeof(PropertyConcernAttribute).FullName),
                    "propertyConcernAttributeType");
            }
            if (noConcernAttributeType != null && !noConcernAttributeType.IsSubclassOf(typeof(NoConcernAttribute)))
            {
                throw new ArgumentException(
                    string.Format("No concern attribute type must be a sub class of {0}", typeof(NoConcernAttribute).FullName),
                    "noConcernAttributeType");
            }
            if(classConcernAttributeType == null && methodConcernAttributeType == null && propertyConcernAttributeType == null)
            {
                throw new ArgumentException("classConcernAttributeType, methodConcernAttributeType and propertyConcernAttributeType can't all be null.");
            }
            ClassConcernAttributeType = classConcernAttributeType;
            MethodConcernAttributeType = methodConcernAttributeType;
            PropertyConcernAttributeType = propertyConcernAttributeType;
            NoConcernAttributeType = noConcernAttributeType;

            PointCutAtEntry = true;
            PointCutAtException = true;
            PointCutAtExit = true;
        }

        public override IAspect GetAspect(IMethod method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }
            ReadOnly.Assert(true);
            var aspect = AspectFactory.InitializeAspect(Switch);

            // NoConcernAttribute takes priority
            if (!NoConcern(method.CustomAttributes))
            {
                var joniPoints = Enum.GetValues(typeof (JoinPoint)).Cast<JoinPoint>().ToList();
                foreach (var joinPoint in joniPoints)
                {
                    if (ContainsJoinPoint(joinPoint) && CutAt(joinPoint, method))
                    {
                        aspect.SetJoinPointAdvice(joinPoint, GetAdvice(joinPoint));
                    }
                }
            }
            return aspect.Convert();
        }

        public override IPropertyAspect GetAspect(IProperty property)
        {
            if(property == null)
            {
                throw new ArgumentNullException("property");
            }
            ReadOnly.Assert(true);
            var getterAspect = AspectFactory.InitializeAspect(Switch);
            var setterAspect = AspectFactory.InitializeAspect(Switch);
            if (!NoConcern(property.CustomAttributes))
            {
                var joinPoints = Enum.GetValues(typeof(JoinPoint)).Cast<JoinPoint>().ToList();
                if (!NoConcern(property.GetterCustomAttributes))
                {
                    foreach (var joinPoint in joinPoints)
                    {
                        if (ContainsJoinPoint(joinPoint) && CutAtGetter(joinPoint, property))
                        {
                            getterAspect.SetJoinPointAdvice(joinPoint, GetAdvice(joinPoint));
                        }
                    }
                }
                if (!NoConcern(property.SetterCustomAttributes))
                {
                    foreach (var joinPoint in joinPoints)
                    {
                        if (ContainsJoinPoint(joinPoint) && CutAtSetter(joinPoint, property))
                        {
                            setterAspect.SetJoinPointAdvice(joinPoint, GetAdvice(joinPoint));
                        }
                    }
                }
            }
            return AspectFactory.InitializePropertyAspect(getterAspect.Convert(), setterAspect.Convert());
        }

        private bool CutAt(JoinPoint joinPoint, IMethod method)
        {
            switch(joinPoint)
            {
                case JoinPoint.Entry:
                    return CutAt(method, MethodConcernAttribute.PointCutAtEntryPropertyName,
                           ClassConcernAttribute.PointCutAtEntryPropertyName, PointCutAtEntry);
                case JoinPoint.Exception:
                    return CutAt(method, MethodConcernAttribute.PointCutAtExceptionPropertyName,
                           ClassConcernAttribute.PointCutAtExceptionPropertyName, PointCutAtException);
                case JoinPoint.Exit:
                    return CutAt(method, MethodConcernAttribute.PointCutAtExitPropertyName,
                           ClassConcernAttribute.PointCutAtExitPropertyName, PointCutAtExit);
            }
            throw new ArgumentException(string.Format("Unsupported join point type {0}", joinPoint), "joinPoint");
        }

        private bool CutAt(IMethod method, string methodJoinPointProperty, string classJoinPointProperty, bool pointCutDefault)
        {
            // MethodConcernAttribute overwrites ClassConcernAttribute
            if (MethodConcernAttributeType != null)
            {
                var concernMethodAttribute =
                method.CustomAttributes.SingleOrDefault(
                    attr => attr.TypeName.Equals(MethodConcernAttributeType.FullName));
                if (concernMethodAttribute != null)
                {
                    // by default concern entry, exception or exit
                    return BooleanAttributePropertyValue(concernMethodAttribute, methodJoinPointProperty, pointCutDefault);
                }
            }

            // ClassConcernAttribute checking
            if (ClassConcernAttributeType != null)
            {
                var concernClassAttribute =
                method.ClassCustomAttributes.SingleOrDefault(
                    attr => attr.TypeName.Equals(ClassConcernAttributeType.FullName));
                if (concernClassAttribute != null)
                {
                    // check if method is to be concerned
                    if (!BooleanAttributePropertyValue(concernClassAttribute,
                        ClassConcernAttribute.ConcernMethodPropertyName, ConcernMethod))
                    {
                        return false;
                    }

                    // check for pointcuts settings
                    // by default concern entry, exception or exit
                    if (!BooleanAttributePropertyValue(concernClassAttribute, classJoinPointProperty, pointCutDefault))
                    {
                        return false;
                    }

                    // check for constructor
                    if (method.IsConstructor &&
                        !BooleanAttributePropertyValue(concernClassAttribute,
                        ClassConcernAttribute.ConcernConstructorPropertyName, ConcernConstructor))
                    {
                        return false;
                    }

                    // check for method instance or static
                    // by default concern instance methods
                    if (method.IsInstance &&
                        !BooleanAttributePropertyValue(concernClassAttribute, ClassConcernAttribute.ConcernInstancePropertyName,
                                                       ConcernInstance))
                    {
                        return false;
                    }
                    // by default concern static methods
                    if (!method.IsInstance &&
                        !BooleanAttributePropertyValue(concernClassAttribute, ClassConcernAttribute.ConcernStaticPropertyName, ConcernStatic))
                    {
                        return false;
                    }
                    return AccessibilityCheckWithClassAttribute(method.Accessibility, concernClassAttribute);
                }
            }
            return false;
        }

        private bool CutAtGetter(JoinPoint joinPoint, IProperty property)
        {
            switch (joinPoint)
            {
                case JoinPoint.Entry:
                    return CutAtGetter(property, MethodConcernAttribute.PointCutAtEntryPropertyName, PropertyConcernAttribute.PointCutAtEntryPropertyName,
                        ClassConcernAttribute.PointCutAtEntryPropertyName, PointCutAtEntry);
                case JoinPoint.Exception:
                    return CutAtGetter(property, MethodConcernAttribute.PointCutAtExceptionPropertyName, PropertyConcernAttribute.PointCutAtExceptionPropertyName,
                           ClassConcernAttribute.PointCutAtExceptionPropertyName, PointCutAtException);
                case JoinPoint.Exit:
                    return CutAtGetter(property, MethodConcernAttribute.PointCutAtExitPropertyName, PropertyConcernAttribute.PointCutAtExitPropertyName,
                           ClassConcernAttribute.PointCutAtExitPropertyName, PointCutAtExit);
            }
            throw new ArgumentException(string.Format("Unsupported join point type {0}", joinPoint), "joinPoint");
        }

        private bool CutAtSetter(JoinPoint joinPoint, IProperty property)
        {
            switch (joinPoint)
            {
                case JoinPoint.Entry:
                    return CutAtSetter(property, MethodConcernAttribute.PointCutAtEntryPropertyName, PropertyConcernAttribute.PointCutAtEntryPropertyName,
                        ClassConcernAttribute.PointCutAtEntryPropertyName, PointCutAtEntry);
                case JoinPoint.Exception:
                    return CutAtSetter(property, MethodConcernAttribute.PointCutAtExceptionPropertyName, PropertyConcernAttribute.PointCutAtExceptionPropertyName,
                           ClassConcernAttribute.PointCutAtExceptionPropertyName, PointCutAtException);
                case JoinPoint.Exit:
                    return CutAtSetter(property, MethodConcernAttribute.PointCutAtExitPropertyName, PropertyConcernAttribute.PointCutAtExitPropertyName,
                           ClassConcernAttribute.PointCutAtExitPropertyName, PointCutAtExit);
            }
            throw new ArgumentException(string.Format("Unsupported join point type {0}", joinPoint), "joinPoint");
        }

        private bool CutAtGetter(IProperty property, string methodJoinPointProperty, string propertyJoinPointProperty, string classJoinPointProperty, bool pointCutDefault)
        {
            if (MethodConcernAttributeType != null)
            {
                // MethodConcernAttribute overwrites PropertyConcernAttribute
                var concernMethodAttribute = property.GetterCustomAttributes.SingleOrDefault(
                        attr => attr.TypeName.Equals(MethodConcernAttributeType.FullName));
                if (concernMethodAttribute != null)
                {
                    // by default concern entry, exception or exit
                    return BooleanAttributePropertyValue(concernMethodAttribute, methodJoinPointProperty, pointCutDefault);
                }
            }
            
            // PropertyConcernAttribute overwrites ClassConcernAttribute
            if (NoConcern(property.CustomAttributes))
            {
                return false;
            }
            
            if (PropertyConcernAttributeType != null)
            {
                var concernPropertyAttribute = property.CustomAttributes.FirstOrDefault(
                attr => attr.TypeName.Equals(PropertyConcernAttributeType.FullName));
                if (concernPropertyAttribute != null)
                {
                    return BooleanAttributePropertyValue(concernPropertyAttribute, PropertyConcernAttribute.ConcernGetterPropertyName, ConcernPropertyGetter) &&
                        BooleanAttributePropertyValue(concernPropertyAttribute, propertyJoinPointProperty, pointCutDefault);
                }
            }

            // ClassConcernAttribute checking
            if (ClassConcernAttributeType != null)
            {
                var concernClassAttribute = property.ClassCustomAttributes.SingleOrDefault(attr => attr.TypeName.Equals(ClassConcernAttributeType.FullName));
                if (concernClassAttribute != null)
                {
                    // check if property getter is to be concerned
                    if (!BooleanAttributePropertyValue(concernClassAttribute,
                        ClassConcernAttribute.ConcernPropertyGetterPropertyName, ConcernPropertyGetter))
                    {
                        return false;
                    }

                    // check for pointcuts settings
                    // by default concern entry, exception or exit
                    if (!BooleanAttributePropertyValue(concernClassAttribute, classJoinPointProperty, pointCutDefault))
                    {
                        return false;
                    }

                    // check for method instance or static
                    // by default concern instance methods
                    if (property.IsInstance &&
                        !BooleanAttributePropertyValue(concernClassAttribute, ClassConcernAttribute.ConcernInstancePropertyName,
                                                       ConcernInstance))
                    {
                        return false;
                    }
                    // by default concern static methods
                    if (!property.IsInstance &&
                        !BooleanAttributePropertyValue(concernClassAttribute, ClassConcernAttribute.ConcernStaticPropertyName, ConcernStatic))
                    {
                        return false;
                    }
                    return property.GetterAccessibility.HasValue && AccessibilityCheckWithClassAttribute(property.GetterAccessibility.Value, concernClassAttribute);
                }
            }
            return false;
        }

        private bool CutAtSetter(IProperty property, string methodJoinPointProperty, string propertyJoinPointProperty, string classJoinPointProperty, bool pointCutDefault)
        {
            // MethodConcernAttribute overwrites PropertyConcernAttribute
            if (MethodConcernAttributeType != null)
            {
                var concernMethodAttribute = property.SetterCustomAttributes.SingleOrDefault(
                    attr => attr.TypeName.Equals(MethodConcernAttributeType.FullName));
                if (concernMethodAttribute != null)
                {
                    // by default concern entry, exception or exit
                    return BooleanAttributePropertyValue(concernMethodAttribute, methodJoinPointProperty, pointCutDefault);
                }
            }

            // PropertyConcernAttribute overwrites ClassConcernAttribute
            if (NoConcern(property.CustomAttributes))
            {
                return false;
            }
            if (PropertyConcernAttributeType != null)
            {
                var concernPropertyAttribute = property.CustomAttributes.FirstOrDefault(
                attr => attr.TypeName.Equals(PropertyConcernAttributeType.FullName));
                if (concernPropertyAttribute != null)
                {
                    return BooleanAttributePropertyValue(concernPropertyAttribute, PropertyConcernAttribute.ConcernSetterPropertyName, ConcernPropertySetter) &&
                        BooleanAttributePropertyValue(concernPropertyAttribute, propertyJoinPointProperty, pointCutDefault);
                }
            }

            // ClassConcernAttribute checking
            if (ClassConcernAttributeType != null)
            {
                var concernClassAttribute = property.ClassCustomAttributes.SingleOrDefault(attr => attr.TypeName.Equals(ClassConcernAttributeType.FullName));
                if (concernClassAttribute != null)
                {
                    // check if property setter is to be concerned
                    if (!BooleanAttributePropertyValue(concernClassAttribute,
                        ClassConcernAttribute.ConcernPropertySetterPropertyName, ConcernPropertySetter))
                    {
                        return false;
                    }

                    // check for pointcuts settings
                    // by default concern entry, exception or exit
                    if (!BooleanAttributePropertyValue(concernClassAttribute, classJoinPointProperty, pointCutDefault))
                    {
                        return false;
                    }

                    // check for method instance or static
                    // by default concern instance methods
                    if (property.IsInstance &&
                        !BooleanAttributePropertyValue(concernClassAttribute, ClassConcernAttribute.ConcernInstancePropertyName,
                                                       ConcernInstance))
                    {
                        return false;
                    }
                    // by default concern static methods
                    if (!property.IsInstance &&
                        !BooleanAttributePropertyValue(concernClassAttribute, ClassConcernAttribute.ConcernStaticPropertyName, ConcernStatic))
                    {
                        return false;
                    }
                    return property.SetterAccessibility.HasValue && AccessibilityCheckWithClassAttribute(property.SetterAccessibility.Value, concernClassAttribute);
                }
            }
            return false;
        }

        private bool NoConcern(IEnumerable<ICustomAttribute> attributes)
        {
            return NoConcernAttributeType != null &&
                attributes.Any(attr => attr.TypeName.Equals(NoConcernAttributeType.FullName));
        }

        private bool AccessibilityCheckWithClassAttribute(Accessibility accessibility, ICustomAttribute concernClass)
        {
            // check for accessibility
            // by default not concern internal, private or protected methods, only concern public methods
            if (accessibility == Accessibility.Internal &&
                !BooleanAttributePropertyValue(concernClass, ClassConcernAttribute.ConcernInternalPropertyName, ConcernInternal))
            {
                return false;
            }
            if (accessibility == Accessibility.Private &&
                !BooleanAttributePropertyValue(concernClass, ClassConcernAttribute.ConcernPrivatePropertyName, ConcernPrivate))
            {
                return false;
            }
            if (accessibility == Accessibility.Protected &&
                !BooleanAttributePropertyValue(concernClass, ClassConcernAttribute.ConcernProtectedPropertyName, ConcernProtected))
            {
                return false;
            }
            if (accessibility == Accessibility.Public &&
                !BooleanAttributePropertyValue(concernClass, ClassConcernAttribute.ConcernPublicPropertyName, ConcernPublic))
            {
                return false;
            }
            return true;
        }

        private static bool BooleanAttributePropertyValue(ICustomAttribute attribute, string propertyName,
                                                        bool defaultValue)
        {
            if (defaultValue)
            {
                return !attribute.HasProperty(propertyName) || System.Convert.ToBoolean(attribute.GetProperty(propertyName).Value);
            }
            return attribute.HasProperty(propertyName) && System.Convert.ToBoolean(attribute.GetProperty(propertyName).Value);
        }
    }
}
