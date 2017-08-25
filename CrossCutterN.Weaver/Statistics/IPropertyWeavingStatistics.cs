// <copyright file="IPropertyWeavingStatistics.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Statistics
{
    using System.Collections.Generic;

    /// <summary>
    /// Property weaving statistics.
    /// </summary>
    public interface IPropertyWeavingStatistics
    {
        /// <summary>
        /// Gets name of the property.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets full name of the property.
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Gets count of getter join points inserted.
        /// </summary>
        int GetterJoinPointCount { get; }

        /// <summary>
        /// Gets count of setter join points inserted.
        /// </summary>
        int SetterJoinPointCount { get; }

        /// <summary>
        /// Gets count of join points injected.
        /// </summary>
        int JoinPointCount { get; }

        /// <summary>
        /// Gets detailed getter weaving records.
        /// </summary>
        IReadOnlyCollection<IWeavingRecord> GetterRecords { get; }

        /// <summary>
        /// Gets detailed setter weaving records.
        /// </summary>
        IReadOnlyCollection<IWeavingRecord> SetterRecords { get; }
    }
}
