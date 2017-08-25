// <copyright file="IAssemblyWeavingStatistics.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Statistics
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Assembly weaving statistics.
    /// </summary>
    public interface IAssemblyWeavingStatistics
    {
        /// <summary>
        /// Gets name of the assembly.
        /// </summary>
        string AssemblyName { get; }

        /// <summary>
        /// Gets count of weaved modules.
        /// </summary>
        int WeavedModuleCount { get; }

        /// <summary>
        /// Gets count of weaved classes.
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
        /// Gets sum of count of weaved methods and properties.
        /// </summary>
        int WeavedMethodPropertyCount { get; }

        /// <summary>
        /// Gets count of join points injected into methods.
        /// </summary>
        int MethodJoinPointCount { get; }

        /// <summary>
        /// Gets count of join points injected to property getters.
        /// </summary>
        int PropertyGetterJoinPointCount { get; }

        /// <summary>
        /// Gets count of join points injected into property setters.
        /// </summary>
        int PropertySetterJoinPointCount { get; }

        /// <summary>
        /// Gets count of join points injected to properties.
        /// </summary>
        int PropertyJoinPointCount { get; }

        /// <summary>
        /// Gets count of injected aspect switches.
        /// </summary>
        int WeavedSwitchCount { get; }

        /// <summary>
        /// Gets exception happened during weaving.
        /// </summary>
        Exception Exception { get; }

        /// <summary>
        /// Gets weaving statistics for modules inside the assembly.
        /// </summary>
        IReadOnlyCollection<IModuleWeavingStatistics> ModuleWeavingStatistics { get; }
    }
}
