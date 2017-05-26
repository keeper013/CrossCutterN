/**
 * Description: switchable aspect options
 * Author: David Cui
 */

namespace CrossCutterN.Aspect
{
    public enum SwitchStatus
    {
        NotSwitchable,
        On,
        Off
    }

    public static class SwitchStatusExtention
    {
        public static bool IsSwitchable(this SwitchStatus status)
        {
            return status != SwitchStatus.NotSwitchable;
        }
    }
}
