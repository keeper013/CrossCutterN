// <copyright file="ReturnBuilderReference.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Metadata
{
    using System;
    using System.Reflection;
    using Mono.Cecil;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Metadata.IReturnBuilder"/> interface implementation.
    /// </summary>
    internal sealed class ReturnBuilderReference : ReferenceBase, IReturnBuilderReference, IReturnBuilderReferenceBuilder
    {
        private int typeReferenceIndex = -1;
        private int readOnlyTypeReferenceIndex = -1;
        private int hasReturnSetter = -1;
        private int valueSetterSetter = -1;
        private int buildMethodIndex = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnBuilderReference"/> class.
        /// </summary>
        /// <param name="module">The module that this reference is built for.</param>
        public ReturnBuilderReference(ModuleDefinition module)
            : base(module)
        {
        }

        /// <inheritdoc/>
        TypeReference IReturnBuilderReference.TypeReference => GetType(typeReferenceIndex);

        /// <inheritdoc/>
        public Type TypeReference
        {
            set => typeReferenceIndex = AddType(value);
        }

        /// <inheritdoc/>
        TypeReference IReturnBuilderReference.ReadOnlyTypeReference => GetType(readOnlyTypeReferenceIndex);

        /// <inheritdoc/>
        public Type ReadOnlyTypeReference
        {
            set => readOnlyTypeReferenceIndex = AddType(value);
        }

        /// <inheritdoc/>
        MethodReference IReturnBuilderReference.HasReturnSetter => GetMethod(hasReturnSetter);

        /// <inheritdoc/>
        public MethodInfo HasReturnSetter
        {
            set => hasReturnSetter = AddMethod(value);
        }

        /// <inheritdoc/>
        MethodReference IReturnBuilderReference.ValueSetter => GetMethod(valueSetterSetter);

        /// <inheritdoc/>
        public MethodInfo ValueSetter
        {
            set => valueSetterSetter = AddMethod(value);
        }

        /// <inheritdoc/>
        MethodReference IReturnBuilderReference.BuildMethod => GetMethod(buildMethodIndex);

        /// <inheritdoc/>
        public MethodInfo BuildMethod
        {
            set => buildMethodIndex = AddMethod(value);
        }

        /// <inheritdoc/>
        public IReturnBuilderReference Build()
        {
            CompleteAdding();
            return this;
        }
    }
}
