// <copyright file="OptionsTestTargetBase.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.OptionsTest
{
    using System;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// Base target class for options test.
    /// </summary>
    [OptionsConcernClass]
    internal abstract class OptionsTestTargetBase
    {
        private static string str1;
        private readonly StringBuilder stringBuilder = new StringBuilder("OptionsTestTargetBase");

        /// <summary>
        /// Initializes static members of the <see cref="OptionsTestTargetBase"/> class.
        /// Static constructor.
        /// </summary>
        static OptionsTestTargetBase()
        {
            str1 = "x";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsTestTargetBase"/> class.
        /// </summary>
        protected OptionsTestTargetBase()
        {
            InternalInstanceFunc = Square;
        }

        /// <summary>
        /// Gets some public static integer.
        /// </summary>
        public static int PublicStaticInt => 1;

        /// <summary>
        /// Gets or sets some internal instance function.
        /// </summary>
        internal Func<int, int> InternalInstanceFunc { get; set; }

        /// <summary>
        /// Sets some instance string value.
        /// </summary>
        protected string ProtectedInstanceString
        {
            set { str1 = value; }
        }

        /// <summary>
        /// Gets some private string builder.
        /// </summary>
        private StringBuilder StringBuilder => stringBuilder;

        /// <summary>
        /// Some public method returns a string builder.
        /// </summary>
        /// <param name="x">Some integer.</param>
        /// <param name="y">Some string.</param>
        /// <returns>Some string builder.</returns>
        public StringBuilder PublicReturnObj(int x, string y)
        {
            Console.Out.WriteLine("{0} {1}", x, y);
            var strb = StringBuilder;
            strb.Append(x);
            strb.Append(y + 1);
            strb.Append(str1);
            return new StringBuilder(strb.ToString());
        }

        /// <summary>
        /// Some abstract test target method.
        /// </summary>
        /// <param name="i">Some integer.</param>
        public abstract void TestAbstract(int i);

        /// <summary>
        /// internal multiple parameter method.
        /// </summary>
        /// <param name="strb">String string builder.</param>
        /// <param name="x">Some integer.</param>
        /// <param name="y">Some string.</param>
        /// <param name="action">Some action.</param>
        /// <returns>Some function.</returns>
        internal Func<int, int> InternalMultipleParameter(ref StringBuilder strb, int x, string y, Action<int> action)
        {
            strb?.Append(x)?.Append(y);
            action?.Invoke(x);

            return InternalInstanceFunc;
        }

        /// <summary>
        /// Protected return string method.
        /// </summary>
        /// <returns>Some string.</returns>
        protected static string ProtectedReturnString()
        {
            return PublicStaticInt.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Some private method.
        /// </summary>
        /// <param name="i">Some integer.</param>
        /// <returns>Input integer squared.</returns>
        private static int Square(int i)
        {
            return i * i;
        }
    }
}
