/**
 * Description: class advice switch interface
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Switch
{
    internal interface IClassAdviceSwitch
    {
        bool IsAspectApplied(string aspect);

        int SwitchMethod(string methodSignature);
        int SwitchProperty(string propertyName);
        int SwitchAspect(string aspect);
        int SwitchMethodAspect(string methodSignature, string aspect);
        int SwitchPropertyAspect(string propertyName, string aspect);

        int SwitchOnMethod(string methodSignature);
        int SwitchOnProperty(string propertyName);
        int SwitchOnAspect(string aspect);
        int SwitchOnMethodAspect(string methodSignature, string aspect);
        int SwitchOnPropertyAspect(string propertyName, string aspect);

        int SwitchOffMethod(string methodSignature);
        int SwitchOffProperty(string propertyName);
        int SwitchOffAspect(string aspect);
        int SwitchOffMethodAspect(string methodSignature, string aspect);
        int SwitchOffPropertyAspect(string propertyName, string aspect);
    }
}
