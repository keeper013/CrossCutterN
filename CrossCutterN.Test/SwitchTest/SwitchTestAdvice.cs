/**
 * Description: switch test advice
 * Author: David Cui
 */

namespace CrossCutterN.Test.SwitchTest
{
    using Advice.Parameter;
    using Utilities;

    internal static class SwitchTestAdvice
    {
        public static void Entry1(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Entry1", execution, null, null, null));
        }

        public static void Entry2(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Entry2", execution, null, null, null));
        }

        public static void Entry3(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Entry3", execution, null, null, null));
        }

        public static void Exit1(IExecution execution, IReturn rtn)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exit1", execution, null, rtn, null));
        }

        public static void Exit2(IExecution execution, IReturn rtn)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exit2", execution, null, rtn, null));
        }

        public static void Exit3(IExecution execution, IReturn rtn)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exit3", execution, null, rtn, null));
        }
    }
}
