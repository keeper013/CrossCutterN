// <copyright file="ICustomAttributeBuilderReference.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Metadata
{
    using Mono.Cecil;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Metadata.ICustomAttributeBuilder"/> interface.
    /// </summary>
    internal interface ICustomAttributeBuilderReference
    {
        /// <summary>
        /// Gets reference to the type.
        /// </summary>
        TypeReference TypeReference { get; }

        /// <summary>
        /// Gets reference to the readonly only interface type it can build to.
        /// </summary>
        TypeReference ReadOnlyTypeReference { get; }

        /// <summary>
        /// Gets reference to AddAttributeProperty method.
        /// </summary>
        MethodReference AddAttributePropertyMethod { get; }

        /// <summary>
        /// Gets reference to Build method.
        /// </summary>
        MethodReference BuildMethod { get; }
    }
}
