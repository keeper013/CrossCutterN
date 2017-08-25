// <copyright file="AttributePropertyReference.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Metadata
{
    using System;
    using Mono.Cecil;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Metadata.IAttributeProperty"/> interface implementation.
    /// </summary>
    internal sealed class AttributePropertyReference : ReferenceBase, IAttributePropertyReference, IAttributePropertyReferenceBuilder
    {
        private int typeReferenceIndex = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributePropertyReference"/> class.
        /// </summary>
        /// <param name="module">The module that this reference is built for.</param>
        public AttributePropertyReference(ModuleDefinition module)
            : base(module)
        {
        }

        /// <inheritdoc/>
        TypeReference IAttributePropertyReference.TypeReference => GetType(typeReferenceIndex);

        /// <inheritdoc/>
        public Type TypeReference
        {
            set => typeReferenceIndex = AddType(value);
        }

        /// <inheritdoc/>
        public IAttributePropertyReference Build()
        {
            CompleteAdding();
            return this;
        }
    }
}
