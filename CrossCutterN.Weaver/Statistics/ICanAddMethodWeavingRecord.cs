/**
* Description: Write only weaving record interface
* Author: David Cui
*/

namespace CrossCutterN.Weaver.Statistics
{
    using Advice.Common;

    internal interface ICanAddMethodWeavingRecord
    {
        void AddWeavingRecord(IWeavingRecord record);
    }

    internal interface ICanAddMethodWeavingRecord<out T> : ICanAddMethodWeavingRecord, ICanConvert<T> where T : class
    {
    }
}
