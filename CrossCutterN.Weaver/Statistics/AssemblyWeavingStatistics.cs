// <copyright file="AssemblyWeavingStatistics.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Assembly weaving statistics.
    /// </summary>
    internal sealed class AssemblyWeavingStatistics : IAssemblyWeavingStatistics, IAssemblyWeavingStatisticsBuilder
    {
        private readonly List<IModuleWeavingStatistics> statistics = new List<IModuleWeavingStatistics>();
        private readonly IrreversibleOperation readOnly = new IrreversibleOperation();
        private Exception exception;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyWeavingStatistics"/> class.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        public AssemblyWeavingStatistics(string assemblyName)
        {
#if DEBUG
            if (string.IsNullOrWhiteSpace(assemblyName))
            {
                throw new ArgumentNullException("assemblyName");
            }
#endif
            AssemblyName = assemblyName;
        }

        /// <inheritdoc/>
        public string AssemblyName { get; private set; }

        /// <summary>
        /// Gets or sets exceptions happened during weaving.
        /// </summary>
        public Exception Exception
        {
            get
            {
                readOnly.Assert(true);
                return exception;
            }

            set
            {
                readOnly.Assert(false);
                exception = value;
            }
        }

        /// <inheritdoc/>
        public IReadOnlyCollection<IModuleWeavingStatistics> ModuleWeavingStatistics
        {
            get
            {
                readOnly.Assert(true);
                return statistics.AsReadOnly();
            }
        }

        /// <inheritdoc/>
        public int MethodJoinPointCount
        {
            get
            {
                readOnly.Assert(true);
                return statistics.Sum(moduleStatistics => moduleStatistics.MethodJoinPointCount);
            }
        }

        /// <inheritdoc/>
        public int PropertyGetterJoinPointCount
        {
            get
            {
                readOnly.Assert(true);
                return statistics.Sum(statistics => statistics.PropertyGetterJoinPointCount);
            }
        }

        /// <inheritdoc/>
        public int PropertyJoinPointCount
        {
            get
            {
                readOnly.Assert(true);
                return statistics.Sum(statistics => statistics.PropertyJoinPointCount);
            }
        }

        /// <inheritdoc/>
        public int PropertySetterJoinPointCount
        {
            get
            {
                readOnly.Assert(true);
                return statistics.Sum(statistics => statistics.PropertySetterJoinPointCount);
            }
        }

        /// <inheritdoc/>
        public int WeavedClassCount
        {
            get
            {
                readOnly.Assert(true);
                return statistics.Sum(statistics => statistics.WeavedClassCount);
            }
        }

        /// <inheritdoc/>
        public int WeavedMethodPropertyCount
        {
            get
            {
                readOnly.Assert(true);
                return statistics.Sum(statistics => statistics.WeavedMethodPropertyCount);
            }
        }

        /// <inheritdoc/>
        public int WeavedMethodCount
        {
            get
            {
                readOnly.Assert(true);
                return statistics.Sum(statistics => statistics.WeavedMethodCount);
            }
        }

        /// <inheritdoc/>
        public int WeavedModuleCount
        {
            get
            {
                readOnly.Assert(true);
                return statistics.Count;
            }
        }

        /// <inheritdoc/>
        public int WeavedPropertyCount
        {
            get
            {
                readOnly.Assert(true);
                return statistics.Sum(statistics => statistics.WeavedPropertyCount);
            }
        }

        /// <inheritdoc/>
        public int WeavedSwitchCount
        {
            get
            {
                readOnly.Assert(true);
                return statistics.Sum(statistics => statistics.WeavedSwitchCount);
            }
        }

        /// <inheritdoc/>
        public void AddModuleWeavingStatistics(IModuleWeavingStatistics statistics)
        {
            readOnly.Assert(false);
            if (statistics == null)
            {
                throw new ArgumentNullException("statistics");
            }

            this.statistics.Add(statistics);
        }

        /// <inheritdoc/>
        public IAssemblyWeavingStatistics Build()
        {
            readOnly.Apply();
            return this;
        }
    }
}
