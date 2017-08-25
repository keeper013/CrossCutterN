// <copyright file="IExecutionBuilderReference.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Metadata
{
    using Mono.Cecil;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Metadata.IExecutionBuilder"/> interface.
    /// </summary>
    internal interface IExecutionBuilderReference
    {
        /// <summary>
        /// Gets reference to the type.
        /// </summary>
        TypeReference TypeReference { get; }

        /// <summary>
        /// Gets reference to the read only interface type it can build to.
        /// </summary>
        TypeReference ReadOnlyTypeReference { get; }

        /// <summary>
        /// Gets reference to AddParameter method.
        /// </summary>
        MethodReference AddParameterMethod { get; }

        /// <summary>
        /// Gets reference to Build method.
        /// </summary>
        MethodReference BuildMethod { get; }
    }
}
