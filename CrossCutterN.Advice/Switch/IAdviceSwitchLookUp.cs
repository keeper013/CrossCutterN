/**
 * Description: advice swtich lookup interface
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Switch
{
    internal interface IAdviceSwitchLookUp
    {
        bool IsOn(int id);
    }
}
