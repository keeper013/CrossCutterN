// <copyright file="TestAspectBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.LoadAspectBuilderTest
{
    using CrossCutterN.Aspect.Aspect;
    using CrossCutterN.Aspect.Builder;

    /// <summary>
    /// Test aspect builder.
    /// </summary>
    public sealed class TestAspectBuilder : AspectBuilder
    {
        /// <inheritdoc/>
        public override IAspect Build(IAdviceUtility utility, string defaultAdviceAssemblyKey)
        {
            var aspect = new TestAspect();
            return Build(aspect, utility, defaultAdviceAssemblyKey);
        }
    }
}
