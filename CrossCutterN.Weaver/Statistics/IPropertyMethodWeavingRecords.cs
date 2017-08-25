// <copyright file="IPropertyMethodWeavingRecords.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Statistics
{
    using System.Collections.Generic;

    /// <summary>
    /// Property and method detailed weaving record.
    /// </summary>
    internal interface IPropertyMethodWeavingRecords
    {
        /// <summary>
        /// Gets the detailed weaving records.
        /// </summary>
        IReadOnlyCollection<IWeavingRecord> Records { get; }
    }
}
