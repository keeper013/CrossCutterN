/**
 * Description: Batch factory
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Batch
{
    using System.Reflection;

    internal static class BatchFactory
    {
        public static IAdviceInfo InitializeAdviceInfo(MethodInfo method, string builderId, AdviceParameterFlag parameterFlag)
        {
            return new AdviceInfo(method, builderId, parameterFlag);
        }

        public static ICanAddJoinPoint InitializeWeavingPlan()
        {
            return new WeavingPlan();
        }

        public static ICanAddAspectBuilder InitializeBatch()
        {
            return new WeavingBatch();
        }

        public static IPropertyWeavingPlan InitializePropertyWeavingPlan(IWeavingPlan getterPlan, IWeavingPlan setterPlan)
        {
            return new PropertyWeavingPlan(getterPlan, setterPlan);
        }
    }
}
