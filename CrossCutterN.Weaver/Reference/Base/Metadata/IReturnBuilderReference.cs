// <copyright file="IReturnBuilderReference.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Metadata
{
    using Mono.Cecil;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Metadata.IReturnBuilder"/> interface.
    /// </summary>
    internal interface IReturnBuilderReference
    {
        /// <summary>
        /// Gets reference to type interface type.
        /// </summary>
        TypeReference TypeReference { get; }

        /// <summary>
        /// Gets reference to the readonly only interface type it can build to.
        /// </summary>
        TypeReference ReadOnlyTypeReference { get; }

        /// <summary>
        /// Gets reference to setter method of HasReturn property.
        /// </summary>
        MethodReference HasReturnSetter { get; }

        /// <summary>
        /// Gets reference to setter method of Value property.
        /// </summary>
        MethodReference ValueSetter { get; }

        /// <summary>
        /// Gets reference to Build method.
        /// </summary>
        MethodReference BuildMethod { get; }
    }
}
