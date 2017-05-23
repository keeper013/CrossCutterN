/**
 * Description: build up interface for IAdviceSwitch
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Switch
{
    public interface IAdviceSwitchBuildUp
    {
        int RegisterSwitch(string clazz, string property, string method, string aspect, bool value);
        void Complete(string clazz);
    }
}
