/**
 * Description: IAdviceSwitchController reference interface
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Switch
{
    using Mono.Cecil;

    internal interface ISwitchBackStageReference
    {
        MethodReference LookUpGetterReference { get; }
        MethodReference BuildUpGetterReference { get; }
    }
}
