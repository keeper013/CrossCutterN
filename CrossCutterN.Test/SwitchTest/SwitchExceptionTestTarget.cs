// <copyright file="SwitchExceptionTestTarget.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.SwitchTest
{
    using System;

    /// <summary>
    /// Switch exception test target.
    /// </summary>
    internal sealed class SwitchExceptionTestTarget
    {
        /// <summary>
        /// Some test target method.
        /// </summary>
        /// <param name="x">Some integer.</param>
        /// <returns>No return, an exception will be thrown.</returns>
        public static int Test1(int x)
        {
            if (x == 2)
            {
                throw new Exception();
            }

            return x * x;
        }
    }
}
