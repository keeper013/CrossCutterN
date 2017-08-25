// <copyright file="IClassWeavingStatisticsBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Statistics
{
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Class weaving statistics to be built up.
    /// </summary>
    internal interface IClassWeavingStatisticsBuilder : IBuilder<IClassWeavingStatistics>
    {
        /// <summary>
        /// Adds method weaving statistics.
        /// </summary>
        /// <param name="statistics">The method weaving statistics to be added.</param>
        void AddMethodWeavingStatistics(IMethodWeavingStatistics statistics);

        /// <summary>
        /// Adds property weaving statistics.
        /// </summary>
        /// <param name="statistics">The property weaving statistics to be added.</param>
        void AddPropertyWeavingStatistics(IPropertyWeavingStatistics statistics);

        /// <summary>
        /// Adds switch weaving record.
        /// </summary>
        /// <param name="record">The switch weaving record to be added.</param>
        void AddSwitchWeavingRecord(ISwitchWeavingRecord record);
    }
}
