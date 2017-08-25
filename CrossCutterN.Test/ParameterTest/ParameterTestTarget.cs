// <copyright file="ParameterTestTarget.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.ParameterTest
{
    using System;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// Parameter test target class.
    /// </summary>
    public sealed class ParameterTestTarget
    {
        /// <summary>
        /// To test with all parameters which thrown exceptions.
        /// </summary>
        /// <param name="x">Some integer.</param>
        /// <param name="strb">Some string builder.</param>
        /// <param name="func">Some function.</param>
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

        /// <summary>
        /// To test with all parameters which has void return type.
        /// </summary>
        public static void AllVoidReturnTest()
        {
            Console.Out.WriteLine("AllVoidReturn");
        }

        /// <summary>
        /// To test with all parameters.
        /// </summary>
        /// <returns>Some integer.</returns>
        public static int AllTest()
        {
            var x = 0;
            for (var i = 0; i < 10; i++)
            {
                x += i;
            }

            return x;
        }

        /// <summary>
        /// To test with no <see cref="CrossCutterN.Base.Metadata.IExecution"/> parameter.
        /// </summary>
        public static void NoExecutionTest()
        {
            throw new Exception();
        }

        /// <summary>
        /// To test with no <see cref="CrossCutterN.Base.Metadata.IReturn"/> parameter.
        /// </summary>
        /// <param name="x">Some integer.</param>
        /// <param name="func">Some function.</param>
        /// <returns>Some string.</returns>
        public static string NoReturnTest(int x, Func<int, int> func)
        {
            if (func != null)
            {
                return func(x).ToString(CultureInfo.InvariantCulture);
            }

            return "No func for " + x;
        }

        /// <summary>
        /// To test with no HasException parameter.
        /// </summary>
        public static void NoHasExceptionTest()
        {
            throw new Exception();
        }

        /// <summary>
        /// To test with no System.Exception parameter.
        /// </summary>
        public static void NoExceptionTest()
        {
            throw new Exception();
        }

        /// <summary>
        /// To test with only <see cref="CrossCutterN.Base.Metadata.IExecution"/> parameter.
        /// </summary>
        public static void OnlyExecutionTest()
        {
            throw new Exception();
        }
    }
}
