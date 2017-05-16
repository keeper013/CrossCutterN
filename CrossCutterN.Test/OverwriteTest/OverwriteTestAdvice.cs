/**
 * Description: Overwrite test advice
 * Author: David Cui
 */

namespace CrossCutterN.Test.OverwriteTest
{
    using Utilities;

    internal static class OverwriteTestAdvice
    {
        public static void Entry()
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Entry", null, null, null, null));
        }

        public static void Exception()
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exception", null, null, null, null));
        }

        public static void Exit()
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exit", null, null, null, null));
        }
    }
}
