/**
 * Description: common data reference
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference
{
    using Advice.Parameter;

    internal interface IAdviceParameterReference
    {
        IParameterFactoryReference ParameterFactory { get; }
        IExecutionContextReference ExecutionContext { get; }
        ICanAddParameterReference Execution { get; }
        IWriteOnlyReturnReference Return { get; }
        ICanAddCustomAttributeReference Parameter { get; }
        ICanAddAttributePropertyReference CustomAttribute { get; }
        IAttributePropertyReference AttributeProperty { get; }
    }
}
