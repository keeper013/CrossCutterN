// <copyright file="NameExpressionTestAdvice.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.NameExpressionTest
{
    using Utilities;

    /// <summary>
    /// Advices for name expression test.
    /// </summary>
    internal static class NameExpressionTestAdvice
    {
        /// <summary>
        /// Advice to be injected at entry join point.
        /// </summary>
        public static void Entry()
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Entry", null, null, null, null, null));
        }
    }
}
