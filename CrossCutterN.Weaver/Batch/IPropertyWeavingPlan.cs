/**
* Description: property weaving plan interface
* Author: David Cui
*/

namespace CrossCutterN.Weaver.Batch
{
    internal interface IPropertyWeavingPlan
    {
        IWeavingPlan GetterPlan { get; }
        IWeavingPlan SetterPlan { get; }
    }
}
