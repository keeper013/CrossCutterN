/**
 * Description: Switch set
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Switch
{
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class SwitchSet : ISwitchSet
    {
        private bool _isSwitchable;
        private readonly HashSet<int> _switches = new HashSet<int>();

        public IReadOnlyList<int> Switches
        {
            get { return _isSwitchable ? _switches.ToList().AsReadOnly() : null; }
        }

        public void Reset()
        {
            _isSwitchable = true;
            _switches.Clear();
        }

        public bool RegisterSwitch(int variableIndex)
        {
            return _isSwitchable && _switches.Add(variableIndex);
        }

        public void SetUnSwitchable()
        {
            _isSwitchable = false;
            _switches.Clear();
        }
    }
}
