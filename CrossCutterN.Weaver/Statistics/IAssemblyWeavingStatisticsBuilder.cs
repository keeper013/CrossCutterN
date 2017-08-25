// <copyright file="IAssemblyWeavingStatisticsBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Statistics
{
    using System;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Assembly weaving statistics builder.
    /// </summary>
    internal interface IAssemblyWeavingStatisticsBuilder : IBuilder<IAssemblyWeavingStatistics>
    {
        /// <summary>
        /// Sets exception happened during weaving.
        /// </summary>
        Exception Exception { set; }

        /// <summary>
        /// Adds module weaving statistics into the assembly.
        /// </summary>
        /// <param name="statistics">Module weaving statistics.</param>
        void AddModuleWeavingStatistics(IModuleWeavingStatistics statistics);
    }
}
