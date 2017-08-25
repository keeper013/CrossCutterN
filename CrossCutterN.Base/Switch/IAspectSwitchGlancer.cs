// <copyright file="IAspectSwitchGlancer.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Switch
{
    /// <summary>
    /// Switch status glancer interface.
    /// </summary>
    public interface IAspectSwitchGlancer
    {
        /// <summary>
        /// Gets a value indicates whether the switch of the id is on. This method is only supposed to be called by generated code, not CrossCutterN users.
        /// </summary>
        /// <param name="id">Id of the switch.</param>
        /// <returns>True if the switch is on, false elsewise.</returns>
        bool IsOn(int id);
    }
}
