// <copyright file="ISwitchBackStageReference.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Switch
{
    using Mono.Cecil;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Switch.SwitchBackStage"/> class.
    /// </summary>
    internal interface ISwitchBackStageReference
    {
        /// <summary>
        /// Gets reference to getter method of Glancer property.
        /// </summary>
        MethodReference GlancerGetterReference { get; }

        /// <summary>
        /// Gets reference to getter of Builder property.
        /// </summary>
        MethodReference BuilderGetterReference { get; }
    }
}
