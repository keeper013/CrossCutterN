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

    internal interface IWeavingContext
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
        int ReturnVariableIndex { get; set; }
        int ReturnValueVariableIndex { get; set; }

        // exception finally handler
        Instruction TryStartInstruction { get; set; }
        Instruction EndingInstruction { get; set; }

        // switch
        int PendingSwitchIndex { get; set; }
        IReadOnlyDictionary<FieldReference, int> FieldLocalVariableDictionary { get; }
        int GetLocalVariableForField(FieldReference field);
        void RecordLocalVariableForField(FieldReference field, int variableIndex);
        IReadOnlyList<int> NeedExecutionParameterSwitches { get; }
        IReadOnlyList<int> NeedReturnParameterSwitches { get; }
        bool RegisterNeedExecutionParameterSwitch(int variableIndex);
        void SetExecutionParameterUnSwitchable();
        Instruction ExecutionParameterStartInstruction { get; set; }
        int ExecutionParameterEndInstructionIndex { get; set; }
        Instruction ExecutionParameterEndInstruction { get; set; }
        bool RegisterNeedReturnParameterSwitch(int variableIndex);
        void SetReturnParameterUnSwitchable();
        Instruction ReturnParameterStartInstruction { get; set; }
        int ReturnParameterEndInstructionIndex { get; set; }
        Instruction ReturnParameterEndInstruction { get; set; }
        Instruction ReturnConvertStartInstruction { get; set; }
        int ReturnConvertEndInstructionIndex { get; set; }
        Instruction ReturnConvertEndInstruction { get; set; }

        // reset before weaving each method
        void ResetVolatileData();
    }
}
