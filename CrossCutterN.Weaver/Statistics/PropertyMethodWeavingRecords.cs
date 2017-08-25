// <copyright file="PropertyMethodWeavingRecords.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Statistics
{
    using System;
    using System.Collections.Generic;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Property or method weaving records implementation.
    /// </summary>
    internal sealed class PropertyMethodWeavingRecords : ICanAddMethodWeavingRecord<IPropertyMethodWeavingRecords>, IPropertyMethodWeavingRecords
    {
        private readonly List<IWeavingRecord> records = new List<IWeavingRecord>();
        private readonly IrreversibleOperation readOnly = new IrreversibleOperation();

        /// <inheritdoc/>
        public IReadOnlyCollection<IWeavingRecord> Records
        {
            get
            {
                readOnly.Assert(true);
                return records.AsReadOnly();
            }
        }

        /// <inheritdoc/>
        public void AddWeavingRecord(IWeavingRecord record)
        {
#if DEBUG
            if (record == null)
            {
                throw new ArgumentNullException("record");
            }
#endif
            readOnly.Assert(false);
            records.Add(record);
        }

        /// <inheritdoc/>
        public IPropertyMethodWeavingRecords Build()
        {
            readOnly.Apply();
            return this;
        }
    }
}
