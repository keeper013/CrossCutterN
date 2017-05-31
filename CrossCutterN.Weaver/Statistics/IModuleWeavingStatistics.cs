/**
* Description: Module weaving statistics
* Author: David Cui
*/

namespace CrossCutterN.Weaver.Statistics
{
    using System.Collections.Generic;

    public interface IModuleWeavingStatistics
    {
        string Name { get; }
        int WeavedClassCount { get; }
        int WeavedMethodCount { get; }
        int WeavedPropertyCount { get; }
        int WeavedMethodPropertyCount { get; }
        int WeavedSwitchCount { get; }
        int MethodJoinPointCount { get; }
        int PropertyGetterJoinPointCount { get; }
        int PropertySetterJoinPointCount { get; }
        int PropertyJoinPointCount { get; }
        IReadOnlyCollection<IClassWeavingStatistics> ClassWeavingStatistics { get; }
    }
}
