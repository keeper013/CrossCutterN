/**
 * Description: IWriteOnlyReturn reference
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using Mono.Cecil;

    internal interface IWriteOnlyReturnReference
    {
        TypeReference TypeReference { get; }
        TypeReference ReadOnlyTypeReference { get; }

        MethodReference HasReturnSetter { get; }
        MethodReference ValueSetter { get; }
        MethodReference ConvertMethod { get; }
    }
}
