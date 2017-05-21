/**
 * Description: IWriteOnlyCustomAttriute reference implementation
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using System;
    using System.Reflection;
    using Mono.Cecil;
    using CrossCutterN.Advice.Common;

    internal sealed class WriteOnlyCustomAttributeReference : ICanAddAttributePropertyReference, ICanAddAttributePropertyWriteOnlyReference
    {
        private readonly ModuleDefinition _module;
        private TypeReference _typeReference;
        private TypeReference _readOnlyReference;

        private MethodReference _addPropertyMethod;
        private MethodReference _toReadOnlyMethod;

        private readonly IrreversibleOperation _readOnly = new IrreversibleOperation();

        public WriteOnlyCustomAttributeReference(ModuleDefinition module)
        {
            if (module == null)
            {
                throw new ArgumentNullException("module");
            }
            _module = module;
        }

        TypeReference ICanAddAttributePropertyReference.TypeReference
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

        TypeReference ICanAddAttributePropertyReference.ReadOnlyTypeReference
        {
            get
            {
                _readOnly.Assert(true);
                return _readOnlyReference;
            }
        }

        public Type ReadOnlyTypeReference
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _readOnly.Assert(false);
                _readOnlyReference = _module.Import(value);
            }
        }

        MethodReference ICanAddAttributePropertyReference.AddAttributePropertyMethod
        {
            get
            {
                _readOnly.Assert(true);
                return _addPropertyMethod;
            }
        }

        public MethodInfo AddAttributePropertyMethod
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _readOnly.Assert(false);
                _addPropertyMethod = _module.Import(value);
            }
        }

        MethodReference ICanAddAttributePropertyReference.ConvertMethod
        {
            get
            {
                _readOnly.Assert(true);
                return _toReadOnlyMethod;
            }
        }

        public MethodInfo ConvertMethod
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _readOnly.Assert(false);
                _toReadOnlyMethod = _module.Import(value);
            }
        }

        public ICanAddAttributePropertyReference Convert()
        {
            if(_addPropertyMethod == null || _readOnlyReference == null || 
                _toReadOnlyMethod == null || _typeReference == null)
            {
                throw new InvalidOperationException("Necessary reference not set yet");
            }
            _readOnly.Apply();
            return this;
        }
    }
}
