// <copyright file="MethodWeavingStatistics.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Statistics
{
    using System;
    using System.Collections.Generic;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Method weaving statistics implementation.
    /// </summary>
    internal sealed class MethodWeavingStatistics : ICanAddMethodWeavingRecord<IMethodWeavingStatistics>, IMethodWeavingStatistics
    {
        private readonly List<IWeavingRecord> records = new List<IWeavingRecord>();
        private readonly IrreversibleOperation readOnly = new IrreversibleOperation();

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodWeavingStatistics"/> class.
        /// </summary>
        /// <param name="name">Name of the method.</param>
        /// <param name="signature">Signature of the method.</param>
        public MethodWeavingStatistics(string name, string signature)
        {
#if DEBUG
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }

            if (string.IsNullOrWhiteSpace(signature))
            {
                throw new ArgumentNullException("signature");
            }
#endif
            Name = name;
            Signature = signature;
        }

        /// <inheritdoc/>
        public string Name { get; private set; }

        /// <inheritdoc/>
        public string Signature { get; private set; }

        /// <inheritdoc/>
        public int JoinPointCount
        {
            get
            {
                readOnly.Assert(true);
                return records.Count;
            }
        }

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
        public IMethodWeavingStatistics Build()
        {
            readOnly.Apply();
            return this;
        }
    }
}
