// <copyright file="IParameterBuilderReference.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Metadata
{
    using Mono.Cecil;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Metadata.IParameterBuilder"/> interface.
    /// </summary>
    internal interface IParameterBuilderReference
    {
        /// <summary>
        /// Gets reference to the interface type.
        /// </summary>
        TypeReference TypeReference { get; }

        /// <summary>
        /// Gets reference to the readonly only interface type it can build to.
        /// </summary>
        TypeReference ReadOnlyTypeReference { get; }

        /// <summary>
        /// Gets reference to AddCustomAttribute method.
        /// </summary>
        MethodReference AddCustomAttributeMethod { get; }

        /// <summary>
        /// Gets reference to Build method.
        /// </summary>
        MethodReference BuildMethod { get; }
    }
}
