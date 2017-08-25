// <copyright file="IWeavingPlanner.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Weaver
{
    using CrossCutterN.Aspect.Metadata;

    /// <summary>
    /// Planner to make weaving plans.
    /// </summary>
    internal interface IWeavingPlanner
    {
        /// <summary>
        /// Makes a weaving plan for a method.
        /// </summary>
        /// <param name="method">The method to make weaving plan for.</param>
        /// <returns>The weaving plan made.</returns>
        IWeavingPlan MakePlan(IMethod method);

        /// <summary>
        /// Makes a weaving plan for a property.
        /// </summary>
        /// <param name="property">The property to make weaving plan for.</param>
        /// <returns>The weaving plan made.</returns>
        IPropertyWeavingPlan MakePlan(IProperty property);
    }
}
