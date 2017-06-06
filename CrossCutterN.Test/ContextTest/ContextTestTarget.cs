/**
 * Description: Context Test Targets
 * Author: David Cui
 */

namespace CrossCutterN.Test.ContextTest
{
    using System;

    internal class ContextTestTarget
    {
        [ContextConcernMethod]
        public static int Test1(int x)
        {
            return x*x;
        }

        [ContextConcernMethod]
        public static int Test2(int x)
        {
            throw new Exception();
        }

        [ContextEntryExceptionConcernMethod]
        public static int Test3(int x)
        {
            return x * x;
        }

        [ContextEntryExceptionConcernMethod]
        public static int Test4(int x)
        {
            throw new Exception();
        }

        [ContextEntryExitConcernMethod]
        public static int Test5(int x)
        {
            return x * x;
        }

        [ContextEntryExitConcernMethod]
        public static int Test6(int x)
        {
            throw new Exception();
        }

        [ContextExceptionExitConcernMethod]
        public static int Test7(int x)
        {
            return x * x;
        }

        [ContextExceptionExitConcernMethod]
        public static int Test8(int x)
        {
            throw new Exception();
        }
    }
}
