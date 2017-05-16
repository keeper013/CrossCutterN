/**
 * Description: name expression test target
 * Author: David Cui
 */

using System.Globalization;

namespace CrossCutterN.Test.ParameterTest
{
    using System;
    using System.Text;

    public class ParameterTestTarget
    {
        //All
        public static void AllExceptionTest(int x, StringBuilder strb, Func<int, int> func)
        {
            var value = new StringBuilder(strb.ToString());
            if (func != null)
            {
                value.Append(func(x));
            }
            else
            {
                value.Append("func is null").Append(x);
            }
            throw new Exception(value.ToString());
        }

        public static void AllVoidReturnTest()
        {
            Console.Out.WriteLine("AllVoidReturn");
        }

        public static int AllTest()
        {
            var x = 0;
            for (var i = 0; i < 10; i++)
            {
                x += i;
            }
            return x;
        }

        //NoExecution
        public static void NoExecutionTest()
        {
            throw new Exception();
        }
        
        //NoReturn
        public static string NoReturnTest(int x, Func<int, int> func)
        {
            if (func != null)
            {
                return func(x).ToString(CultureInfo.InvariantCulture);
            }
            return "No func for " + x;
        }

        //NoHasException
        public static void NoHasExceptionTest()
        {
            throw new Exception();
        }

        //NoException
        public static void NoExceptionTest()
        {
            throw new Exception();
        }

        //OnlyExecution
        public static void OnlyExecutionTest()
        {
            throw new Exception();
        }
    }
}
