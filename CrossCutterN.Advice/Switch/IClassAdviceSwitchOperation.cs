/**
* Description: Operation record for classes that are not loaded yet interface
* Author: David Cui
*/

namespace CrossCutterN.Advice.Switch
{
    interface IClassAdviceSwitchOperation
    {
        void Switch(SwitchStatus status);
        void SwitchMethod(string methodSignature, SwitchStatus status);
        void SwitchProperty(string propertyName, SwitchStatus status);
        void SwitchAspect(string aspect, SwitchStatus status);
        void SwitchMethodAspect(string methodSignature, string aspect, SwitchStatus status);
        void SwitchPropertyAspect(string propertyName, string aspect, SwitchStatus status);

        bool GetSwitchValue(bool value, string propertyName, string methodSignature, string aspect);
    }
}
