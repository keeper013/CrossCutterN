/**
 * Description: IWriteOnlyExecution reference
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using System;
    using System.Reflection;
    using Mono.Cecil;
    using CrossCutterN.Advice.Common;

    internal sealed class WriteOnlyExecutionReference : ICanAddParameterReference, ICanAddParameterWriteOnlyReference
    {
        private readonly ModuleDefinition _module;
        private TypeReference _typeReference;
        private TypeReference _readOnlyReference;

        private MethodReference _addParameterMethod;
        private MethodReference _toReadOnlyMethod;

        private readonly IrreversibleOperation _readOnly = new IrreversibleOperation();

        public WriteOnlyExecutionReference(ModuleDefinition module)
        {
            if (module == null)
            {
                throw new ArgumentNullException("module");
            }
            _module = module;
        }

        TypeReference ICanAddParameterReference.TypeReference
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

        TypeReference ICanAddParameterReference.ReadOnlyTypeReference
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

        MethodReference ICanAddParameterReference.AddParameterMethod
        {
            get
            {
                _readOnly.Assert(true);
                return _addParameterMethod;
            }
        }

        public MethodInfo AddParameterMethod
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _readOnly.Assert(false);
                _addParameterMethod = _module.Import(value);
            }
        }

        MethodReference ICanAddParameterReference.ToReadOnlyMethod
        {
            get
            {
                _readOnly.Assert(true);
                return _toReadOnlyMethod;
            }
        }

        public MethodInfo ToReadOnlyMethod
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

        public ICanAddParameterReference ToReadOnly()
        {
            if (_addParameterMethod == null || _typeReference == null ||
                _readOnlyReference == null || _toReadOnlyMethod == null)
            {
                throw new InvalidOperationException("Necessary reference not set yet");
            }
            _readOnly.Apply();
            return this;
        }
    }
}
