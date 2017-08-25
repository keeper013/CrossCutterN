// <copyright file="ContextConcernMethodAttribute.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.ContextTest
{
    using CrossCutterN.Base.Concern;

    /// <summary>
    /// Concern method attribute for context test that inject into entry, exception and exit join points.
    /// </summary>
    internal sealed class ContextConcernMethodAttribute : ConcernMethodAttribute
    {
    }
}
