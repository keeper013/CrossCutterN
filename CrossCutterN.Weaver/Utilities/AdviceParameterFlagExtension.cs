/**
 * Description: Advice parameter flag extension
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Utilities
{
    using Batch;

    internal static class AdviceParameterFlagExtension
    {
        public static bool HasContextParameter(this AdviceParameterFlag parameterFlag)
        {
            return (parameterFlag & AdviceParameterFlag.Context) == AdviceParameterFlag.Context;
        }

        public static bool HasExecutionParameter(this AdviceParameterFlag parameterFlag)
        {
            return (parameterFlag & AdviceParameterFlag.Execution) == AdviceParameterFlag.Execution;
        }

        public static bool HasReturnParameter(this AdviceParameterFlag parameterFlag)
        {
            return (parameterFlag & AdviceParameterFlag.Return) == AdviceParameterFlag.Return;
        }

        public static bool HasExceptionParameter(this AdviceParameterFlag parameterFlag)
        {
            return (parameterFlag & AdviceParameterFlag.Exception) == AdviceParameterFlag.Exception;
        }

        public static bool HasHasExceptionParameter(this AdviceParameterFlag parameterFlag)
        {
            return (parameterFlag & AdviceParameterFlag.HasException) == AdviceParameterFlag.HasException;
        }
    }
}
