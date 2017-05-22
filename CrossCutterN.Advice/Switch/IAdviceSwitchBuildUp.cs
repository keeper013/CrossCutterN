/**
 * Description: build up interface for IAdviceSwitch
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Switch
{
    public interface IAdviceSwitchBuildUp
    {
        void RegisterSwitch(int id, string clazz, string property, string method, string aspect, bool value);
        void Complete(string clazz);
    }
}
