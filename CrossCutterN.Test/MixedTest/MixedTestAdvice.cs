/**
 * Description: mixed test advice
 * Author: David Cui
 */

namespace CrossCutterN.Test.MixedTest
{
    using Advice.Parameter;
    using Utilities;

    internal static class MixedTestAdvice
    {
        public static void EntryByAttribute1(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("EntryByAttribute1", execution, null, null, null));
        }

        public static void EntryByAttribute2(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("EntryByAttribute2", execution, null, null, null));
        }

        public static void EntryByAttribute3(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("EntryByAttribute3", execution, null, null, null));
        }

        public static void ExitByAttribute1()
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExitByAttribute1", null, null, null, null));
        }

        public static void ExitByAttribute2()
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExitByAttribute2", null, null, null, null));
        }

        public static void ExitByAttribute3()
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExitByAttribute3", null, null, null, null));
        }

        public static void ExitByNameExpression(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExitByNameExpression", execution, null, null, null));
        }
    }
}
