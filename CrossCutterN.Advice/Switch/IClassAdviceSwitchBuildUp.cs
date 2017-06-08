/**
 * Description: class advice switch build up interface
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Switch
{
    using System.Collections.Generic;

    internal interface IClassAdviceSwitchBuildUp
    {
        void RegisterSwitch(int id, string property, string method, string aspect);
        IClassAdviceSwitch Convert(string clazz, IClassAdviceSwitchOperation classOperations, Dictionary<string, SwitchOperationStatus> aspectOperations);
    }
}
