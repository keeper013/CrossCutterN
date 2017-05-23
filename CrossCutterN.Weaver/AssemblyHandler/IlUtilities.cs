/**
 * Description: Intermediate language utilities
 * Author: David Cui
 */

using System;

namespace CrossCutterN.Weaver.AssemblyHandler
{
    using System.Collections.Generic;
    using Mono.Cecil;
    using Mono.Cecil.Cil;

    public static class IlUtilities
    {
        private static readonly IDictionary<MetadataType, OpCode> IndirectLoadingOpCodeDictionary;
        private static readonly IDictionary<MetadataType, OpCode> LoadingOpCodeDictionary; 

        static IlUtilities()
        {
            IndirectLoadingOpCodeDictionary = new Dictionary<MetadataType, OpCode>
                {
                    {MetadataType.Boolean, OpCodes.Ldind_I1},
                    {MetadataType.Byte, OpCodes.Ldind_U1},
                    {MetadataType.Char, OpCodes.Ldind_U2},
                    {MetadataType.Double, OpCodes.Ldind_R8},
                    {MetadataType.Int16, OpCodes.Ldind_I2},
                    {MetadataType.Int32, OpCodes.Ldind_I4},
                    {MetadataType.Int64, OpCodes.Ldind_I8},
                    {MetadataType.SByte, OpCodes.Ldind_I1},
                    {MetadataType.Single, OpCodes.Ldind_R4},
                    {MetadataType.UInt16, OpCodes.Ldind_U2},
                    {MetadataType.UInt32, OpCodes.Ldind_U4},
                    {MetadataType.UInt64, OpCodes.Ldind_I8}
                };
            LoadingOpCodeDictionary = new Dictionary<MetadataType, OpCode>
                {
                    {MetadataType.Boolean, OpCodes.Ldc_I4},
                    {MetadataType.Byte, OpCodes.Ldc_I4},
                    {MetadataType.Char, OpCodes.Ldc_I4},
                    {MetadataType.Double, OpCodes.Ldc_R8},
                    {MetadataType.Int16, OpCodes.Ldc_I4},
                    {MetadataType.Int32, OpCodes.Ldc_I4},
                    {MetadataType.Int64, OpCodes.Ldc_I8},
                    {MetadataType.SByte, OpCodes.Ldc_I4},
                    {MetadataType.Single, OpCodes.Ldc_R4},
                    {MetadataType.UInt16, OpCodes.Ldc_I4},
                    {MetadataType.UInt32, OpCodes.Ldc_I4},
                    {MetadataType.UInt64, OpCodes.Ldc_I8}
                };
        }

        public static bool CustomAttributePropertyTypeIsSupported(CustomAttributeArgument argument)
        {
            if (argument.Type.FullName.Equals(typeof (string).FullName))
            {
                return true;
            }
            var referencedTypeSpec = argument.Type as TypeSpecification;
            return referencedTypeSpec != null &&
                   LoadingOpCodeDictionary.ContainsKey(referencedTypeSpec.ElementType.MetadataType);
        }

        public static Instruction CreateLoadCustomAttributePropertyValueInstruction(this ILProcessor processor, CustomAttributeArgument argument)
        {
            if (argument.Type.FullName.Equals(typeof (string).FullName))
            {
                return processor.Create(OpCodes.Ldstr, argument.Value.ToString());
            }
            var referencedTypeSpec = argument.Type as TypeSpecification;
            if (referencedTypeSpec != null)
            {
                var metaDataType = referencedTypeSpec.ElementType.MetadataType;
                if(IndirectLoadingOpCodeDictionary.ContainsKey(metaDataType))
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
            throw new ArgumentException(string.Format("Invalid argument type to be loaded for custom attribute property: {0}", argument.Type.FullName), "argument");
        }

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

        public static Instruction CreateBoxValueTypeInstruction(this ILProcessor processor, TypeReference type)
        {
            Instruction result;
            var referencedTypeSpec = type as TypeSpecification;
            if (referencedTypeSpec != null &&
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

        public static bool IsStaticConstructor(this MethodDefinition method)
        {
            return method.IsConstructor && method.IsStatic && !method.HasParameters;
        }
    }
}
