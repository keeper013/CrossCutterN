// <copyright file="SwitchManyParametersTestTarget.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.SwitchTest
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Test target that has many parameters
    /// </summary>
    internal class SwitchManyParametersTestTarget
    {
        /// <summary>
        /// Target method to be injected.
        /// </summary>
        /// <param name="p1">Test parameter 1.</param>
        /// <param name="p2">Test parameter 2.</param>
        /// <param name="p3">Test parameter 3.</param>
        /// <param name="p4">Test parameter 4.</param>
        /// <param name="p5">Test parameter 5.</param>
        /// <param name="p6">Test parameter 6.</param>
        /// <param name="p7">Test parameter 7.</param>
        /// <param name="p8">Test parameter 8.</param>
        /// <param name="p9">Test parameter 9.</param>
        /// <param name="p10">Test parameter 10.</param>
        /// <param name="p11">Test parameter 11.</param>
        /// <param name="p12">Test parameter 12.</param>
        /// <param name="p13">Test parameter 13.</param>
        /// <param name="p14">Test parameter 14.</param>
        /// <param name="p15">Test parameter 15.</param>
        /// <param name="p16">Test parameter 16.</param>
        /// <param name="p17">Test parameter 17.</param>
        /// <param name="p18">Test parameter 18.</param>
        /// <param name="p19">Test parameter 19.</param>
        /// <param name="p20">Test parameter 20.</param>
        /// <returns>Some meaningless value.</returns>
        internal static StringBuilder Test(int p1, string p2, object p3, StringBuilder p4, ParameterObject p5, ParameterStruct p6, Action p7, Func<object> p8, int[] p9, IReadOnlyList<string> p10, int p11, string p12, object p13, StringBuilder p14, ParameterObject p15, ParameterStruct p16, Action p17, Func<object> p18, int[] p19, IReadOnlyList<string> p20)
        {
            return new StringBuilder("Test");
        }

        /// <summary>
        /// An internal structore as test parameter type.
        /// </summary>
        internal struct ParameterStruct
        {
            /// <summary>
            /// Gets or sets a test property.
            /// </summary>
            public int Property1 { get; set; }

            /// <summary>
            /// Gets or sets a test property.
            /// </summary>
            public StringBuilder Property2 { get; set; }

            /// <summary>
            /// Gets a test property.
            /// </summary>
            public string Property3 { get => Property2?.ToString(); }
        }

        /// <summary>
        /// An internal class to server as parameter type.
        /// </summary>
        internal class ParameterObject
        {
            /// <summary>
            /// Gets or sets a test property.
            /// </summary>
            public int Property1 { get; set; }

            /// <summary>
            /// Gets or sets a test property.
            /// </summary>
            public StringBuilder Property2 { get; set; }

            /// <summary>
            /// Gets a test property.
            /// </summary>
            public string Property3 { get => Property2?.ToString(); }
        }
    }
}
