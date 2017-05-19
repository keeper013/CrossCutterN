/**
 * Description: mixed test target
 * Author: David Cui
 */

namespace CrossCutterN.Test.MixedTest
{
    using System;

    [MixedConcernClass3(ConcernPublic = true, ConcernStatic = true, ConcernMethod = true)]
    internal class MixedTestTarget
    {
        [MixedConcernMethod1]
        [MixedConcernMethod2]
        public static void ConcernedByAttributeAndClassName()
        {
            Console.Out.WriteLine("called");
        }
    }
}
