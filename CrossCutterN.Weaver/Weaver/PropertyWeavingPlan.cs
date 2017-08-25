// <copyright file="PropertyWeavingPlan.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Weaver
{
    /// <summary>
    /// Property weaving plan.
    /// </summary>
    internal sealed class PropertyWeavingPlan : IPropertyWeavingPlan
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyWeavingPlan"/> class.
        /// </summary>
        /// <param name="getterPlan">Weaving plan for getter method.</param>
        /// <param name="setterPlan">Weaving plan for setter method.</param>
        public PropertyWeavingPlan(IWeavingPlan getterPlan, IWeavingPlan setterPlan)
        {
            // with possibility to be empty, plan may be empty if the population process isn't monitored
            // and the code looks dirty to monitor weaving plan population
            // so allow plan to be empty, handle empty plan at weaving phase
            // no empty checking here
            GetterPlan = getterPlan;
            SetterPlan = setterPlan;
        }

        /// <inheritdoc/>
        public IWeavingPlan GetterPlan { get; private set; }

        /// <inheritdoc/>
        public IWeavingPlan SetterPlan { get; private set; }
    }
}
