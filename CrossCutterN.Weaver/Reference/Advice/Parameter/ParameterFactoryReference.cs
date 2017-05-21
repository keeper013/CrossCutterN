/**
 * Description: data factory reference implementation
 * Author: David CUui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using System;
    using System.Reflection;
    using Mono.Cecil;
    using CrossCutterN.Advice.Common;

    internal sealed class ParameterFactoryReference : IParameterFactoryReference, IParameterFactoryWriteOnlyReference
    {
        private readonly ModuleDefinition _module;
        private MethodReference _initializeExecution;
        private MethodReference _initializeExecutionContext;
        private MethodReference _initializeParameter;
        private MethodReference _initializeCustomAttribute;
        private MethodReference _initializeAttributeProperty;
        private MethodReference _initializeReturn;

        private readonly IrreversibleOperation _readOnly = new IrreversibleOperation();

        public ParameterFactoryReference(ModuleDefinition module)
        {
            if (module == null)
            {
                throw new ArgumentNullException("module");
            }
            _module = module;
        }

        MethodReference IParameterFactoryReference.InitializeExecutionMethod
        {
            get
            {
                _readOnly.Assert(true);
                return _initializeExecution;
            }
        }

        public MethodInfo InitializeExecutionMethod
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _readOnly.Assert(false);
                _initializeExecution = _module.Import(value);
            }
        }

        MethodReference IParameterFactoryReference.InitializeExecutionContextMethod
        {
            get
            {
                _readOnly.Assert(true);
                return _initializeExecutionContext;
            }
        }

        public MethodInfo InitializeExecutionContextMethod
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _readOnly.Assert(false);
                _initializeExecutionContext = _module.Import(value);
            }
        }

        MethodReference IParameterFactoryReference.InitializeParameterMethod
        {
            get
            {
                _readOnly.Assert(true);
                return _initializeParameter;
            }
        }

        public MethodInfo InitializeParameterMethod
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _readOnly.Assert(false);
                _initializeParameter = _module.Import(value);
            }
        }

        MethodReference IParameterFactoryReference.InitializeCustomAttributeMethod
        {
            get
            {
                _readOnly.Assert(true);
                return _initializeCustomAttribute;
            }
        }

        public MethodInfo InitializeCustomAttributeMethod
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _readOnly.Assert(false);
                _initializeCustomAttribute = _module.Import(value);
            }
        }

        MethodReference IParameterFactoryReference.InitializeAttributePropertyMethod
        {
            get
            {
                _readOnly.Assert(true);
                return _initializeAttributeProperty;
            }
        }

        public MethodInfo InitializeAttributePropertyMethod
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _readOnly.Assert(false);
                _initializeAttributeProperty = _module.Import(value);
            }
        }

        MethodReference IParameterFactoryReference.InitializeReturnMethod
        {
            get
            {
                _readOnly.Assert(true);
                return _initializeReturn;
            }
        }

        public MethodInfo InitializeReturnMethod
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _readOnly.Assert(false);
                _initializeReturn = _module.Import(value);
            }
        }

        public IParameterFactoryReference Convert()
        {
            if (_initializeExecution == null || _initializeExecutionContext == null || _initializeReturn == null)
            {
                throw new InvalidOperationException("Necessary reference missing");
            }
            _readOnly.Apply();
            return this;
        }
    }
}
