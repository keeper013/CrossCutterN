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
        public static void Entry1(IExecutionContext context, IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Entry1", context, execution, null, null, null));
        }

        public static void Entry2(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Entry2", null, execution, null, null, null));
        }

        public static void Entry3(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Entry3", null, execution, null, null, null));
        }

        public static void Exit1(IExecutionContext context, IExecution execution, IReturn rtn)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exit1", context, execution, null, rtn, null));
        }

        public static void Exit2(IExecution execution, IReturn rtn)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exit2", null, execution, null, rtn, null));
        }

        public static void Exit3(IExecution execution, IReturn rtn)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exit3", null, execution, null, rtn, null));
        }

        public static void Exit4(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exit4", null, execution, null, rtn, hasException));
        }

        public static void Exit5(bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exit5", null, null, null, null, hasException));
        }

        public static void Entry6(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Entry6", null, execution, null, null, null));
        }

        public static void Exit6(bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exit6", null, null, null, null, hasException));
        }

        public static void Entry7()
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Entry7", null, null, null, null, null));
        }

        public static void Exit7(IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exit7", null, null, null, rtn, hasException));
        }

        public static void Entry8(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Entry8", null, execution, null, null, null));
        }

        public static void Exit8(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exit8", null, execution, null, null, null));
        }
    }
}
