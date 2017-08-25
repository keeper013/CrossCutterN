// <copyright file="ICanAddMethodWeavingRecord.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Statistics
{
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Statistics that contains method weaving record, implementation should be class weaving statistics.
    /// </summary>
    internal interface ICanAddMethodWeavingRecord
    {
        /// <summary>
        /// Adds a method weaving record.
        /// </summary>
        /// <param name="record">Method weaving record to be added.</param>
        void AddWeavingRecord(IWeavingRecord record);
    }

    /// <summary>
    /// Statistics that contains method weaving record and can be built to a certain type.
    /// </summary>
    /// <typeparam name="T">The type that this interface can be built to.</typeparam>
    internal interface ICanAddMethodWeavingRecord<out T> : ICanAddMethodWeavingRecord, IBuilder<T>
        where T : class
    {
    }
}
