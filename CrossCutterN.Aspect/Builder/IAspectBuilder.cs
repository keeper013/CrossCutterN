// <copyright file="IAspectBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Builder
{
    using CrossCutterN.Aspect.Aspect;

    /// <summary>
    /// Interface for aspect builder.
    /// </summary>
    public interface IAspectBuilder
    {
        /// <summary>
        /// Builds to <see cref="IAspect"/>.
        /// </summary>
        /// <param name="utility">Advice utility which contains advice information.</param>
        /// <param name="defaultAdviceAssemblyKey">Default advice assembly key if relevant key is not set.</param>
        /// <returns>The <see cref="IAspect"/> built to.</returns>
        IAspect Build(IAdviceUtility utility, string defaultAdviceAssemblyKey);
    }
}
