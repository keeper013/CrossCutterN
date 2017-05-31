/**
* Description: property getter/setter weaving records
* Author: David Cui
*/

namespace CrossCutterN.Weaver.Statistics
{
    using System;
    using System.Collections.Generic;
    using Advice.Common;

    internal class PropertyMethodWeavingRecords : ICanAddMethodWeavingRecord<IPropertyMethodWeavingRecords>, IPropertyMethodWeavingRecords
    {
        private readonly List<IWeavingRecord> _records = new List<IWeavingRecord>();
        private readonly IrreversibleOperation _readOnly = new IrreversibleOperation();

        public void AddWeavingRecord(IWeavingRecord record)
        {
            if (record == null)
            {
                throw new ArgumentNullException("record");
            }
            _readOnly.Assert(false);
            _records.Add(record);
        }

        public IPropertyMethodWeavingRecords Convert()
        {
            _readOnly.Apply();
            return this;
        }

        public IReadOnlyCollection<IWeavingRecord> Records
        {
            get
            {
                _readOnly.Assert(true);
                return _records.AsReadOnly();
            }
        }
    }
}
