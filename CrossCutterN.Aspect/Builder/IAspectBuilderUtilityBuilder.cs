// <copyright file="IAspectBuilderUtilityBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Builder
{
    using System;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Interface of utility to store aspect configuration
    /// </summary>
    public interface IAspectBuilderUtilityBuilder : IBuilder<IAspectBuilderUtility>
    {
        /// <summary>
        /// Adds an aspect build constructor to the utility.
        /// </summary>
        /// <param name="assemblyKey">Key of aspect assembly.</param>
        /// <param name="aspectName">Name of the aspect, serves as key of the aspect in configuration.</param>
        /// <param name="constructor">Constructor of aspect configuration.</param>
        void AddAspectBuilderConstructor(string assemblyKey, string aspectName, Func<IAspectBuilder> constructor);
    }
}
