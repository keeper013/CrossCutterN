/**
 * Description: IAdviceSwitchLookup write only reference interface
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Switch
{
    using System.Reflection;
    using CrossCutterN.Advice.Common;

    internal interface IAdviceSwitchLookUpWriteOnlyReference : ICanConvert<IAdviceSwitchLookUpReference>
    {
        MethodInfo IsOnMethod { set; }
    }
}
