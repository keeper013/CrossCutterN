/**
 * Description: DataFactory reference
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using Mono.Cecil;

    internal interface IParameterFactoryReference
    {
        MethodReference InitializeExecutionMethod { get; }
        MethodReference InitializeExecutionContextMethod { get; }
        MethodReference InitializeParameterMethod { get; }
        MethodReference InitializeCustomAttributeMethod { get; }
        MethodReference InitializeAttributePropertyMethod { get; }
        MethodReference InitializeReturnMethod { get; }
    }
}
