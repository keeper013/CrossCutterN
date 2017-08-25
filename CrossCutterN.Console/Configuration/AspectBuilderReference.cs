// <copyright file="AspectBuilderReference.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Console.Configuration
{
    /// <summary>
    /// aspect reference configuration.
    /// </summary>
    public sealed class AspectBuilderReference
    {
        /// <summary>
        /// Gets or sets key for aspect assembly.
        /// </summary>
        public string AspectAssemblyKey { get; set; }

        /// <summary>
        /// Gets or sets key for aspect.
        /// </summary>
        public string AspectBuilderKey { get; set; }
    }
}
