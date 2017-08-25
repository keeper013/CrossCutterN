// <copyright file="IAspectSwitchGlancerReferenceBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Switch
{
    using System.Reflection;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Switch.IAspectSwitchBuilder"/> interface to be built up.
    /// </summary>
    internal interface IAspectSwitchGlancerReferenceBuilder : IBuilder<IAspectSwitchGlancerReference>
    {
        /// <summary>
        /// Sets reference to IsOn method.
        /// </summary>
        MethodInfo IsOnMethod { set; }
    }
}
