// <copyright file="SwitchMethodTestTarget.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.SwitchTest
{
    /// <summary>
    /// Switch method test target.
    /// </summary>
    internal sealed class SwitchMethodTestTarget
    {
        /// <summary>
        /// Some test method.
        /// </summary>
        /// <param name="x">Some integer.</param>
        /// <returns>Input integer squared.</returns>
        public static int Test1(int x)
        {
            return x * x;
        }

        /// <summary>
        /// Some test method.
        /// </summary>
        /// <param name="x">Some integer.</param>
        /// <returns>Input integer squared.</returns>
        public static int Test2(int x)
        {
            return x * x;
        }
    }
}
