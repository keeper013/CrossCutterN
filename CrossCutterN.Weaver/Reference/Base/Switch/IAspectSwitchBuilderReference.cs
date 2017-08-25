// <copyright file="IAspectSwitchBuilderReference.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Switch
{
    using Mono.Cecil;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Switch.IAspectSwitchBuilder"/> interface.
    /// </summary>
    internal interface IAspectSwitchBuilderReference
    {
        /// <summary>
        /// Gets reference to RegisterSwitch method.
        /// </summary>
        MethodReference RegisterSwitchMethod { get; }

        /// <summary>
        /// Gets Reference to Complete method.
        /// </summary>
        MethodReference CompleteMethod { get; }
    }
}
