/**
 * Description: Property weaving statistics
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Statistics
{
    using System.Collections.Generic;

    public interface IPropertyWeavingStatistics
    {
        string Name { get; }
        string FullName { get; }
        int GetterJoinPointCount { get; }
        int SetterJoinPointCount { get; }
        int JoinPointCount { get; }
        IReadOnlyCollection<IWeavingRecord> GetterRecords { get; }
        IReadOnlyCollection<IWeavingRecord> SetterRecords { get; }
    }
}
