// <copyright file="IAspectSwitchBuilderReferenceBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Switch
{
    using System.Reflection;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Switch.IAspectSwitchBuilder"/> interface.
    /// </summary>
    internal interface IAspectSwitchBuilderReferenceBuilder : IBuilder<IAspectSwitchBuilderReference>
    {
        /// <summary>
        /// Sets reference to RegisterSwitch method.
        /// </summary>
        MethodInfo RegisterSwitchMethod { set; }

        /// <summary>
        /// Sets reference to Complete method.
        /// </summary>
        MethodInfo CompleteMethod { set; }
    }
}
