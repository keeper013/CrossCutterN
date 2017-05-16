/**
 * Description: IAttributeProperty reference
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using Mono.Cecil;

    internal interface IAttributePropertyReference
    {
        TypeReference TypeReference { get; }
    }
}
