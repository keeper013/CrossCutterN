/**
 * Description: common data reference
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference
{
    using Advice.Parameter;
    using Advice.Switch;

    internal interface IAdviceReference
    {
        IParameterFactoryReference ParameterFactory { get; }
        IExecutionContextReference ExecutionContext { get; }
        ICanAddParameterReference Execution { get; }
        IWriteOnlyReturnReference Return { get; }
        ICanAddCustomAttributeReference Parameter { get; }
        ICanAddAttributePropertyReference CustomAttribute { get; }
        IAttributePropertyReference AttributeProperty { get; }
        IAdviceSwitchBuildUpReference BuildUp { get; }
        ISwitchBackStageReference Controller { get; }
        IAdviceSwitchLookUpReference LookUp { get; }
    }
}
