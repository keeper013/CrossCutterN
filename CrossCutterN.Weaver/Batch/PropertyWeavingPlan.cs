/**
* Description: property weaving plan implementation
* Author: David Cui
*/

namespace CrossCutterN.Weaver.Batch
{
    internal class PropertyWeavingPlan : IPropertyWeavingPlan
    {
        public IWeavingPlan GetterPlan { get; private set; }
        public IWeavingPlan SetterPlan { get; private set; }

        public PropertyWeavingPlan(IWeavingPlan getterPlan, IWeavingPlan setterPlan)
        {
            // with possibility to be empty, plan may be empty if the population process isn't monitored
            // and the code looks dirty to monitor weaving plan population
            // so allow plan to be empty, handle empty plan at weaving phase
            // no empty checking here
            GetterPlan = getterPlan;
            SetterPlan = setterPlan;
        }
    }
}
