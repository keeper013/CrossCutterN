/**
 * Description: ExecutionContext reference implementation
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using System;
    using System.Reflection;
    using Mono.Cecil;

    internal sealed class ExecutionContextReference : IExecutionContextReference, IExecutionContextWriteOnlyReference
    {
        private readonly ModuleDefinition _module;
        private TypeReference _typeReference;
        private MethodReference _exceptionThrownGetter;
        private MethodReference _markExceptionThrown;

        public ExecutionContextReference(ModuleDefinition module)
        {
            if (module == null)
            {
                throw new ArgumentNullException("module");
            }
            _module = module;
        }

        TypeReference IExecutionContextReference.TypeReference
        {
            get
            {
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
                _typeReference = _module.Import(value);
            }
        }

        MethodReference IExecutionContextReference.ExceptionThrownGetter
        {
            get
            {
                return _exceptionThrownGetter;
            }
        }

        public MethodInfo ExceptionThrownGetter
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _exceptionThrownGetter = _module.Import(value);
            }
        }

        MethodReference IExecutionContextReference.MarkExceptionThrownMethod
        {
            get
            {
                return _markExceptionThrown;
            }
        }

        public MethodInfo MarkExceptionThrownMethod
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _markExceptionThrown = _module.Import(value);
            }
        }
    }
}
