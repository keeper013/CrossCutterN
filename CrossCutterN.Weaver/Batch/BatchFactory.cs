/**
 * Description: Batch factory
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Batch
{
    using System.Reflection;
    using Aspect;

    internal static class BatchFactory
    {
        public static IAdviceInfo InitializeAdviceInfo(MethodInfo method, string builderId, AdviceParameterFlag parameterFlag, SwitchStatus switchStatus)
        {
            return new AdviceInfo(method, builderId, parameterFlag, switchStatus);
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
