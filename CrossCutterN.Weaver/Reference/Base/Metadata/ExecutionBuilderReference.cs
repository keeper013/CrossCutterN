// <copyright file="ExecutionBuilderReference.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Metadata
{
    using System;
    using System.Reflection;
    using Mono.Cecil;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Metadata.IExecutionBuilder"/> interface implementation.
    /// </summary>
    internal sealed class ExecutionBuilderReference : ReferenceBase, IExecutionBuilderReference, IExecutionBuilderReferenceBuilder
    {
        private int typeReferenceIndex = -1;
        private int readOnlyTypeReferenceIndex = -1;
        private int addParameterMethodIndex = -1;
        private int buildMethodIndex = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionBuilderReference"/> class.
        /// </summary>
        /// <param name="module">The module that the execution metadata reference will be initialized for.</param>
        public ExecutionBuilderReference(ModuleDefinition module)
            : base(module)
        {
        }

        /// <inheritdoc/>
        TypeReference IExecutionBuilderReference.TypeReference => GetType(typeReferenceIndex);

        /// <inheritdoc/>
        public Type TypeReference
        {
            set => typeReferenceIndex = AddType(value);
        }

        /// <inheritdoc/>
        TypeReference IExecutionBuilderReference.ReadOnlyTypeReference => GetType(readOnlyTypeReferenceIndex);

        /// <inheritdoc/>
        public Type ReadOnlyTypeReference
        {
            set => readOnlyTypeReferenceIndex = AddType(value);
        }

        /// <inheritdoc/>
        MethodReference IExecutionBuilderReference.AddParameterMethod => GetMethod(addParameterMethodIndex);

        /// <inheritdoc/>
        public MethodInfo AddParameterMethod
        {
            set => addParameterMethodIndex = AddMethod(value);
        }

        /// <inheritdoc/>
        MethodReference IExecutionBuilderReference.BuildMethod => GetMethod(buildMethodIndex);

        /// <inheritdoc/>
        public MethodInfo BuildMethod
        {
            set => buildMethodIndex = AddMethod(value);
        }

        /// <inheritdoc/>
        public IExecutionBuilderReference Build()
        {
            CompleteAdding();
            return this;
        }
    }
}
