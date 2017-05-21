/**
 * Description: write only module weaving statistics
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Statistics
{
    using Advice.Common;

    internal interface ICanAddClassWeavingStatistics : ICanConvert<IModuleWeavingStatistics>
    {
        void AddClassWeavingStatistics(IClassWeavingStatistics statistics);
    }
}
