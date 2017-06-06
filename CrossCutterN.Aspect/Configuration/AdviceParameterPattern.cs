/**
 * Description: advice parameter pattern
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Configuration
{
    using System;

    /// <summary>
    /// This implementation is similar to AdviceParameterFlags of Weaver
    /// The reason for duplicating the enumeration implementation is to hide the implementation detail for weaver
    /// </summary>
    [Flags]
    public enum AdviceParameterPattern
    {
        Empty = 0,
        Context = 1,
        Execution = 2,
        Exception = 4,
        Return = 8,
        HasException = 16,
        ContextExecution = Context | Execution,
        ContextException = Context | Exception,
        ContextReturn = Context | Return,
        ContextHasException = Context | HasException,
        ExecutionException = Execution | Exception,
        ExecutionReturn = Execution | Return,
        ExecutionHasException = Execution | HasException,
        ReturnHasException = Return | HasException,
        ContextExecutionException = Context | Execution | Exception,
        ContextExecutionReturn = Context | Execution | Return,
        ContextExecutionHasException = Context | Execution | HasException,
        ContextReturnHasException = Context | Return | HasException,
        ExecutionReturnHasException = Execution | Return | HasException,
        ContextExecutionReturnHasException = Context | Execution | Return | HasException
    }

    internal static class AdviceParameterPatternExtension
    {
        public static bool NeedExecutionContextParameter(this AdviceParameterPattern parameterPattern)
        {
            return (parameterPattern & AdviceParameterPattern.Context) == AdviceParameterPattern.Context;
        }

        public static bool NeedExecutionParameter(this AdviceParameterPattern parameterPattern)
        {
            return (parameterPattern & AdviceParameterPattern.Execution) == AdviceParameterPattern.Execution;
        }

        public static bool NeedReturnParameter(this AdviceParameterPattern parameterPattern)
        {
            return (parameterPattern & AdviceParameterPattern.Return) == AdviceParameterPattern.Return;
        }

        public static bool NeedExceptionParameter(this AdviceParameterPattern parameterPattern)
        {
            return (parameterPattern & AdviceParameterPattern.Exception) == AdviceParameterPattern.Exception;
        }

        public static bool NeedHasExceptionParameter(this AdviceParameterPattern parameterPattern)
        {
            return (parameterPattern & AdviceParameterPattern.HasException) == AdviceParameterPattern.HasException;
        }
    }
}
