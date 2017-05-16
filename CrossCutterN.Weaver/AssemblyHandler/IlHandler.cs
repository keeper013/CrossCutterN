/**
 * Description: Intermediate language handler
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.AssemblyHandler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Mono.Cecil;
    using Mono.Cecil.Cil;
    using Batch;

    internal class IlHandler
    {
        private readonly MethodDefinition _method;
        private readonly ILProcessor _processor;
        private readonly IWeavingContext _context;
        private readonly IList<Instruction> _instructions = new List<Instruction>();

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
            _context.ResetVolatileData();
        }

        #region SetLocalVariables

        public void AddReturnValueVariable()
        {
            _context.ReturnValueVariableIndex = _method.Body.Variables.Count;
            _method.Body.Variables.Add(new VariableDefinition(_method.ReturnType));
        }

        public void AddExecutionContextVariable()
        {
            _context.ExecutionContextVariableIndex = _method.Body.Variables.Count;
            _method.Body.Variables.Add(new VariableDefinition(_context.AdviceParameterReference.ExecutionContext.TypeReference));
            _instructions.Add(_processor.Create(OpCodes.Call, _context.AdviceParameterReference.ParameterFactory.InitializeExecutionContextMethod));
            _instructions.Add(_processor.Create(OpCodes.Stloc, _context.ExecutionContextVariableIndex));
        }

        public void AddExecutionParameter()
        {
            // evaluation stack: bottom
            _method.Body.Variables.Add(new VariableDefinition(_context.AdviceParameterReference.Execution.TypeReference));
            _instructions.Add(_processor.Create(OpCodes.Ldstr, _method.Module.Assembly.FullName));
            _instructions.Add(_processor.Create(OpCodes.Ldstr, _method.DeclaringType.Namespace));
            _instructions.Add(_processor.Create(OpCodes.Ldstr, _method.DeclaringType.FullName));
            _instructions.Add(_processor.Create(OpCodes.Ldstr, _method.DeclaringType.Name));
            _instructions.Add(_processor.Create(OpCodes.Ldstr, _method.FullName));
            _instructions.Add(_processor.Create(OpCodes.Ldstr, _method.Name));
            _instructions.Add(_processor.Create(OpCodes.Ldstr, _method.ReturnType.FullName));
            // evaluation stack after the following statement: bottom->ICanAddParameters
            _instructions.Add(_processor.Create(OpCodes.Call, _context.AdviceParameterReference.ParameterFactory.InitializeExecutionMethod));
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
                    _instructions.Add(_processor.Create(OpCodes.Call, _context.AdviceParameterReference.ParameterFactory.InitializeParameterMethod));
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
                            _instructions.Add(_processor.Create(OpCodes.Call, _context.AdviceParameterReference.ParameterFactory.InitializeCustomAttributeMethod));
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
                                        _instructions.Add(_processor.Create(OpCodes.Call, _context.AdviceParameterReference.ParameterFactory.InitializeAttributePropertyMethod));
                                        // evaluation stack after the following statement: bottom->ICanAddParameters->ICanAddParameters->ICanAddCustomAttribute->ICanAddCustomAttribute->ICanAddAttributeProperty
                                        _instructions.Add(_processor.Create(OpCodes.Callvirt, _context.AdviceParameterReference.CustomAttribute.AddAttributePropertyMethod));
                                    }
                                }
                            }
                            // evaluation stack after the following statement: bottom->ICanAddParameters->ICanAddParameters->ICanAddCustomAttribute->ICanAddCustomAttribute->ICustomAttribute
                            _instructions.Add(_processor.Create(OpCodes.Callvirt, _context.AdviceParameterReference.CustomAttribute.ToReadOnlyMethod));
                            // evaluation stack after the following statement: bottom->ICanAddParameters->ICanAddParameters->ICanAddCustomAttribute
                            _instructions.Add(_processor.Create(OpCodes.Callvirt, _context.AdviceParameterReference.Parameter.AddCustomAttributeMethod));
                        }
                    }
                    // evaluation stack after the following statement: bottom->ICanAddParameters->ICanAddParameters->IParameter
                    _instructions.Add(_processor.Create(OpCodes.Callvirt, _context.AdviceParameterReference.Parameter.ToReadOnlyMethod));
                    // evaluation stack after the following statement: bottom->ICanAddParameters
                    _instructions.Add(_processor.Create(OpCodes.Callvirt, _context.AdviceParameterReference.Execution.AddParameterMethod));
                }
            }
            // evaluation stack after the following statement: bottom->IMethodExecution
            _instructions.Add(_processor.Create(OpCodes.Callvirt, _context.AdviceParameterReference.Execution.ToReadOnlyMethod));
            _context.ExecutionVariableIndex = _method.Body.Variables.Count;
            _method.Body.Variables.Add(new VariableDefinition(_context.AdviceParameterReference.Execution.ReadOnlyTypeReference));
            _instructions.Add(_processor.Create(OpCodes.Stloc, _context.ExecutionVariableIndex));
            // evaluation stack after the following statement: bottom
        }

        public void AddExceptionParameter()
        {
            _context.ExceptionVariableIndex = _method.Body.Variables.Count;
            _method.Body.Variables.Add(new VariableDefinition(_context.GetTypeReference(typeof(Exception))));
        }

        public void AddReturnParameter()
        {
            _context.ReturnVariableIndex = _method.Body.Variables.Count;
            _method.Body.Variables.Add(new VariableDefinition(_context.AdviceParameterReference.Return.TypeReference));
            var returnTypeName = _method.ReturnType.FullName;
            _instructions.Add(_processor.Create(returnTypeName.Equals(typeof(void).FullName) ? OpCodes.Ldc_I4_0 : OpCodes.Ldc_I4_1));
            _instructions.Add(_processor.Create(OpCodes.Ldstr, returnTypeName));
            _instructions.Add(_processor.Create(OpCodes.Call, _context.AdviceParameterReference.ParameterFactory.InitializeReturnMethod));
            _instructions.Add(_processor.Create(OpCodes.Stloc, _context.ReturnVariableIndex));
        }

        public void FinalizeSetLocalVariableInstructions()
        {
            _context.TryStartInstruction = _method.Body.Instructions.First();
            PersistentInstructions(_context.TryStartInstruction);
        }

        #endregion

        #region WeavingEntry

        public void FinalizeWeavingEntry()
        {
            var firstEntryInstruction = _instructions.FirstOrDefault();
            PersistentInstructions(_context.TryStartInstruction);
            _context.TryStartInstruction = firstEntryInstruction ?? _context.TryStartInstruction;
        }

        #endregion

        #region WeavingException

        public void UpdateLocalVariablesOnException()
        {
            if (_context.ExecutionContextVariableIndex >= 0)
            {
                _instructions.Add(_processor.Create(OpCodes.Ldloc, _context.ExecutionContextVariableIndex));
                _instructions.Add(_processor.Create(OpCodes.Callvirt, _context.AdviceParameterReference.ExecutionContext.MarkExceptionThrownMethod));
            }
            // set return variable has return property
            if (_context.ReturnVariableIndex >= 0)
            {
                _instructions.Add(_processor.Create(OpCodes.Ldloc, _context.ReturnVariableIndex));
                _instructions.Add(_processor.Create(OpCodes.Ldc_I4_0));
                _instructions.Add(_processor.Create(OpCodes.Callvirt, _context.AdviceParameterReference.Return.HasReturnSetter));
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
            if (_context.ReturnVariableIndex >= 0)
            {
                if (_context.ReturnValueVariableIndex >= 0)
                {
                    // evaluation stack after the following statement: bottom->IExecutionContext
                    _instructions.Add(_processor.Create(OpCodes.Ldloc, _context.ExecutionContextVariableIndex));
                    // evaluation stack after the following statement: bottom-><exception thrown>
                    _instructions.Add(_processor.Create(OpCodes.Callvirt,
                                                        _context.AdviceParameterReference.ExecutionContext
                                                                .ExceptionThrownGetter));
                    //later, insert if start statement here
                    var ifStartIndex = _instructions.Count;
                    _instructions.Add(null);
                    // evaluation stack after the following statement: bottom->IWriteOnlyReturn
                    _instructions.Add(_processor.Create(OpCodes.Ldloc, _context.ReturnVariableIndex));
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
                    _instructions.Add(_processor.Create(OpCodes.Callvirt,
                                                        _context.AdviceParameterReference.Return.ValueSetter));
                    var ifEndInstruction = _processor.Create(OpCodes.Ldloc, _context.ReturnVariableIndex);
                    _instructions[ifStartIndex] = _processor.Create(OpCodes.Brtrue_S, ifEndInstruction);
                    // evaluation stack after the following statement: bottom->IWriteOnlyReturn
                    _instructions.Add(ifEndInstruction);
                }
                else
                {
                    // evaluation stack after the following statement: bottom->IWriteOnlyReturn
                    _instructions.Add(_processor.Create(OpCodes.Ldloc, _context.ReturnVariableIndex));
                }
                // evaluation stack after the following statement: bottom->IReturn
                _instructions.Add(_processor.Create(OpCodes.Callvirt, _context.AdviceParameterReference.Return.ToReadOnlyMethod));
                _context.ReturnVariableIndex = _method.Body.Variables.Count;
                _method.Body.Variables.Add(new VariableDefinition(_context.AdviceParameterReference.Return.ReadOnlyTypeReference));
                _instructions.Add(_processor.Create(OpCodes.Stloc, _context.ReturnVariableIndex));
            }
        }

        public void FinalizeWeavingExit()
        {
            if (_instructions.Any())
            {
                // must add end finally instruction at the end
                _instructions.Add(_processor.Create(OpCodes.Endfinally));
                FixReturnInstructions();
                var tryEndInstruction = _instructions.First();
                PersistentInstructions(_context.EndingInstruction);
                // if catch block exists, it's handler end must be updated
                if (_context.ExceptionHandlerIndex >= 0)
                {
                    _method.Body.ExceptionHandlers[_context.ExceptionHandlerIndex].HandlerEnd = tryEndInstruction;
                }
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

        private void FixReturnInstructions()
        {
            if(_context.EndingInstruction == null)
            {
                var instructions = _method.Body.Instructions;
                var count = instructions.Count;
                if (_method.ReturnType.FullName.Equals(typeof(void).FullName))
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

        public void CallAdvice(IAdviceInfo advice)
        {
            if (advice == null)
            {
                throw new ArgumentNullException("advice");
            }
            if (advice.ParameterFlag.NeedExecutionParameter())
            {
                _instructions.Add(_processor.Create(OpCodes.Ldloc, _context.ExecutionVariableIndex));
            }
            if (advice.ParameterFlag.NeedExceptionParameter())
            {
                _instructions.Add(_processor.Create(OpCodes.Ldloc, _context.ExceptionVariableIndex));
            }
            if (advice.ParameterFlag.NeedReturnParameter())
            {
                _instructions.Add(_processor.Create(OpCodes.Ldloc, _context.ReturnVariableIndex));
            }
            if (advice.ParameterFlag.NeedHasException())
            {
                _instructions.Add(_processor.Create(OpCodes.Ldloc, _context.ExecutionContextVariableIndex));
                _instructions.Add(_processor.Create(OpCodes.Callvirt, _context.AdviceParameterReference.ExecutionContext.ExceptionThrownGetter));
            }
            _instructions.Add(_processor.Create(OpCodes.Call, _context.GetMethodReference(advice.Advice)));
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
