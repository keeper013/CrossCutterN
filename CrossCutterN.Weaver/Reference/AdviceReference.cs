/**
 * Description: Reference to common data implementation
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference
{
    using Advice.Parameter;
    using Advice.Switch;

    internal sealed class AdviceReference : IAdviceReference
    {
        public IAttributePropertyReference AttributeProperty { get; set; }
        public ICanAddAttributePropertyReference CustomAttribute { get; set; }
        public ICanAddParameterReference Execution { get; set; }
        public IExecutionContextReference ExecutionContext { get; set; }
        public ICanAddCustomAttributeReference Parameter { get; set; }
        public IParameterFactoryReference ParameterFactory { get; set; }
        public IWriteOnlyReturnReference Return { get; set; }
        public IAdviceSwitchBuildUpReference BuildUp { get; set; }
        public ISwitchBackStageReference Controller { get; set; }
        public IAdviceSwitchLookUpReference LookUp { get; set; }
    }
}
