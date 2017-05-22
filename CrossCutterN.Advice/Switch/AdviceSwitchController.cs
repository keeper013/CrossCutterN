/**
* Description: Static user interface for advice switch
* Author: David Cui
*/

namespace CrossCutterN.Advice.Switch
{
    public static class AdviceSwitchController
    {
        private static readonly AdviceSwitch Instance = SwitchFactory.InitializeAdviceSwitch();

        public static IAdviceSwitch Controller { get { return Instance; } }
        public static IAdviceSwitchLookUp LookUp { get { return Instance; } }
        public static IAdviceSwitchBuildUp BuildUp { get { return Instance; } }
    }
}
