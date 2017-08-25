// <copyright file="SwitchLookUpTestTarget.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.SwitchTest
{
    /// <summary>
    /// Switch lookup test target.
    /// </summary>
    internal class SwitchLookUpTestTarget
    {
        /// <summary>
        /// Gets or sets some value.
        /// </summary>
        public static int Value1 { get; set; }

        /// <summary>
        /// Some test target method.
        /// </summary>
        /// <param name="x">Some integer.</param>
        /// <returns>Input integer squared.</returns>
        public static int Test1(int x)
        {
            return x * x;
        }
    }
}
