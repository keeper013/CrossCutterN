/**
 * Description: Property getter/setter weaving records
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Statistics
{
    using System.Collections.Generic;

    internal interface IPropertyMethodWeavingRecords
    {
        IReadOnlyCollection<IWeavingRecord> Records { get; }
    }
}
