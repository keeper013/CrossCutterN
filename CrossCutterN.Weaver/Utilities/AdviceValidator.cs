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

    internal static class AdviceValidator
    {
        private static readonly Type MethodExecutionType = typeof(IExecution);
        private static readonly Type ExceptionType = typeof(Exception);
        private static readonly Type ReturnType = typeof(IReturn);
        private static readonly Type BooType = typeof(bool);
        private static readonly Type VoidType = typeof(void);

        public static AdviceParameterFlag Validate(JoinPoint joinPoint, MethodInfo advice)
        {
            GeneralMethodValidation(advice);
            switch (joinPoint)
            {
                case JoinPoint.Entry:
                    return ValidateEntry(advice);
                case JoinPoint.Exception:
                    return ValidateException(advice);
                case JoinPoint.Exit:
                    return ValidateExit(advice);
            }
            throw new ArgumentException(string.Format("Unsupported join point type {0}", joinPoint), "joinPoint");
        }

        private static AdviceParameterFlag ValidateEntry(MethodInfo advice)
        {
            var parameters = advice.GetParameters();
            var parameterCount = parameters.Length;
            if (parameterCount > 1)
            {
                throw new ArgumentException("Advice method for entry can have at most 1 parameter.");
            }
            var parameterType = AdviceParameterFlag.None;
            if (parameterCount == 1)
            {
                if (!parameters[0].IsOfType(MethodExecutionType))
                {
                    throw new ArgumentException(
                        string.Format("Only allowed parameter type of advice method for entry with 1 parameter is \"{0}\"",
                                      MethodExecutionType.FullName));
                }
                parameterType = AdviceParameterFlag.Execution;
            }
            return parameterType;
        }

        private static AdviceParameterFlag ValidateException(MethodInfo advice)
        {
            var parameters = advice.GetParameters();
            var parameterCount = parameters.Length;
            var parameterType = AdviceParameterFlag.None;
            switch (parameterCount)
            {
                case 0:
                    break;
                case 1:
                    if (parameters[0].IsOfType(MethodExecutionType))
                    {
                        parameterType |= AdviceParameterFlag.Execution;
                    }
                    else if (parameters[0].IsOfType(ExceptionType))
                    {
                        parameterType |= AdviceParameterFlag.Exception;
                    }
                    else
                    {
                        throw new ArgumentException(
                        string.Format(
                            "Only allowed parameter type of advice method for exception with one parameter is \"{0}\" or \"{1}\"",
                            MethodExecutionType.FullName, ExceptionType.FullName));
                    }
                    break;
                case 2:
                    if (!parameters[0].IsOfType(MethodExecutionType) || !parameters[1].IsOfType(ExceptionType))
                    {
                        throw new ArgumentException(
                            string.Format(
                                "Only allowed parameter type and order for advice method for execption with 2 parameters is (\"{0}\", \"{1}\")",
                                MethodExecutionType.FullName, ExceptionType.FullName));
                    }
                    parameterType |= AdviceParameterFlag.Execution;
                    parameterType |= AdviceParameterFlag.Exception;
                    break;
                default:
                    throw new ArgumentException("Advice method for exception can have at most 2 parameters.");
            }
            return parameterType;
        }

        private static AdviceParameterFlag ValidateExit(MethodInfo advice)
        {
            var parameters = advice.GetParameters();
            var parameterCount = parameters.Length;
            var parameterType = AdviceParameterFlag.None;
            switch (parameterCount)
            {
                case 0:
                    break;
                case 1:
                    if (parameters[0].IsOfType(MethodExecutionType))
                    {
                        parameterType |= AdviceParameterFlag.Execution;
                    }
                    else if (parameters[0].IsOfType(ReturnType))
                    {
                        parameterType |= AdviceParameterFlag.Return;
                    }
                    else if (parameters[0].IsOfType(BooType))
                    {
                        parameterType |= AdviceParameterFlag.HasException;
                    }
                    else
                    {
                        throw new ArgumentException(
                            string.Format("Only allowed parameter type for injectable method for exit with 1 parameter is \"{0}\" or \"{1}\" or \"{2}\"",
                                          MethodExecutionType.FullName, ReturnType.FullName, BooType));
                    }
                    break;
                case 2:
                    if (parameters[0].IsOfType(MethodExecutionType) && parameters[1].IsOfType(ReturnType))
                    {
                        parameterType |= AdviceParameterFlag.Execution;
                        parameterType |= AdviceParameterFlag.Return;
                    }
                    else if (parameters[0].IsOfType(MethodExecutionType) && parameters[1].IsOfType(BooType))
                    {
                        parameterType |= AdviceParameterFlag.Execution;
                        parameterType |= AdviceParameterFlag.HasException;
                    }
                    else if (parameters[0].IsOfType(ReturnType) && parameters[1].IsOfType(BooType))
                    {
                        parameterType |= AdviceParameterFlag.Return;
                        parameterType |= AdviceParameterFlag.HasException;
                    }
                    else
                    {
                        throw new ArgumentException(
                            string.Format(
                                "Only allowed parameter type and order for advice method for exit with 2 parameters are (\"{0}\", \"{1}\") or (\"{0}\", \"{2}\") or (\"{1}\", \"{2}\")",
                                MethodExecutionType.FullName, ReturnType.FullName, BooType.FullName));
                    }
                    break;
                case 3:
                    if (!parameters[0].IsOfType(MethodExecutionType) || !parameters[1].IsOfType(ReturnType) || !parameters[2].IsOfType(BooType))
                    {
                        throw new ArgumentException(
                            string.Format(
                                "Only allowed parameter type and order for injectable method for exit with 3 parameters is (\"{0}\", \"{1}\", \"{2}\")",
                                MethodExecutionType.FullName, ReturnType.FullName, BooType.FullName));
                    }
                    parameterType |= AdviceParameterFlag.Execution;
                    parameterType |= AdviceParameterFlag.Return;
                    parameterType |= AdviceParameterFlag.HasException;
                    break;
                default:
                    throw new ArgumentException("Advice method for exit can have at most 3 parameters.");
            }
            return parameterType;
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
            if (!method.ReturnType.FullName.Equals(VoidType.FullName))
            {
                throw new ArgumentException("Return type of advice method must be void");
            }
        }

        private static bool IsOfType(this ParameterInfo info, Type type)
        {
            return info.ParameterType.FullName.Equals(type.FullName);
        }
    }
}
