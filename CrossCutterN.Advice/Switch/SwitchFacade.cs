/**
* Description: Static user facade for advice switch
* Author: David Cui
*/

namespace CrossCutterN.Advice.Switch
{
    public static class SwitchFacade
    {
        public static IAdviceSwitch Controller { get { return SwitchBackStage.Switch; } }
    }
}
