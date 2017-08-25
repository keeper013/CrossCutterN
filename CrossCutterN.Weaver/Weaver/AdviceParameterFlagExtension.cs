// <copyright file="AdviceParameterFlagExtension.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Weaver
{
    /// <summary>
    /// Extention methods to <see cref="AdviceParameterFlag"/>.
    /// </summary>
    internal static class AdviceParameterFlagExtension
    {
        /// <summary>
        /// Checks if the given advice parameter flag contains all contents of the other.
        /// </summary>
        /// <param name="self">The given advice parameter flag.</param>
        /// <param name="flag">The advice parameter flag to check against.</param>
        /// <returns>True if given advice parameter flag does contain all content of the other, false elsewise.</returns>
        public static bool Contains(this AdviceParameterFlag self, AdviceParameterFlag flag) => (self & flag) == flag;
    }
}
