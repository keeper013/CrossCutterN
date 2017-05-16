/**
 * Description: mixed test target
 * Author: David Cui
 */

namespace CrossCutterN.Test.MixedTest
{
    using System;

    internal class MixedTestTarget
    {
        [MixedConcernMethod]
        public static void ConcernedByAttributeAndClassName()
        {
            Console.Out.WriteLine("called");
        }
    }
}
