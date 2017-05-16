/**
 * Description: ICanAddCustomAttribute reference
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using Mono.Cecil;

    internal interface ICanAddCustomAttributeReference
    {
        TypeReference TypeReference { get; }
        TypeReference ReadOnlyTypeReference { get; }

        MethodReference AddCustomAttributeMethod { get; }
        MethodReference ToReadOnlyMethod { get; }
    }
}
