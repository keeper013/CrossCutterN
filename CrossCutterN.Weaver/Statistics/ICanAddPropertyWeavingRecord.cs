/**
 * Description: write only property weaving record
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Statistics
{
    using Advice.Common;

    internal interface ICanAddPropertyWeavingRecord : ICanConvert<IPropertyWeavingStatistics>
    {
        ICanAddMethodWeavingRecord<IPropertyMethodWeavingRecords> GetterContainer { get; }
        ICanAddMethodWeavingRecord<IPropertyMethodWeavingRecords> SetterContainer { get; }
    }
}
