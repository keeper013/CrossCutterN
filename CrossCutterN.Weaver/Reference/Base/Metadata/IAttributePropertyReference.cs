// <copyright file="IAttributePropertyReference.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Metadata
{
    using Mono.Cecil;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Metadata.IAttributeProperty"/> interface.
    /// </summary>
    internal interface IAttributePropertyReference
    {
        /// <summary>
        /// Gets reference to the interface type.
        /// </summary>
        TypeReference TypeReference { get; }
    }
}
