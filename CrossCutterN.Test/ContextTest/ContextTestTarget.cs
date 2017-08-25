// <copyright file="ContextTestTarget.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.ContextTest
{
    using System;

    /// <summary>
    /// Advices for <see cref="CrossCutterN.Base.Metadata.IExecutionContext"/> parameter test target.
    /// </summary>
    internal sealed class ContextTestTarget
    {
        /// <summary>
        /// Test 1.
        /// </summary>
        /// <param name="x">Some integer</param>
        /// <returns>Input integer squared.</returns>
        [ContextConcernMethod]
        public static int Test1(int x)
        {
            return x * x;
        }

        /// <summary>
        /// Test 2.
        /// </summary>
        /// <param name="x">Some integer.</param>
        /// <returns>Supposed to just thrown an exception, no return.</returns>
        [ContextConcernMethod]
        public static int Test2(int x)
        {
            throw new Exception();
        }

        /// <summary>
        /// Test 3.
        /// </summary>
        /// <param name="x">Some integer.</param>
        /// <returns>Input integer squared.</returns>
        [ContextEntryExceptionConcernMethod]
        public static int Test3(int x)
        {
            return x * x;
        }

        /// <summary>
        /// Test 4.
        /// </summary>
        /// <param name="x">Some integer.</param>
        /// <returns>Supposed to just thrown an exception, no return.</returns>
        [ContextEntryExceptionConcernMethod]
        public static int Test4(int x)
        {
            throw new Exception();
        }

        /// <summary>
        /// Test 5.
        /// </summary>
        /// <param name="x">Some integer.</param>
        /// <returns>Input integer squared.</returns>
        [ContextEntryExitConcernMethod]
        public static int Test5(int x)
        {
            return x * x;
        }

        /// <summary>
        /// Test 6.
        /// </summary>
        /// <param name="x">Some integer.</param>
        /// <returns>Supposed to just thrown an exception, no return.</returns>
        [ContextEntryExitConcernMethod]
        public static int Test6(int x)
        {
            throw new Exception();
        }

        /// <summary>
        /// Test 7.
        /// </summary>
        /// <param name="x">Some integer.</param>
        /// <returns>Input integer squared.</returns>
        [ContextExceptionExitConcernMethod]
        public static int Test7(int x)
        {
            return x * x;
        }

        /// <summary>
        /// Test 8.
        /// </summary>
        /// <param name="x">Some integer.</param>
        /// <returns>Supposed to just thrown an exception, no return.</returns>
        [ContextExceptionExitConcernMethod]
        public static int Test8(int x)
        {
            throw new Exception();
        }
    }
}
