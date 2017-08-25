// <copyright file="OptionsTestTargetDerived.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.OptionsTest
{
    using System;

    /// <summary>
    /// Derived target class for options test.
    /// </summary>
    internal sealed class OptionsTestTargetDerived : OptionsTestTargetBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsTestTargetDerived"/> class.
        /// </summary>
        public OptionsTestTargetDerived()
        {
            StaticInt = 100;
        }

        /// <summary>
        /// Gets or sets some public static integer.
        /// </summary>
        public static int StaticInt { get; set; }

        /// <summary>
        /// Some public method.
        /// </summary>
        /// <param name="i">Some integer.</param>
        /// <returns>Input integer squared.</returns>
        public int Square(int i)
        {
            return i * i;
        }

        /// <summary>
        /// Some overridden method.
        /// </summary>
        /// <param name="i">Some integer.</param>
        public override void TestAbstract(int i)
        {
            var str = ProtectedReturnString();
            var x = i + StaticInt;
            Console.Out.WriteLine(str + Square(x));
        }
    }
}
