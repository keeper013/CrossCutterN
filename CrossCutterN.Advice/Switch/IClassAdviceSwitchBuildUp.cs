/**
 * Description: class advice switch build up interface
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Switch
{
    using Common;

    internal interface IClassAdviceSwitchBuildUp : ICanConvert<IClassAdviceSwitch>
    {
        void RegisterSwitch(int id, string property, string method, string aspect);
    }
}
