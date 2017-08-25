// <copyright file="IModuleWeavingStatisticsBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Statistics
{
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Module weaving statistics to be built up.
    /// </summary>
    internal interface IModuleWeavingStatisticsBuilder : IBuilder<IModuleWeavingStatistics>
    {
        /// <summary>
        /// Adds class weaving statistics.
        /// </summary>
        /// <param name="statistics">The class weaving statistics to be added.</param>
        void AddClassWeavingStatistics(IClassWeavingStatistics statistics);

        /// <summary>
        /// Adds assembly reference.
        /// </summary>
        /// <param name="assembly">Assembly name that is added as a reference.</param>
        void AddAssemblyReference(string assembly);
    }
}
