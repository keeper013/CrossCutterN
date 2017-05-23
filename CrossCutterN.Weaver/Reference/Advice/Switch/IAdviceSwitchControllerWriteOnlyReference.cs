/**
 * Description: IAdviceSwitchController writeonly reference interface
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Switch
{
    using System.Reflection;
    using CrossCutterN.Advice.Common;

    internal interface IAdviceSwitchControllerWriteOnlyReference : ICanConvert<IAdviceSwitchControllerReference>
    {
        MethodInfo LookUpGetterReference { set; }
        MethodInfo BuildUpGetterReference { set; }
    }
}
