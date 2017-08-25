// <copyright file="LoadAspectBuilderTestAdvice.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.LoadAspectBuilderTest
{
    using CrossCutterN.Test.Utilities;

    /// <summary>
    /// Load aspect builder test advices.
    /// </summary>
    internal static class LoadAspectBuilderTestAdvice
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
