// <copyright file="SwitchInitializationWeaver.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Weaver
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CrossCutterN.Weaver.Statistics;
    using CrossCutterN.Weaver.Switch;
    using CrossCutterN.Weaver.Utilities;
    using Mono.Cecil;
    using Mono.Cecil.Cil;

    /// <summary>
    /// Switch initialization weaver that addes switch registering into static constructor.
    /// </summary>
    internal static class SwitchInitializationWeaver
    {
        /// <summary>
        /// Add switch registering to a class.
        /// </summary>
        /// <param name="clazz">The class to register switch.</param>
        /// <param name="context">The weaving context.</param>
        /// <param name="switchData">Data of switches.</param>
        /// <param name="statistics">Weaving statistics.</param>
        public static void Weave(TypeDefinition clazz, IWeavingContext context, IReadOnlyList<SwitchInitializingData> switchData, IClassWeavingStatisticsBuilder statistics)
        {
#if DEBUG
            if (clazz == null)
            {
                throw new ArgumentNullException("clazz");
            }

            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (switchData == null || !switchData.Any())
            {
                throw new ArgumentNullException("switchData");
            }

            if (statistics == null)
            {
                throw new ArgumentNullException("statistics");
            }
#endif
            var staticConstructor = clazz.Methods.FirstOrDefault(mthd => mthd.IsStaticConstructor());
            MethodDefinition method = null;
            ILProcessor processor = null;
            if (staticConstructor == null)
            {
                method = new MethodDefinition(
                    ".cctor",
                    MethodAttributes.Static | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName,
                    context.GetTypeReference(typeof(void)));
                processor = method.Body.GetILProcessor();
                method.Body.Instructions.Add(processor.Create(OpCodes.Ret));
                clazz.Methods.Add(method);
            }
            else
            {
                method = staticConstructor;
                processor = method.Body.GetILProcessor();
            }

            var typeName = clazz.GetFullName();
            var instructions = new List<Instruction>();
            foreach (var data in switchData)
            {
                RegisterSwitch(instructions, processor, context, data, typeName);
                statistics.AddSwitchWeavingRecord(StatisticsFactory.InitializeSwitchWeavingRecord(typeName, data.Property, data.MethodSignature, data.Aspect, data.Field.Name, data.Value));
            }

            CompleteSwitchRegistration(method, instructions, processor, context, typeName);
        }

        private static void RegisterSwitch(List<Instruction> instructions, ILProcessor processor, IWeavingContext context, SwitchInitializingData data, string typeName)
        {
            instructions.Add(processor.Create(OpCodes.Call, context.BaseReference.BackStage.BuilderGetterReference));
            instructions.Add(processor.Create(OpCodes.Ldstr, typeName));
            instructions.Add(processor.Create(OpCodes.Ldstr, data.Property ?? string.Empty));
            instructions.Add(processor.Create(OpCodes.Ldstr, data.MethodSignature));
            instructions.Add(processor.Create(OpCodes.Ldstr, data.Aspect));
            instructions.Add(processor.Create(data.Value ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0));
            instructions.Add(processor.Create(OpCodes.Callvirt, context.BaseReference.Builder.RegisterSwitchMethod));
            instructions.Add(processor.Create(OpCodes.Stsfld, data.Field));
        }

        private static void CompleteSwitchRegistration(MethodDefinition method, List<Instruction> instructions, ILProcessor processor, IWeavingContext context, string clazz)
        {
            instructions.Add(processor.Create(OpCodes.Call, context.BaseReference.BackStage.BuilderGetterReference));
            instructions.Add(processor.Create(OpCodes.Ldstr, clazz));
            instructions.Add(processor.Create(OpCodes.Callvirt, context.BaseReference.Builder.CompleteMethod));
            IlUtilities.PersistentInstructions(instructions, processor, method.Body.Instructions.First());
        }
    }
}
