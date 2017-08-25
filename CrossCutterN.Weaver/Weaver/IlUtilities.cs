// <copyright file="IlUtilities.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Weaver
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Mono.Cecil;
    using Mono.Cecil.Cil;

    /// <summary>
    /// Utilities functions for IL manipulation.
    /// </summary>
    internal static class IlUtilities
    {
        private static readonly IDictionary<MetadataType, OpCode> IndirectLoadingOpCodeDictionary;
        private static readonly IDictionary<MetadataType, OpCode> LoadingOpCodeDictionary;

        static IlUtilities()
        {
            IndirectLoadingOpCodeDictionary = new Dictionary<MetadataType, OpCode>
                {
                    { MetadataType.Boolean, OpCodes.Ldind_I1 },
                    { MetadataType.Byte, OpCodes.Ldind_U1 },
                    { MetadataType.Char, OpCodes.Ldind_U2 },
                    { MetadataType.Double, OpCodes.Ldind_R8 },
                    { MetadataType.Int16, OpCodes.Ldind_I2 },
                    { MetadataType.Int32, OpCodes.Ldind_I4 },
                    { MetadataType.Int64, OpCodes.Ldind_I8 },
                    { MetadataType.SByte, OpCodes.Ldind_I1 },
                    { MetadataType.Single, OpCodes.Ldind_R4 },
                    { MetadataType.UInt16, OpCodes.Ldind_U2 },
                    { MetadataType.UInt32, OpCodes.Ldind_U4 },
                    { MetadataType.UInt64, OpCodes.Ldind_I8 },
                };
            LoadingOpCodeDictionary = new Dictionary<MetadataType, OpCode>
                {
                    { MetadataType.Boolean, OpCodes.Ldc_I4 },
                    { MetadataType.Byte, OpCodes.Ldc_I4 },
                    { MetadataType.Char, OpCodes.Ldc_I4 },
                    { MetadataType.Double, OpCodes.Ldc_R8 },
                    { MetadataType.Int16, OpCodes.Ldc_I4 },
                    { MetadataType.Int32, OpCodes.Ldc_I4 },
                    { MetadataType.Int64, OpCodes.Ldc_I8 },
                    { MetadataType.SByte, OpCodes.Ldc_I4 },
                    { MetadataType.Single, OpCodes.Ldc_R4 },
                    { MetadataType.UInt16, OpCodes.Ldc_I4 },
                    { MetadataType.UInt32, OpCodes.Ldc_I4 },
                    { MetadataType.UInt64, OpCodes.Ldc_I8 },
                };
        }

        /// <summary>
        /// Checks whether type of custom attribute property is supported.
        /// </summary>
        /// <param name="argument">Custom attribute argument.</param>
        /// <returns>True if the type is supported, false elsewise.</returns>
        public static bool CustomAttributePropertyTypeIsSupported(CustomAttributeArgument argument)
        {
            if (argument.Type.FullName.Equals(typeof(string).FullName))
            {
                return true;
            }

            var referencedTypeSpec = argument.Type as TypeSpecification;
            return referencedTypeSpec != null &&
                   LoadingOpCodeDictionary.ContainsKey(referencedTypeSpec.ElementType.MetadataType);
        }

        /// <summary>
        /// Create instruction for loading custom attribute property value.
        /// </summary>
        /// <param name="processor">The <see cref="ILProcessor"/>.</param>
        /// <param name="argument">Custom attribute argument.</param>
        /// <returns>The instruction created.</returns>
        public static Instruction CreateLoadCustomAttributePropertyValueInstruction(this ILProcessor processor, CustomAttributeArgument argument)
        {
            if (argument.Type.FullName.Equals(typeof(string).FullName))
            {
                return processor.Create(OpCodes.Ldstr, argument.Value.ToString());
            }

            if (argument.Type is TypeSpecification referencedTypeSpec)
            {
                var metaDataType = referencedTypeSpec.ElementType.MetadataType;
                if (IndirectLoadingOpCodeDictionary.ContainsKey(metaDataType))
                {
                    var opCode = LoadingOpCodeDictionary[metaDataType];
                    switch (metaDataType)
                    {
                        case MetadataType.Boolean:
                        case MetadataType.SByte:
                            return processor.Create(opCode, Convert.ToSByte(argument.Value));
                        case MetadataType.Byte:
                            return processor.Create(opCode, Convert.ToByte(argument.Value));
                        case MetadataType.Char:
                            return processor.Create(opCode, Convert.ToChar(argument.Value));
                        case MetadataType.Double:
                            return processor.Create(opCode, Convert.ToDouble(argument.Value));
                        case MetadataType.Int16:
                            return processor.Create(opCode, Convert.ToInt16(argument.Value));
                        case MetadataType.Int32:
                            return processor.Create(opCode, Convert.ToInt32(argument.Value));
                        case MetadataType.Int64:
                            return processor.Create(opCode, Convert.ToInt64(argument.Value));
                        case MetadataType.Single:
                            return processor.Create(opCode, Convert.ToSingle(argument.Value));
                        case MetadataType.UInt16:
                            return processor.Create(opCode, Convert.ToUInt16(argument.Value));
                        case MetadataType.UInt32:
                            return processor.Create(opCode, Convert.ToUInt32(argument.Value));
                        case MetadataType.UInt64:
                            return processor.Create(opCode, Convert.ToUInt64(argument.Value));
                    }
                }
            }

            throw new ArgumentException($"Invalid argument type to be loaded for custom attribute property: {argument.Type.FullName}", "argument");
        }

        /// <summary>
        /// Creates indirect load instruction, for loading parameter value.
        /// </summary>
        /// <param name="processor">The <see cref="ILProcessor"/>.</param>
        /// <param name="type">Type reference of the parameter.</param>
        /// <returns>The instruction created.</returns>
        public static Instruction CreateIndirectLoadInstruction(this ILProcessor processor, TypeReference type)
        {
            Instruction result;
            var referencedTypeSpec = type as TypeSpecification;
            if (referencedTypeSpec != null && IndirectLoadingOpCodeDictionary.ContainsKey(referencedTypeSpec.ElementType.MetadataType))
            {
                result = processor.Create(IndirectLoadingOpCodeDictionary[referencedTypeSpec.ElementType.MetadataType]);
            }
            else if (referencedTypeSpec != null && referencedTypeSpec.ElementType.IsValueType)
            {
                result = processor.Create(OpCodes.Ldobj, referencedTypeSpec.ElementType);
            }
            else
            {
                result = processor.Create(OpCodes.Ldind_Ref);
            }

            return result;
        }

        /// <summary>
        /// Creates box value type instruction, for loading value type value into reference type value.
        /// </summary>
        /// <param name="processor">The <see cref="ILProcessor"/>.</param>
        /// <param name="type">Type reference of value type.</param>
        /// <returns>The instruction created.</returns>
        public static Instruction CreateBoxValueTypeInstruction(this ILProcessor processor, TypeReference type)
        {
            Instruction result;
            if (type is TypeSpecification referencedTypeSpec &&
                IndirectLoadingOpCodeDictionary.ContainsKey(referencedTypeSpec.ElementType.MetadataType))
            {
                result = processor.Create(OpCodes.Box, referencedTypeSpec.ElementType);
            }
            else
            {
                result = processor.Create(OpCodes.Box, type);
            }

            return result;
        }

        /// <summary>
        /// Persistent instructions.
        /// </summary>
        /// <param name="instructions">Instructions to persist.</param>
        /// <param name="processor">The MSIL processor from the persistent target.</param>
        /// <param name="beforeTarget">Instructions will be inserted before this instruction.</param>
        public static void PersistentInstructions(List<Instruction> instructions, ILProcessor processor, Instruction beforeTarget)
        {
            if (instructions.Any())
            {
                foreach (var instruction in instructions)
                {
                    processor.InsertBefore(beforeTarget, instruction);
                }
            }

            instructions.Clear();
        }

        /// <summary>
        /// Checks if the method definition represents a static constructor.
        /// </summary>
        /// <param name="method">The method reference.</param>
        /// <returns>True if the method reference is a static constructor, false elsewise.</returns>
        public static bool IsStaticConstructor(this MethodDefinition method) => method.IsConstructor && method.IsStatic && !method.HasParameters;
    }
}
