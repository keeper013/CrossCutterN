/**
* Description: Operation record for classes that are not loaded yet interface
* Author: David Cui
*/

namespace CrossCutterN.Advice.Switch
{
    internal interface IClassAdviceSwitchOperation
    {
        void Switch(SwitchOperation operation);
        void SwitchMethod(string methodSignature, SwitchOperation operation);
        void SwitchProperty(string getterSignature, string setterSignature, SwitchOperation operation);
        void SwitchAspect(string aspect, SwitchOperation operation);
        void SwitchAspect(string aspect, SwitchOperation operation, int sequence);
        void SwitchMethodAspect(string methodSignature, string aspect, SwitchOperation operation);
        void SwitchPropertyAspect(string getterSignature, string setterSignature, string aspect, SwitchOperation operation);

        bool GetSwitchValue(bool value, string methodSignature, string aspect);
    }
}
