/**
 * Description: Weaving plan extension
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.AssemblyHandler
{
    using System;
    using System.Linq;
    using Aspect;
    using Batch;

    internal static class WeavingPlanExtension
    {
        public static bool IsEmpty(this IWeavingPlan plan)
        {
            return !plan.PointCut.Any();
        }

        public static bool IsEmpty(this IPropertyWeavingPlan plan)
        {
            return plan.GetterPlan.IsEmpty() && plan.SetterPlan.IsEmpty();
        }

        public static bool NeedToStoreReturnValueAsLocalVariable(this IWeavingPlan plan)
        {
            if (plan == null)
            {
                throw new ArgumentNullException("plan");
            }
            var pointCut = plan.PointCut;
            return pointCut.Contains(JoinPoint.Exception) || pointCut.Contains(JoinPoint.Exit);
        }

        public static bool NeedHasException(this IWeavingPlan plan)
        {
            if (plan == null)
            {
                throw new ArgumentNullException("plan");
            }
            return plan.ParameterFlag.NeedHasException();
        }

        public static bool NeedExecutionParameter(this IWeavingPlan plan)
        {
            if (plan == null)
            {
                throw new ArgumentNullException("plan");
            }
            return plan.ParameterFlag.NeedExecutionParameter();
        }

        public static bool NeedExceptionParameter(this IWeavingPlan plan)
        {
            if (plan == null)
            {
                throw new ArgumentNullException("plan");
            }
            return plan.ParameterFlag.NeedExceptionParameter();
        }

        public static bool NeedReturnParameter(this IWeavingPlan plan)
        {
            if (plan == null)
            {
                throw new ArgumentNullException("plan");
            }
            return plan.ParameterFlag.NeedReturnParameter();
        }
    }
}
