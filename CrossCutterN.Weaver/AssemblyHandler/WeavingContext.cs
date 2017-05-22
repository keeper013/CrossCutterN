/**
 * Description: Weaving context implementation
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.AssemblyHandler
{
    using System;
    using System.Reflection;
    using System.Collections.Generic;
    using Mono.Cecil;
    using Mono.Cecil.Cil;
    using Advice.Common;
    using Reference;

    internal class WeavingContext : IWeavingContext
    {
        private readonly IDictionary<string, MethodReference> _methodReferences = new Dictionary<string, MethodReference>();
        private readonly IDictionary<string, TypeReference> _typeReferences = new Dictionary<string, TypeReference>();
        private readonly ModuleDefinition _module;
        private readonly IAdviceParameterReference _adviceParameterReference;

        public IAdviceParameterReference AdviceParameterReference
        {
            get { return _adviceParameterReference; }
        }
        public int ExecutionContextVariableIndex { get; set; }
        public int ExecutionVariableIndex { get; set; }
        public int ExceptionVariableIndex { get; set; }
        public int ExceptionHandlerIndex { get; set; }
        public int ReturnValueVariableIndex { get; set; }
        public int ReturnVariableIndex { get; set; }

        public Instruction TryStartInstruction { get; set; }
        public Instruction EndingInstruction { get; set; }

        public WeavingContext(ModuleDefinition module)
        {
            if (module == null)
            {
                throw new ArgumentNullException("module");
            }
            _module = module;
            _adviceParameterReference = AdviceParameterReferenceFactory.InitializeAdviceParameterReference(module);
        }

        public void AddMethodReference(MethodInfo method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }
            var key = method.GetSignatureWithTypeFullName();
            if (!_methodReferences.ContainsKey(key))
            {
                _methodReferences.Add(key, _module.Import(method));
            }
        }

        public MethodReference GetMethodReference(MethodInfo method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }
            var key = method.GetSignatureWithTypeFullName();
            if (!_methodReferences.ContainsKey(key))
            {
                _methodReferences.Add(key, _module.Import(method));
            }
            return _methodReferences[key];
        }

        public void AddTypeReference(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            var key = type.GetFullName();
            if (!_typeReferences.ContainsKey(key))
            {
                _typeReferences.Add(key, _module.Import(type));
            }
        }

        public TypeReference GetTypeReference(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            var key = type.GetFullName();
            if (!_typeReferences.ContainsKey(key))
            {
                _typeReferences.Add(key, _module.Import(type));
            }
            return _typeReferences[key];
        }

        public virtual void ResetVolatileData()
        {
            ExecutionContextVariableIndex = -1;
            ExecutionVariableIndex = -1;
            ExceptionVariableIndex = -1;
            ExceptionHandlerIndex = -1;
            ReturnValueVariableIndex = -1;
            ReturnVariableIndex = -1;
            TryStartInstruction = null;
            EndingInstruction = null;
        }
    }
}
