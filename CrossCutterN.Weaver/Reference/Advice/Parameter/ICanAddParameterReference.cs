/**
 * Description: ICanAddParameter reference
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using Mono.Cecil;

    internal interface ICanAddParameterReference
    {
        TypeReference TypeReference { get; }
        TypeReference ReadOnlyTypeReference { get; }

        MethodReference AddParameterMethod { get; }
        MethodReference ToReadOnlyMethod { get; }
    }
}
