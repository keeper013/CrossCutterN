// <copyright file="SwitchFacade.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Switch
{
    /// <summary>
    /// Facade for switching aspects, intended to be used by CrossCutterN users.
    /// </summary>
    public static class SwitchFacade
    {
        /// <summary>
        /// Gets the switch controller
        /// </summary>
        public static IAspectSwitch Controller => SwitchBackStage.Switch;
    }
}
