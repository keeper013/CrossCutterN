/**
 * Description: Switchable advice handler
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Switch
{
    using System.Collections.Generic;

    internal interface ISwitchHandler
    {
        IEnumerable<SwitchInitializingData> GetData();
    }
}
