// <copyright file="IAspectBuilderUtility.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Builder
{
    using System;

    /// <summary>
    /// Interface of aspect utility to store available aspects.
    /// </summary>
    public interface IAspectBuilderUtility
    {
        /// <summary>
        /// Gets constructor of aspect configuration.
        /// </summary>
        /// <param name="assemblyKey">Key of assembly that contains aspects.</param>
        /// <param name="aspectName">Name of the aspect, serves as key to the aspect in configuration.</param>
        /// <returns>Consturctor for aspect configuration.</returns>
        Func<IAspectBuilder> GetAspectConstructor(string assemblyKey, string aspectName);
    }
}
