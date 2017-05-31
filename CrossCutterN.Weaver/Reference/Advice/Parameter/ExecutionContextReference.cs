/**
 * Description: ExecutionContext reference implementation
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using System;
    using System.Reflection;
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

        MethodReference IExecutionContextReference.ExceptionThrownGetter
        {
            get { return GetMethod("ExceptionThrownGetter"); }
        }

        public MethodInfo ExceptionThrownGetter
        {
            set { SetMethod("ExceptionThrownGetter", value); }
        }

        MethodReference IExecutionContextReference.MarkExceptionThrownMethod
        {
            get { return GetMethod("MarkExceptionThrownMethod"); }
        }

        public MethodInfo MarkExceptionThrownMethod
        {
            set { SetMethod("MarkExceptionThrownMethod", value); }
        }
    }
}
