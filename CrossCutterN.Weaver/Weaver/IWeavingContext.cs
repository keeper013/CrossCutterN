// <copyright file="IWeavingContext.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Weaver
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using CrossCutterN.Weaver.Reference;
    using CrossCutterN.Weaver.Switch;
    using CrossCutterN.Weaver.Utilities;
    using Mono.Cecil;
    using Mono.Cecil.Cil;

    /// <summary>
    /// Interface for weaving context of a method. Reset method from <see cref="IResetable"/> should be called every time when beginning to weave a method/property getter/property/setter.
    /// </summary>
    internal interface IWeavingContext : IResetable
    {
        /// <summary>
        /// Gets reference to base module.
        /// </summary>
        IBaseReference BaseReference { get; }

        /// <summary>
        /// Gets reference to assemblies.
        /// </summary>
        List<AssemblyNameReference> AssemblyReferences { get; }

        /// <summary>
        /// Gets or sets ndex of <see cref="CrossCutterN.Base.Metadata.IExecutionContext"/> variable.
        /// </summary>
        int ExecutionContextVariableIndex { get; set; }

        /// <summary>
        /// Gets or sets index of <see cref="CrossCutterN.Base.Metadata.IExecutionContext"/> variable.
        /// </summary>
        int ExecutionVariableIndex { get; set; }

        /// <summary>
        /// Gets or sets index of System.Exception variable.
        /// </summary>
        int ExceptionVariableIndex { get; set; }

        /// <summary>
        /// Gets or sets index of <see cref="CrossCutterN.Base.Metadata.IReturn"/> variable.
        /// </summary>
        int ReturnVariableIndex { get; set; }

        /// <summary>
        /// Gets or sets index of return value variable.
        /// </summary>
        int ReturnValueVariableIndex { get; set; }

        /// <summary>
        /// Gets or sets index of HasException variable.
        /// </summary>
        int HasExceptionVariableIndex { get; set; }

        /// <summary>
        /// Gets or sets index of exception block handler of a method.
        /// </summary>
        int ExceptionHandlerIndex { get; set; }

        /// <summary>
        /// Gets or sets index of finally block handler of a method.
        /// </summary>
        int FinallyHandlerIndex { get; set; }

        /// <summary>
        /// Gets or sets starting instruction for over all try block, if it's necessary.
        /// </summary>
        Instruction TryStartInstruction { get; set; }

        /// <summary>
        /// Gets or sets ending instruction for over all try block, if it's necessary.
        /// </summary>
        Instruction EndingInstruction { get; set; }

        /// <summary>
        /// Gets or sets index for break instruction that is not filled in the instruction list because of switch.
        /// </summary>
        int PendingSwitchIndex { get; set; }

        /// <summary>
        /// Gets a dictionary that contains mapping between switch fields and local variables.
        /// </summary>
        IReadOnlyDictionary<FieldReference, int> LocalVariableSwitchFieldDictionary { get; }

        /// <summary>
        /// Gets switch set for <see cref="CrossCutterN.Base.Metadata.IExecution"/> local variable initialization in try block.
        /// </summary>
        ISwitchSet ExecutionSwitches { get; }

        /// <summary>
        /// Gets switch set for <see cref="CrossCutterN.Base.Metadata.IReturn"/> local variable initialization in try block.
        /// </summary>
        ISwitchSet ReturnSwitches { get; }

        /// <summary>
        /// Gets switch set for <see cref="CrossCutterN.Base.Metadata.IExecutionContext"/> local variable initialization in try block.
        /// </summary>
        ISwitchSet ExecutionContextSwitches { get; }

        /// <summary>
        /// Gets switchable section for <see cref="CrossCutterN.Base.Metadata.IExecution"/> local variable initialization in try block.
        /// </summary>
        ISwitchableSection ExecutionVariableSwitchableSection { get; }

        /// <summary>
        /// Gets switchable section for <see cref="CrossCutterN.Base.Metadata.IReturn"/> local variable initialization in try block.
        /// </summary>
        ISwitchableSection ReturnVariableSwitchableSection { get; }

        /// <summary>
        /// Gets switchable section for <see cref="CrossCutterN.Base.Metadata.IReturn"/> local variable updating in finally block.
        /// </summary>
        ISwitchableSection ReturnFinallySwitchableSection { get; }

        /// <summary>
        /// Gets switchable section for <see cref="CrossCutterN.Base.Metadata.IExecutionContext"/> local variable initialization in try block.
        /// </summary>
        ISwitchableSection ExecutionContextVariableSwitchableSection { get; }

        /// <summary>
        /// Gets local variable index by switch field reference.
        /// </summary>
        /// <param name="field">Switch field reference.</param>
        /// <returns>Local variable index.</returns>
        int GetLocalVariableForField(FieldReference field);

        /// <summary>
        /// Records mapping between a switch field reference and a local variable index.
        /// </summary>
        /// <param name="field">Switch field reference.</param>
        /// <param name="variableIndex">Local variable index.</param>
        void RecordLocalVariableForField(FieldReference field, int variableIndex);

        /// <summary>
        /// Adds a reference to a type.
        /// </summary>
        /// <param name="type">The type whose reference to be added.</param>
        void AddTypeReference(Type type);

        /// <summary>
        /// Gets reference to a method. If the reference doesn't exist in the context, it will be created and returned.
        /// </summary>
        /// <param name="info">The method whose reference it to be retrieved.</param>
        /// <returns>Reference to the input method.</returns>
        MethodReference GetMethodReference(MethodInfo info);

        /// <summary>
        /// Gets reference to a type. If the reference doesn't exist in the context, it will be created and returned.
        /// </summary>
        /// <param name="type">The type whose reference is to be retrieved.</param>
        /// <returns>Reference to the input type.</returns>
        TypeReference GetTypeReference(Type type);
    }
}
