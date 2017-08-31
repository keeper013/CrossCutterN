// <copyright file="MethodWeaver.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Weaver
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CrossCutterN.Aspect.Aspect;
    using CrossCutterN.Base.Common;
    using CrossCutterN.Weaver.Statistics;
    using CrossCutterN.Weaver.Switch;
    using CrossCutterN.Weaver.Utilities;
    using Mono.Cecil;
    using Mono.Cecil.Cil;

    /// <summary>
    /// Method weaver for methods and property getter/setters in a class.
    /// </summary>
    internal sealed class MethodWeaver
    {
        private readonly IReadOnlyList<CrossCutterN.Aspect.Metadata.ICustomAttribute> classCustomAttributes;
        private readonly IWeavingContext context;
        private readonly ISwitchHandlerBuilder switchHandlerBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodWeaver"/> class.
        /// </summary>
        /// <param name="classCustomAttributes">Custom attributes of the class of the method.</param>
        /// <param name="context">Method weaving context.</param>
        /// <param name="switchHandlerBuilder">Builder of switch handler, used for populating switch initialization of the class.</param>
        public MethodWeaver(
            IReadOnlyList<CrossCutterN.Aspect.Metadata.ICustomAttribute> classCustomAttributes,
            IWeavingContext context,
            ISwitchHandlerBuilder switchHandlerBuilder)
        {
            this.classCustomAttributes = classCustomAttributes ?? throw new ArgumentNullException("classCustomAttributes");
            this.context = context ?? throw new ArgumentNullException("context");
            this.switchHandlerBuilder = switchHandlerBuilder ?? throw new ArgumentNullException("switchHandlerBuilder");
        }

        /// <summary>
        /// Weave the method.
        /// </summary>
        /// <param name="method">Method to be weaved.</param>
        /// <param name="plan">Weaving plan.</param>
        /// <param name="propertyName">Name of the property in case the method is a getter or setter method.</param>
        /// <param name="statistics">Weaving statistics.</param>
        public void Weave(MethodDefinition method, IWeavingPlan plan, string propertyName, ICanAddMethodWeavingRecord statistics)
        {
#if DEBUG
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            if (plan == null)
            {
                throw new ArgumentNullException("plan");
            }
#endif
            context.Reset();
            var instructions = new List<Instruction>();
            var methodSignature = method.GetSignature();
            var processor = method.Body.GetILProcessor();

            AddLocalVariables(method, processor, plan);
            WeaveEntryJoinPoint(method, processor, plan.GetAdvices(JoinPoint.Entry), statistics, propertyName, methodSignature);
            WeaveExceptionJoinPoint(method, processor, plan.GetAdvices(JoinPoint.Exception), statistics, propertyName, methodSignature);
            WeaveExitJoinPoint(method, processor, plan.GetAdvices(JoinPoint.Exit), statistics, propertyName, methodSignature);
            WeaveSwitchInitialization(method, processor);
        }

        private static bool NeedToStoreReturnValueAsLocalVariable(MethodDefinition method, IWeavingPlan plan) => !method.IsVoidReturn() && plan.NeedToStoreReturnValueAsLocalVariable();

        private static bool NeedHasExceptionVariable(MethodDefinition method, IWeavingPlan plan) => plan.NeedHasExceptionVariable() || (plan.NeedReturnVariable() && !method.IsVoidReturn());

        private static int GetTransferOffset(Instruction from, Instruction to)
        {
            var offset = 0;
            for (var current = from; current != to; current = current.Next)
            {
                offset += current.GetSize();
            }

            return offset;
        }

        private static bool OffsetIsShort(int offset)
        {
            return offset >= -128 && offset <= 127;
        }

        private void AddLocalVariables(MethodDefinition method, ILProcessor processor, IWeavingPlan plan)
        {
            var instructions = new List<Instruction>();

            // ordering of declarations of the local variables matters when applying switches in the end
            if (plan.NeedContextVariable())
            {
                AddExecutionContextVariable(method, instructions, processor);
            }

            if (plan.NeedExecutionVariable())
            {
                AddExecutionVariable(method, instructions, processor);
            }

            if (plan.NeedExceptionVariable())
            {
                AddExceptionVariable(method);
            }

            if (plan.NeedReturnVariable())
            {
                AddReturnVariable(method, instructions, processor);
            }

            if (NeedToStoreReturnValueAsLocalVariable(method, plan))
            {
                AddReturnValueVariable(method);
            }

            if (NeedHasExceptionVariable(method, plan))
            {
                AddHasExceptionVariable(method, instructions, processor);
            }

            CompleteAddingLocalVariableInstructions(method, instructions, processor);
        }

        private void WeaveEntryJoinPoint(
            MethodDefinition method,
            ILProcessor processor,
            IReadOnlyCollection<IAdviceInfo> advices,
            ICanAddMethodWeavingRecord statistics,
            string propertyName,
            string methodSignature)
        {
            var instructions = new List<Instruction>();
            if (advices != null && advices.Any())
            {
                for (var i = 0; i < advices.Count; i++)
                {
                    var advice = advices.ElementAt(i);
                    CallAdvice(method, processor, advice, instructions, propertyName, methodSignature);
                    var record = StatisticsFactory.InitializeWeavingRecord(
                        JoinPoint.Entry, advice.AspectName, advice.Advice.GetFullName(), advice.Advice.GetSignatureWithTypeFullName(), i);
                    statistics.AddWeavingRecord(record);
                }

                CompleteWeavingEntry(instructions, processor);
            }
        }

        private void WeaveExceptionJoinPoint(
            MethodDefinition method,
            ILProcessor processor,
            IReadOnlyCollection<IAdviceInfo> advices,
            ICanAddMethodWeavingRecord statistics,
            string propertyName,
            string methodSignature)
        {
            var instructions = new List<Instruction>();
            UpdateLocalVariablesOnException(instructions, processor);
            if (advices != null && advices.Any())
            {
                for (var i = 0; i < advices.Count; i++)
                {
                    var advice = advices.ElementAt(i);
                    CallAdvice(method, processor, advice, instructions, propertyName, methodSignature);
                    var record = StatisticsFactory.InitializeWeavingRecord(
                        JoinPoint.Exception, advice.AspectName, advice.Advice.GetFullName(), advice.Advice.GetSignatureWithTypeFullName(), i);
                    statistics.AddWeavingRecord(record);
                }
            }

            CompleteWeavingException(method, instructions, processor);
        }

        private void WeaveExitJoinPoint(
            MethodDefinition method,
            ILProcessor processor,
            IReadOnlyCollection<IAdviceInfo> advices,
            ICanAddMethodWeavingRecord statistics,
            string propertyName,
            string methodSignature)
        {
            var instructions = new List<Instruction>();
            if (advices != null && advices.Any())
            {
                UpdateLocalVariablesOnExit(method, instructions, processor);

                // inject advices
                for (var i = 0; i < advices.Count; i++)
                {
                    var advice = advices.ElementAt(i);
                    CallAdvice(method, processor, advice, instructions, propertyName, methodSignature);
                    var record = StatisticsFactory.InitializeWeavingRecord(
                        JoinPoint.Exit, advice.AspectName, advice.Advice.GetFullName(), advice.Advice.GetSignatureWithTypeFullName(), i);
                    statistics.AddWeavingRecord(record);
                }
            }

            CompleteWeavingExit(method, instructions, processor);
        }

        private void WeaveSwitchInitialization(MethodDefinition method, ILProcessor processor)
        {
            var instructions = new List<Instruction>();
            var dict = context.LocalVariableSwitchFieldDictionary;
            if (dict.Any())
            {
                foreach (var fieldVariable in dict)
                {
                    instructions.Add(processor.Create(OpCodes.Call, context.BaseReference.BackStage.GlancerGetterReference));
                    instructions.Add(processor.Create(OpCodes.Ldsfld, fieldVariable.Key));
                    instructions.Add(processor.Create(OpCodes.Callvirt, context.BaseReference.Glancer.IsOnMethod));
                    instructions.Add(processor.Create(OpCodes.Stloc, fieldVariable.Value));
                }

                IlUtilities.PersistentInstructions(instructions, processor, method.Body.Instructions.First());
            }

            // hasException is only one bool value, swithcing it on and off costs more than just leaving it being set, don't switch it
            var executionVariableSwitches = context.ExecutionSwitches.Switches;
            var returnVariableSwitches = context.ReturnSwitches.Switches;
            var executionContextVariableSwitches = context.ExecutionContextSwitches.Switches;

            // inject with reverse order from declaration to adjust jump to statement
            // local variables
            var originalStart = context.ReturnVariableSwitchableSection.StartInstruction;
            var newStart = ApplySwitches(processor, returnVariableSwitches, context.ReturnVariableSwitchableSection);
            if (newStart != null)
            {
                context.ExecutionVariableSwitchableSection.AdjustEndInstruction(originalStart, newStart);
            }

            originalStart = context.ExecutionVariableSwitchableSection.StartInstruction ?? originalStart;
            newStart = ApplySwitches(processor, executionVariableSwitches, context.ExecutionVariableSwitchableSection) ?? newStart;
            if (newStart != null)
            {
                context.ExecutionContextVariableSwitchableSection.AdjustEndInstruction(originalStart, newStart);
            }

            ApplySwitches(processor, executionContextVariableSwitches, context.ExecutionContextVariableSwitchableSection);

            // finally block
            originalStart = context.ReturnFinallySwitchableSection.StartInstruction;
            newStart = ApplySwitches(processor, returnVariableSwitches, context.ReturnFinallySwitchableSection);

            // new start can't be null if original start isn't null
            if (newStart != null)
            {
                AdjustExceptionFinallyBorder(method, originalStart, newStart);
            }
        }

        private Instruction ApplySwitches(ILProcessor processor, IReadOnlyList<int> switches, ISwitchableSection section)
        {
            if (switches != null && switches.Any() && section.HasSetStartEndInstruction)
            {
                var count = switches.Count;
                var instructions = new List<Instruction>(count * 2);
                var sectionOffset = GetTransferOffset(section.StartInstruction, section.EndInstruction);
                var offset = 0;
                var instruction = processor.Create(OffsetIsShort(sectionOffset) ? OpCodes.Brfalse_S : OpCodes.Brfalse, section.EndInstruction);
                offset += instruction.GetSize();
                instructions.Add(instruction);
                instruction = processor.Create(OpCodes.Ldloc, switches[count - 1]);
                offset += instruction.GetSize();
                instructions.Add(instruction);
                for (var i = count - 2; i >= 0; i--)
                {
                    instruction = processor.Create(OffsetIsShort(offset) ? OpCodes.Brtrue_S : OpCodes.Brtrue, section.StartInstruction);
                    offset += instruction.GetSize();
                    instructions.Add(instruction);
                    instruction = processor.Create(OpCodes.Ldloc, switches[i]);
                    offset += instruction.GetSize();
                    instructions.Add(instruction);
                }

                instructions.Reverse();
                var result = instructions.First();
                IlUtilities.PersistentInstructions(instructions, processor, section.StartInstruction);
                return result;
            }

            return null;
        }

        private void CallAdvice(MethodDefinition method, ILProcessor processor, IAdviceInfo advice, List<Instruction> instructions, string propertyName, string methodSignature)
        {
            if (advice == null)
            {
                throw new ArgumentNullException("advice");
            }

            var pendingSwitchIndex = context.PendingSwitchIndex;
            var firstIndex = instructions.Count;
            var needContextVariable = advice.ParameterFlag.Contains(AdviceParameterFlag.Context);
            var needExecutionVariable = advice.ParameterFlag.Contains(AdviceParameterFlag.Execution);
            var needReturnVariable = advice.ParameterFlag.Contains(AdviceParameterFlag.Return);
            if (advice.IsSwitchedOn.HasValue)
            {
                var field = switchHandlerBuilder.GetSwitchField(
                    propertyName,
                    methodSignature,
                    advice.AspectName,
                    advice.IsSwitchedOn.Value);
                var index = context.GetLocalVariableForField(field);
                if (index < 0)
                {
                    index = method.Body.Variables.Count;
                    method.Body.Variables.Add(new VariableDefinition(context.GetTypeReference(typeof(bool))));
                    context.RecordLocalVariableForField(field, index);
                }

                if (needContextVariable)
                {
                    context.ExecutionContextSwitches.RegisterSwitch(index);
                }

                if (needExecutionVariable)
                {
                    context.ExecutionSwitches.RegisterSwitch(index);
                }

                if (needReturnVariable)
                {
                    context.ReturnSwitches.RegisterSwitch(index);
                }

                instructions.Add(processor.Create(OpCodes.Ldloc, index));

                // the null instruction is to be filled in later
                context.PendingSwitchIndex = instructions.Count;
                instructions.Add(null);
            }
            else
            {
                if (needContextVariable)
                {
                    context.ExecutionContextSwitches.SetUnSwitchable();
                }

                if (needExecutionVariable)
                {
                    context.ExecutionSwitches.SetUnSwitchable();
                }

                if (needReturnVariable)
                {
                    context.ReturnSwitches.SetUnSwitchable();
                }
            }

            if (needContextVariable)
            {
                instructions.Add(processor.Create(OpCodes.Ldloc, context.ExecutionContextVariableIndex));
            }

            if (needExecutionVariable)
            {
                instructions.Add(processor.Create(OpCodes.Ldloc, context.ExecutionVariableIndex));
            }

            if (advice.ParameterFlag.Contains(AdviceParameterFlag.Exception))
            {
                instructions.Add(processor.Create(OpCodes.Ldloc, context.ExceptionVariableIndex));
            }

            if (needReturnVariable)
            {
                instructions.Add(processor.Create(OpCodes.Ldloc, context.ReturnVariableIndex));
            }

            if (advice.ParameterFlag.Contains(AdviceParameterFlag.HasException))
            {
                instructions.Add(processor.Create(OpCodes.Ldloc, context.HasExceptionVariableIndex));
            }

            instructions.Add(processor.Create(OpCodes.Call, context.GetMethodReference(advice.Advice)));

            // apply switch from last advice call
            if (pendingSwitchIndex >= 0)
            {
                // here brfalse_S is safe, considering only several instructions are inserted after break instruction.
                instructions[pendingSwitchIndex] = processor.Create(OpCodes.Brfalse_S, instructions[firstIndex]);

                // reset pending switch index if no switch this time
                if (!advice.IsSwitchedOn.HasValue)
                {
                    context.PendingSwitchIndex = -1;
                }
            }
        }

        private void AddExecutionContextVariable(MethodDefinition method, List<Instruction> instructions, ILProcessor processor)
        {
            context.ExecutionContextVariableIndex = method.Body.Variables.Count;
            method.Body.Variables.Add(new VariableDefinition(context.BaseReference.ExecutionContext.TypeReference));
            context.ExecutionContextVariableSwitchableSection.StartIndex = instructions.Count;
            instructions.Add(processor.Create(OpCodes.Call, context.BaseReference.MetadataFactory.InitializeExecutionContextMethod));
            instructions.Add(processor.Create(OpCodes.Stloc, context.ExecutionContextVariableIndex));
            context.ExecutionContextVariableSwitchableSection.EndIndex = instructions.Count;
        }

        private void AddExecutionVariable(MethodDefinition method, List<Instruction> instructions, ILProcessor processor)
        {
            // evaluation stack: bottom
            method.Body.Variables.Add(new VariableDefinition(context.BaseReference.Execution.TypeReference));
            context.ExecutionVariableSwitchableSection.StartIndex = instructions.Count;
            instructions.Add(processor.Create(OpCodes.Ldstr, method.Module.Assembly.FullName));
            instructions.Add(processor.Create(OpCodes.Ldstr, method.DeclaringType.Namespace));
            instructions.Add(processor.Create(OpCodes.Ldstr, method.DeclaringType.FullName));
            instructions.Add(processor.Create(OpCodes.Ldstr, method.DeclaringType.Name));
            instructions.Add(processor.Create(OpCodes.Ldstr, method.FullName));
            instructions.Add(processor.Create(OpCodes.Ldstr, method.Name));
            instructions.Add(processor.Create(OpCodes.Ldstr, method.ReturnType.FullName));

            // evaluation stack after the following statement: bottom->ICanAddParameters
            instructions.Add(processor.Create(OpCodes.Call, context.BaseReference.MetadataFactory.InitializeExecutionMethod));
            if (method.HasParameters)
            {
                foreach (var parameter in method.Parameters)
                {
                    // evaluation stack after the following statement: bottom->ICanAddParameters->ICanAddParameters
                    instructions.Add(processor.Create(OpCodes.Dup));
                    instructions.Add(processor.Create(OpCodes.Ldstr, parameter.Name));
                    instructions.Add(processor.Create(OpCodes.Ldstr, parameter.ParameterType.FullName));
                    instructions.Add(processor.Create(OpCodes.Ldc_I4, parameter.Sequence));
                    instructions.Add(processor.Create(OpCodes.Ldarg, parameter.Sequence));

                    // insert indirect load instruction
                    if (parameter.ParameterType.IsByReference && parameter.ParameterType is TypeSpecification)
                    {
                        instructions.Add(processor.CreateIndirectLoadInstruction(parameter.ParameterType));
                    }

                    // insert box instruction
                    if (parameter.ParameterType.IsValueType)
                    {
                        instructions.Add(processor.CreateBoxValueTypeInstruction(parameter.ParameterType));
                    }

                    // evaluation stack after the following statement: bottom->ICanAddParameters->ICanAddParameters->ICanAddCustomAttribute
                    instructions.Add(processor.Create(OpCodes.Call, context.BaseReference.MetadataFactory.InitializeParameterMethod));
                    if (parameter.HasCustomAttributes)
                    {
                        for (var i = 0; i < parameter.CustomAttributes.Count; i++)
                        {
                            var attribute = parameter.CustomAttributes[i];

                            // evaluation stack after the following statement: bottom->ICanAddParameters->ICanAddParameters->ICanAddCustomAttribute->ICanAddCustomAttribute
                            instructions.Add(processor.Create(OpCodes.Dup));
                            instructions.Add(processor.Create(OpCodes.Ldstr, attribute.AttributeType.FullName));
                            instructions.Add(processor.Create(OpCodes.Ldc_I4, i));

                            // evaluation stack after the following statement: bottom->ICanAddParameters->ICanAddParameters->ICanAddCustomAttribute->ICanAddCustomAttribute->ICanAddAttributeProperty
                            instructions.Add(processor.Create(OpCodes.Call, context.BaseReference.MetadataFactory.InitializeCustomAttributeMethod));
                            if (attribute.HasProperties)
                            {
                                for (var j = 0; j < attribute.Properties.Count; j++)
                                {
                                    var property = attribute.Properties[j];
                                    if (IlUtilities.CustomAttributePropertyTypeIsSupported(property.Argument))
                                    {
                                        // evaluation stack after the following statement: bottom->ICanAddParameters->ICanAddParameters->ICanAddCustomAttribute->ICanAddCustomAttribute->ICanAddAttributeProperty->ICanAddAttributeProperty
                                        instructions.Add(processor.Create(OpCodes.Dup));
                                        instructions.Add(processor.Create(OpCodes.Ldstr, property.Name));
                                        instructions.Add(processor.Create(OpCodes.Ldstr, property.Argument.Type.FullName));
                                        instructions.Add(processor.Create(OpCodes.Ldc_I4, j));
                                        instructions.Add(processor.CreateLoadCustomAttributePropertyValueInstruction(property.Argument));
                                        if (property.Argument.Type.IsValueType)
                                        {
                                            instructions.Add(processor.CreateBoxValueTypeInstruction(property.Argument.Type));
                                        }

                                        // evaluation stack after the following statement: bottom->ICanAddParameters->ICanAddParameters->ICanAddCustomAttribute->ICanAddCustomAttribute->ICanAddAttributeProperty->ICanAddAttributeProperty->IAttributeProperty
                                        instructions.Add(processor.Create(OpCodes.Call, context.BaseReference.MetadataFactory.InitializeAttributePropertyMethod));

                                        // evaluation stack after the following statement: bottom->ICanAddParameters->ICanAddParameters->ICanAddCustomAttribute->ICanAddCustomAttribute->ICanAddAttributeProperty
                                        instructions.Add(processor.Create(OpCodes.Callvirt, context.BaseReference.CustomAttribute.AddAttributePropertyMethod));
                                    }
                                }
                            }

                            // evaluation stack after the following statement: bottom->ICanAddParameters->ICanAddParameters->ICanAddCustomAttribute->ICanAddCustomAttribute->ICustomAttribute
                            instructions.Add(processor.Create(OpCodes.Callvirt, context.BaseReference.CustomAttribute.BuildMethod));

                            // evaluation stack after the following statement: bottom->ICanAddParameters->ICanAddParameters->ICanAddCustomAttribute
                            instructions.Add(processor.Create(OpCodes.Callvirt, context.BaseReference.Parameter.AddCustomAttributeMethod));
                        }
                    }

                    // evaluation stack after the following statement: bottom->ICanAddParameters->ICanAddParameters->IParameter
                    instructions.Add(processor.Create(OpCodes.Callvirt, context.BaseReference.Parameter.BuildMethod));

                    // evaluation stack after the following statement: bottom->ICanAddParameters
                    instructions.Add(processor.Create(OpCodes.Callvirt, context.BaseReference.Execution.AddParameterMethod));
                }
            }

            // evaluation stack after the following statement: bottom->IMethodExecution
            instructions.Add(processor.Create(OpCodes.Callvirt, context.BaseReference.Execution.BuildMethod));
            context.ExecutionVariableIndex = method.Body.Variables.Count;
            method.Body.Variables.Add(new VariableDefinition(context.BaseReference.Execution.ReadOnlyTypeReference));
            instructions.Add(processor.Create(OpCodes.Stloc, context.ExecutionVariableIndex));

            // evaluation stack after the following statement: bottom
            context.ExecutionVariableSwitchableSection.EndIndex = instructions.Count;
        }

        private void AddExceptionVariable(MethodDefinition method)
        {
            context.ExceptionVariableIndex = method.Body.Variables.Count;
            method.Body.Variables.Add(new VariableDefinition(context.GetTypeReference(typeof(Exception))));
        }

        private void AddReturnVariable(MethodDefinition method, List<Instruction> instructions, ILProcessor processor)
        {
            context.ReturnVariableIndex = method.Body.Variables.Count;
            method.Body.Variables.Add(new VariableDefinition(context.BaseReference.Return.TypeReference));
            context.ReturnVariableSwitchableSection.StartIndex = instructions.Count;
            instructions.Add(processor.Create(OpCodes.Ldstr, method.ReturnType.FullName));
            instructions.Add(processor.Create(OpCodes.Call, context.BaseReference.MetadataFactory.InitializeReturnMethod));
            instructions.Add(processor.Create(OpCodes.Stloc, context.ReturnVariableIndex));
            context.ReturnVariableSwitchableSection.EndIndex = instructions.Count;
        }

        private void AddReturnValueVariable(MethodDefinition method)
        {
            context.ReturnValueVariableIndex = method.Body.Variables.Count;
            method.Body.Variables.Add(new VariableDefinition(method.ReturnType));
        }

        private void AddHasExceptionVariable(MethodDefinition method, List<Instruction> instructions, ILProcessor processor)
        {
            context.HasExceptionVariableIndex = method.Body.Variables.Count;
            method.Body.Variables.Add(new VariableDefinition(context.GetTypeReference(typeof(bool))));
            instructions.Add(processor.Create(OpCodes.Ldc_I4_0));
            instructions.Add(processor.Create(OpCodes.Stloc, context.HasExceptionVariableIndex));
        }

        private void CompleteAddingLocalVariableInstructions(MethodDefinition method, List<Instruction> instructions, ILProcessor processor)
        {
            context.TryStartInstruction = method.Body.Instructions.First();
            context.ExecutionContextVariableSwitchableSection.SetInstructions(instructions, context.TryStartInstruction);
            context.ExecutionVariableSwitchableSection.SetInstructions(instructions, context.TryStartInstruction);
            context.ReturnVariableSwitchableSection.SetInstructions(instructions, context.TryStartInstruction);

            IlUtilities.PersistentInstructions(instructions, processor, context.TryStartInstruction);
        }

        private void CompleteWeavingEntry(List<Instruction> instructions, ILProcessor processor)
        {
            // apply switch from last advice call
            if (context.PendingSwitchIndex >= 0)
            {
                // here Brfalse_S is safe considering only several instructions are inserted after break instruction.
                instructions[context.PendingSwitchIndex] = processor.Create(OpCodes.Brfalse_S, context.TryStartInstruction);
                context.PendingSwitchIndex = -1;
            }

            var firstEntryInstruction = instructions.FirstOrDefault();
            IlUtilities.PersistentInstructions(instructions, processor, context.TryStartInstruction);
            if (firstEntryInstruction != null)
            {
                var originalTryStart = context.TryStartInstruction;
                context.TryStartInstruction = firstEntryInstruction;
                context.ExecutionContextVariableSwitchableSection.AdjustEndInstruction(originalTryStart, firstEntryInstruction);
                context.ExecutionVariableSwitchableSection.AdjustEndInstruction(originalTryStart, firstEntryInstruction);
                context.ReturnVariableSwitchableSection.AdjustEndInstruction(originalTryStart, firstEntryInstruction);
            }
        }

        private void UpdateLocalVariablesOnException(List<Instruction> instructions, ILProcessor processor)
        {
            if (context.HasExceptionVariableIndex >= 0)
            {
                instructions.Add(processor.Create(OpCodes.Ldc_I4_1));
                instructions.Add(processor.Create(OpCodes.Stloc, context.HasExceptionVariableIndex));
            }
        }

        private void CompleteWeavingException(MethodDefinition method, List<Instruction> instructions, ILProcessor processor)
        {
            if (instructions.Any())
            {
                FixReturnInstructions(method, processor);
                var rethrowInstruction = processor.Create(OpCodes.Rethrow);
                processor.InsertBefore(context.EndingInstruction, rethrowInstruction);
                var handleExceptionInstruction = context.ExceptionVariableIndex >= 0
                    ? processor.Create(OpCodes.Stloc, context.ExceptionVariableIndex)
                    : processor.Create(OpCodes.Pop);
                processor.InsertBefore(rethrowInstruction, handleExceptionInstruction);

                // apply switch from last advice call
                if (context.PendingSwitchIndex >= 0)
                {
                    // here Brfalse_S is safe considering only several instructions are inserted after break instruction.
                    instructions[context.PendingSwitchIndex] = processor.Create(OpCodes.Brfalse_S, rethrowInstruction);
                    context.PendingSwitchIndex = -1;
                }

                // instruction persistent
                IlUtilities.PersistentInstructions(instructions, processor, rethrowInstruction);
                var handler = new ExceptionHandler(ExceptionHandlerType.Catch)
                {
                    CatchType = context.GetTypeReference(typeof(Exception)),
                    TryStart = context.TryStartInstruction,
                    TryEnd = handleExceptionInstruction,
                    HandlerStart = handleExceptionInstruction,
                    HandlerEnd = context.EndingInstruction,
                };
                context.ExceptionHandlerIndex = method.Body.ExceptionHandlers.Count;
                method.Body.ExceptionHandlers.Add(handler);
            }
        }

        private void UpdateLocalVariablesOnExit(MethodDefinition method, List<Instruction> instructions, ILProcessor processor)
        {
            // evaluation stack: bottom
            if (context.ReturnVariableIndex >= 0)
            {
                var returnVariableIndex = context.ReturnVariableIndex;
                context.ReturnVariableIndex = method.Body.Variables.Count;
                method.Body.Variables.Add(new VariableDefinition(context.BaseReference.Return.ReadOnlyTypeReference));
                if (context.ReturnValueVariableIndex >= 0)
                {
                    // hasExceptionIndex must have been initialized in this branch, according to need parameter rules
                    context.ReturnFinallySwitchableSection.StartIndex = instructions.Count;

                    // evaluation stack after the following statement: bottom->IReturnBuilder
                    instructions.Add(processor.Create(OpCodes.Ldloc, returnVariableIndex));

                    // evaluation stack after the following statement: bottom->IReturnBuilder-><has exception>
                    instructions.Add(processor.Create(OpCodes.Ldloc, context.HasExceptionVariableIndex));
                    instructions.Add(processor.Create(OpCodes.Ldc_I4_0));

                    // evaluation stack after the following statement: bottom->IReturnBuilder-><has return>
                    instructions.Add(processor.Create(OpCodes.Ceq));

                    // evaluation stack after the following statement: bottom
                    instructions.Add(processor.Create(OpCodes.Callvirt, context.BaseReference.Return.HasReturnSetter));

                    // evaluation stack after the following statement: bottom->IReturnBuilder-><has exception>
                    instructions.Add(processor.Create(OpCodes.Ldloc, context.HasExceptionVariableIndex));

                    // later, insert if start statement here
                    var ifStartIndex = instructions.Count;

                    // evaluation stack after the following statement: bottom
                    instructions.Add(null);

                    // evaluation stack after the following statement: bottom->IReturnBuilder
                    instructions.Add(processor.Create(OpCodes.Ldloc, returnVariableIndex));

                    // evaluation stack after the following statement: bottom->IReturnBuilder-><return value>
                    instructions.Add(processor.Create(OpCodes.Ldloc, context.ReturnValueVariableIndex));

                    // insert indirect load instruction
                    if (method.ReturnType.IsByReference && method.ReturnType is TypeSpecification)
                    {
                        instructions.Add(processor.CreateIndirectLoadInstruction(method.ReturnType));
                    }

                    // insert box instruction
                    if (method.ReturnType.IsValueType)
                    {
                        instructions.Add(processor.CreateBoxValueTypeInstruction(method.ReturnType));
                    }

                    // evaluation stack after the following statement: bottom
                    instructions.Add(processor.Create(OpCodes.Callvirt, context.BaseReference.Return.ValueSetter));
                    var ifEndInstruction = processor.Create(OpCodes.Ldloc, returnVariableIndex);

                    // evaluation stack after the following statement: bottom->IReturnBuilder
                    instructions.Add(ifEndInstruction);

                    // fill back if start instruction, BrTrue.S should be safe here, there aren't enough instructions to exceed offset of 127
                    instructions[ifStartIndex] = processor.Create(OpCodes.Brtrue_S, ifEndInstruction);
                }
                else
                {
                    // in this branch, the method must return null, according to need parameter rule
                    context.ReturnFinallySwitchableSection.StartIndex = instructions.Count;

                    // evaluation stack after the following statement: bottom->IReturnBuilder
                    instructions.Add(processor.Create(OpCodes.Ldloc, returnVariableIndex));

                    // evaluation stack after the following statement: bottom->IReturnBuilder->IReturnBuilder
                    instructions.Add(processor.Create(OpCodes.Dup));

                    // evaluation stack after the following statement: bottom->IReturnBuilder->IReturnBuilder->false
                    instructions.Add(processor.Create(OpCodes.Ldc_I4_0));

                    // evaluation stack after the following statement: bottom->IReturnBuilder
                    instructions.Add(processor.Create(OpCodes.Callvirt, context.BaseReference.Return.HasReturnSetter));
                }

                // evaluation stack after the following statement: bottom->IReturn
                instructions.Add(processor.Create(OpCodes.Callvirt, context.BaseReference.Return.BuildMethod));
                instructions.Add(processor.Create(OpCodes.Stloc, context.ReturnVariableIndex));
                context.ReturnFinallySwitchableSection.EndIndex = instructions.Count;
            }
        }

        private void CompleteWeavingExit(MethodDefinition method, List<Instruction> instructions, ILProcessor processor)
        {
            if (instructions.Any())
            {
                // must add end finally instruction at the end
                var endFinally = processor.Create(OpCodes.Endfinally);
                instructions.Add(endFinally);
                FixReturnInstructions(method, processor);

                // return variable switching
                context.ReturnFinallySwitchableSection.SetInstructions(instructions, endFinally);

                // apply switch from last advice call
                if (context.PendingSwitchIndex >= 0)
                {
                    // here Brfalse_S is safe considering only several instructions are inserted after break instruction.
                    instructions[context.PendingSwitchIndex] = processor.Create(OpCodes.Brfalse_S, endFinally);
                    context.PendingSwitchIndex = -1;
                }

                var tryEndInstruction = instructions.First();
                IlUtilities.PersistentInstructions(instructions, processor, context.EndingInstruction);

                // if catch block exists, it's handler end must be updated
                if (context.ExceptionHandlerIndex >= 0)
                {
                    method.Body.ExceptionHandlers[context.ExceptionHandlerIndex].HandlerEnd = tryEndInstruction;
                }

                context.FinallyHandlerIndex = method.Body.ExceptionHandlers.Count;
                var handler = new ExceptionHandler(ExceptionHandlerType.Finally)
                {
                    CatchType = context.GetTypeReference(typeof(Exception)),
                    TryStart = context.TryStartInstruction,
                    TryEnd = tryEndInstruction,
                    HandlerStart = tryEndInstruction,
                    HandlerEnd = context.EndingInstruction,
                };
                method.Body.ExceptionHandlers.Add(handler);
            }
        }

        private void AdjustExceptionFinallyBorder(MethodDefinition method, Instruction original, Instruction newStart)
        {
            if (newStart != null)
            {
                if (context.ExceptionHandlerIndex >= 0)
                {
                    if (method.Body.ExceptionHandlers[context.ExceptionHandlerIndex].HandlerEnd == original)
                    {
                        method.Body.ExceptionHandlers[context.ExceptionHandlerIndex].HandlerEnd = newStart;
                    }
                }

                if (context.FinallyHandlerIndex >= 0)
                {
                    if (method.Body.ExceptionHandlers[context.FinallyHandlerIndex].TryEnd == original)
                    {
                        method.Body.ExceptionHandlers[context.FinallyHandlerIndex].TryEnd = newStart;
                        method.Body.ExceptionHandlers[context.FinallyHandlerIndex].HandlerStart = newStart;
                    }
                }
            }
        }

        private void FixReturnInstructions(MethodDefinition method, ILProcessor processor)
        {
            if (context.EndingInstruction == null)
            {
                var instructions = method.Body.Instructions;
                var count = instructions.Count;
                if (method.IsVoidReturn())
                {
                    context.EndingInstruction = processor.Create(OpCodes.Ret);
                    instructions.Add(context.EndingInstruction);
                    for (var i = 0; i < count; i++)
                    {
                        if (instructions[i].OpCode == OpCodes.Ret)
                        {
                            instructions[i] = processor.Create(OpCodes.Leave, context.EndingInstruction);
                        }
                    }
                }
                else
                {
                    context.EndingInstruction = processor.Create(OpCodes.Ldloc, context.ReturnValueVariableIndex);
                    instructions.Add(context.EndingInstruction);
                    instructions.Add(processor.Create(OpCodes.Ret));
                    for (var i = 0; i < count; i++)
                    {
                        if (instructions[i].OpCode != OpCodes.Ret)
                        {
                            continue;
                        }

                        instructions[i] = processor.Create(OpCodes.Leave, context.EndingInstruction);
                        instructions.Insert(i, processor.Create(OpCodes.Stloc, context.ReturnValueVariableIndex));
                        i++;
                        count++;
                    }
                }
            }
        }
    }
}
