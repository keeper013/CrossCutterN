/**
* Description: weaving context interface
* Author: David Cui
*/

namespace CrossCutterN.Weaver.AssemblyHandler
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Mono.Cecil;
    using Mono.Cecil.Cil;
    using Reference;
    using Utilities;
    using Switch;

    internal interface IWeavingContext : ICanReset
    {
        // references
        IAdviceReference AdviceReference { get; }
        void AddMethodReference(MethodInfo method);
        void AddTypeReference(Type type);
        MethodReference GetMethodReference(MethodInfo info);
        TypeReference GetTypeReference(Type type);

        // local variable settings
        int ExecutionContextVariableIndex { get; set; }
        int ExecutionVariableIndex { get; set; }
        int ExceptionVariableIndex { get; set; }
        int ExceptionHandlerIndex { get; set; }
        int FinallyHandlerIndex { get; set; }
        int ReturnVariableIndex { get; set; }
        int ReturnValueVariableIndex { get; set; }
        int HasExceptionVariableIndex { get; set; }

        // exception finally handler
        Instruction TryStartInstruction { get; set; }
        Instruction EndingInstruction { get; set; }

        // switch
        int PendingSwitchIndex { get; set; }
        IReadOnlyDictionary<FieldReference, int> FieldLocalVariableDictionary { get; }
        int GetLocalVariableForField(FieldReference field);
        void RecordLocalVariableForField(FieldReference field, int variableIndex);
        ISwitchSet ExecutionSwitches { get; }
        ISwitchSet ReturnSwitches { get; }
        ISwitchSet ExecutionContextSwitches { get; }
        ISwitchableSection ExecutionVariableSwitchableSection { get; }
        ISwitchableSection ReturnVariableSwitchableSection { get; }
        ISwitchableSection ReturnFinallySwitchableSection { get; }
        ISwitchableSection ExecutionContextVariableSwitchableSection { get; }
    }
}
