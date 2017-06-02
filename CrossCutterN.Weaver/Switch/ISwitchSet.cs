/**
 * Description: Switch set Interface
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Switch
{
    using System.Collections.Generic;
    using Utilities;

    internal interface ISwitchSet : ICanReset
    {
        IReadOnlyList<int> Switches { get; }
        bool RegisterSwitch(int variableIndex);
        void SetUnSwitchable();
    }
}
