/**
 * Description: IWriteOnlyReturn reference implementation
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using System;
    using System.Reflection;
    using Mono.Cecil;

    internal sealed class WriteOnlyReturnReference : ReferenceBase, IWriteOnlyReturnReference, IWriteOnlyReturnWriteOnlyReference
    {
        public WriteOnlyReturnReference(ModuleDefinition module) : base(module, true)
        {
        }

        TypeReference IWriteOnlyReturnReference.TypeReference
        {
            get { return GetType("TypeReference"); }
        }

        public Type TypeReference
        {
            set { SetType("TypeReference", value); }
        }

        TypeReference IWriteOnlyReturnReference.ReadOnlyTypeReference
        {
            get { return GetType("ReadOnlyTypeReference"); }
        }

        public Type ReadOnlyTypeReference
        {
            set { SetType("ReadOnlyTypeReference", value); }
        }

        MethodReference IWriteOnlyReturnReference.HasReturnSetter
        {
            get { return GetMethod("HasReturnSetter"); }
        }

        public MethodInfo HasReturnSetter
        {
            set { SetMethod("HasReturnSetter", value); }
        }

        MethodReference IWriteOnlyReturnReference.ValueSetter
        {
            get { return GetMethod("ValueSetter"); }
        }

        public MethodInfo ValueSetter
        {
            set { SetMethod("ValueSetter", value); }
        }

        MethodReference IWriteOnlyReturnReference.ConvertMethod
        {
            get { return GetMethod("ConvertMethod"); }
        }

        public MethodInfo ConvertMethod
        {
            set { SetMethod("ConvertMethod", value); }
        }

        public IWriteOnlyReturnReference Convert()
        {
            ValidateConvert("TypeReference", "ReadOnlyTypeReference", "HasReturnSetter", 
                "ValueSetter", "ConvertMethod");
            return this;
        }
    }
}
