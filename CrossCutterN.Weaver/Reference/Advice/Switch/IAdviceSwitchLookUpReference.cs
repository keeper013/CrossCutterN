/**
 * Description: IAdviceSwitchLookup reference interface
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Switch
{
    using Mono.Cecil;

    internal interface IAdviceSwitchLookUpReference
    {
        MethodReference IsOnMethod { get; }
    }
}
