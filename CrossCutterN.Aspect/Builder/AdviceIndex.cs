// <copyright file="AdviceIndex.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Builder
{
    /// <summary>
    /// Index of advice in advice assembly.
    /// </summary>
    public sealed class AdviceIndex
    {
        /// <summary>
        /// Gets or sets advice assembly key.
        /// </summary>
        public string AdviceAssemblyKey { get; set; }

        /// <summary>
        /// Gets or sets method key of advice.
        /// </summary>
        public string MethodKey { get; set; }
    }
}
