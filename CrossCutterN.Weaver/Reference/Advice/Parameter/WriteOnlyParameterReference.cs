/**
 * Description: IWriteOnlyParameter reference implementation
 * Author: David Cui
 */
namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using System;
    using System.Reflection;
    using Mono.Cecil;

    internal sealed class WriteOnlyParameterReference : ReferenceBase, ICanAddCustomAttributeReference, ICanAddCustomAttributeWriteOnlyReference
    {

        public WriteOnlyParameterReference(ModuleDefinition module) : base(module, true)
        {
        }

        TypeReference ICanAddCustomAttributeReference.TypeReference
        {
            get { return GetType("TypeReference"); }
        }

        public Type TypeReference
        {
            set { SetType("TypeReference", value); }
        }

        TypeReference ICanAddCustomAttributeReference.ReadOnlyTypeReference
        {
            get { return GetType("ReadOnlyTypeReference"); }
        }

        public Type ReadOnlyTypeReference
        {
            set { SetType("ReadOnlyTypeReference", value); }
        }

        MethodReference ICanAddCustomAttributeReference.AddCustomAttributeMethod
        {
            get { return GetMethod("AddCustomAttributeMethod"); }
        }

        public MethodInfo AddCustomAttributeMethod
        {
            set { SetMethod("AddCustomAttributeMethod", value); }
        }

        MethodReference ICanAddCustomAttributeReference.ConvertMethod
        {
            get { return GetMethod("ConvertMethod"); }
        }

        public MethodInfo ConvertMethod
        {
            set { SetMethod("ConvertMethod", value); }
        }

        public ICanAddCustomAttributeReference Convert()
        {
            ValidateConvert("TypeReference", "ReadOnlyTypeReference", "AddCustomAttributeMethod", "ConvertMethod");
            return this;
        }
    }
}
