// <copyright file="SwitchMethodAspectTestTarget.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.SwitchTest
{
    /// <summary>
    /// Switch method aspect test target.
    /// </summary>
    internal sealed class SwitchMethodAspectTestTarget
    {
        /// <summary>
        /// Gets or sets some test property.
        /// </summary>
        public static int Value { get; set; }

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
        /// <param name="x">Some integer</param>
        /// <returns>Input integer squared.</returns>
        public static int Test2(int x)
        {
            return x * x;
        }
    }
}
