// <copyright file="ICanAddJoinPoint.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Weaver
{
    using System.Reflection;
    using CrossCutterN.Aspect.Aspect;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Weaving plan to be built up.
    /// </summary>
    internal interface ICanAddJoinPoint : IBuilder<IWeavingPlan>
    {
        /// <summary>
        /// Adds an advice method to the join point.
        /// </summary>
        /// <param name="joinPoint">The join point to inject an advice.</param>
        /// <param name="aspectName">Name of the aspect, also used as key of the aspect.</param>
        /// <param name="advice">The advice method to be injected.</param>
        /// <param name="sequence">Sequence of this advice method among all other advices for this join point.</param>
        /// <param name="flag">Parameter flag for this advice moethod.</param>
        /// <param name="isSwitchedOn">Default switch status of the aspect.</param>
        void AddJoinPoint(JoinPoint joinPoint, string aspectName, MethodInfo advice, int sequence, AdviceParameterFlag flag, bool? isSwitchedOn);
    }
}
