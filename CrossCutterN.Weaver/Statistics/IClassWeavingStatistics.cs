// <copyright file="IClassWeavingStatistics.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Statistics
{
    using System.Collections.Generic;

    /// <summary>
    /// Class weaving statistics.
    /// </summary>
    public interface IClassWeavingStatistics
    {
        /// <summary>
        /// Gets name of the class.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets full name of the class.
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Gets namespace of the class.
        /// </summary>
        string Namespace { get; }

        /// <summary>
        /// Gets count of weaved methods in the class.
        /// </summary>
        int WeavedMethodCount { get; }

        /// <summary>
        /// Gets count of weaved properties in the class.
        /// </summary>
        int WeavedPropertyCount { get; }

        /// <summary>
        /// Gets the sum of count of weaved methods and properties.
        /// </summary>
        int WeavedMethodPropertyCount { get; }

        /// <summary>
        /// Gets count of join points injected to methods.
        /// </summary>
        int MethodJoinPointCount { get; }

        /// <summary>
        /// Gets count of join point injected to property getters.
        /// </summary>
        int PropertyGetterJoinPointCount { get; }

        /// <summary>
        /// Gets count of join point injected to property setters.
        /// </summary>
        int PropertySetterJoinPointCount { get; }

        /// <summary>
        /// Gets count of join point injected to properties.
        /// </summary>
        int PropertyJoinPointCount { get; }

        /// <summary>
        /// Gets count of aspect switches injected.
        /// </summary>
        int WeavedSwitchCount { get; }

        /// <summary>
        /// Gets statistics of weaved methods.
        /// </summary>
        IReadOnlyCollection<IMethodWeavingStatistics> MethodWeavingStatistics { get; }

        /// <summary>
        /// Gets statistics of weaved properties.
        /// </summary>
        IReadOnlyCollection<IPropertyWeavingStatistics> PropertyWeavingStatistics { get; }

        /// <summary>
        /// Gets statistics of switches injected.
        /// </summary>
        IReadOnlyCollection<ISwitchWeavingRecord> SwitchWeavingRecords { get; }
    }
}
