/**
 * Description: Batch factory
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Batch
{
    using System.Reflection;

    internal static class BatchFactory
    {
        public static IAdviceInfo InitializeAdviceInfo(MethodInfo method, string builderId, AdviceParameterFlag parameterFlag, bool? switchValue)
        {
            return new AdviceInfo(method, builderId, parameterFlag, switchValue);
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
