/**
 * Description: advice swtich lookup interface
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Switch
{
    public interface IAdviceSwitchLookUp
    {
        bool IsOn(int id);
    }
}
