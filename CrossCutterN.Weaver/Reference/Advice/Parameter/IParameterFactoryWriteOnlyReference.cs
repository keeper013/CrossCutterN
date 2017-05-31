/**
 * Description: DataFactory write only reference
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using System.Reflection;
    using CrossCutterN.Advice.Common;

    internal interface IParameterFactoryWriteOnlyReference : ICanConvert<IParameterFactoryReference>
    {
        MethodInfo InitializeExecutionMethod { set; }
        MethodInfo InitializeExecutionContextMethod { set; }
        MethodInfo InitializeParameterMethod { set; }
        MethodInfo InitializeCustomAttributeMethod { set; }
        MethodInfo InitializeAttributePropertyMethod { set; }
        MethodInfo InitializeReturnMethod { set; }
    }
}
