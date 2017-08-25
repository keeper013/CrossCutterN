// <copyright file="SwitchClassAspectTestTarget.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.SwitchTest
{
    /// <summary>
    /// Switch class aspect test target.
    /// </summary>
    internal sealed class SwitchClassAspectTestTarget
    {
        /// <summary>
        /// Some test method.
        /// </summary>
        /// <param name="x">Some integer.</param>
        /// <returns>Input integer squared.</returns>
        public static int Test(int x)
        {
            return x * x;
        }
    }
}
