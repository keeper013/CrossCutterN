/**
 * Description: IAdviceSwitchBuildUp reference interface
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Switch
{
    using Mono.Cecil;

    internal interface IAdviceSwitchBuildUpReference
    {
        MethodReference RegisterSwitchMethod { get; }
        MethodReference CompleteMethod { get; }
    }
}
