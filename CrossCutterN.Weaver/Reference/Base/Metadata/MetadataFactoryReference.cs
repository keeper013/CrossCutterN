// <copyright file="MetadataFactoryReference.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Metadata
{
    using System.Reflection;
    using Mono.Cecil;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Metadata.MetadataFactory"/> class implementation.
    /// </summary>
    internal sealed class MetadataFactoryReference : ReferenceBase, IMetadataFactoryReference, IMetadataFactoryReferenceBuilder
    {
        private int initializeExecutionMethodIndex = -1;
        private int initializeExecutionContextMethodIndex = -1;
        private int initializeParameterMethodIndex = -1;
        private int initializeCustomAttributeMethodIndex = -1;
        private int initializeAttributePropertyMethodIndex = -1;
        private int initializeReturnMethodIndex = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataFactoryReference"/> class.
        /// </summary>
        /// <param name="module">The module that this reference is built for.</param>
        public MetadataFactoryReference(ModuleDefinition module)
            : base(module)
        {
        }

        /// <inheritdoc/>
        MethodReference IMetadataFactoryReference.InitializeExecutionMethod => GetMethod(initializeExecutionMethodIndex);

        /// <inheritdoc/>
        public MethodInfo InitializeExecutionMethod
        {
            set => initializeExecutionMethodIndex = AddMethod(value);
        }

        /// <inheritdoc/>
        MethodReference IMetadataFactoryReference.InitializeExecutionContextMethod => GetMethod(initializeExecutionContextMethodIndex);

        /// <inheritdoc/>
        public MethodInfo InitializeExecutionContextMethod
        {
            set => initializeExecutionContextMethodIndex = AddMethod(value);
        }

        /// <inheritdoc/>
        MethodReference IMetadataFactoryReference.InitializeParameterMethod => GetMethod(initializeParameterMethodIndex);

        /// <inheritdoc/>
        public MethodInfo InitializeParameterMethod
        {
            set => initializeParameterMethodIndex = AddMethod(value);
        }

        /// <inheritdoc/>
        MethodReference IMetadataFactoryReference.InitializeCustomAttributeMethod => GetMethod(initializeCustomAttributeMethodIndex);

        /// <inheritdoc/>
        public MethodInfo InitializeCustomAttributeMethod
        {
            set => initializeCustomAttributeMethodIndex = AddMethod(value);
        }

        /// <inheritdoc/>
        MethodReference IMetadataFactoryReference.InitializeAttributePropertyMethod => GetMethod(initializeAttributePropertyMethodIndex);

        /// <inheritdoc/>
        public MethodInfo InitializeAttributePropertyMethod
        {
            set => initializeAttributePropertyMethodIndex = AddMethod(value);
        }

        /// <inheritdoc/>
        MethodReference IMetadataFactoryReference.InitializeReturnMethod => GetMethod(initializeReturnMethodIndex);

        /// <inheritdoc/>
        public MethodInfo InitializeReturnMethod
        {
            set => initializeReturnMethodIndex = AddMethod(value);
        }

        /// <inheritdoc/>
        public IMetadataFactoryReference Build()
        {
            CompleteAdding();
            return this;
        }
    }
}
