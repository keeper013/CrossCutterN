/**
 * Description: advice method validator
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Utilities
{
    using System;
    using System.Reflection;
    using Aspect;
    using Batch;
    using Advice.Parameter;
    using Advice.Common;

    internal static class AdviceValidator
    {
        private static readonly string MethodExecutionContextTypeName = typeof (IExecutionContext).FullName;
        private static readonly string MethodExecutionTypeName = typeof(IExecution).FullName;
        private static readonly string ExceptionTypeName = typeof(Exception).FullName;
        private static readonly string ReturnTypeName = typeof(IReturn).FullName;
        private static readonly string BooTypeName = typeof(bool).FullName;

        private const AdviceParameterFlag EntryValidParameterFlag = 
            AdviceParameterFlag.Context | AdviceParameterFlag.Execution;

        private const AdviceParameterFlag ExceptionValidParameterFlag =
            AdviceParameterFlag.Context | AdviceParameterFlag.Execution | AdviceParameterFlag.Exception;

        private const AdviceParameterFlag ExitValidParameterFlag =
            AdviceParameterFlag.Context | AdviceParameterFlag.Execution | AdviceParameterFlag.Return | AdviceParameterFlag.HasException;

        public static AdviceParameterFlag Validate(JoinPoint joinPoint, MethodInfo advice)
        {
            if (advice == null)
            {
                throw new ArgumentNullException("advice");
            }
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
            throw new ArgumentException(string.Format("Unsupported join point type {0}", joinPoint), "joinPoint");
        }

        private static void GeneralMethodValidation(MethodInfo method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }
            if (!method.IsPublic)
            {
                throw new ArgumentException("Advice method must be public");
            }
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
                if(temp <= current)
                {
                    throw new ArgumentException(string.Format("Wrong order or redundant type of parameter {0} for advice", parameters[i].Name));
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
                if (!flag.HasContextParameter())
                {
                    throw new ArgumentException(string.Format("Parameter type {0} is not allowed for this advice", typeFullName));
                }
                return AdviceParameterFlag.Context;
            }
            if (typeFullName.Equals(MethodExecutionTypeName))
            {
                if (!flag.HasExecutionParameter())
                {
                    throw new ArgumentException(string.Format("Parameter type {0} is not allowed for this advice", typeFullName));
                }
                return AdviceParameterFlag.Execution;
            }
            if (typeFullName.Equals(ExceptionTypeName))
            {
                if (!flag.HasExceptionParameter())
                {
                    throw new ArgumentException(string.Format("Parameter type {0} is not allowed for this advice", typeFullName));
                }
                return AdviceParameterFlag.Exception;
            }
            if (typeFullName.Equals(ReturnTypeName))
            {
                if (!flag.HasReturnParameter())
                {
                    throw new ArgumentException(string.Format("Parameter type {0} is not allowed for this advice", typeFullName));
                }
                return AdviceParameterFlag.Return;
            }
            if (typeFullName.Equals(BooTypeName))
            {
                if (!flag.HasHasExceptionParameter())
                {
                    throw new ArgumentException(string.Format("Parameter type {0} is not allowed for this advice", typeFullName));
                }
                return AdviceParameterFlag.HasException;
            }
            throw new ArgumentException(string.Format("Parameter type {0} is not allowed", typeFullName));
        }
    }
}
