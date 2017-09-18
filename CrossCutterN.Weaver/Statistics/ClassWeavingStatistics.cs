// <copyright file="ClassWeavingStatistics.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Weaving statistics of class.
    /// </summary>
    internal sealed class ClassWeavingStatistics : IClassWeavingStatistics, IClassWeavingStatisticsBuilder
    {
        private readonly List<IMethodWeavingStatistics> methodStatistics = new List<IMethodWeavingStatistics>();
        private readonly List<IPropertyWeavingStatistics> propertyStatistics = new List<IPropertyWeavingStatistics>();
        private readonly List<ISwitchWeavingRecord> switchWeavingRecords = new List<ISwitchWeavingRecord>();
        private readonly IrreversibleOperation readOnly = new IrreversibleOperation();

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassWeavingStatistics"/> class.
        /// </summary>
        /// <param name="name">Name of the class.</param>
        /// <param name="fullName">Full name of the class.</param>
        /// <param name="nameSpace">namespace of the class.</param>
        public ClassWeavingStatistics(string name, string fullName, string nameSpace)
        {
#if DEBUG
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }

            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentNullException("fullName");
            }
#endif
            Name = name;
            FullName = fullName;
            Namespace = nameSpace;
        }

        /// <inheritdoc/>
        public string FullName { get; private set; }

        /// <inheritdoc/>
        public string Name { get; private set; }

        /// <inheritdoc/>
        public string Namespace { get; private set; }

        /// <inheritdoc/>
        public IReadOnlyCollection<IMethodWeavingStatistics> MethodWeavingStatistics
        {
            get
            {
                readOnly.Assert(true);
                return methodStatistics.AsReadOnly();
            }
        }

        /// <inheritdoc/>
        public IReadOnlyCollection<IPropertyWeavingStatistics> PropertyWeavingStatistics
        {
            get
            {
                readOnly.Assert(true);
                return propertyStatistics.AsReadOnly();
            }
        }

        /// <inheritdoc/>
        public IReadOnlyCollection<ISwitchWeavingRecord> SwitchWeavingRecords
        {
            get
            {
                readOnly.Assert(true);
                return switchWeavingRecords.AsReadOnly();
            }
        }

        /// <inheritdoc/>
        public int WeavedMethodPropertyCount
        {
            get
            {
                readOnly.Assert(true);
                return WeavedMethodCount + WeavedPropertyCount;
            }
        }

        /// <inheritdoc/>
        public int WeavedMethodCount
        {
            get
            {
                readOnly.Assert(true);
                return methodStatistics.Count;
            }
        }

        /// <inheritdoc/>
        public int WeavedPropertyCount
        {
            get
            {
                readOnly.Assert(true);
                return propertyStatistics.Count;
            }
        }

        /// <inheritdoc/>
        public int MethodJoinPointCount
        {
            get
            {
                readOnly.Assert(true);
                return methodStatistics.Sum(statistics => statistics.JoinPointCount);
            }
        }

        /// <inheritdoc/>
        public int PropertyGetterJoinPointCount
        {
            get
            {
                readOnly.Assert(true);
                return propertyStatistics.Sum(statistics => statistics.GetterJoinPointCount);
            }
        }

        /// <inheritdoc/>
        public int PropertySetterJoinPointCount
        {
            get
            {
                readOnly.Assert(true);
                return propertyStatistics.Sum(statistics => statistics.SetterJoinPointCount);
            }
        }

        /// <inheritdoc/>
        public int PropertyJoinPointCount
        {
            get
            {
                readOnly.Assert(true);
                return propertyStatistics.Sum(statisics => statisics.JoinPointCount);
            }
        }

        /// <inheritdoc/>
        public int WeavedSwitchCount
        {
            get
            {
                readOnly.Assert(true);
                return switchWeavingRecords.Count;
            }
        }

        /// <inheritdoc/>
        public void AddMethodWeavingStatistics(IMethodWeavingStatistics statistics)
        {
            if (statistics == null)
            {
                throw new ArgumentNullException("statistics");
            }

            readOnly.Assert(false);
            methodStatistics.Add(statistics);
        }

        /// <inheritdoc/>
        public void AddPropertyWeavingStatistics(IPropertyWeavingStatistics statistics)
        {
            if (statistics == null)
            {
                throw new ArgumentNullException("statistics");
            }

            readOnly.Assert(false);
            propertyStatistics.Add(statistics);
        }

        /// <inheritdoc/>
        public void AddSwitchWeavingRecord(ISwitchWeavingRecord record)
        {
            if (record == null)
            {
                throw new ArgumentNullException("record");
            }

            readOnly.Assert(false);
            switchWeavingRecords.Add(record);
        }

        /// <inheritdoc/>
        public IClassWeavingStatistics Build()
        {
            readOnly.Apply();
            return this;
        }
    }
}
