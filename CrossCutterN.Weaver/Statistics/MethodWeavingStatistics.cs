/**
* Description: method weaving statistics
* Author: David Cui
*/

namespace CrossCutterN.Weaver.Statistics
{
    using System;
    using System.Collections.Generic;
    using Advice.Common;

    internal sealed class MethodWeavingStatistics : ICanAddMethodWeavingRecord<IMethodWeavingStatistics>, IMethodWeavingStatistics
    {
        private readonly List<IWeavingRecord> _records = new List<IWeavingRecord>();
        private readonly IrreversibleOperation _readOnly = new IrreversibleOperation();

        public string Name { get; private set; }
        public string Signature { get; private set; }

        public int JoinPointCount
        {
            get
            {
                _readOnly.Assert(true);
                return _records.Count;
            }
        }

        public MethodWeavingStatistics(string name, string signature)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }
            if(string.IsNullOrWhiteSpace(signature))
            {
                throw new ArgumentNullException("signature");
            }
            Name = name;
            Signature = signature;
        }

        public IReadOnlyCollection<IWeavingRecord> Records
        {
            get
            {
                _readOnly.Assert(true);
                return _records.AsReadOnly();
            }
        }

        public void AddWeavingRecord(IWeavingRecord record)
        {
            if(record == null)
            {
                throw new ArgumentNullException("record");
            }
            _readOnly.Assert(false);
            _records.Add(record);
        }

        public IMethodWeavingStatistics Convert()
        {
            _readOnly.Apply();
            return this;
        }
    }
}
