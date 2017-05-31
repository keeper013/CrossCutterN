/**
 * Description: IWriteOnlyCustomAttriute reference implementation
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using System;
    using System.Reflection;
    using Mono.Cecil;

    internal sealed class WriteOnlyCustomAttributeReference : ReferenceBase, ICanAddAttributePropertyReference, ICanAddAttributePropertyWriteOnlyReference
    {
        public WriteOnlyCustomAttributeReference(ModuleDefinition module) : base(module, true)
        {
        }

        TypeReference ICanAddAttributePropertyReference.TypeReference
        {
            get { return GetType("TypeReference"); }
        }

        public Type TypeReference
        {
            set { SetType("TypeReference", value); }
        }

        TypeReference ICanAddAttributePropertyReference.ReadOnlyTypeReference
        {
            get { return GetType("ReadOnlyTypeReference"); }
        }

        public Type ReadOnlyTypeReference
        {
            set { SetType("ReadOnlyTypeReference", value); }
        }

        MethodReference ICanAddAttributePropertyReference.AddAttributePropertyMethod
        {
            get { return GetMethod("AddAttributePropertyMethod"); }
        }

        public MethodInfo AddAttributePropertyMethod
        {
            set { SetMethod("AddAttributePropertyMethod", value); }
        }

        MethodReference ICanAddAttributePropertyReference.ConvertMethod
        {
            get { return GetMethod("ConvertMethod"); }
        }

        public MethodInfo ConvertMethod
        {
            set { SetMethod("ConvertMethod", value); }
        }

        public ICanAddAttributePropertyReference Convert()
        {
            ValidateConvert("TypeReference", "ReadOnlyTypeReference", "AddAttributePropertyMethod", "ConvertMethod");
            return this;
        }
    }
}
