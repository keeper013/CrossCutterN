// <copyright file="TargetAssemblies.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Console.Configuration
{
    using System.Collections.Generic;
    using CrossCutterN.Aspect.Aspect;

    /// <summary>
    /// Target asssembly configuration.
    /// </summary>
    public sealed class TargetAssemblies
    {
        /// <summary>
        /// Gets or sets default advice assembly key.
        /// </summary>
        public string DefaultAdviceAssemblyKey { get; set; }

        /// <summary>
        /// Gets or sets aspects configuration.
        /// </summary>
        public Dictionary<string, AspectBuilderReference> AspectBuilders { get; set; }

        /// <summary>
        /// Gets or sets order configuration for the advices in join points.
        /// </summary>
        public Dictionary<JoinPoint, List<string>> Order { get; set; }

        /// <summary>
        /// Gets or sets target assemblies configuration.
        /// </summary>
        public List<AssemblySetting> Targets { get; set; }
    }
}
