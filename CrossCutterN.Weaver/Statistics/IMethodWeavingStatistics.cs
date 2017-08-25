// <copyright file="IMethodWeavingStatistics.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Statistics
{
    using System.Collections.Generic;

    /// <summary>
    /// Method weaving statistics.
    /// </summary>
    public interface IMethodWeavingStatistics
    {
        /// <summary>
        /// Gets name of the method.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets signature of the method.
        /// </summary>
        string Signature { get; }

        /// <summary>
        /// Gets number of join points injected in the method.
        /// </summary>
        int JoinPointCount { get; }

        /// <summary>
        /// Gets weaving details for the method.
        /// </summary>
        IReadOnlyCollection<IWeavingRecord> Records { get; }
    }
}
