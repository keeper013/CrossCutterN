/**
 * Description: Advice parameter flag extension
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.AssemblyHandler
{
    using Batch;

    internal static class AdviceParameterFlagExtension
    {
        public static bool NeedExecutionParameter(this AdviceParameterFlag parameterType)
        {
            return (parameterType & AdviceParameterFlag.Execution) == AdviceParameterFlag.Execution;
        }

        public static bool NeedReturnParameter(this AdviceParameterFlag parameterType)
        {
            return (parameterType & AdviceParameterFlag.Return) == AdviceParameterFlag.Return;
        }

        public static bool NeedExceptionParameter(this AdviceParameterFlag parameterType)
        {
            return (parameterType & AdviceParameterFlag.Exception) == AdviceParameterFlag.Exception;
        }

        public static bool NeedHasException(this AdviceParameterFlag parameterType)
        {
            return (parameterType & AdviceParameterFlag.HasException) == AdviceParameterFlag.HasException;
        }
    }
}
