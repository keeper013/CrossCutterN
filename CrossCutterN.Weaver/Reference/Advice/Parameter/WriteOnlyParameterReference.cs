/**
 * Description: IWriteOnlyParameter reference implementation
 * Author: David Cui
 */
namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using System;
    using System.Reflection;
    using Mono.Cecil;
    using CrossCutterN.Advice.Common;

    internal sealed class WriteOnlyParameterReference : ICanAddCustomAttributeReference, ICanAddCustomAttributeWriteOnlyReference
    {
        private readonly ModuleDefinition _module;
        private TypeReference _typeReference;
        private TypeReference _readOnlyTypeReference;

        private MethodReference _addCustomAttributeMethod;
        private MethodReference _toReadOnlyMethod;

        private readonly IrreversibleOperation _readOnly = new IrreversibleOperation();

        public WriteOnlyParameterReference(ModuleDefinition module)
        {
            if(module == null)
            {
                throw new ArgumentNullException("module");
            }
            _module = module;
        }

        TypeReference ICanAddCustomAttributeReference.TypeReference
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

        TypeReference ICanAddCustomAttributeReference.ReadOnlyTypeReference
        {
            get
            {
                _readOnly.Assert(true);
                return _readOnlyTypeReference;
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
                _readOnlyTypeReference = _module.Import(value);
            }
        }

        MethodReference ICanAddCustomAttributeReference.AddCustomAttributeMethod
        {
            get
            {
                _readOnly.Assert(true);
                return _addCustomAttributeMethod;
            }
        }

        public MethodInfo AddCustomAttributeMethod
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _readOnly.Assert(false);
                _addCustomAttributeMethod = _module.Import(value);
            }
        }

        MethodReference ICanAddCustomAttributeReference.ConvertMethod
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

        public ICanAddCustomAttributeReference Convert()
        {
            if(_addCustomAttributeMethod == null || _readOnlyTypeReference == null ||  
                _toReadOnlyMethod == null || _typeReference == null)
            {
                throw new InvalidOperationException("Necessary reference not set yet");
            }
            _readOnly.Apply();
            return this;
        }
    }
}
