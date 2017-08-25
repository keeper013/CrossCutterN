// <copyright file="ConcernAttributeAspect.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Aspect
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CrossCutterN.Aspect.Metadata;
    using CrossCutterN.Base.Concern;

    /// <summary>
    /// Aspect which recognizes injection target methods and properties by attributes.
    /// </summary>
    internal sealed class ConcernAttributeAspect : SwitchableAspectWithDefaultOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConcernAttributeAspect"/> class.
        /// </summary>
        /// <param name="classConcernAttributeType">Type of class concern attribute.</param>
        /// <param name="methodConcernAttributeType">Type of method concern attribute.</param>
        /// <param name="propertyConcernAttributeType">Type of property concern attribute.</param>
        /// <param name="noConcernAttributeType">Type of no concern attribute.</param>
        public ConcernAttributeAspect(
            Type classConcernAttributeType,
            Type methodConcernAttributeType,
            Type propertyConcernAttributeType,
            Type noConcernAttributeType)
        {
            if (classConcernAttributeType != null && !classConcernAttributeType.IsSubclassOf(typeof(ConcernClassAttribute)))
            {
                throw new ArgumentException(
                    $"Class concern attribute type must be a sub class of {typeof(ConcernClassAttribute).FullName}",
                    "classConcernAttributeType");
            }

            if (methodConcernAttributeType != null && !methodConcernAttributeType.IsSubclassOf(typeof(ConcernMethodAttribute)))
            {
                throw new ArgumentException(
                    $"Method concern attribute type must be a sub class of {typeof(ConcernMethodAttribute).FullName}",
                    "methodConcernAttributeType");
            }

            if (propertyConcernAttributeType != null && !propertyConcernAttributeType.IsSubclassOf(typeof(ConcernPropertyAttribute)))
            {
                throw new ArgumentException(
                    $"Property concern attribute type must be a sub class of {typeof(ConcernPropertyAttribute).FullName}",
                    "propertyConcernAttributeType");
            }

            if (noConcernAttributeType != null && !noConcernAttributeType.IsSubclassOf(typeof(NoConcernAttribute)))
            {
                throw new ArgumentException(
                    $"No concern attribute type must be a sub class of {typeof(NoConcernAttribute).FullName}",
                    "noConcernAttributeType");
            }

            if (classConcernAttributeType == null && methodConcernAttributeType == null && propertyConcernAttributeType == null)
            {
                throw new ArgumentException("classConcernAttributeType, methodConcernAttributeType and propertyConcernAttributeType can't all be null.");
            }

            ClassConcernAttributeType = classConcernAttributeType;
            MethodConcernAttributeType = methodConcernAttributeType;
            PropertyConcernAttributeType = propertyConcernAttributeType;
            NoConcernAttributeType = noConcernAttributeType;
        }

        /// <summary>
        /// Gets type of class concern attribute.
        /// </summary>
        public Type ClassConcernAttributeType { get; private set; }

        /// <summary>
        /// Gets type of method concern attribute.
        /// </summary>
        public Type MethodConcernAttributeType { get; private set; }

        /// <summary>
        /// Gets type of property concern attribute.
        /// </summary>
        public Type PropertyConcernAttributeType { get; private set; }

        /// <summary>
        /// Gets type of no concern attribute.
        /// </summary>
        public Type NoConcernAttributeType { get; private set; }

        /// <inheritdoc/>
        public override bool ConcernConstructor
        {
            protected get => base.ConcernConstructor;
            set
            {
                if (ClassConcernAttributeType == null)
                {
                    throw new InvalidOperationException("ClassConcernAttributeType is empty, concern option is meaningless.");
                }

                base.ConcernConstructor = value;
            }
        }

        /// <inheritdoc/>
        public override bool ConcernInstance
        {
            protected get => base.ConcernInstance;
            set
            {
                if (ClassConcernAttributeType == null)
                {
                    throw new InvalidOperationException("ClassConcernAttributeType is empty, concern option is meaningless.");
                }

                base.ConcernInstance = value;
            }
        }

        /// <inheritdoc/>
        public override bool ConcernInternal
        {
            protected get => base.ConcernInternal;
            set
            {
                if (ClassConcernAttributeType == null)
                {
                    throw new InvalidOperationException("ClassConcernAttributeType is empty, concern option is meaningless.");
                }

                base.ConcernInternal = value;
            }
        }

        /// <inheritdoc/>
        public override bool ConcernMethod
        {
            protected get => base.ConcernMethod;
            set
            {
                if (ClassConcernAttributeType == null)
                {
                    throw new InvalidOperationException("ClassConcernAttributeType is empty, concern option is meaningless.");
                }

                base.ConcernMethod = value;
            }
        }

        /// <inheritdoc/>
        public override bool ConcernPrivate
        {
            protected get => base.ConcernPrivate;
            set
            {
                if (ClassConcernAttributeType == null)
                {
                    throw new InvalidOperationException("ClassConcernAttributeType is empty, concern option is meaningless.");
                }

                base.ConcernPrivate = value;
            }
        }

        /// <inheritdoc/>
        public override bool ConcernPropertyGetter
        {
            protected get => base.ConcernPropertyGetter;
            set
            {
                if (ClassConcernAttributeType == null)
                {
                    throw new InvalidOperationException("ClassConcernAttributeType is empty, concern option is meaningless.");
                }

                base.ConcernPropertyGetter = value;
            }
        }

        /// <inheritdoc/>
        public override bool ConcernPropertySetter
        {
            protected get => base.ConcernPropertySetter;
            set
            {
                if (ClassConcernAttributeType == null)
                {
                    throw new InvalidOperationException("ClassConcernAttributeType is empty, concern option is meaningless.");
                }

                base.ConcernPropertySetter = value;
            }
        }

        /// <inheritdoc/>
        public override bool ConcernProtected
        {
            protected get => base.ConcernProtected;
            set
            {
                if (ClassConcernAttributeType == null)
                {
                    throw new InvalidOperationException("ClassConcernAttributeType is empty, concern option is meaningless.");
                }

                base.ConcernProtected = value;
            }
        }

        /// <inheritdoc/>
        public override bool ConcernPublic
        {
            protected get => base.ConcernPublic;
            set
            {
                if (ClassConcernAttributeType == null)
                {
                    throw new InvalidOperationException("ClassConcernAttributeType is empty, concern option is meaningless.");
                }

                base.ConcernPublic = value;
            }
        }

        /// <inheritdoc/>
        public override bool ConcernStatic
        {
            protected get => base.ConcernStatic;
            set
            {
                if (ClassConcernAttributeType == null)
                {
                    throw new InvalidOperationException("ClassConcernAttributeType is empty, concern option is meaningless.");
                }

                base.ConcernStatic = value;
            }
        }

        /// <inheritdoc/>
        public override bool CanApplyTo(IMethod method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            ReadOnly.Assert(true);

            if (NoConcern(method.CustomAttributes))
            {
                return false;
            }

            // MethodConcernAttribute overwrites ClassConcernAttribute
            if (MethodConcernAttributeType != null)
            {
                var concernMethodAttribute =
                method.CustomAttributes.SingleOrDefault(
                    attr => attr.TypeName.Equals(MethodConcernAttributeType.FullName));
                if (concernMethodAttribute != null)
                {
                    return true;
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
                    if (!BooleanAttributePropertyValue(
                        concernClassAttribute,
                        ConcernClassAttribute.ConcernMethodPropertyName,
                        ConcernMethod))
                    {
                        return false;
                    }

                    // check for constructor
                    if (method.IsConstructor &&
                        !BooleanAttributePropertyValue(
                            concernClassAttribute,
                            ConcernClassAttribute.ConcernConstructorPropertyName,
                            ConcernConstructor))
                    {
                        return false;
                    }

                    // check for method instance or static
                    // by default concern instance methods
                    if (method.IsInstance &&
                        !BooleanAttributePropertyValue(
                            concernClassAttribute,
                            ConcernClassAttribute.ConcernInstancePropertyName,
                            ConcernInstance))
                    {
                        return false;
                    }

                    // by default concern static methods
                    if (!method.IsInstance &&
                        !BooleanAttributePropertyValue(concernClassAttribute, ConcernClassAttribute.ConcernStaticPropertyName, ConcernStatic))
                    {
                        return false;
                    }

                    return AccessibilityCheckWithClassAttribute(method.Accessibility, concernClassAttribute);
                }
            }

            return false;
        }

        /// <inheritdoc/>
        public override PropertyConcern CanApplyTo(IProperty property)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }

            ReadOnly.Assert(true);
            var concern = PropertyConcern.None;
            if (!NoConcern(property.CustomAttributes))
            {
                if (CanApplyToGetter(property))
                {
                    concern = concern.ConcernGetter();
                }

                if (CanApplyToSetter(property))
                {
                    concern = concern.ConcernSetter();
                }
            }

            return concern;
        }

        private static bool BooleanAttributePropertyValue(ICustomAttribute attribute, string propertyName, bool defaultValue)
        {
            if (defaultValue)
            {
                return !attribute.HasProperty(propertyName) || System.Convert.ToBoolean(attribute.GetProperty(propertyName).Value);
            }

            return attribute.HasProperty(propertyName) && System.Convert.ToBoolean(attribute.GetProperty(propertyName).Value);
        }

        private bool CanApplyToGetter(IProperty property)
        {
            if (NoConcern(property.GetterCustomAttributes))
            {
                return false;
            }

            if (MethodConcernAttributeType != null)
            {
                // MethodConcernAttribute overwrites PropertyConcernAttribute
                var concernMethodAttribute = property.GetterCustomAttributes.SingleOrDefault(
                    attr => attr.TypeName.Equals(MethodConcernAttributeType.FullName));
                if (concernMethodAttribute != null)
                {
                    return true;
                }
            }

            // PropertyConcernAttribute overwrites ClassConcernAttribute
            if (NoConcern(property.CustomAttributes))
            {
                return false;
            }

            if (PropertyConcernAttributeType != null)
            {
                var concernPropertyAttribute = property.CustomAttributes.SingleOrDefault(
                    attr => attr.TypeName.Equals(PropertyConcernAttributeType.FullName));
                if (concernPropertyAttribute != null)
                {
                    return BooleanAttributePropertyValue(concernPropertyAttribute, ConcernPropertyAttribute.ConcernGetterPropertyName, ConcernPropertyGetter);
                }
            }

            // ClassConcernAttribute checking
            if (ClassConcernAttributeType != null)
            {
                var concernClassAttribute = property.ClassCustomAttributes.SingleOrDefault(attr => attr.TypeName.Equals(ClassConcernAttributeType.FullName));
                if (concernClassAttribute != null)
                {
                    // check if property getter is to be concerned
                    if (!BooleanAttributePropertyValue(
                        concernClassAttribute,
                        ConcernClassAttribute.ConcernPropertyGetterPropertyName,
                        ConcernPropertyGetter))
                    {
                        return false;
                    }

                    // check for method instance or static
                    // by default concern instance
                    if (property.IsInstance &&
                        !BooleanAttributePropertyValue(
                            concernClassAttribute,
                            ConcernClassAttribute.ConcernInstancePropertyName,
                            ConcernInstance))
                    {
                        return false;
                    }

                    // by default concern static
                    if (!property.IsInstance &&
                        !BooleanAttributePropertyValue(concernClassAttribute, ConcernClassAttribute.ConcernStaticPropertyName, ConcernStatic))
                    {
                        return false;
                    }

                    return property.GetterAccessibility.HasValue && AccessibilityCheckWithClassAttribute(property.GetterAccessibility.Value, concernClassAttribute);
                }
            }

            return false;
        }

        private bool CanApplyToSetter(IProperty property)
        {
            if (NoConcern(property.SetterCustomAttributes))
            {
                return false;
            }

            // MethodConcernAttribute overwrites PropertyConcernAttribute
            if (MethodConcernAttributeType != null)
            {
                var concernMethodAttribute = property.SetterCustomAttributes.SingleOrDefault(
                    attr => attr.TypeName.Equals(MethodConcernAttributeType.FullName));
                if (concernMethodAttribute != null)
                {
                    return true;
                }
            }

            // PropertyConcernAttribute overwrites ClassConcernAttribute
            if (NoConcern(property.CustomAttributes))
            {
                return false;
            }

            if (PropertyConcernAttributeType != null)
            {
                var concernPropertyAttribute = property.CustomAttributes.SingleOrDefault(
                    attr => attr.TypeName.Equals(PropertyConcernAttributeType.FullName));
                if (concernPropertyAttribute != null)
                {
                    return BooleanAttributePropertyValue(concernPropertyAttribute, ConcernPropertyAttribute.ConcernSetterPropertyName, ConcernPropertySetter);
                }
            }

            // ClassConcernAttribute checking
            if (ClassConcernAttributeType != null)
            {
                var concernClassAttribute = property.ClassCustomAttributes.SingleOrDefault(attr => attr.TypeName.Equals(ClassConcernAttributeType.FullName));
                if (concernClassAttribute != null)
                {
                    // check if property setter is to be concerned
                    if (!BooleanAttributePropertyValue(
                        concernClassAttribute,
                        ConcernClassAttribute.ConcernPropertySetterPropertyName,
                        ConcernPropertySetter))
                    {
                        return false;
                    }

                    // check for method instance or static
                    // by default concern instance
                    if (property.IsInstance &&
                        !BooleanAttributePropertyValue(
                            concernClassAttribute,
                            ConcernClassAttribute.ConcernInstancePropertyName,
                            ConcernInstance))
                    {
                        return false;
                    }

                    // by default concern static
                    if (!property.IsInstance &&
                        !BooleanAttributePropertyValue(concernClassAttribute, ConcernClassAttribute.ConcernStaticPropertyName, ConcernStatic))
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
                !BooleanAttributePropertyValue(concernClass, ConcernClassAttribute.ConcernInternalPropertyName, ConcernInternal))
            {
                return false;
            }

            if (accessibility == Accessibility.Private &&
                !BooleanAttributePropertyValue(concernClass, ConcernClassAttribute.ConcernPrivatePropertyName, ConcernPrivate))
            {
                return false;
            }

            if (accessibility == Accessibility.Protected &&
                !BooleanAttributePropertyValue(concernClass, ConcernClassAttribute.ConcernProtectedPropertyName, ConcernProtected))
            {
                return false;
            }

            if (accessibility == Accessibility.Public &&
                !BooleanAttributePropertyValue(concernClass, ConcernClassAttribute.ConcernPublicPropertyName, ConcernPublic))
            {
                return false;
            }

            return true;
        }
    }
}
