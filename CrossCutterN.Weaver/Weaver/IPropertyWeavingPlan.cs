// <copyright file="IPropertyWeavingPlan.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Weaver
{
    /// <summary>
    /// Property weaving plan.
    /// </summary>
    internal interface IPropertyWeavingPlan
    {
        /// <summary>
        /// Gets weaving plan for getter method.
        /// </summary>
        IWeavingPlan GetterPlan { get; }

        /// <summary>
        /// Gets weaving plan for setter method.
        /// </summary>
        IWeavingPlan SetterPlan { get; }
    }
}
