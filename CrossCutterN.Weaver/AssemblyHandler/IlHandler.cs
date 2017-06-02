/**
 * Description: Intermediate language handler
 * Author: David Cui
 */

using CrossCutterN.Aspect;

namespace CrossCutterN.Weaver.AssemblyHandler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Mono.Cecil;
    using Mono.Cecil.Cil;
    using Batch;
    using Switch;
    using Utilities;

    internal class IlHandler
    {
        private readonly MethodDefinition _method;
        private readonly ILProcessor _processor;
        private readonly IWeavingContext _context;
        private readonly IList<Instruction> _instructions = new List<Instruction>();
        private string _methodSignature;

        // to boost performance a bit by avoiding repeatedly build up method signature
        private string MethodSignature
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_methodSignature))
                {
                    _methodSignature = _method.GetSignature();
                }
                return _methodSignature;
            }
        }

        public IlHandler(MethodDefinition method, IWeavingContext context)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }
            if (!method.HasBody)
            {
                throw new ArgumentException(string.Format("Method {0} without body can't be weaved.", method.FullName), "method");
            }
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _method = method;
            _processor = method.Body.GetILProcessor();
            _context = context;
            _context.Reset();
        }

        public IlHandler(TypeDefinition clazz, IWeavingContext context)
        {
            if(clazz == null)
            {
                throw new ArgumentNullException("clazz");
            }
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            _context = context;
            _context.Reset();
            var staticConstructor = clazz.Methods.FirstOrDefault(method => method.IsStaticConstructor());
            if (staticConstructor == null)
            {
                _method = new MethodDefinition(".cctor",
                    MethodAttributes.Static | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName,
                    _context.GetTypeReference(typeof(void)));
                _processor = _method.Body.GetILProcessor();
                _method.Body.Instructions.Add(_processor.Create(OpCodes.Ret));
                clazz.Methods.Add(_method);
            }
            else
            {
                _method = staticConstructor;
                _processor = _method.Body.GetILProcessor();
            }
        }

        #region SetLocalVariables

        public void AddLocalVariables(bool addExecution, bool addException, bool addReturn, bool addReturnValue, bool addExecutionContext)
        {
            if (addExecution)
            {
                AddExecutionVariable();
            }
            if (addException)
            {
                AddExceptionVariable();
            }
            if (addReturn)
            {
                AddReturnVariable();
            }
            if (addReturnValue)
            {
                AddReturnValueVariable();
            }
            if (addExecutionContext)
            {
                AddExecutionContextVariable();
            }
            FinalizeSetLocalVariableInstructions();
        }

        private void AddReturnValueVariable()
        {
            _context.ReturnValueVariableIndex = _method.Body.Variables.Count;
            _method.Body.Variables.Add(new VariableDefinition(_method.ReturnType));
        }

        private void AddExecutionContextVariable()
        {
            _context.ExecutionContextVariableIndex = _method.Body.Variables.Count;
            _method.Body.Variables.Add(new VariableDefinition(_context.AdviceReference.ExecutionContext.TypeReference));
            _context.ExecutionContextVariableSwitchableSection.StartIndex = _instructions.Count;
            _instructions.Add(_processor.Create(OpCodes.Call, _context.AdviceReference.ParameterFactory.InitializeExecutionContextMethod));
            _instructions.Add(_processor.Create(OpCodes.Stloc, _context.ExecutionContextVariableIndex));
            _context.ExecutionContextVariableSwitchableSection.EndIndex = _instructions.Count;
        }

        private void AddExecutionVariable()
        {
            // evaluation stack: bottom
            _method.Body.Variables.Add(new VariableDefinition(_context.AdviceReference.Execution.TypeReference));
            _context.ExecutionVariableSwitchableSection.StartIndex = _instructions.Count;
            _instructions.Add(_processor.Create(OpCodes.Ldstr, _method.Module.Assembly.FullName));
            _instructions.Add(_processor.Create(OpCodes.Ldstr, _method.DeclaringType.Namespace));
            _instructions.Add(_processor.Create(OpCodes.Ldstr, _method.DeclaringType.FullName));
            _instructions.Add(_processor.Create(OpCodes.Ldstr, _method.DeclaringType.Name));
            _instructions.Add(_processor.Create(OpCodes.Ldstr, _method.FullName));
            _instructions.Add(_processor.Create(OpCodes.Ldstr, _method.Name));
            _instructions.Add(_processor.Create(OpCodes.Ldstr, _method.ReturnType.FullName));
            // evaluation stack after the following statement: bottom->ICanAddParameters
            _instructions.Add(_processor.Create(OpCodes.Call, _context.AdviceReference.ParameterFactory.InitializeExecutionMethod));
            if (_method.HasParameters)
            {
                foreach (var parameter in _method.Parameters)
                {
                    // evaluation stack after the following statement: bottom->ICanAddParameters->ICanAddParameters
                    _instructions.Add(_processor.Create(OpCodes.Dup));
                    _instructions.Add(_processor.Create(OpCodes.Ldstr, parameter.Name));
                    _instructions.Add(_processor.Create(OpCodes.Ldstr, parameter.ParameterType.FullName));
                    _instructions.Add(_processor.Create(OpCodes.Ldc_I4, parameter.Sequence));
                    _instructions.Add(_processor.Create(OpCodes.Ldarg, parameter.Sequence));
                    // insert indirect load instruction
                    if (parameter.ParameterType.IsByReference && parameter.ParameterType is TypeSpecification)
                    {
                        _instructions.Add(_processor.CreateIndirectLoadInstruction(parameter.ParameterType));
                    }
                    // insert box instruction
                    if (parameter.ParameterType.IsValueType)
                    {
                        _instructions.Add(_processor.CreateBoxValueTypeInstruction(parameter.ParameterType));
                    }
                    // evaluation stack after the following statement: bottom->ICanAddParameters->ICanAddParameters->ICanAddCustomAttribute
                    _instructions.Add(_processor.Create(OpCodes.Call, _context.AdviceReference.ParameterFactory.InitializeParameterMethod));
                    if (parameter.HasCustomAttributes)
                    {
                        for (var i = 0; i < parameter.CustomAttributes.Count; i++)
                        {
                            var attribute = parameter.CustomAttributes[i];
                            // evaluation stack after the following statement: bottom->ICanAddParameters->ICanAddParameters->ICanAddCustomAttribute->ICanAddCustomAttribute
                            _instructions.Add(_processor.Create(OpCodes.Dup));
                            _instructions.Add(_processor.Create(OpCodes.Ldstr, attribute.AttributeType.FullName));
                            _instructions.Add(_processor.Create(OpCodes.Ldc_I4, i));
                            // evaluation stack after the following statement: bottom->ICanAddParameters->ICanAddParameters->ICanAddCustomAttribute->ICanAddCustomAttribute->ICanAddAttributeProperty
                            _instructions.Add(_processor.Create(OpCodes.Call, _context.AdviceReference.ParameterFactory.InitializeCustomAttributeMethod));
                            if (attribute.HasProperties)
                            {
                                for (var j = 0; j < attribute.Properties.Count; j++)
                                {
                                    var property = attribute.Properties[j];
                                    if (IlUtilities.CustomAttributePropertyTypeIsSupported(property.Argument))
                                    {
                                        // evaluation stack after the following statement: bottom->ICanAddParameters->ICanAddParameters->ICanAddCustomAttribute->ICanAddCustomAttribute->ICanAddAttributeProperty->ICanAddAttributeProperty
                                        _instructions.Add(_processor.Create(OpCodes.Dup));
                                        _instructions.Add(_processor.Create(OpCodes.Ldstr, property.Name));
                                        _instructions.Add(_processor.Create(OpCodes.Ldstr, property.Argument.Type.FullName));
                                        _instructions.Add(_processor.Create(OpCodes.Ldc_I4, j));
                                        _instructions.Add(_processor.CreateLoadCustomAttributePropertyValueInstruction(property.Argument));
                                        if (property.Argument.Type.IsValueType)
                                        {
                                            _instructions.Add(_processor.CreateBoxValueTypeInstruction(property.Argument.Type));
                                        }
                                        // evaluation stack after the following statement: bottom->ICanAddParameters->ICanAddParameters->ICanAddCustomAttribute->ICanAddCustomAttribute->ICanAddAttributeProperty->ICanAddAttributeProperty->IAttributeProperty
                                        _instructions.Add(_processor.Create(OpCodes.Call, _context.AdviceReference.ParameterFactory.InitializeAttributePropertyMethod));
                                        // evaluation stack after the following statement: bottom->ICanAddParameters->ICanAddParameters->ICanAddCustomAttribute->ICanAddCustomAttribute->ICanAddAttributeProperty
                                        _instructions.Add(_processor.Create(OpCodes.Callvirt, _context.AdviceReference.CustomAttribute.AddAttributePropertyMethod));
                                    }
                                }
                            }
                            // evaluation stack after the following statement: bottom->ICanAddParameters->ICanAddParameters->ICanAddCustomAttribute->ICanAddCustomAttribute->ICustomAttribute
                            _instructions.Add(_processor.Create(OpCodes.Callvirt, _context.AdviceReference.CustomAttribute.ConvertMethod));
                            // evaluation stack after the following statement: bottom->ICanAddParameters->ICanAddParameters->ICanAddCustomAttribute
                            _instructions.Add(_processor.Create(OpCodes.Callvirt, _context.AdviceReference.Parameter.AddCustomAttributeMethod));
                        }
                    }
                    // evaluation stack after the following statement: bottom->ICanAddParameters->ICanAddParameters->IParameter
                    _instructions.Add(_processor.Create(OpCodes.Callvirt, _context.AdviceReference.Parameter.ConvertMethod));
                    // evaluation stack after the following statement: bottom->ICanAddParameters
                    _instructions.Add(_processor.Create(OpCodes.Callvirt, _context.AdviceReference.Execution.AddParameterMethod));
                }
            }
            // evaluation stack after the following statement: bottom->IMethodExecution
            _instructions.Add(_processor.Create(OpCodes.Callvirt, _context.AdviceReference.Execution.ConvertMethod));
            _context.ExecutionVariableIndex = _method.Body.Variables.Count;
            _method.Body.Variables.Add(new VariableDefinition(_context.AdviceReference.Execution.ReadOnlyTypeReference));
            _instructions.Add(_processor.Create(OpCodes.Stloc, _context.ExecutionVariableIndex));
            // evaluation stack after the following statement: bottom
            _context.ExecutionVariableSwitchableSection.EndIndex = _instructions.Count;
        }

        private void AddExceptionVariable()
        {
            _context.ExceptionVariableIndex = _method.Body.Variables.Count;
            _method.Body.Variables.Add(new VariableDefinition(_context.GetTypeReference(typeof(Exception))));
        }

        private void AddReturnVariable()
        {
            _context.ReturnVariableIndex = _method.Body.Variables.Count;
            _method.Body.Variables.Add(new VariableDefinition(_context.AdviceReference.Return.TypeReference));
            _context.ReturnVariableSwitchableSection.StartIndex = _instructions.Count;
            _instructions.Add(_processor.Create(OpCodes.Ldstr, _method.ReturnType.FullName));
            _instructions.Add(_processor.Create(OpCodes.Call, _context.AdviceReference.ParameterFactory.InitializeReturnMethod));
            _instructions.Add(_processor.Create(OpCodes.Stloc, _context.ReturnVariableIndex));
            _context.ReturnVariableSwitchableSection.EndIndex = _instructions.Count;
        }

        private void FinalizeSetLocalVariableInstructions()
        {
            _context.TryStartInstruction = _method.Body.Instructions.First();
            if (_context.ExecutionVariableSwitchableSection.CanSetInstructions)
            {
                _context.ExecutionVariableSwitchableSection.SetInstructions(_instructions, _context.TryStartInstruction);
            }
            if (_context.ReturnVariableSwitchableSection.CanSetInstructions)
            {
                _context.ReturnVariableSwitchableSection.SetInstructions(_instructions, _context.TryStartInstruction);
            }
            if (_context.ExecutionContextVariableSwitchableSection.CanSetInstructions)
            {
                _context.ExecutionContextVariableSwitchableSection.SetInstructions(_instructions, _context.TryStartInstruction);
            }
            PersistentInstructions(_context.TryStartInstruction);
        }

        #endregion

        #region WeavingEntry

        public void FinalizeWeavingEntry()
        {
            // apply switch from last advice call
            if (_context.PendingSwitchIndex >= 0)
            {
                _instructions[_context.PendingSwitchIndex] = _processor.Create(OpCodes.Brfalse_S, _context.TryStartInstruction);
                _context.PendingSwitchIndex = -1;
            }
            var firstEntryInstruction = _instructions.FirstOrDefault();
            PersistentInstructions(_context.TryStartInstruction);
            if(firstEntryInstruction != null)
            {
                var originalTryStart = _context.TryStartInstruction;
                _context.TryStartInstruction = firstEntryInstruction;
                _context.ExecutionVariableSwitchableSection.AdjustEndInstruction(originalTryStart, firstEntryInstruction);
                _context.ReturnVariableSwitchableSection.AdjustEndInstruction(originalTryStart, firstEntryInstruction);
                _context.ExecutionContextVariableSwitchableSection.AdjustEndInstruction(originalTryStart, firstEntryInstruction);
            }
        }

        #endregion

        #region WeavingException

        public void UpdateLocalVariablesOnException()
        {
            if (_context.ExecutionContextVariableIndex >= 0)
            {
                _context.ExecutionContextExceptionSwitchableSection.StartIndex = _instructions.Count;
                _instructions.Add(_processor.Create(OpCodes.Ldloc, _context.ExecutionContextVariableIndex));
                _instructions.Add(_processor.Create(OpCodes.Callvirt, _context.AdviceReference.ExecutionContext.MarkExceptionThrownMethod));
                _context.ExecutionContextExceptionSwitchableSection.EndIndex = _instructions.Count;
            }
        }

        public void FinalizeWeavingException()
        {
            if(_instructions.Any())
            {
                FixReturnInstructions();
                var rethrowInstruction = _processor.Create(OpCodes.Rethrow);
                _processor.InsertBefore(_context.EndingInstruction, rethrowInstruction);
                var handleExceptionInstruction = _context.ExceptionVariableIndex >= 0
                    ? _processor.Create(OpCodes.Stloc, _context.ExceptionVariableIndex)
                    : _processor.Create(OpCodes.Pop);
                _processor.InsertBefore(rethrowInstruction, handleExceptionInstruction);
                if (_context.ExecutionContextExceptionSwitchableSection.CanSetInstructions)
                {
                    _context.ExecutionContextExceptionSwitchableSection.SetInstructions(_instructions, rethrowInstruction);
                }
                // apply switch from last advice call
                if (_context.PendingSwitchIndex >= 0)
                {
                    _instructions[_context.PendingSwitchIndex] = _processor.Create(OpCodes.Brfalse_S, rethrowInstruction);
                    _context.PendingSwitchIndex = -1;
                }

                // instruction persistent
                PersistentInstructions(rethrowInstruction);
                var handler = new ExceptionHandler(ExceptionHandlerType.Catch)
                {
                    CatchType = _context.GetTypeReference(typeof(Exception)),
                    TryStart = _context.TryStartInstruction,
                    TryEnd = handleExceptionInstruction,
                    HandlerStart = handleExceptionInstruction,
                    HandlerEnd = _context.EndingInstruction
                };
                _context.ExceptionHandlerIndex = _method.Body.ExceptionHandlers.Count;
                _method.Body.ExceptionHandlers.Add(handler);
            }
        }

        #endregion

        #region WeavingExit

        public void UpdateLocalVariablesOnExit()
        {
            // evaluation stack: bottom
            if (_context.ExecutionContextVariableIndex >= 0)
            {
                _context.HasExceptionVariableIndex = _method.Body.Variables.Count;
                _method.Body.Variables.Add(new VariableDefinition(_context.GetTypeReference(typeof(bool))));
                _context.ExecutionContextFinallySwitchableSection.StartIndex = _instructions.Count;
                // evaluation stack after the following statement: bottom->IExecutionContext
                _instructions.Add(_processor.Create(OpCodes.Ldloc, _context.ExecutionContextVariableIndex));
                // evaluation stack after the following statement: bottom-><exception thrown>
                _instructions.Add(_processor.Create(OpCodes.Callvirt, _context.AdviceReference.ExecutionContext.ExceptionThrownGetter));
                // evaluation stack after the following statement: bottom
                _instructions.Add(_processor.Create(OpCodes.Stloc, _context.HasExceptionVariableIndex));
                _context.ExecutionContextFinallySwitchableSection.EndIndex = _instructions.Count;
            }
            // evaluation stack: bottom
            if (_context.ReturnVariableIndex >= 0)
            {
                var returnVariableIndex = _context.ReturnVariableIndex;
                _context.ReturnVariableIndex = _method.Body.Variables.Count;
                _method.Body.Variables.Add(new VariableDefinition(_context.AdviceReference.Return.ReadOnlyTypeReference));
                if (_context.ReturnValueVariableIndex >= 0)
                {
                    // hasExceptionIndex must have been initialized in this branch, according to need parameter rules
                    _context.ReturnFinallySwitchableSection.StartIndex = _instructions.Count;
                    // evaluation stack after the following statement: bottom->IWriteOnlyReturn
                    _instructions.Add(_processor.Create(OpCodes.Ldloc, returnVariableIndex));
                    // evaluation stack after the following statement: bottom->IWriteOnlyReturn-><has exception>
                    _instructions.Add(_processor.Create(OpCodes.Ldloc, _context.HasExceptionVariableIndex));
                    _instructions.Add(_processor.Create(OpCodes.Ldc_I4_0));
                    // evaluation stack after the following statement: bottom->IWriteOnlyReturn-><has return>
                    _instructions.Add(_processor.Create(OpCodes.Ceq));
                    // evaluation stack after the following statement: bottom
                    _instructions.Add(_processor.Create(OpCodes.Callvirt, _context.AdviceReference.Return.HasReturnSetter));

                    // evaluation stack after the following statement: bottom->IWriteOnlyReturn-><has exception>
                    _instructions.Add(_processor.Create(OpCodes.Ldloc, _context.HasExceptionVariableIndex));
                    //later, insert if start statement here
                    var ifStartIndex = _instructions.Count;
                    // evaluation stack after the following statement: bottom
                    _instructions.Add(null);
                    // evaluation stack after the following statement: bottom->IWriteOnlyReturn
                    _instructions.Add(_processor.Create(OpCodes.Ldloc, returnVariableIndex));
                    // evaluation stack after the following statement: bottom->IWriteOnlyReturn-><return value>
                    _instructions.Add(_processor.Create(OpCodes.Ldloc, _context.ReturnValueVariableIndex));
                    // insert indirect load instruction
                    if (_method.ReturnType.IsByReference && _method.ReturnType is TypeSpecification)
                    {
                        _instructions.Add(_processor.CreateIndirectLoadInstruction(_method.ReturnType));
                    }
                    // insert box instruction
                    if (_method.ReturnType.IsValueType)
                    {
                        _instructions.Add(_processor.CreateBoxValueTypeInstruction(_method.ReturnType));
                    }
                    // evaluation stack after the following statement: bottom
                    _instructions.Add(_processor.Create(OpCodes.Callvirt, _context.AdviceReference.Return.ValueSetter));
                    var ifEndInstruction = _processor.Create(OpCodes.Ldloc, returnVariableIndex);
                    // evaluation stack after the following statement: bottom->IWriteOnlyReturn
                    _instructions.Add(ifEndInstruction);
                    // fill back if start instruction
                    _instructions[ifStartIndex] = _processor.Create(OpCodes.Brtrue_S, ifEndInstruction);
                }
                else
                {
                    // in this branch, the method must return null, according to need parameter rule
                    _context.ReturnFinallySwitchableSection.StartIndex = _instructions.Count;
                    // evaluation stack after the following statement: bottom->IWriteOnlyReturn
                    _instructions.Add(_processor.Create(OpCodes.Ldloc, returnVariableIndex));
                    // evaluation stack after the following statement: bottom->IWriteOnlyReturn->IWriteOnlyReturn
                    _instructions.Add(_processor.Create(OpCodes.Dup));
                    // evaluation stack after the following statement: bottom->IWriteOnlyReturn->IWriteOnlyReturn->false
                    _instructions.Add(_processor.Create(OpCodes.Ldc_I4_0));
                    // evaluation stack after the following statement: bottom->IWriteOnlyReturn
                    _instructions.Add(_processor.Create(OpCodes.Callvirt, _context.AdviceReference.Return.HasReturnSetter));
                }
                // evaluation stack after the following statement: bottom->IReturn
                _instructions.Add(_processor.Create(OpCodes.Callvirt, _context.AdviceReference.Return.ConvertMethod));
                _instructions.Add(_processor.Create(OpCodes.Stloc, _context.ReturnVariableIndex));
                _context.ReturnFinallySwitchableSection.EndIndex = _instructions.Count;
            }
        }

        public void FinalizeWeavingExit()
        {
            if (_instructions.Any())
            {
                // must add end finally instruction at the end
                var endFinally = _processor.Create(OpCodes.Endfinally);
                _instructions.Add(endFinally);
                FixReturnInstructions();
                // execution context variable switch
                if (_context.ExecutionContextFinallySwitchableSection.CanSetInstructions)
                {
                    _context.ExecutionContextFinallySwitchableSection.SetInstructions(_instructions, endFinally);
                }
                // return variable switching
                if (_context.ReturnFinallySwitchableSection.CanSetInstructions)
                {
                    _context.ReturnFinallySwitchableSection.SetInstructions(_instructions, endFinally);
                }
                // apply switch from last advice call
                if (_context.PendingSwitchIndex >= 0)
                {
                    _instructions[_context.PendingSwitchIndex] = _processor.Create(OpCodes.Brfalse_S, endFinally);
                    _context.PendingSwitchIndex = -1;
                }
                var tryEndInstruction = _instructions.First();
                PersistentInstructions(_context.EndingInstruction);
                // if catch block exists, it's handler end must be updated
                if (_context.ExceptionHandlerIndex >= 0)
                {
                    _method.Body.ExceptionHandlers[_context.ExceptionHandlerIndex].HandlerEnd = tryEndInstruction;
                }
                _context.FinallyHandlerIndex = _method.Body.ExceptionHandlers.Count;
                var handler = new ExceptionHandler(ExceptionHandlerType.Finally)
                {
                    CatchType = _context.GetTypeReference(typeof(Exception)),
                    TryStart = _context.TryStartInstruction,
                    TryEnd = tryEndInstruction,
                    HandlerStart = tryEndInstruction,
                    HandlerEnd = _context.EndingInstruction
                };
                _method.Body.ExceptionHandlers.Add(handler);
            }
        }

        #endregion

        #region Advice Switching

        public void RegisterSwitch(FieldReference field, string clazz, string property, string method, string aspect, bool value)
        {
            _instructions.Add(_processor.Create(OpCodes.Call, _context.AdviceReference.Controller.BuildUpGetterReference));
            _instructions.Add(_processor.Create(OpCodes.Ldstr, clazz));
            _instructions.Add(_processor.Create(OpCodes.Ldstr, property??string.Empty));
            _instructions.Add(_processor.Create(OpCodes.Ldstr, method));
            _instructions.Add(_processor.Create(OpCodes.Ldstr, aspect));
            _instructions.Add(_processor.Create(value ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0));
            _instructions.Add(_processor.Create(OpCodes.Callvirt, _context.AdviceReference.BuildUp.RegisterSwitchMethod));
            _instructions.Add(_processor.Create(OpCodes.Stsfld, field));
        }

        public void FinalizeSwitchRegistration(string clazz)
        {
            _instructions.Add(_processor.Create(OpCodes.Call, _context.AdviceReference.Controller.BuildUpGetterReference));
            _instructions.Add(_processor.Create(OpCodes.Ldstr, clazz));
            _instructions.Add(_processor.Create(OpCodes.Callvirt, _context.AdviceReference.BuildUp.CompleteMethod));
            PersistentInstructions(_method.Body.Instructions.First());
        }

        public void WeaveSwitchInitialization()
        {
            var dict = _context.FieldLocalVariableDictionary;
            if (dict.Any())
            {
                foreach (var fieldVariable in dict)
                {
                    _instructions.Add(_processor.Create(OpCodes.Call, _context.AdviceReference.Controller.LookUpGetterReference));
                    _instructions.Add(_processor.Create(OpCodes.Ldsfld, fieldVariable.Key));
                    _instructions.Add(_processor.Create(OpCodes.Callvirt, _context.AdviceReference.LookUp.IsOnMethod));
                    _instructions.Add(_processor.Create(OpCodes.Stloc, fieldVariable.Value));
                }
                PersistentInstructions(_method.Body.Instructions.First());
            }

            var executionContextVariableSwitches = _context.ExecutionContextSwitches.Switches;
            var executionVariableSwitches = _context.ExecutionSwitches.Switches;
            var returnVariableSwitches = _context.ReturnSwitches.Switches;

            // inject with reverse order from declaration to adjust jump to statement
            // local variables
            var originalStart = _context.ExecutionContextVariableSwitchableSection.StartInstruction;
            var newStart = ApplySwitches(executionContextVariableSwitches, _context.ExecutionContextVariableSwitchableSection);
            if(newStart != null)
            {
                _context.ReturnVariableSwitchableSection.AdjustEndInstruction(originalStart, newStart);
            }
            originalStart = _context.ReturnVariableSwitchableSection.StartInstruction ?? originalStart;
            newStart = ApplySwitches(returnVariableSwitches, _context.ReturnVariableSwitchableSection) ?? newStart;
            if(newStart != null)
            {
                _context.ExecutionVariableSwitchableSection.AdjustEndInstruction(originalStart, newStart);
            }
            ApplySwitches(executionVariableSwitches, _context.ExecutionVariableSwitchableSection);

            // exception block
            ApplySwitches(executionContextVariableSwitches, _context.ExecutionContextExceptionSwitchableSection);

            // finally block
            originalStart = _context.ReturnFinallySwitchableSection.StartInstruction;
            newStart = ApplySwitches(returnVariableSwitches, _context.ReturnFinallySwitchableSection);
            if(newStart != null)
            {
                _context.ExecutionContextFinallySwitchableSection.AdjustEndInstruction(originalStart, newStart);
            }
            originalStart = _context.ExecutionContextFinallySwitchableSection.StartInstruction ?? originalStart;
            newStart = ApplySwitches(executionContextVariableSwitches, _context.ExecutionContextFinallySwitchableSection) ?? newStart;
            if (originalStart != null)// new start can't be null if original start isn't null
            {
                AdjustExceptionFinallyBorder(originalStart, newStart);
            }
        }

        private Instruction ApplySwitches(IReadOnlyList<int> switches, ISwitchableSection section)
        {
            if (switches != null && switches.Any() && section.HasContent)
            {
                var count = switches.Count;
                for (var i = 0; i < count - 1; i++)
                {
                    _instructions.Add(_processor.Create(OpCodes.Ldloc, switches[i]));
                    _instructions.Add(_processor.Create(OpCodes.Brtrue_S, section.StartInstruction));
                }
                _instructions.Add(_processor.Create(OpCodes.Ldloc, switches[count - 1]));
                _instructions.Add(_processor.Create(OpCodes.Brfalse_S, section.EndInstruction));
                var result = _instructions.First();
                PersistentInstructions(section.StartInstruction);
                return result;
            }
            return null;
        }

        private void AdjustExceptionFinallyBorder(Instruction original, Instruction newStart)
        {
            if (newStart != null)
            {
                if (_context.ExceptionHandlerIndex >= 0)
                {
                    if (_method.Body.ExceptionHandlers[_context.ExceptionHandlerIndex].HandlerEnd == original)
                    {
                        _method.Body.ExceptionHandlers[_context.ExceptionHandlerIndex].HandlerEnd = newStart;
                    }
                }
                if (_context.FinallyHandlerIndex >= 0)
                {
                    if (_method.Body.ExceptionHandlers[_context.FinallyHandlerIndex].TryEnd == original)
                    {
                        _method.Body.ExceptionHandlers[_context.FinallyHandlerIndex].TryEnd = newStart;
                        _method.Body.ExceptionHandlers[_context.FinallyHandlerIndex].HandlerStart = newStart;
                    }
                }
            }
        }

        #endregion

        private void FixReturnInstructions()
        {
            if(_context.EndingInstruction == null)
            {
                var instructions = _method.Body.Instructions;
                var count = instructions.Count;
                if (_method.IsVoidReturn())
                {
                    _context.EndingInstruction = _processor.Create(OpCodes.Ret);
                    instructions.Add(_context.EndingInstruction);
                    for (var i = 0; i < count; i++)
                    {
                        if (instructions[i].OpCode == OpCodes.Ret)
                        {
                            instructions[i] = _processor.Create(OpCodes.Leave, _context.EndingInstruction);
                        }
                    }
                }
                else
                {
                    _context.EndingInstruction = _processor.Create(OpCodes.Ldloc, _context.ReturnValueVariableIndex);
                    instructions.Add(_context.EndingInstruction);
                    instructions.Add(_processor.Create(OpCodes.Ret));
                    for (var i = 0; i < count; i++)
                    {
                        if (instructions[i].OpCode != OpCodes.Ret)
                        {
                            continue;
                        }
                        instructions[i] = _processor.Create(OpCodes.Leave, _context.EndingInstruction);
                        instructions.Insert(i, _processor.Create(OpCodes.Stloc, _context.ReturnValueVariableIndex));
                        i++;
                        count++;
                    }
                }
            }
        }

        public void CallAdvice(IAdviceInfo advice, IWriteOnlySwitchHandler switchHandler)
        {
            if (advice == null)
            {
                throw new ArgumentNullException("advice");
            }
            var pendingSwitchIndex = _context.PendingSwitchIndex;
            var firstIndex = _instructions.Count;
            var needExecutionVariable = advice.ParameterFlag.NeedExecutionParameter();
            var needReturnVariable = advice.ParameterFlag.NeedReturnParameter();
            var needExecutionContextVariable = advice.ParameterFlag.NeedHasException() || 
                (needReturnVariable && !_method.IsVoidReturn());
            if (advice.SwitchStatus.IsSwitchable())
            {
                var field = switchHandler.GetSwitchField(MethodSignature, advice.BuilderId,
                                                         advice.SwitchStatus == SwitchStatus.On);
                var index = _context.GetLocalVariableForField(field);
                if (index < 0)
                {
                    index = _method.Body.Variables.Count;
                    _method.Body.Variables.Add(new VariableDefinition(_context.GetTypeReference(typeof (bool))));
                    _context.RecordLocalVariableForField(field, index);
                }
                if (needExecutionVariable)
                {
                    _context.ExecutionSwitches.RegisterSwitch(index);
                }
                if (needReturnVariable)
                {
                    _context.ReturnSwitches.RegisterSwitch(index);
                }
                if (needExecutionContextVariable)
                {
                    _context.ExecutionContextSwitches.RegisterSwitch(index);
                }
                _instructions.Add(_processor.Create(OpCodes.Ldloc, index));
                // the null instruction is to be filled in later
                _context.PendingSwitchIndex = _instructions.Count;
                _instructions.Add(null);
            }
            else
            {
                if (needExecutionVariable)
                {
                    _context.ExecutionSwitches.SetUnSwitchable();
                }
                if (needReturnVariable)
                {
                    _context.ReturnSwitches.SetUnSwitchable();
                }
                if (needExecutionContextVariable)
                {
                    _context.ExecutionContextSwitches.SetUnSwitchable();
                }
            }
            if (needExecutionVariable)
            {
                _instructions.Add(_processor.Create(OpCodes.Ldloc, _context.ExecutionVariableIndex));
            }
            if (advice.ParameterFlag.NeedExceptionParameter())
            {
                _instructions.Add(_processor.Create(OpCodes.Ldloc, _context.ExceptionVariableIndex));
            }
            if (needReturnVariable)
            {
                _instructions.Add(_processor.Create(OpCodes.Ldloc, _context.ReturnVariableIndex));
            }
            if (advice.ParameterFlag.NeedHasException())
            {
                _instructions.Add(_processor.Create(OpCodes.Ldloc, _context.HasExceptionVariableIndex));
            }
            _instructions.Add(_processor.Create(OpCodes.Call, _context.GetMethodReference(advice.Advice)));
            // apply switch from last advice call
            if (pendingSwitchIndex >= 0)
            {
                _instructions[pendingSwitchIndex] = _processor.Create(OpCodes.Brfalse_S, _instructions[firstIndex]);
                // reset pending switch index if no switch this time
                if (!advice.SwitchStatus.IsSwitchable())
                {
                    _context.PendingSwitchIndex = -1;
                }
            }
        }

        private void PersistentInstructions(Instruction beforeTarget)
        {
            if(_instructions.Any())
            {
                foreach (var instruction in _instructions)
                {
                    _processor.InsertBefore(beforeTarget, instruction);
                }
            }
            _instructions.Clear();
        }
    }
}
