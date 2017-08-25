// <copyright file="MixedTestTarget.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.MixedTest
{
    using System;

    /// <summary>
    /// Target for mixed aspect test.
    /// </summary>
    [MixedConcernClass3(ConcernPublic = true, ConcernStatic = true, ConcernMethod = true)]
    internal sealed class MixedTestTarget
    {
        /// <summary>
        /// Target method to be weaved according to attribute and name expression.
        /// </summary>
        [MixedConcernMethod1]
        [MixedConcernMethod2]
        public static void ConcernedByAttributeAndClassName()
        {
            Console.Out.WriteLine("ConcernedByAttributeAndClassName called");
        }
    }
}
