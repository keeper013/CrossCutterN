/**
 * Description: IAttributeProperty reference implementation
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using System;
    using Mono.Cecil;
    using CrossCutterN.Advice.Common;

    internal sealed class AttributePropertyReference : IAttributePropertyReference, IAttributePropertyWriteOnlyReference
    {
        private readonly ModuleDefinition _module;
        private TypeReference _typeReference;
        private readonly IrreversibleOperation _readOnly = new IrreversibleOperation();

        public AttributePropertyReference(ModuleDefinition module)
        {
            if (module == null)
            {
                throw new ArgumentNullException("module");
            }
            _module = module;
        }

        TypeReference IAttributePropertyReference.TypeReference
        {
            get
            {
                _readOnly.Assert(true);
                return _typeReference;
            }
        }

        public Type TypeReference
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _readOnly.Assert(false);
                _typeReference = _module.Import(value);
            }
        }

        public IAttributePropertyReference Convert()
        {
            if(_typeReference == null)
            {
                throw new InvalidOperationException("Necessary reference not set yet");
            }
            _readOnly.Apply();
            return this;
        }
    }
}
