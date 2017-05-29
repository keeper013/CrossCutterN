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
        IAdviceReference AdviceReference { get; }
        int ExecutionContextVariableIndex { get; set; }
        int ExecutionVariableIndex { get; set; }
        int ExceptionVariableIndex { get; set; }
        int ExceptionHandlerIndex { get; set; }
        int ReturnVariableIndex { get; set; }
        int ReturnValueVariableIndex { get; set; }
        Instruction TryStartInstruction { get; set; }
        Instruction EndingInstruction { get; set; }
        int PendingSwitchIndex { get; set; }
        IReadOnlyDictionary<FieldReference, int> FieldLocalVariableDictionary { get; }
        void AddMethodReference(MethodInfo method);
        void AddTypeReference(Type type);
        MethodReference GetMethodReference(MethodInfo info);
        TypeReference GetTypeReference(Type type);
        int GetLocalVariableForField(FieldReference field);
        void RecordLocalVariableForField(FieldReference field, int variableIndex);
        void ResetVolatileData();
    }
}
