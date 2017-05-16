/**
 * Description: Class weaving statistics
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Statistics
{
    using System.Collections.Generic;

    public interface IClassWeavingStatistics
    {
        string Name { get; }
        string FullName { get; }
        string Namespace { get; }
        int WeavedMethodCount { get; }
        int WeavedPropertyCount { get; }
        int WeavedMethodPropertyCount { get; }
        int MethodJoinPointCount { get; }
        int PropertyGetterJoinPointCount { get; }
        int PropertySetterJoinPointCount { get; }
        int PropertyJoinPointCount { get; }
        IReadOnlyCollection<IMethodWeavingStatistics> MethodWeavingStatistics { get; }
        IReadOnlyCollection<IPropertyWeavingStatistics> PropertyWeavingStatistics { get; }
    }
}
