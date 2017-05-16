/**
* Description: injection rule interface
* Author: David Cui
*/

namespace CrossCutterN.Aspect.Builder
{
    using System.Reflection;
    using System.Collections.Generic;
    using Concern;

    public interface IAspectBuilder
    {
        MethodInfo GetAdvice(JoinPoint joinPoint);

        /// <summary>
        /// All join points this aspect build may add advice to
        /// </summary>
        IReadOnlyCollection<JoinPoint> PointCut { get; }

        IAspect GetAspect(IMethod method);
        IPropertyAspect GetAspect(IProperty property);
    }
}
