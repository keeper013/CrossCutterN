/**
 * Description: Context test advice
 * Author: David Cui
 */

namespace CrossCutterN.Test.ContextTest
{
    using System;
    using Advice.Parameter;
    using Utilities;

    internal static class ContextTestAdvice
    {
        public static void Entry1(IExecutionContext context, IExecution execution)
        {
            context.Set("Entry1", new TestObj { Value1 = 1, Value2 = "Entry1"});
            MethodAdviceContainer.Add(new MethodAdviceRecord("Entry1", context, execution, null, null, null));
        }

        public static void Exception1(IExecutionContext context, Exception e)
        {
            if (context.Exist("Entry1"))
            {
                context.Remove("Entry1");
            }
            context.Set("Exception1", new TestObj { Value1 = 100, Value2 = "Exception1"});
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exception1", context, null, e, null, null));
        }

        public static void Exit1(IExecutionContext context, IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exit1", context, execution, null, rtn, hasException));
        }

        public static void Entry2(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Entry2", null, execution, null, null, null));
        }

        public static void Exception2(Exception e)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exception2", null, null, e, null, null));
        }

        public static void Exit2(bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exit2", null, null, null, null, hasException));
        }
    }

    internal class TestObj
    {
        public int Value1 { get; set; }
        public string Value2 { get; set; }
    }
}
