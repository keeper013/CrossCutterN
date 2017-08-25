// <copyright file="AspectSwitchBuilderReference.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Switch
{
    using System.Reflection;
    using Mono.Cecil;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Switch.IAspectSwitchBuilder"/> implementation.
    /// </summary>
    internal sealed class AspectSwitchBuilderReference : ReferenceBase, IAspectSwitchBuilderReference, IAspectSwitchBuilderReferenceBuilder
    {
        private int registerSwitchMethodIndex = -1;
        private int completeMethodIndex = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="AspectSwitchBuilderReference"/> class.
        /// </summary>
        /// <param name="module">The module that this reference is built for.</param>
        public AspectSwitchBuilderReference(ModuleDefinition module)
            : base(module)
        {
        }

        /// <inheritdoc/>
        MethodReference IAspectSwitchBuilderReference.RegisterSwitchMethod => GetMethod(registerSwitchMethodIndex);

        /// <inheritdoc/>
        public MethodInfo RegisterSwitchMethod
        {
            set => registerSwitchMethodIndex = AddMethod(value);
        }

        /// <inheritdoc/>
        MethodReference IAspectSwitchBuilderReference.CompleteMethod => GetMethod(completeMethodIndex);

        /// <inheritdoc/>
        public MethodInfo CompleteMethod
        {
            set => completeMethodIndex = AddMethod(value);
        }

        /// <inheritdoc/>
        public IAspectSwitchBuilderReference Build()
        {
            CompleteAdding();
            return this;
        }
    }
}
