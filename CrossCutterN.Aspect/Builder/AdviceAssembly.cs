// <copyright file="AdviceAssembly.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Builder
{
    using System.Collections.Generic;

    /// <summary>
    /// Advice assembly definition.
    /// </summary>
    public sealed class AdviceAssembly
    {
        /// <summary>
        /// Gets or sets path of the assembly, can be absolute path or relative path.
        /// </summary>
        public string AssemblyPath { get; set; }

        /// <summary>
        /// Gets or sets concern attributes in the assembly.
        /// </summary>
        public Dictionary<string, string> Attributes { get; set; }

        /// <summary>
        /// Gets or sets advices in the assembly.
        /// </summary>
        public Dictionary<string, Dictionary<string, Advice>> Advices { get; set; }
    }
}
