/**
 * Description: IWriteOnlyReturn reference implementation
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using System;
    using System.Reflection;
    using Mono.Cecil;
    using CrossCutterN.Advice.Common;

    internal sealed class WriteOnlyReturnReference : IWriteOnlyReturnReference, IWriteOnlyReturnWriteOnlyReference
    {
        private readonly ModuleDefinition _module;
        private TypeReference _typeReference;
        private TypeReference _readOnlyTypeReference;
        private MethodReference _hasReturnSetter;
        private MethodReference _valueSetter;
        private MethodReference _toReadOnlyMethod;

        private readonly IrreversibleOperation _readOnly = new IrreversibleOperation();

        public WriteOnlyReturnReference(ModuleDefinition module)
        {
            if (module == null)
            {
                throw new ArgumentNullException("module");
            }
            _module = module;
        }

        TypeReference IWriteOnlyReturnReference.TypeReference
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

        TypeReference IWriteOnlyReturnReference.ReadOnlyTypeReference
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

        MethodReference IWriteOnlyReturnReference.HasReturnSetter
        {
            get
            {
                _readOnly.Assert(true);
                return _hasReturnSetter;
            }
        }

        public MethodInfo HasReturnSetter
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _readOnly.Assert(false);
                _hasReturnSetter = _module.Import(value);
            }
        }

        MethodReference IWriteOnlyReturnReference.ValueSetter
        {
            get
            {
                _readOnly.Assert(true);
                return _valueSetter;
            }
        }

        public MethodInfo ValueSetter
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _readOnly.Assert(false);
                _valueSetter = _module.Import(value);
            }
        }

        MethodReference IWriteOnlyReturnReference.ConvertMethod
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

        public IWriteOnlyReturnReference Convert()
        {
            if (_hasReturnSetter == null || _typeReference == null || _valueSetter == null || 
                _toReadOnlyMethod == null || _readOnlyTypeReference == null)
            {
                throw new InvalidOperationException("Necessary reference not set yet");
            }
            _readOnly.Apply();
            return this;
        }
    }
}
