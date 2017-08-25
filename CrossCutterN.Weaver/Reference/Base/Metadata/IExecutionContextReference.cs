// <copyright file="IExecutionContextReference.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Metadata
{
    using Mono.Cecil;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Metadata.IExecutionContext"/> interface.
    /// </summary>
    internal interface IExecutionContextReference
    {
        /// <summary>
        /// Gets reference to the type.
        /// </summary>
        TypeReference TypeReference { get; }
    }
}
