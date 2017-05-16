/**
* Description: Weaving batch interface
* Author: David Cui
*/

namespace CrossCutterN.Weaver.Batch
{
    using Aspect.Concern;

    internal interface IWeavingBatch
    {
        IWeavingPlan BuildPlan(IMethod method);
        IPropertyWeavingPlan BuildPlan(IProperty property);
    }
}
