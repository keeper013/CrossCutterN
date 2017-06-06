/**
 * Description: mixed test advice
 * Author: David Cui
 */

namespace CrossCutterN.Test.NameExpressionTest
{
    using Utilities;

    internal static class NameExpressionTestAdvice
    {
        public static void Entry()
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Entry", null, null, null, null, null));
        }
    }
}
