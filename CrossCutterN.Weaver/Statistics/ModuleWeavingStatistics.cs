/**
 * Description: module weaving statistics
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Advice.Common;

    internal sealed class ModuleWeavingStatistics : ICanAddClassWeavingStatistics, IModuleWeavingStatistics
    {
        private readonly List<IClassWeavingStatistics> _statistics = new List<IClassWeavingStatistics>();
        private readonly IrreversibleOperation _readOnly = new IrreversibleOperation();

        public string Name { get; private set; }

        public IReadOnlyCollection<IClassWeavingStatistics> ClassWeavingStatistics
        {
            get
            {
                _readOnly.Assert(true);
                return _statistics.AsReadOnly();
            }
        }

        public int MethodJoinPointCount
        {
            get
            {
                _readOnly.Assert(true);
                return _statistics.Sum(statistic => statistic.MethodJoinPointCount);
            }
        }

        public int PropertyGetterJoinPointCount
        {
            get
            {
                _readOnly.Assert(true);
                return _statistics.Sum(statistics => statistics.PropertyGetterJoinPointCount);
            }
        }

        public int PropertyJoinPointCount
        {
            get
            {
                _readOnly.Assert(true);
                return _statistics.Sum(statistics => statistics.PropertyJoinPointCount);
            }
        }

        public int PropertySetterJoinPointCount
        {
            get
            {
                _readOnly.Assert(true);
                return _statistics.Sum(statistics => statistics.PropertySetterJoinPointCount);
            }
        }

        public int WeavedClassCount
        {
            get
            {
                _readOnly.Assert(true);
                return _statistics.Count;
            }
        }

        public int WeavedMethodPropertyCount
        {
            get
            {
                _readOnly.Assert(true);
                return _statistics.Sum(statistics => statistics.WeavedMethodPropertyCount);
            }
        }

        public int WeavedMethodCount
        {
            get
            {
                _readOnly.Assert(true);
                return _statistics.Sum(statistics => statistics.WeavedMethodCount);
            }
        }

        public int WeavedPropertyCount
        {
            get
            {
                _readOnly.Assert(true);
                return _statistics.Sum(statistics => statistics.WeavedPropertyCount);
            }
        }

        public int WeavedSwitchCount
        {
            get
            {
                _readOnly.Assert(true);
                return _statistics.Sum(statistics => statistics.WeavedSwitchCount);
            }
        }

        public ModuleWeavingStatistics(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name");
            }
            Name = name;
        }

        public void AddClassWeavingStatistics(IClassWeavingStatistics statistics)
        {
            if(statistics == null)
            {
                throw new ArgumentNullException("statistics");
            }
            _readOnly.Assert(false);
            _statistics.Add(statistics);
        }

        public IModuleWeavingStatistics Convert()
        {
            _readOnly.Apply();
            return this;
        }
    }
}
