/**
 * Description: IWriteOnlyExecution reference
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using System;
    using System.Reflection;
    using Mono.Cecil;

    internal sealed class WriteOnlyExecutionReference : ReferenceBase, ICanAddParameterReference, ICanAddParameterWriteOnlyReference
    {
        public WriteOnlyExecutionReference(ModuleDefinition module) : base(module, true)
        {
        }

        TypeReference ICanAddParameterReference.TypeReference
        {
            get { return GetType("TypeReference"); }
        }

        public Type TypeReference
        {
            set { SetType("TypeReference", value); }
        }

        TypeReference ICanAddParameterReference.ReadOnlyTypeReference
        {
            get { return GetType("ReadOnlyTypeReference"); }
        }

        public Type ReadOnlyTypeReference
        {
            set { SetType("ReadOnlyTypeReference", value); }
        }

        MethodReference ICanAddParameterReference.AddParameterMethod
        {
            get { return GetMethod("AddParameterMethod"); }
        }

        public MethodInfo AddParameterMethod
        {
            set { SetMethod("AddParameterMethod", value); }
        }

        MethodReference ICanAddParameterReference.ConvertMethod
        {
            get { return GetMethod("ConvertMethod"); }
        }

        public MethodInfo ConvertMethod
        {
            set { SetMethod("ConvertMethod", value); }
        }

        public ICanAddParameterReference Convert()
        {
            ValidateConvert("TypeReference", "ReadOnlyTypeReference", "AddParameterMethod", "ConvertMethod");
            return this;
        }
    }
}
