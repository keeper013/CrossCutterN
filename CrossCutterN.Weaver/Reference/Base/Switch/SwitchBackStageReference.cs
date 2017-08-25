// <copyright file="SwitchBackStageReference.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Switch
{
    using System.Reflection;
    using Mono.Cecil;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Switch.SwitchBackStage"/> class implementation.
    /// </summary>
    internal sealed class SwitchBackStageReference : ReferenceBase, ISwitchBackStageReference, ISwitchBackStageReferenceBuilder
    {
        private int glancerGetterReferenceIndex = -1;
        private int builderGetterReferenceIndex = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="SwitchBackStageReference"/> class.
        /// </summary>
        /// <param name="module">The module that this reference is built for.</param>
        public SwitchBackStageReference(ModuleDefinition module)
            : base(module)
        {
        }

        /// <inheritdoc/>
        MethodReference ISwitchBackStageReference.GlancerGetterReference => GetMethod(glancerGetterReferenceIndex);

        /// <inheritdoc/>
        public MethodInfo GlancerGetterReference
        {
            set => glancerGetterReferenceIndex = AddMethod(value);
        }

        /// <inheritdoc/>
        MethodReference ISwitchBackStageReference.BuilderGetterReference => GetMethod(builderGetterReferenceIndex);

        /// <inheritdoc/>
        public MethodInfo BuilderGetterReference
        {
            set => builderGetterReferenceIndex = AddMethod(value);
        }

        /// <inheritdoc/>
        public ISwitchBackStageReference Build()
        {
            CompleteAdding();
            return this;
        }
    }
}
