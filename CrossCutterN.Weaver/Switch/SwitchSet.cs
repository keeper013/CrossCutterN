// <copyright file="SwitchSet.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Switch
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Switch set implementation.
    /// </summary>
    internal sealed class SwitchSet : ISwitchSet
    {
        private readonly HashSet<int> switches = new HashSet<int>();
        private bool isSwitchable;

        /// <inheritdoc/>
        public IReadOnlyList<int> Switches => isSwitchable ? switches.ToList().AsReadOnly() : null;

        /// <inheritdoc/>
        public void Reset()
        {
            isSwitchable = true;
            switches.Clear();
        }

        /// <inheritdoc/>
        public bool RegisterSwitch(int variableIndex) => isSwitchable && switches.Add(variableIndex);

        /// <inheritdoc/>
        public void SetUnSwitchable()
        {
            isSwitchable = false;
            switches.Clear();
        }
    }
}
