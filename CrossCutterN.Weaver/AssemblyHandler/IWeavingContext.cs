/**
* Description: weaving context interface
* Author: David Cui
*/

namespace CrossCutterN.Weaver.AssemblyHandler
{
    using System;
    using System.Reflection;
    using Mono.Cecil;
    using Mono.Cecil.Cil;
    using Reference;

    internal interface IWeavingContext
    {
        IAdviceParameterReference AdviceParameterReference { get; }
        int ExecutionContextVariableIndex { get; set; }
        int ExecutionVariableIndex { get; set; }
        int ExceptionVariableIndex { get; set; }
        int ExceptionHandlerIndex { get; set; }
        int ReturnVariableIndex { get; set; }
        int ReturnValueVariableIndex { get; set; }
        Instruction TryStartInstruction { get; set; }
        Instruction EndingInstruction { get; set; }
        void AddMethodReference(MethodInfo method);
        void AddTypeReference(Type type);
        MethodReference GetMethodReference(MethodInfo info);
        TypeReference GetTypeReference(Type type);
        void ResetVolatileData();
    }
}
