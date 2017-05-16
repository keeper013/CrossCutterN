/**
 * Description: Aspect builder factory
 * Author: David Cui
 */
namespace CrossCutterN.Aspect.Builder
{
    using System;
    using System.Collections.Generic;

    public static class AspectBuilderFactory
    {
        public static IWriteOnlyConcernAttributeAspectBuilder InitializeConcernAttributeAspectBuilder(Type classConcernAttributeType, 
            Type methodConcernAttributeType, Type propertyConcernAttributeType, Type noConcernAttributeType)
        {
            return new ConcernAttributeAspectBuilder(classConcernAttributeType, methodConcernAttributeType, 
                propertyConcernAttributeType, noConcernAttributeType);
        }

        public static IWriteOnlyNameExpressionAspectBuilder InitializeNameExpressionAspectBuilder(
            ICollection<string> includes, ICollection<string> excludes)
        {
            return new NameExpressionAspectBuilder(includes, excludes);
        }
    }
}
