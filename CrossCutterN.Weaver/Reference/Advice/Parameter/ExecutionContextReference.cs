/**
 * Description: ExecutionContext reference implementation
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using System;
    using Mono.Cecil;

    internal sealed class ExecutionContextReference : ReferenceBase, IExecutionContextReference, IExecutionContextWriteOnlyReference
    {
        public ExecutionContextReference(ModuleDefinition module) : base(module, false)
        {
        }

        TypeReference IExecutionContextReference.TypeReference
        {
            get { return GetType("TypeReference"); }
        }
        
        public Type TypeReference
        {
            set { SetType("TypeReference", value); }
        }

        public IExecutionContextReference Convert()
        {
            ValidateConvert("TypeReference");
            return this;
        }
    }
}
