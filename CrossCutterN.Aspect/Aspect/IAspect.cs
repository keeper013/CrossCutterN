// <copyright file="IAspect.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Aspect
{
    using System.Collections.Generic;
    using System.Reflection;
    using CrossCutterN.Aspect.Metadata;

    /// <summary>
    /// Aspect interface.
    /// </summary>
    public interface IAspect
    {
        /// <summary>
        /// Gets all join points this aspect build may add advice to.
        /// </summary>
        IReadOnlyCollection<JoinPoint> PointCut { get; }

        /// <summary>
        /// Gets a value indicating whether the aspect is switched on by default.
        /// </summary>
        bool? IsSwitchedOn { get; }

        /// <summary>
        /// Gets advice for a join point.
        /// </summary>
        /// <param name="joinPoint">The join point, <see cref="JoinPoint"/></param>
        /// <returns>The advice retrieved.</returns>
        MethodInfo GetAdvice(JoinPoint joinPoint);

        /// <summary>
        /// Gets a value indicating whether this aspect can apply to the input method.
        /// </summary>
        /// <param name="method">Target method.</param>
        /// <returns>The value indicating whether this aspect can apply to the input method.</returns>
        bool CanApplyTo(IMethod method);

        /// <summary>
        /// Gets a value indicating whether this aspect can apply to the input property.
        /// </summary>
        /// <param name="property">The target property.</param>
        /// <returns>Item 1 indicates whether the aspect can be applied to getter method, Item 2 indicates whether the aspect can be applied to setter method.</returns>
        PropertyConcern CanApplyTo(IProperty property);
    }
}
