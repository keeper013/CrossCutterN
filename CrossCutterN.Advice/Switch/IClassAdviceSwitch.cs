/**
 * Description: class advice switch interface
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Switch
{
    internal interface IClassAdviceSwitch
    {
        bool IsAspectApplied(string aspect);

        int Switch(SwitchOperation operation);
        int SwitchMethod(string methodSignature, SwitchOperation operation);
        int SwitchProperty(string propertyName, SwitchOperation operation);
        int SwitchAspect(string aspect, SwitchOperation operation);
        int SwitchMethodAspect(string methodSignature, string aspect, SwitchOperation operation);
        int SwitchPropertyAspect(string propertyName, string aspect, SwitchOperation operation);

        bool? LookUp(string methodSignature, string aspect);
    }
}
