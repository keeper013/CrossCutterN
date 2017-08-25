// <copyright file="PropertyWeavingStatistics.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Statistics
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Property weaving statistics implementation.
    /// </summary>
    internal sealed class PropertyWeavingStatistics : IPropertyWeavingStatistics, ICanAddPropertyWeavingRecord
    {
        private ICanAddMethodWeavingRecord<IPropertyMethodWeavingRecords> getterRecordsBuilder = new PropertyMethodWeavingRecords();
        private ICanAddMethodWeavingRecord<IPropertyMethodWeavingRecords> setterRecordsBuilder = new PropertyMethodWeavingRecords();
        private IPropertyMethodWeavingRecords getterRecords;
        private IPropertyMethodWeavingRecords setterRecords;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyWeavingStatistics"/> class.
        /// </summary>
        /// <param name="name">Name of the property.</param>
        /// <param name="fullName">Full name of the property.</param>
        public PropertyWeavingStatistics(string name, string fullName)
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
        }

        /// <inheritdoc/>
        public string FullName { get; private set; }

        /// <inheritdoc/>
        public string Name { get; private set; }

        /// <inheritdoc/>
        public int GetterJoinPointCount
        {
            get
            {
                if (getterRecords == null)
                {
                    throw new InvalidOperationException("Getter records are not ready for counting.");
                }

                return getterRecords.Records.Count;
            }
        }

        /// <inheritdoc/>
        public IReadOnlyCollection<IWeavingRecord> GetterRecords
        {
            get
            {
#if DEBUG
                if (getterRecords == null)
                {
                    throw new InvalidOperationException("Getter records are not ready for being retrieved.");
                }
#endif
                return getterRecords.Records;
            }
        }

        /// <inheritdoc/>
        public int SetterJoinPointCount
        {
            get
            {
#if DEBUG
                if (setterRecords == null)
                {
                    throw new InvalidOperationException("Setter records are not ready for counting.");
                }
#endif
                return setterRecords.Records.Count;
            }
        }

        /// <inheritdoc/>
        public IReadOnlyCollection<IWeavingRecord> SetterRecords
        {
            get
            {
#if DEBUG
                if (setterRecords == null)
                {
                    throw new InvalidOperationException("Setter records are not ready for being retrieved.");
                }
#endif
                return setterRecords.Records;
            }
        }

        /// <inheritdoc/>
        public int JoinPointCount => GetterJoinPointCount + SetterJoinPointCount;

        /// <inheritdoc/>
        public ICanAddMethodWeavingRecord<IPropertyMethodWeavingRecords> GetterContainer
        {
            get
            {
                if (getterRecordsBuilder == null)
                {
                    throw new InvalidOperationException("Statistics can't accept weaving records any more.");
                }

                return getterRecordsBuilder;
            }
        }

        /// <inheritdoc/>
        public ICanAddMethodWeavingRecord<IPropertyMethodWeavingRecords> SetterContainer
        {
            get
            {
                if (setterRecordsBuilder == null)
                {
                    throw new InvalidOperationException("Statistics can't accept weaving records any more.");
                }

                return setterRecordsBuilder;
            }
        }

        /// <inheritdoc/>
        public IPropertyWeavingStatistics Build()
        {
            getterRecords = getterRecordsBuilder.Build();
            getterRecordsBuilder = null;
            setterRecords = setterRecordsBuilder.Build();
            setterRecordsBuilder = null;
            return this;
        }
    }
}
