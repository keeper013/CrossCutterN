/**
 * Description: IAdviceSwitchController reference interface
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Switch
{
    using Mono.Cecil;

    internal interface IAdviceSwitchControllerReference
    {
        MethodReference LookUpGetterReference { get; }
        MethodReference BuildUpGetterReference { get; }
    }
}
