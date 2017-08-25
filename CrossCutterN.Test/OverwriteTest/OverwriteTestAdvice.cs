// <copyright file="OverwriteTestAdvice.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.OverwriteTest
{
    using Utilities;

    /// <summary>
    /// Advices for overwrite test.
    /// </summary>
    internal static class OverwriteTestAdvice
    {
        /// <summary>
        /// Advice to be injected at entry join point.
        /// </summary>
        public static void Entry()
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Entry", null, null, null, null, null));
        }

        /// <summary>
        /// Advice to be injected at exception join point.
        /// </summary>
        public static void Exception()
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exception", null, null, null, null, null));
        }

        /// <summary>
        /// Advice to be injected at exit join point.
        /// </summary>
        public static void Exit()
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exit", null, null, null, null, null));
        }
    }
}