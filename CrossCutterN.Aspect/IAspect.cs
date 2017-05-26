/**
* Description: injection advice from a single rule
* Author: David Cui
*/
namespace CrossCutterN.Aspect
{
    using System.Collections.Generic;
    using System.Reflection;

    public interface IAspect
    {
        IReadOnlyDictionary<JoinPoint, MethodInfo> PointCut { get; }
        SwitchStatus SwitchStatus { get; }
    }
}
