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
    using Utilities;

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

        public static bool NeedContentVariable(this IWeavingPlan plan)
        {
            if (plan == null)
            {
                throw new ArgumentNullException("plan");
            }
            return plan.ParameterFlag.HasContextParameter();
        }

        public static bool NeedHasExceptionVariable(this IWeavingPlan plan)
        {
            if (plan == null)
            {
                throw new ArgumentNullException("plan");
            }
            return plan.ParameterFlag.HasHasExceptionParameter();
        }

        public static bool NeedExecutionVariable(this IWeavingPlan plan)
        {
            if (plan == null)
            {
                throw new ArgumentNullException("plan");
            }
            return plan.ParameterFlag.HasExecutionParameter();
        }

        public static bool NeedExceptionVariable(this IWeavingPlan plan)
        {
            if (plan == null)
            {
                throw new ArgumentNullException("plan");
            }
            return plan.ParameterFlag.HasExceptionParameter();
        }

        public static bool NeedReturnVariable(this IWeavingPlan plan)
        {
            if (plan == null)
            {
                throw new ArgumentNullException("plan");
            }
            return plan.ParameterFlag.HasReturnParameter();
        }
    }
}
