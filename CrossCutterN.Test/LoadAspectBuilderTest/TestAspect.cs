// <copyright file="TestAspect.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.LoadAspectBuilderTest
{
    using CrossCutterN.Aspect.Aspect;
    using CrossCutterN.Aspect.Metadata;

    /// <summary>
    /// Test aspect
    /// </summary>
    internal sealed class TestAspect : SwitchableAspectWithDefaultOptions
    {
        /// <inheritdoc/>
        public override bool CanApplyTo(IMethod method) => method.ClassFullName.Equals(typeof(LoadAspectBuilderTestTarget).FullName) && method.MethodName.Equals("MethodToBeConcerned");

        /// <inheritdoc/>
        public override PropertyConcern CanApplyTo(IProperty property) => PropertyConcern.None;
    }
}
