/**
 * Description: mixed test advice
 * Author: David Cui
 */

namespace CrossCutterN.Test.ParameterTest
{
    using System;
    using Advice.Parameter;
    using Utilities;

    internal static class ParameterTestAdvice
    {
        public static void EntryEmpty()
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("EntryEmpty", null, null, null, null, null));
        }

        public static void EntryExecution(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("EntryEmpty", null, execution, null, null, null));
        }

        public static void ExceptionEmpty()
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExceptionEmpty", null, null, null, null, null));
        }

        public static void ExceptionExecution(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExceptionEmpty", null, execution, null, null, null));
        }

        public static void ExceptionException(Exception e)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExceptionEmpty", null, null, e, null, null));
        }

        public static void ExceptionExecutionException(IExecution execution, Exception exception)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExceptionEmpty", null, execution, exception, null, null));
        }

        public static void ExitEmpty()
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExitEmpty", null, null, null, null, null));
        }

        public static void ExitExecution(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExitExecution", null, execution, null, null, null));
        }

        public static void ExitReturn(IReturn rtn)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExitExecution", null, null, null, rtn, null));
        }

        public static void ExitHasException(bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExitExecution", null, null, null, null, hasException));
        }

        public static void ExitExecutionReturn(IExecution execution, IReturn rtn)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExitExecution", null, execution, null, rtn, null));
        }

        public static void ExitExecutionHasException(IExecution execution, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExitExecution", null, execution, null, null, hasException));
        }

        public static void ExitReturnHasException(IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExitExecution", null, null, null, rtn, hasException));
        }

        public static void ExitExecutionReturnHasException(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExitExecution", null, execution, null, rtn, hasException));
        }
    }
}
