// <copyright file="BaseReference.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference
{
    using CrossCutterN.Weaver.Reference.Base.Metadata;
    using CrossCutterN.Weaver.Reference.Base.Switch;

    /// <summary>
    /// Reference to advice module content implemetnation.
    /// </summary>
    internal sealed class BaseReference : IBaseReference
    {
        /// <inheritdoc/>
        public IAttributePropertyReference AttributeProperty { get; set; }

        /// <inheritdoc/>
        public ICustomAttributeBuilderReference CustomAttribute { get; set; }

        /// <inheritdoc/>
        public IExecutionBuilderReference Execution { get; set; }

        /// <inheritdoc/>
        public IExecutionContextReference ExecutionContext { get; set; }

        /// <inheritdoc/>
        public IParameterBuilderReference Parameter { get; set; }

        /// <inheritdoc/>
        public IMetadataFactoryReference MetadataFactory { get; set; }

        /// <inheritdoc/>
        public IReturnBuilderReference Return { get; set; }

        /// <inheritdoc/>
        public IAspectSwitchBuilderReference Builder { get; set; }

        /// <inheritdoc/>
        public ISwitchBackStageReference BackStage { get; set; }

        /// <inheritdoc/>
        public IAspectSwitchGlancerReference Glancer { get; set; }
    }
}
