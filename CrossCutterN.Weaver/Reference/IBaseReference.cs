// <copyright file="IBaseReference.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference
{
    using CrossCutterN.Weaver.Reference.Base.Metadata;
    using CrossCutterN.Weaver.Reference.Base.Switch;

    /// <summary>
    /// Interface of base module content reference.
    /// </summary>
    internal interface IBaseReference
    {
        /// <summary>
        /// Gets reference to <see cref="CrossCutterN.Base.Metadata.MetadataFactory"/> class.
        /// </summary>
        IMetadataFactoryReference MetadataFactory { get; }

        /// <summary>
        /// Gets reference to <see cref="CrossCutterN.Base.Metadata.IExecutionContext"/> interface.
        /// </summary>
        IExecutionContextReference ExecutionContext { get; }

        /// <summary>
        /// Gets reference to <see cref="CrossCutterN.Base.Metadata.IExecutionBuilder"/> interface.
        /// </summary>
        IExecutionBuilderReference Execution { get; }

        /// <summary>
        /// Gets reference to <see cref="CrossCutterN.Base.Metadata.IReturnBuilder"/> interface.
        /// </summary>
        IReturnBuilderReference Return { get; }

        /// <summary>
        /// Gets reference to <see cref="CrossCutterN.Base.Metadata.IParameterBuilder"/> interface.
        /// </summary>
        IParameterBuilderReference Parameter { get; }

        /// <summary>
        /// Gets reference to <see cref="CrossCutterN.Base.Metadata.ICustomAttributeBuilder"/> interface.
        /// </summary>
        ICustomAttributeBuilderReference CustomAttribute { get; }

        /// <summary>
        /// Gets reference to <see cref="CrossCutterN.Base.Metadata.IAttributeProperty"/> interface.
        /// </summary>
        IAttributePropertyReference AttributeProperty { get; }

        /// <summary>
        /// Gets reference to <see cref="CrossCutterN.Base.Switch.IAspectSwitchBuilder"/> interface.
        /// </summary>
        IAspectSwitchBuilderReference Builder { get; }

        /// <summary>
        /// Gets reference to <see cref="CrossCutterN.Base.Switch.SwitchBackStage"/> class.
        /// </summary>
        ISwitchBackStageReference BackStage { get; }

        /// <summary>
        /// Gets reference to <see cref="CrossCutterN.Base.Switch.IAspectSwitchGlancer"/> interface.
        /// </summary>
        IAspectSwitchGlancerReference Glancer { get; }
    }
}
