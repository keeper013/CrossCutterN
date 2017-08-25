// <copyright file="IAspectBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Aspect
{
    using System.Reflection;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Interface for aspect to be built.
    /// </summary>
    public interface IAspectBuilder : IBuilder<IAspect>
    {
        /// <summary>
        /// Sets a valud indicating the default switch status.
        /// </summary>
        bool? IsSwitchedOn { set; }

        /// <summary>
        /// Adds an advice method to a join point.
        /// Advice verification will happen in weaver, since this extention point is not under weaver's control
        /// </summary>
        /// <param name="joinPoint">Join point to call advice</param>
        /// <param name="advice">advice method to be called</param>
        void SetAdvice(JoinPoint joinPoint, MethodInfo advice);
    }
}
