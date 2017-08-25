// <copyright file="SwitchBackStage.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Switch
{
    /// <summary>
    /// Back stage switch user interface, only supposed to be called by injected code of CrossCutterN, not supposed to be called by CrossCutterN users.
    /// </summary>
    public static class SwitchBackStage
    {
        private static readonly AspectSwitch Instance = SwitchFactory.InitializeAspectSwitch();

        /// <summary>
        /// Gets the look up interface.
        /// </summary>
        public static IAspectSwitchGlancer Glancer => Instance;

        /// <summary>
        /// Gets the build up interface
        /// </summary>
        public static IAspectSwitchBuilder Builder => Instance;

        /// <summary>
        /// Gets the aspect switch.
        /// </summary>
        internal static AspectSwitch Switch => Instance;
    }
}
