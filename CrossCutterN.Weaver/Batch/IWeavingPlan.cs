/**
* Description: weaving plan interface
* Author: David Cui
*/

namespace CrossCutterN.Weaver.Batch
{
    using Aspect;
    using System.Collections.Generic;

    internal interface IWeavingPlan
    {
        IReadOnlyCollection<JoinPoint> PointCut { get; }
        IReadOnlyCollection<IAdviceInfo> GetAdvices(JoinPoint joinPoint);
        AdviceParameterFlag ParameterFlag { get; }
    }
}
