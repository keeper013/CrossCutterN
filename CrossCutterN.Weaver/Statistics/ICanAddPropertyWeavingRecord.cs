// <copyright file="ICanAddPropertyWeavingRecord.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Statistics
{
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Statistics that contains method weaving record and be built to <see cref="IPropertyWeavingStatistics"/>, implementation should be class weaving statistics.
    /// </summary>
    internal interface ICanAddPropertyWeavingRecord : IBuilder<IPropertyWeavingStatistics>
    {
        /// <summary>
        /// Gets getter weaving statistics container.
        /// </summary>
        ICanAddMethodWeavingRecord<IPropertyMethodWeavingRecords> GetterContainer { get; }

        /// <summary>
        /// Gets setter weaving statistics container.
        /// </summary>
        ICanAddMethodWeavingRecord<IPropertyMethodWeavingRecords> SetterContainer { get; }
    }
}
