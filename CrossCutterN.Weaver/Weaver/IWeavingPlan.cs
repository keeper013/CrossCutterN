// <copyright file="IWeavingPlan.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Weaver
{
    using System.Collections.Generic;
    using CrossCutterN.Aspect.Aspect;

    /// <summary>
    /// Weaving plan.
    /// </summary>
    internal interface IWeavingPlan
    {
        /// <summary>
        /// Gets join points in the weaving plan.
        /// </summary>
        IReadOnlyCollection<JoinPoint> PointCut { get; }

        /// <summary>
        /// Gets flag of parameters of the advice.
        /// </summary>
        AdviceParameterFlag ParameterFlag { get; }

        /// <summary>
        /// Gets ordered advices of a join point.
        /// </summary>
        /// <param name="joinPoint">The join point.</param>
        /// <returns>Ordered advice collection.</returns>
        IReadOnlyCollection<IAdviceInfo> GetAdvices(JoinPoint joinPoint);
    }
}
