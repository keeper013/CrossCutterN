/**
 * Description: switch test target
 * Author: David Cui
 */

namespace CrossCutterN.Test.SwitchTest
{
    using System;

    internal class SwitchClassAspectTestTarget
    {
        public static int Test(int x)
        {
            return x*x;
        }
    }

    internal class SwitchMethodAspectTestTarget
    {
        public static int Test1(int x)
        {
            return x * x;
        }

        public static int Test2(int x)
        {
            return x * x;
        }

        public static int Value { get; set; }
    }

    internal class SwitchPropertyAspectTestTarget
    {
        public static int Value { get; set; }
    }

    internal class SwitchAspectTestTarget
    {
        public static int Test1(int x)
        {
            return x * x;
        }

        public static int Test2(int x)
        {
            return x * x;
        }
    }

    internal class SwitchClassTestTarget
    {
        public static int Test(int x)
        {
            return x * x;
        }
    }

    internal class SwitchMethodTestTarget
    {
        public static int Test1(int x)
        {
            return x * x;
        }

        public static int Test2(int x)
        {
            return x * x;
        }
    }

    internal class SwitchPropertyTestTarget
    {
        public static int Value1 { get; set; }
        public static int Value2 { get; set; }
    }

    internal class SwitchCompareTestTarget
    {
        public static int Test1(int x)
        {
            return x * x;
        }
    }

    internal class SwitchLookUpTestTarget
    {
        public static int Test1(int x)
        {
            return x * x;
        }

        public static int Value1 { get; set; }
    }

    internal class SwitchExceptionTestTarget
    {
        public static int Test1(int x)
        {
            if (x == 2)
            {
                throw new Exception();
            }
            return x * x;
        }
    }

    internal class SwitchNoReturnTestTarget
    {
        public static int Test(int x)
        {
            return x * x;
        }
    }

    internal class SwitchNoExecutionTestTarget
    {
        public static int Test(int x)
        {
            return x * x;
        }
    }

    internal class SwitchOnlyExecutionTestTarget
    {
        public static int Test(int x)
        {
            return x * x;
        }
    }
}
