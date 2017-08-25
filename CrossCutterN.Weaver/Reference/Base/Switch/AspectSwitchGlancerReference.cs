// <copyright file="AspectSwitchGlancerReference.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Switch
{
    using System.Reflection;
    using Mono.Cecil;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Switch.IAspectSwitchGlancer"/> interface implementation.
    /// </summary>
    internal sealed class AspectSwitchGlancerReference : ReferenceBase, IAspectSwitchGlancerReference, IAspectSwitchGlancerReferenceBuilder
    {
        private int isOnMethodIndex = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="AspectSwitchGlancerReference"/> class.
        /// </summary>
        /// <param name="module">The module that this reference is built for.</param>
        public AspectSwitchGlancerReference(ModuleDefinition module)
            : base(module)
        {
        }

        /// <inheritdoc/>
        MethodReference IAspectSwitchGlancerReference.IsOnMethod => GetMethod(isOnMethodIndex);

        /// <inheritdoc/>
        public MethodInfo IsOnMethod
        {
            set => isOnMethodIndex = AddMethod(value);
        }

        /// <inheritdoc/>
        public IAspectSwitchGlancerReference Build()
        {
            CompleteAdding();
            return this;
        }
    }
}
