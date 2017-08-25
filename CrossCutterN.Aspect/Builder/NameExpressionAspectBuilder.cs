// <copyright file="NameExpressionAspectBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Builder
{
    using System.Collections.Generic;
    using CrossCutterN.Aspect.Aspect;

    /// <summary>
    /// Definition for aspect that identifies target methods and properties by name.
    /// </summary>
    public sealed class NameExpressionAspectBuilder : AspectBuilder
    {
        /// <summary>
        /// Gets or sets included patterns.
        /// </summary>
        public List<string> Includes { get; set; }

        /// <summary>
        /// Gets or sets excluded patterns.
        /// </summary>
        public List<string> Excludes { get; set; }

        /// <inheritdoc/>
        public override IAspect Build(IAdviceUtility utility, string defaultAdviceAssemblyKey)
        {
            var aspect = AspectFactory.InitializeNameExpressionAspect(Includes, Excludes);
            return Build(aspect, utility, defaultAdviceAssemblyKey);
        }
    }
}
