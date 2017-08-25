// <copyright file="ISwitchSet.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Switch
{
    using System.Collections.Generic;
    using CrossCutterN.Weaver.Utilities;

    /// <summary>
    /// Switch set used during weaving to decide whether some section is switchable or not, and provide the set of switches if it is switchable.
    /// This approach means to optimize the performance a bit by switching off the local variables that takes time to initialize and avoiding adding switches to sections that may not be switchable at all.
    /// For example, switchable aspect A and B both needs <see cref="CrossCutterN.Base.Metadata.IExecution"/> local variable for their advices. If both A and B are switched off, then Initialization of <see cref="CrossCutterN.Base.Metadata.IExecution"/>  should be switched off.
    /// Considering initializing <see cref="CrossCutterN.Base.Metadata.IExecution"/> variable takes a long time to initialize, switching it off will help to boost the performance.
    /// However, if one of the aspects, let's say A, is not switchable, then we shouldn't apply switching to initialization of <see cref="CrossCutterN.Base.Metadata.IExecution"/> because it will be needed for aspect B anyway.
    /// </summary>
    internal interface ISwitchSet : IResetable
    {
        /// <summary>
        /// Gets the switch set in case a code section is switchable.
        /// </summary>
        IReadOnlyList<int> Switches { get; }

        /// <summary>
        /// Registers a switch with the static field index of that switch.
        /// </summary>
        /// <param name="fieldIndex">Index of the switch static field</param>
        /// <returns>Whether the registration is successful. If the section is not switchable or </returns>
        bool RegisterSwitch(int fieldIndex);

        /// <summary>
        /// Sets the switch set to be unswitchable, this method should be called when an unswitchable aspect is applied to a switchable section.
        /// </summary>
        void SetUnSwitchable();
    }
}
