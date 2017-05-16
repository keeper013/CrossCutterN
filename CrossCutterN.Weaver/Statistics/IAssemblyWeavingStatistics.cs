/**
* Description: Weaving statistics
* Author: David Cui
*/

namespace CrossCutterN.Weaver.Statistics
{
    using System;
    using System.Collections.Generic;

    public interface IAssemblyWeavingStatistics
    {
        string AssemblyName { get; }
        int WeavedModuleCount { get; }
        int WeavedClassCount { get; }
        int WeavedMethodCount { get; }
        int WeavedPropertyCount { get; }
        int WeavedMethodPropertyCount { get; }
        int MethodJoinPointCount { get; }
        int PropertyGetterJoinPointCount { get; }
        int PropertySetterJoinPointCount { get; }
        int PropertyJoinPointCount { get; }
        Exception Exception { get; }
        IReadOnlyCollection<IModuleWeavingStatistics> ModuleWeavingStatistics { get; }
    }
}
