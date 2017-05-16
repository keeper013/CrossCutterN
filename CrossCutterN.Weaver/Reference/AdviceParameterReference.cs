/**
 * Description: Reference to common data implementation
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference
{
    using Advice.Parameter;

    internal sealed class AdviceParameterReference : IAdviceParameterReference
    {
        public IAttributePropertyReference AttributeProperty { get; set; }
        public ICanAddAttributePropertyReference CustomAttribute { get; set; }
        public ICanAddParameterReference Execution { get; set; }
        public IExecutionContextReference ExecutionContext { get; set; }
        public ICanAddCustomAttributeReference Parameter { get; set; }
        public IParameterFactoryReference ParameterFactory { get; set; }
        public IWriteOnlyReturnReference Return { get; set; }
    }
}
