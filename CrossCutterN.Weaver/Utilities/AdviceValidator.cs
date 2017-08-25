// <copyright file="AdviceValidator.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Utilities
{
    using System;
    using System.Reflection;
    using CrossCutterN.Aspect.Aspect;
    using CrossCutterN.Base.Metadata;
    using CrossCutterN.Weaver.Weaver;

    /// <summary>
    /// Advice validator. This class verifies format of input advice.
    /// Considering user may extend aspect module to have customized aspects, validation is put in weaver module.
    /// </summary>
    internal static class AdviceValidator
    {
        private const AdviceParameterFlag EntryValidParameterFlag =
            AdviceParameterFlag.Context | AdviceParameterFlag.Execution;

        private const AdviceParameterFlag ExceptionValidParameterFlag =
            AdviceParameterFlag.Context | AdviceParameterFlag.Execution | AdviceParameterFlag.Exception;

        private const AdviceParameterFlag ExitValidParameterFlag =
            AdviceParameterFlag.Context | AdviceParameterFlag.Execution | AdviceParameterFlag.Return | AdviceParameterFlag.HasException;

        private static readonly string MethodExecutionContextTypeName = typeof(IExecutionContext).FullName;
        private static readonly string MethodExecutionTypeName = typeof(IExecution).FullName;
        private static readonly string ExceptionTypeName = typeof(Exception).FullName;
        private static readonly string ReturnTypeName = typeof(IReturn).FullName;
        private static readonly string BooTypeName = typeof(bool).FullName;

        /// <summary>
        /// Validate an advice method based on the join point it is to be injected to.
        /// </summary>
        /// <param name="joinPoint">Join point that the advice method to be injected to.</param>
        /// <param name="advice">Advice method to be validated.</param>
        /// <returns>Flag for parameter pattern of the advice.</returns>
        public static AdviceParameterFlag Validate(JoinPoint joinPoint, MethodInfo advice)
        {
            GeneralMethodValidation(advice);
            switch (joinPoint)
            {
                case JoinPoint.Entry:
                    return ValidateParameters(advice, EntryValidParameterFlag);
                case JoinPoint.Exception:
                    return ValidateParameters(advice, ExceptionValidParameterFlag);
                case JoinPoint.Exit:
                    return ValidateParameters(advice, ExitValidParameterFlag);
            }

            throw new ArgumentException($"Unsupported join point type {joinPoint}", "joinPoint");
        }

        private static void GeneralMethodValidation(MethodInfo method)
        {
            // Considering the possibility of setting ReflectionPermissionFlag.RestrictedMemberAccess, no accessibility validation is performed here.
            // So users will be free to use internal, protected or private methods as AOP code to be injected.
            // If they don't have the permission to access the non-public AOP methods in target assemblies, the injected code will throw security exceptions at runtime.
            if (!method.IsStatic)
            {
                throw new ArgumentException("Advice method must be static");
            }

            if (!method.IsVoidReturn())
            {
                throw new ArgumentException("Return type of advice method must be void");
            }
        }

        private static AdviceParameterFlag ValidateParameters(MethodInfo advice, AdviceParameterFlag flag)
        {
            var parameters = advice.GetParameters();
            var parameterCount = parameters.Length;
            if (parameterCount == 0)
            {
                return AdviceParameterFlag.None;
            }

            var result = parameters[0].Validate(flag);
            var current = result;
            for (var i = 1; i < parameterCount; i++)
            {
                var temp = parameters[i].Validate(flag);
                if (temp <= current)
                {
                    throw new ArgumentException($"Wrong order or redundant type of parameter {parameters[i].Name} for advice");
                }

                current = temp;
                result |= current;
            }

            return result;
        }

        private static AdviceParameterFlag Validate(this ParameterInfo info, AdviceParameterFlag flag)
        {
            var typeFullName = info.ParameterType.FullName;
            if (typeFullName.Equals(MethodExecutionContextTypeName))
            {
                if (!flag.Contains(AdviceParameterFlag.Context))
                {
                    throw new ArgumentException($"Parameter type {typeFullName} is not allowed for this advice");
                }

                return AdviceParameterFlag.Context;
            }

            if (typeFullName.Equals(MethodExecutionTypeName))
            {
                if (!flag.Contains(AdviceParameterFlag.Execution))
                {
                    throw new ArgumentException($"Parameter type {typeFullName} is not allowed for this advice");
                }

                return AdviceParameterFlag.Execution;
            }

            if (typeFullName.Equals(ExceptionTypeName))
            {
                if (!flag.Contains(AdviceParameterFlag.Exception))
                {
                    throw new ArgumentException($"Parameter type {typeFullName} is not allowed for this advice");
                }

                return AdviceParameterFlag.Exception;
            }

            if (typeFullName.Equals(ReturnTypeName))
            {
                if (!flag.Contains(AdviceParameterFlag.Return))
                {
                    throw new ArgumentException($"Parameter type {typeFullName} is not allowed for this advice");
                }

                return AdviceParameterFlag.Return;
            }

            if (typeFullName.Equals(BooTypeName))
            {
                if (!flag.Contains(AdviceParameterFlag.HasException))
                {
                    throw new ArgumentException($"Parameter type {typeFullName} is not allowed for this advice");
                }

                return AdviceParameterFlag.HasException;
            }

            throw new ArgumentException($"Parameter type {typeFullName} is not allowed");
        }
    }
}
