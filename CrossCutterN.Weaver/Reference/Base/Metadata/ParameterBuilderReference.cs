// <copyright file="ParameterBuilderReference.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Metadata
{
    using System;
    using System.Reflection;
    using Mono.Cecil;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Metadata.IParameterBuilder"/> interface implementation.
    /// </summary>
    internal sealed class ParameterBuilderReference : ReferenceBase, IParameterBuilderReference, IParameterBuilderReferenceBuilder
    {
        private int typeReferenceIndex = -1;
        private int readOnlyTypeReferenceIndex = -1;
        private int addCustomAttributeMethodIndex = -1;
        private int buildMethodIndex = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterBuilderReference"/> class.
        /// </summary>
        /// <param name="module">The module that this reference is built for.</param>
        public ParameterBuilderReference(ModuleDefinition module)
            : base(module)
        {
        }

        /// <inheritdoc/>
        TypeReference IParameterBuilderReference.TypeReference => GetType(typeReferenceIndex);

        /// <inheritdoc/>
        public Type TypeReference
        {
            set => typeReferenceIndex = AddType(value);
        }

        /// <inheritdoc/>
        TypeReference IParameterBuilderReference.ReadOnlyTypeReference => GetType(readOnlyTypeReferenceIndex);

        /// <inheritdoc/>
        public Type ReadOnlyTypeReference
        {
            set => readOnlyTypeReferenceIndex = AddType(value);
        }

        /// <inheritdoc/>
        MethodReference IParameterBuilderReference.AddCustomAttributeMethod => GetMethod(addCustomAttributeMethodIndex);

        /// <inheritdoc/>
        public MethodInfo AddCustomAttributeMethod
        {
            set => addCustomAttributeMethodIndex = AddMethod(value);
        }

        /// <inheritdoc/>
        MethodReference IParameterBuilderReference.BuildMethod => GetMethod(buildMethodIndex);

        /// <inheritdoc/>
        public MethodInfo BuildMethod
        {
            set => buildMethodIndex = AddMethod(value);
        }

        /// <inheritdoc/>
        public IParameterBuilderReference Build()
        {
            CompleteAdding();
            return this;
        }
    }
}