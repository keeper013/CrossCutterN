/**
 * Description: class advice switch interface
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Switch
{
    internal interface IClassAdviceSwitch
    {
        bool IsAspectApplied(string aspect);

        int Switch(SwitchStatus status);
        int SwitchMethod(string methodSignature, SwitchStatus status);
        int SwitchProperty(string propertyName, SwitchStatus status);
        int SwitchAspect(string aspect, SwitchStatus status);
        int SwitchMethodAspect(string methodSignature, string aspect, SwitchStatus status);
        int SwitchPropertyAspect(string propertyName, string aspect, SwitchStatus status);

        bool? LookUp(string methodSignature, string aspect);
    }
}
