/**
 * Description: Method weaving statistics
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Statistics
{
    using System.Collections.Generic;

    public interface IMethodWeavingStatistics
    {
        string Name { get; }
        string Signature { get; }
        int JoinPointCount { get; }
        IReadOnlyCollection<IWeavingRecord> Records { get; }
    }
}
