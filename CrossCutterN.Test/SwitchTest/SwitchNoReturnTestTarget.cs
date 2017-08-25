// <copyright file="SwitchNoReturnTestTarget.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.SwitchTest
{
    /// <summary>
    /// Switchable section test, no <see cref="CrossCutterN.Base.Metadata.IReturn"/> parameter initialization.
    /// </summary>
    internal sealed class SwitchNoReturnTestTarget
    {
        /// <summary>
        /// Some test target method.
        /// </summary>
        /// <param name="x">Some integer.</param>
        /// <returns>Input integer squared.</returns>
        public static int Test(int x)
        {
            return x * x;
        }
    }
}
