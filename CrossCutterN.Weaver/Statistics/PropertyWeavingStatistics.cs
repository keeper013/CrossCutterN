/**
* Description: property weaving statistics
* Author: David Cui
*/

namespace CrossCutterN.Weaver.Statistics
{
    using System;
    using System.Collections.Generic;

    internal sealed class PropertyWeavingStatistics : IPropertyWeavingStatistics, ICanAddPropertyWeavingRecord
    {
        private ICanAddMethodWeavingRecord<IPropertyMethodWeavingRecords> _getterRecordsWriteOnly = new PropertyMethodWeavingRecords();
        private ICanAddMethodWeavingRecord<IPropertyMethodWeavingRecords> _setterRecordsWriteOnly = new PropertyMethodWeavingRecords();
        private IPropertyMethodWeavingRecords _getterRecords;
        private IPropertyMethodWeavingRecords _setterRecords;

        public string FullName { get; private set; }
        public string Name { get; private set; }

        public int GetterJoinPointCount
        {
            get
            {
                if (_getterRecords == null)
                {
                    throw new InvalidOperationException("Getter records are not ready for counting.");
                }
                return _getterRecords.Records.Count;
            }
        }

        public IReadOnlyCollection<IWeavingRecord> GetterRecords
        {
            get
            {
                if (_getterRecords == null)
                {
                    throw new InvalidOperationException("Getter records are not ready for being retrieved.");
                }
                return _getterRecords.Records;
            }
        }

        public int SetterJoinPointCount
        {
            get
            {
                if (_setterRecords == null)
                {
                    throw new InvalidOperationException("Setter records are not ready for counting.");
                }
                return _setterRecords.Records.Count;
            }
        }

        public IReadOnlyCollection<IWeavingRecord> SetterRecords
        {
            get
            {
                if (_setterRecords == null)
                {
                    throw new InvalidOperationException("Setter records are not ready for being retrieved.");
                }
                return _setterRecords.Records;
            }
        }

        public int JoinPointCount
        {
            get
            {
                return GetterJoinPointCount + SetterJoinPointCount;
            }
        }

        public PropertyWeavingStatistics(string name, string fullName)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentNullException("fullName");
            }
            Name = name;
            FullName = fullName;
        }

        public ICanAddMethodWeavingRecord<IPropertyMethodWeavingRecords> GetterContainer
        {
            get
            {
                if (_getterRecordsWriteOnly == null)
                {
                    throw new InvalidOperationException("Statistics can't accept weaving records any more.");
                }
                return _getterRecordsWriteOnly;
            }
        }

        public ICanAddMethodWeavingRecord<IPropertyMethodWeavingRecords> SetterContainer
        {
            get
            {
                if (_setterRecordsWriteOnly == null)
                {
                    throw new InvalidOperationException("Statistics can't accept weaving records any more.");
                }
                return _setterRecordsWriteOnly;
            }
        }

        public IPropertyWeavingStatistics Convert()
        {
            _getterRecords = _getterRecordsWriteOnly.Convert();
            _getterRecordsWriteOnly = null;
            _setterRecords = _setterRecordsWriteOnly.Convert();
            _setterRecordsWriteOnly = null;
            return this;
        }
    }
}
