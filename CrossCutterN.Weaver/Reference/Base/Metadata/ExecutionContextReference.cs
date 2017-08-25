// <copyright file="ExecutionContextReference.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Metadata
{
    using System;
    using Mono.Cecil;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Metadata.IExecutionContext"/> interface implementation.
    /// </summary>
    internal sealed class ExecutionContextReference : ReferenceBase, IExecutionContextReference, IExecutionContextReferenceBuilder
    {
        private int typeReferenceIndex = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionContextReference"/> class.
        /// </summary>
        /// <param name="module">The module that this reference is defined for.</param>
        public ExecutionContextReference(ModuleDefinition module)
            : base(module)
        {
        }

        /// <inheritdoc/>
        TypeReference IExecutionContextReference.TypeReference => GetType(typeReferenceIndex);

        /// <inheritdoc/>
        public Type TypeReference
        {
            set => typeReferenceIndex = AddType(value);
        }

        /// <inheritdoc/>
        public IExecutionContextReference Build()
        {
            CompleteAdding();
            return this;
        }
    }
}
