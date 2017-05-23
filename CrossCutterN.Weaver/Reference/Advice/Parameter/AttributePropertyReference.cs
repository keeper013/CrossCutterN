/**
 * Description: IAttributeProperty reference implementation
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using System;
    using Mono.Cecil;

    internal sealed class AttributePropertyReference : ReferenceBase, IAttributePropertyReference, IAttributePropertyWriteOnlyReference
    {
        public AttributePropertyReference(ModuleDefinition module) : base(module, true)
        {
        }

        TypeReference IAttributePropertyReference.TypeReference
        {
            get { return GetType("TypeReference"); }
        }

        public Type TypeReference
        {
            set { SetType("TypeReference", value); }
        }

        public IAttributePropertyReference Convert()
        {
            base.ValidateConvert("TypeReference");
            return this;
        }
    }
}
