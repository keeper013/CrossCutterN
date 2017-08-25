// <copyright file="AdviceAssemblyTypeIndex.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Builder
{
    /// <summary>
    /// Index for attributes in advice assembly.
    /// </summary>
    public sealed class AdviceAssemblyTypeIndex
    {
        /// <summary>
        /// Gets or sets advice assembly key.
        /// </summary>
        public string AdviceAssemblyKey { get; set; }

        /// <summary>
        /// Gets or sets attribute type key.
        /// </summary>
        public string TypeKey { get; set; }
    }
}
