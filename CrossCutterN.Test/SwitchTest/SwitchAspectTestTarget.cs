// <copyright file="SwitchAspectTestTarget.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.SwitchTest
{
    /// <summary>
    /// Switch aspect test target.
    /// </summary>
    internal sealed class SwitchAspectTestTarget
    {
        /// <summary>
        /// Some test target method.
        /// </summary>
        /// <param name="x">Some integer.</param>
        /// <returns>Input integer squared.</returns>
        public static int Test1(int x)
        {
            return x * x;
        }

        /// <summary>
        /// Some test target method.
        /// </summary>
        /// <param name="x">Some integer.</param>
        /// <returns>Input integer squared.</returns>
        public static int Test2(int x)
        {
            return x * x;
        }
    }
}
