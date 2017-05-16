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
        public static void EntryByAttribute(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("EntryByAttribute", execution, null, null, null));
        }

        public static void ExitByAttribute()
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExitByAttribute", null, null, null, null));
        }

        public static void ExitByNameExpression(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExitByNameExpression", execution, null, null, null));
        }
    }
}
