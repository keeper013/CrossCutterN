// <copyright file="WeavingContext.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Weaver
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using CrossCutterN.Base.Common;
    using CrossCutterN.Weaver.Reference;
    using CrossCutterN.Weaver.Switch;
    using CrossCutterN.Weaver.Utilities;
    using Mono.Cecil;
    using Mono.Cecil.Cil;

    /// <summary>
    /// Weaving context implementation.
    /// </summary>
    internal sealed class WeavingContext : IWeavingContext
    {
        private static readonly FieldReferenceComparer FieldReferenceComparer = new FieldReferenceComparer();
        private readonly IDictionary<string, MethodReference> methodReferences = new Dictionary<string, MethodReference>();
        private readonly IDictionary<string, TypeReference> typeReferences = new Dictionary<string, TypeReference>();
        private readonly ModuleDefinition module;
        private readonly IBaseReference baseReference;
        private readonly Dictionary<FieldReference, int> switchFieldVariableDictionary = new Dictionary<FieldReference, int>(FieldReferenceComparer);
        private readonly ISet<AssemblyNameReference> adviceAssemblies = new HashSet<AssemblyNameReference>(new AssemblyNameReferenceComparer());

        /// <summary>
        /// Initializes a new instance of the <see cref="WeavingContext"/> class.
        /// </summary>
        /// <param name="module">The module that this weaving context is for.</param>
        public WeavingContext(ModuleDefinition module)
        {
            this.module = module ?? throw new ArgumentNullException("module");
            baseReference = ReferenceFactory.InitializeBaseReference(module);
            ExecutionSwitches = SwitchFactory.InitializeSwitchSet();
            ReturnSwitches = SwitchFactory.InitializeSwitchSet();
            ExecutionContextSwitches = SwitchFactory.InitializeSwitchSet();
            ExecutionVariableSwitchableSection = SwitchFactory.InitializeSwitchableSection();
            ReturnVariableSwitchableSection = SwitchFactory.InitializeSwitchableSection();
            ReturnFinallySwitchableSection = SwitchFactory.InitializeSwitchableSection();
            ExecutionContextVariableSwitchableSection = SwitchFactory.InitializeSwitchableSection();
        }

        /// <inheritdoc/>
        public IBaseReference BaseReference => baseReference;

        /// <inheritdoc/>
        public List<AssemblyNameReference> AssemblyReferences => adviceAssemblies.ToList();

        /// <inheritdoc/>
        public int ExecutionContextVariableIndex { get; set; }

        /// <inheritdoc/>
        public int ExecutionVariableIndex { get; set; }

        /// <inheritdoc/>
        public int ExceptionVariableIndex { get; set; }

        /// <inheritdoc/>
        public int ExceptionHandlerIndex { get; set; }

        /// <inheritdoc/>
        public int FinallyHandlerIndex { get; set; }

        /// <inheritdoc/>
        public int ReturnValueVariableIndex { get; set; }

        /// <inheritdoc/>
        public int ReturnVariableIndex { get; set; }

        /// <inheritdoc/>
        public int HasExceptionVariableIndex { get; set; }

        /// <inheritdoc/>
        public Instruction TryStartInstruction { get; set; }

        /// <inheritdoc/>
        public Instruction EndingInstruction { get; set; }

        /// <inheritdoc/>
        public int PendingSwitchIndex { get; set; }

        /// <inheritdoc/>
        public ISwitchSet ExecutionSwitches { get; private set; }

        /// <inheritdoc/>
        public ISwitchSet ReturnSwitches { get; private set; }

        /// <inheritdoc/>
        public ISwitchSet ExecutionContextSwitches { get; private set; }

        /// <inheritdoc/>
        public ISwitchableSection ExecutionVariableSwitchableSection { get; private set; }

        /// <inheritdoc/>
        public ISwitchableSection ReturnVariableSwitchableSection { get; private set; }

        /// <inheritdoc/>
        public ISwitchableSection ReturnFinallySwitchableSection { get; private set; }

        /// <inheritdoc/>
        public ISwitchableSection ExecutionContextVariableSwitchableSection { get; private set; }

        /// <inheritdoc/>
        public IReadOnlyDictionary<FieldReference, int> LocalVariableSwitchFieldDictionary => switchFieldVariableDictionary;

        /// <inheritdoc/>
        public MethodReference GetMethodReference(MethodInfo method)
        {
#if DEBUG
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }
#endif
            var key = method.GetSignatureWithTypeFullName();
            if (!methodReferences.ContainsKey(key))
            {
                var methodReference = module.ImportReference(method);
                methodReferences.Add(key, methodReference);
                adviceAssemblies.Add(methodReference.DeclaringType.Scope as AssemblyNameReference);
            }

            return methodReferences[key];
        }

        /// <inheritdoc/>
        public void AddTypeReference(Type type)
        {
#if DEBUG
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
#endif
            var key = type.GetFullName();
            if (!typeReferences.ContainsKey(key))
            {
                typeReferences.Add(key, module.ImportReference(type));
            }
        }

        /// <inheritdoc/>
        public TypeReference GetTypeReference(Type type)
        {
#if DEBUG
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
#endif
            var key = type.GetFullName();
            if (!typeReferences.ContainsKey(key))
            {
                typeReferences.Add(key, module.ImportReference(type));
            }

            return typeReferences[key];
        }

        /// <inheritdoc/>
        public int GetLocalVariableForField(FieldReference field)
        {
#if DEBUG
            if (field == null)
            {
                throw new ArgumentNullException("field");
            }
#endif
            if (switchFieldVariableDictionary.ContainsKey(field))
            {
                return switchFieldVariableDictionary[field];
            }

            return -1;
        }

        /// <inheritdoc/>
        public void RecordLocalVariableForField(FieldReference field, int variableIndex)
        {
#if DEBUG
            if (field == null)
            {
                throw new ArgumentNullException("field");
            }

            if (variableIndex < 0)
            {
                throw new ArgumentOutOfRangeException("variableIndex");
            }

            if (switchFieldVariableDictionary.ContainsKey(field))
            {
                throw new ArgumentException($"Local variable for field {field.Name} has been added already");
            }
#endif
            switchFieldVariableDictionary.Add(field, variableIndex);
        }

        /// <inheritdoc/>
        public void Reset()
        {
            ExecutionContextVariableIndex = -1;
            ExecutionVariableIndex = -1;
            ExceptionVariableIndex = -1;
            ExceptionHandlerIndex = -1;
            FinallyHandlerIndex = -1;
            ReturnValueVariableIndex = -1;
            ReturnVariableIndex = -1;
            HasExceptionVariableIndex = -1;

            TryStartInstruction = null;
            EndingInstruction = null;

            switchFieldVariableDictionary.Clear();

            PendingSwitchIndex = -1;
            ExecutionSwitches.Reset();
            ReturnSwitches.Reset();
            ExecutionContextSwitches.Reset();
            ExecutionVariableSwitchableSection.Reset();
            ReturnVariableSwitchableSection.Reset();
            ReturnFinallySwitchableSection.Reset();
            ExecutionContextVariableSwitchableSection.Reset();
        }

        private class AssemblyNameReferenceComparer : IEqualityComparer<AssemblyNameReference>
        {
            public bool Equals(AssemblyNameReference x, AssemblyNameReference y) => string.Equals(x.Name, y.Name);

            public int GetHashCode(AssemblyNameReference obj) => obj.Name.GetHashCode();
        }
    }
}
