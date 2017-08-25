// <copyright file="IModuleWeavingStatistics.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Statistics
{
    using System.Collections.Generic;

    /// <summary>
    /// Module weaving statistics.
    /// </summary>
    public interface IModuleWeavingStatistics
    {
        /// <summary>
        /// Gets name of the module.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets count of weaved classes
        /// </summary>
        int WeavedClassCount { get; }

        /// <summary>
        /// Gets count of weaved methods.
        /// </summary>
        int WeavedMethodCount { get; }

        /// <summary>
        /// Gets count of weaved properties.
        /// </summary>
        int WeavedPropertyCount { get; }

        /// <summary>
        /// Gets count of weaved methods and properties.
        /// </summary>
        int WeavedMethodPropertyCount { get; }

        /// <summary>
        /// Gets count of injected switches.
        /// </summary>
        int WeavedSwitchCount { get; }

        /// <summary>
        /// Gets count of injected join points to methods.
        /// </summary>
        int MethodJoinPointCount { get; }

        /// <summary>
        /// Gets count of injected join points to property getters.
        /// </summary>
        int PropertyGetterJoinPointCount { get; }

        /// <summary>
        /// Gets count of injected join points to property setters.
        /// </summary>
        int PropertySetterJoinPointCount { get; }

        /// <summary>
        /// Gets count of injected join points to properties.
        /// </summary>
        int PropertyJoinPointCount { get; }

        /// <summary>
        /// Gets count of added assembly references.
        /// </summary>
        int AddedAssemblyReferenceCount { get; }

        /// <summary>
        /// Gets statistics of weaved classes inside the module
        /// </summary>
        IReadOnlyCollection<IClassWeavingStatistics> ClassWeavingStatistics { get; }

        /// <summary>
        /// Gets statistics of added assembly references.
        /// </summary>
        IReadOnlyCollection<string> AddedAssemblyReferences { get; }
    }
}
