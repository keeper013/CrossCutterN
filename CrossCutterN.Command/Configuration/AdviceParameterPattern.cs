/**
 * Description: advice parameter pattern
 * Author: David Cui
 */

namespace CrossCutterN.Command.Configuration
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
        Execution = 1,
        Exception = 2,
        Return = 4,
        HasException = 8,
        ExecutionException = Execution | Exception,
        ExecutionReturn = Execution | Return,
        ExecutionHasException = Execution | HasException,
        ReturnHasException = Return | HasException,
        ExecutionReturnHasException = Execution | Return | HasException
    }

    internal static class AdviceParameterPatternExtension
    {
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

        public static bool NeedHasException(this AdviceParameterPattern parameterPattern)
        {
            return (parameterPattern & AdviceParameterPattern.HasException) == AdviceParameterPattern.HasException;
        }
    }
}
