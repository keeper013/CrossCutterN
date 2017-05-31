/**
 * Description: data factory reference implementation
 * Author: David CUui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using System.Reflection;
    using Mono.Cecil;

    internal sealed class ParameterFactoryReference : ReferenceBase, IParameterFactoryReference, IParameterFactoryWriteOnlyReference
    {
        public ParameterFactoryReference(ModuleDefinition module) : base(module, true)
        {
        }

        MethodReference IParameterFactoryReference.InitializeExecutionMethod
        {
            get { return GetMethod("InitializeExecutionMethod"); }
        }

        public MethodInfo InitializeExecutionMethod
        {
            set { SetMethod("InitializeExecutionMethod", value); }
        }

        MethodReference IParameterFactoryReference.InitializeExecutionContextMethod
        {
            get { return GetMethod("InitializeExecutionContextMethod"); }
        }

        public MethodInfo InitializeExecutionContextMethod
        {
            set { SetMethod("InitializeExecutionContextMethod", value); }
        }

        MethodReference IParameterFactoryReference.InitializeParameterMethod
        {
            get { return GetMethod("InitializeParameterMethod"); }
        }

        public MethodInfo InitializeParameterMethod
        {
            set { SetMethod("InitializeParameterMethod", value); }
        }

        MethodReference IParameterFactoryReference.InitializeCustomAttributeMethod
        {
            get { return GetMethod("InitializeCustomAttributeMethod"); }
        }

        public MethodInfo InitializeCustomAttributeMethod
        {
            set { SetMethod("InitializeCustomAttributeMethod", value); }
        }

        MethodReference IParameterFactoryReference.InitializeAttributePropertyMethod
        {
            get { return GetMethod("InitializeAttributePropertyMethod"); }
        }

        public MethodInfo InitializeAttributePropertyMethod
        {
            set { SetMethod("InitializeAttributePropertyMethod", value); }
        }

        MethodReference IParameterFactoryReference.InitializeReturnMethod
        {
            get { return GetMethod("InitializeReturnMethod"); }
        }

        public MethodInfo InitializeReturnMethod
        {
            set { SetMethod("InitializeReturnMethod", value); }
        }

        public IParameterFactoryReference Convert()
        {
            ValidateConvert("InitializeExecutionMethod", "InitializeExecutionContextMethod", "InitializeParameterMethod",
                "InitializeCustomAttributeMethod", "InitializeAttributePropertyMethod", "InitializeReturnMethod");
            return this;
        }
    }
}
