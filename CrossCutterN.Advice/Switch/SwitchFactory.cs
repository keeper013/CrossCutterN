/**
* Description: Switch factory
* Author: David Cui
*/

namespace CrossCutterN.Advice.Switch
{
    using System.Collections.Generic;

    internal static class SwitchFactory
    {
        public static IClassAdviceSwitchBuildUp InitializeClassAdviceSwitch(IList<bool> switchList)
        {
            return new ClassAdviceSwitch(switchList);
        }

        public static IClassAdviceSwitchOperation InitializeClassAdviceSwitchOperation(
            IReadOnlyDictionary<string, SwitchOperation> aspectOperations, SequenceGenerator sequenceGenerator)
        {
            return new ClassAdviceSwitchOperation(aspectOperations, sequenceGenerator);
        }

        public static SwitchOperation InitializeSwitchOperation(SequenceGenerator sequenceGenerator, SwitchStatus status)
        {
            return new SwitchOperation(sequenceGenerator, status);
        }

        public static AdviceSwitch InitializeAdviceSwitch()
        {
            return new AdviceSwitch();
        }
    }
}
