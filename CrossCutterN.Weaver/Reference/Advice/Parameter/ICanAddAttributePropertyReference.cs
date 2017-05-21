/**
 * Description: ICanAddAttributeProperty reference
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using Mono.Cecil;

    internal interface ICanAddAttributePropertyReference
    {
        TypeReference TypeReference { get; }
        TypeReference ReadOnlyTypeReference { get; }

        MethodReference AddAttributePropertyMethod { get; }
        MethodReference ConvertMethod { get; }
    }
}
