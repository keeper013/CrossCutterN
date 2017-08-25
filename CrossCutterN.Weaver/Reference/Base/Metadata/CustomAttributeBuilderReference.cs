// <copyright file="CustomAttributeBuilderReference.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Metadata
{
    using System;
    using System.Reflection;
    using Mono.Cecil;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Metadata.ICustomAttributeBuilder"/> interface implementation.
    /// </summary>
    internal sealed class CustomAttributeBuilderReference : ReferenceBase, ICustomAttributeBuilderReference, ICustomAttributeBuilderReferenceBuilder
    {
        private int typeReferenceIndex = -1;
        private int readOnlyTypeReferenceIndex = -1;
        private int addAttributePropertyMethodIndex = -1;
        private int buildMethodIndex = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAttributeBuilderReference"/> class.
        /// </summary>
        /// <param name="module">The module that this reference is built for.</param>
        public CustomAttributeBuilderReference(ModuleDefinition module)
            : base(module)
        {
        }

        /// <inheritdoc/>
        TypeReference ICustomAttributeBuilderReference.TypeReference => GetType(typeReferenceIndex);

        /// <inheritdoc/>
        public Type TypeReference
        {
            set => typeReferenceIndex = AddType(value);
        }

        /// <inheritdoc/>
        TypeReference ICustomAttributeBuilderReference.ReadOnlyTypeReference => GetType(readOnlyTypeReferenceIndex);

        /// <inheritdoc/>
        public Type ReadOnlyTypeReference
        {
            set => readOnlyTypeReferenceIndex = AddType(value);
        }

        /// <inheritdoc/>
        MethodReference ICustomAttributeBuilderReference.AddAttributePropertyMethod => GetMethod(addAttributePropertyMethodIndex);

        /// <inheritdoc/>
        public MethodInfo AddAttributePropertyMethod
        {
            set => addAttributePropertyMethodIndex = AddMethod(value);
        }

        /// <inheritdoc/>
        MethodReference ICustomAttributeBuilderReference.BuildMethod => GetMethod(buildMethodIndex);

        /// <inheritdoc/>
        public MethodInfo BuildMethod
        {
            set => buildMethodIndex = AddMethod(value);
        }

        /// <inheritdoc/>
        public ICustomAttributeBuilderReference Build()
        {
            CompleteAdding();
            return this;
        }
    }
}
