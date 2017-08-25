// <copyright file="ModuleWeavingStatistics.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Module weaving statistics implementation.
    /// </summary>
    internal sealed class ModuleWeavingStatistics : IModuleWeavingStatisticsBuilder, IModuleWeavingStatistics
    {
        private readonly List<IClassWeavingStatistics> statistics = new List<IClassWeavingStatistics>();
        private readonly List<string> assemblyReferences = new List<string>();
        private readonly IrreversibleOperation readOnly = new IrreversibleOperation();

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleWeavingStatistics"/> class.
        /// </summary>
        /// <param name="name">Name of the module</param>
        public ModuleWeavingStatistics(string name)
        {
#if DEBUG
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name");
            }
#endif
            Name = name;
        }

        /// <inheritdoc/>
        public string Name { get; private set; }

        /// <inheritdoc/>
        public IReadOnlyCollection<IClassWeavingStatistics> ClassWeavingStatistics
        {
            get
            {
                readOnly.Assert(true);
                return statistics.AsReadOnly();
            }
        }

        /// <inheritdoc/>
        public IReadOnlyCollection<string> AddedAssemblyReferences
        {
            get
            {
                readOnly.Assert(true);
                return assemblyReferences.AsReadOnly();
            }
        }

        /// <inheritdoc/>
        public int MethodJoinPointCount
        {
            get
            {
                readOnly.Assert(true);
                return statistics.Sum(methodStatistic => methodStatistic.MethodJoinPointCount);
            }
        }

        /// <inheritdoc/>
        public int PropertyGetterJoinPointCount
        {
            get
            {
                readOnly.Assert(true);
                return statistics.Sum(propertyGetterStatistics => propertyGetterStatistics.PropertyGetterJoinPointCount);
            }
        }

        /// <inheritdoc/>
        public int PropertyJoinPointCount
        {
            get
            {
                readOnly.Assert(true);
                return statistics.Sum(propertyStatistics => propertyStatistics.PropertyJoinPointCount);
            }
        }

        /// <inheritdoc/>
        public int PropertySetterJoinPointCount
        {
            get
            {
                readOnly.Assert(true);
                return statistics.Sum(propertySetterStatistics => propertySetterStatistics.PropertySetterJoinPointCount);
            }
        }

        /// <inheritdoc/>
        public int WeavedClassCount
        {
            get
            {
                readOnly.Assert(true);
                return statistics.Count;
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
        public int AddedAssemblyReferenceCount
        {
            get
            {
                readOnly.Assert(true);
                return assemblyReferences.Count();
            }
        }

        /// <inheritdoc/>
        public void AddClassWeavingStatistics(IClassWeavingStatistics statistics)
        {
#if DEBUG
            if (statistics == null)
            {
                throw new ArgumentNullException("statistics");
            }
#endif
            readOnly.Assert(false);
            this.statistics.Add(statistics);
        }

        /// <inheritdoc/>
        public void AddAssemblyReference(string assembly)
        {
#if DEBUG
            if (string.IsNullOrWhiteSpace(assembly))
            {
                throw new ArgumentNullException("assembly");
            }
#endif
            readOnly.Assert(false);
            assemblyReferences.Add(assembly);
        }

        /// <inheritdoc/>
        public IModuleWeavingStatistics Build()
        {
            readOnly.Apply();
            return this;
        }
    }
}
