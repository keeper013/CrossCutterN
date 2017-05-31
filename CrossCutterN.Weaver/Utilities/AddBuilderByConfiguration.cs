/**
 * Description: build aspect builder from configuration
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Utilities
{
    using System;
    using System.Collections.Generic;
    using Aspect;
    using Aspect.Configuration;
    using Aspect.Builder;

    public static class AddBuilderByConfiguration
    {
        public static void AddBuilder(this ICanAddAspectBuilder weaver, ConcernAttributeAspectBuilderElement element)
        {
            if (weaver == null)
            {
                throw new ArgumentNullException("weaver");
            }
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            var factoryType = element.FactoryMethod.Type;
            var methodName = element.FactoryMethod.Method;
            var classConcernAttributeType = element.FactoryMethod.ClassConcernAttributeType;
            var methodConcernAttributeType = element.FactoryMethod.MethodConcernAttributeType;
            var propertyConcernAttributeType = element.FactoryMethod.PropertyConcernAttributeType;
            var noConcernAttributeType = element.FactoryMethod.NoConcernAttributeType;
            var builder = (IWriteOnlyConcernAttributeAspectBuilder)factoryType.GetMethod(methodName).Invoke(null,
                new object[] { classConcernAttributeType, methodConcernAttributeType, propertyConcernAttributeType, noConcernAttributeType });
            SetBuilderOptions(builder, element.Options);
            SetBuilderSwitchable(builder, element.Switch);
            SetAndAddBuilder(weaver, builder, element);
        }

        public static void AddBuilder(this ICanAddAspectBuilder weaver, NameExpressionAspectBuilderElement element)
        {
            if (weaver == null)
            {
                throw new ArgumentNullException("weaver");
            }
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            var factoryType = element.FactoryMethod.Type;
            var methodName = element.FactoryMethod.Method;
            var includes = new List<string>();
            var includeCount = element.FactoryMethod.Includes.Count;
            for (var i = 0; i < includeCount; i++)
            {
                includes.Add(element.FactoryMethod.Includes[i].Expression);
            }
            List<string> excludes = null;
            var excludeCount = element.FactoryMethod.Excludes.Count;
            if (excludeCount > 0)
            {
                excludes = new List<string>();
                for (var i = 0; i < excludeCount; i++)
                {
                    excludes.Add(element.FactoryMethod.Excludes[i].Expression);
                }
            }
            var builder = (IWriteOnlyNameExpressionAspectBuilder)factoryType.GetMethod(methodName).Invoke(null, new object[] { includes, excludes });
            SetBuilderOptions(builder, element.Options);
            SetBuilderSwitchable(builder, element.Switch);
            SetAndAddBuilder(weaver, builder, element);
        }

        private static void SetAndAddBuilder(this ICanAddAspectBuilder weaver, IWriteOnlyAspectBuilder builder, AspectBuilderElement element)
        {
            
            var jointPointCount = element.PointCut.Count;
            var sequenceDict = new Dictionary<JoinPoint, int>();
            for (var i = 0; i < jointPointCount; i++)
            {
                var settings = element.PointCut[i];
                builder.SetAdvice(settings.JoinPoint, settings.ToMethodInfo());
                sequenceDict[settings.JoinPoint] = settings.Sequence;
            }
            weaver.AddAspectBuilder(element.Id, builder.Convert(), sequenceDict);
        }

        private static void SetBuilderOptions(this IWriteOnlyConcernAttributeAspectBuilder builder, ConcernAttributeAspectBuilderOptionsElement options)
        {
            if (options != null)
            {
                builder.ConcernConstructor = options.ConcernConstructor;
                builder.ConcernInstance = options.ConcernInstance;
                builder.ConcernInternal = options.ConcernInternal;
                builder.ConcernMethod = options.ConcernMethod;
                builder.ConcernPrivate = options.ConcernPrivate;
                builder.ConcernPropertyGetter = options.ConcernPropertyGetter;
                builder.ConcernPropertySetter = options.ConcernPropertySetter;
                builder.ConcernProtected = options.ConcernProtected;
                builder.ConcernPublic = options.ConcernPublic;
                builder.ConcernStatic = options.ConcernStatic;
                builder.PointCutAtEntry = options.PointCutAtEntry;
                builder.PointCutAtException = options.PointCutAtException;
                builder.PointCutAtExit = options.PointCutAtExit;
            }
        }

        private static void SetBuilderSwitchable(this ISwitchableAspectBuilder builder, SwitchElement element)
        {
            builder.SwitchStatus = element.Status;
        }

        private static void SetBuilderOptions(this IWriteOnlyNameExpressionAspectBuilder builder, AspectBuilderOptionsElement options)
        {
            if (options != null)
            {
                builder.ConcernConstructor = options.ConcernConstructor;
                builder.ConcernInstance = options.ConcernInstance;
                builder.ConcernInternal = options.ConcernInternal;
                builder.ConcernMethod = options.ConcernMethod;
                builder.ConcernPrivate = options.ConcernPrivate;
                builder.ConcernPropertyGetter = options.ConcernPropertyGetter;
                builder.ConcernPropertySetter = options.ConcernPropertySetter;
                builder.ConcernProtected = options.ConcernProtected;
                builder.ConcernPublic = options.ConcernPublic;
                builder.ConcernStatic = options.ConcernStatic;
            }
        }
    }
}
