// <copyright file="IAspectSwitchGlancerReference.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Switch
{
    using Mono.Cecil;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Switch.IAspectSwitchGlancer"/> interface.
    /// </summary>
    internal interface IAspectSwitchGlancerReference
    {
        /// <summary>
        /// Gets reference to IsOn method.
        /// </summary>
        MethodReference IsOnMethod { get; }
    }
}
