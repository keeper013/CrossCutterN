/**
 * Description: IExecutionContext refernece
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using Mono.Cecil;

    internal interface IExecutionContextReference
    {
        TypeReference TypeReference { get; }
    }
}
