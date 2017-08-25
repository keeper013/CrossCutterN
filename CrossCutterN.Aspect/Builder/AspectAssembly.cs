// <copyright file="AspectAssembly.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Builder
{
    using System.Collections.Generic;

    /// <summary>
    /// Aspect assembly definition.
    /// </summary>
    public sealed class AspectAssembly
    {
        /// <summary>
        /// Gets or sets path of assembly.
        /// </summary>
        public string AssemblyPath { get; set; }

        /// <summary>
        /// Gets or sets configuration for aspect builders.
        /// </summary>
        public Dictionary<string, string> AspectBuilders { get; set; }
    }
}
