/**
* Description: Back stage of advice switch
* Author: David Cui
*/

namespace CrossCutterN.Advice.Switch
{
    public static class SwitchBackStage
    {
        private static readonly AdviceSwitch Instance = SwitchFactory.InitializeAdviceSwitch();

        internal static AdviceSwitch Switch
        {
            get { return Instance; }
        }

        public static IAdviceSwitchLookUp LookUp { get { return Instance; } }
        public static IAdviceSwitchBuildUp BuildUp { get { return Instance; } }
    }
}
