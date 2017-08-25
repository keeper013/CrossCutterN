// <copyright file="IAdviceInfo.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Weaver
{
    using System.Reflection;
    using CrossCutterN.Aspect.Aspect;

    /// <summary>
    /// Information of advice in weaving plan.
    /// </summary>
    internal interface IAdviceInfo
    {
        /// <summary>
        /// Gets aspect method.
        /// </summary>
        MethodInfo Advice { get; }

        /// <summary>
        /// Gets name of the aspect, which is also used as aspect key.
        /// </summary>
        string AspectName { get; }

        /// <summary>
        /// Gets flag of parameters of this advice method.
        /// </summary>
        AdviceParameterFlag ParameterFlag { get; }

        /// <summary>
        /// Gets a valud indicating whether the aspect is switched on by default.
        /// </summary>
        bool? IsSwitchedOn { get; }
    }
}
