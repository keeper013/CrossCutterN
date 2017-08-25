// <copyright file="AspectBuilderFactory.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Builder
{
    /// <summary>
    /// Aspect builder factory.
    /// </summary>
    public static class AspectBuilderFactory
    {
        /// <summary>
        /// Initializes a new instance of of <see cref="IAspectBuilderUtilityBuilder"/>.
        /// </summary>
        /// <returns>The initialized <see cref="IAspectBuilderUtilityBuilder"/>.</returns>
        public static IAspectBuilderUtilityBuilder InitializeAspectBuilderUtility()
        {
            var utility = new AspectUtility();
            utility.AddAspectBuilderConstructor("CrossCutterN.Aspect", "CrossCutterN.Aspect.Builder.ConcernAttributeAspectBuilder", () => new ConcernAttributeAspectBuilder());
            utility.AddAspectBuilderConstructor("CrossCutterN.Aspect", "CrossCutterN.Aspect.Builder.NameExpressionAspectBuilder", () => new NameExpressionAspectBuilder());
            return utility;
        }

        /// <summary>
        /// Initializes a new instance of of <see cref="IAdviceUtilityBuilder"/>.
        /// </summary>
        /// <returns>The initialized <see cref="IAdviceUtilityBuilder"/>.</returns>
        public static IAdviceUtilityBuilder InitializeAdviceUtility()
        {
            return new AdviceUtility();
        }
    }
}
