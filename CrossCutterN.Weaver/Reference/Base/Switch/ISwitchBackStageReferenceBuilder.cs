// <copyright file="ISwitchBackStageReferenceBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Switch
{
    using System.Reflection;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Switch.SwitchBackStage"/> class to be built up.
    /// </summary>
    internal interface ISwitchBackStageReferenceBuilder : IBuilder<ISwitchBackStageReference>
    {
        /// <summary>
        /// Sets reference to getter method of Glancer property.
        /// </summary>
        MethodInfo GlancerGetterReference { set; }

        /// <summary>
        /// Sets reference to getter method of Builder property.
        /// </summary>
        MethodInfo BuilderGetterReference { set; }
    }
}
