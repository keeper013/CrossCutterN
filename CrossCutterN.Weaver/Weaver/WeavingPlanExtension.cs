// <copyright file="WeavingPlanExtension.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Weaver
{
    using System;
    using System.Linq;
    using CrossCutterN.Aspect.Aspect;

    /// <summary>
    /// Weaving plan extension.
    /// </summary>
    internal static class WeavingPlanExtension
    {
        /// <summary>
        /// Checks whether a weaving plan is empty.
        /// </summary>
        /// <param name="plan">The weaving plan to be checked.</param>
        /// <returns>True if the weaving plan is empty, false if not.</returns>
        public static bool IsEmpty(this IWeavingPlan plan) => !plan.PointCut.Any();

        /// <summary>
        /// Checks whether a property weaving plan is empty.
        /// </summary>
        /// <param name="plan">The property weaving plan to be checked.</param>
        /// <returns>True if the property weaving plan is empty, false if not.</returns>
        public static bool IsEmpty(this IPropertyWeavingPlan plan) => plan.GetterPlan.IsEmpty() && plan.SetterPlan.IsEmpty();

        /// <summary>
        /// Checks if return value needs to be stored as local variable during weaving.
        /// </summary>
        /// <param name="plan">The weaving plan to be checked.</param>
        /// <returns>True if return value needs to be stored as local variable, false if not.</returns>
        public static bool NeedToStoreReturnValueAsLocalVariable(this IWeavingPlan plan)
        {
            var pointCut = plan.PointCut;
            return pointCut.Contains(JoinPoint.Exception) || pointCut.Contains(JoinPoint.Exit);
        }

        /// <summary>
        /// Checks if <see cref="CrossCutterN.Base.Metadata.IExecutionContext"/> variable is needed.
        /// </summary>
        /// <param name="plan">The execution plan to be checked.</param>
        /// <returns>True if <see cref="CrossCutterN.Base.Metadata.IExecutionContext"/> variable is needed, false if not.</returns>
        public static bool NeedContextVariable(this IWeavingPlan plan) => plan.ParameterFlag.Contains(AdviceParameterFlag.Context);

        /// <summary>
        /// Checks if HasException variable is needed.
        /// </summary>
        /// <param name="plan">The execution plan to be checked.</param>
        /// <returns>True if HasException variable is needed, false if not.</returns>
        public static bool NeedHasExceptionVariable(this IWeavingPlan plan) => plan.ParameterFlag.Contains(AdviceParameterFlag.HasException);

        /// <summary>
        /// Checks if <see cref="CrossCutterN.Base.Metadata.IExecution"/> variable is needed.
        /// </summary>
        /// <param name="plan">The execution plan to be checked.</param>
        /// <returns>True if <see cref="CrossCutterN.Base.Metadata.IExecution"/> variable is needed, false if not.</returns>
        public static bool NeedExecutionVariable(this IWeavingPlan plan) => plan.ParameterFlag.Contains(AdviceParameterFlag.Execution);

        /// <summary>
        /// Checks if Exception variable is needed.
        /// </summary>
        /// <param name="plan">The execution plan to be checked.</param>
        /// <returns>True if Exception variable is needed, false if not.</returns>
        public static bool NeedExceptionVariable(this IWeavingPlan plan) => plan.ParameterFlag.Contains(AdviceParameterFlag.Exception);

        /// <summary>
        /// Checks if <see cref="CrossCutterN.Base.Metadata.IReturn"/> variable is needed.
        /// </summary>
        /// <param name="plan">The execution plan to be checked.</param>
        /// <returns>True if <see cref="CrossCutterN.Base.Metadata.IReturn"/> variable is needed, false if not.</returns>
        public static bool NeedReturnVariable(this IWeavingPlan plan) => plan.ParameterFlag.Contains(AdviceParameterFlag.Return);
    }
}
