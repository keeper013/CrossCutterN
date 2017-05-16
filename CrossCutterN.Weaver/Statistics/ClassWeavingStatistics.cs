/**
* Description: Class weaving statistics implementation
* Author: David Cui
*/

namespace CrossCutterN.Weaver.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Advice.Common;

    internal sealed class ClassWeavingStatistics : IClassWeavingStatistics, IWriteOnlyClassWeavingStatistics
    {
        private readonly List<IMethodWeavingStatistics> _methodStatistics = new List<IMethodWeavingStatistics>();
        private readonly List<IPropertyWeavingStatistics> _propertyStatistics = new List<IPropertyWeavingStatistics>();
        private readonly IrreversibleOperation _readOnly = new IrreversibleOperation();

        public string FullName { get; private set; }
        public string Name { get; private set; }
        public string Namespace { get; private set; }

        public IReadOnlyCollection<IMethodWeavingStatistics> MethodWeavingStatistics
        {
            get
            {
                _readOnly.Assert(true);
                return _methodStatistics.AsReadOnly();
            }
        }

        public IReadOnlyCollection<IPropertyWeavingStatistics> PropertyWeavingStatistics
        {
            get
            {
                _readOnly.Assert(true);
                return _propertyStatistics.AsReadOnly();
            }
        }

        public int WeavedMethodPropertyCount
        {
            get
            {
                _readOnly.Assert(true);
                return WeavedMethodCount + WeavedPropertyCount;
            }
        }

        public int WeavedMethodCount
        {
            get
            {
                _readOnly.Assert(true);
                return _methodStatistics.Count;
            }
        }

        public int WeavedPropertyCount
        {
            get
            {
                _readOnly.Assert(true);
                return _propertyStatistics.Count;
            }
        }

        public int MethodJoinPointCount
        {
            get
            {
                _readOnly.Assert(true);
                return _methodStatistics.Sum(statistics => statistics.JoinPointCount);
            }
        }

        public int PropertyGetterJoinPointCount
        {
            get
            {
                _readOnly.Assert(true);
                return _propertyStatistics.Sum(statistics => statistics.GetterJoinPointCount);
            }
        }

        public int PropertySetterJoinPointCount
        {
            get
            {
                _readOnly.Assert(true);
                return _propertyStatistics.Sum(statistics => statistics.SetterJoinPointCount);
            }
        }

        public int PropertyJoinPointCount
        {
            get
            {
                _readOnly.Assert(true);
                return _propertyStatistics.Sum(statisics => statisics.JoinPointCount);
            }
        }

        public ClassWeavingStatistics(string name, string fullName, string nameSpace)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentNullException("fullName");
            }
            if (string.IsNullOrWhiteSpace(nameSpace))
            {
                throw new ArgumentNullException("nameSpace");
            }
            Name = name;
            FullName = fullName;
            Namespace = nameSpace;
        }

        public void AddMethodWeavingStatistics(IMethodWeavingStatistics statistics)
        {
            if (statistics == null)
            {
                throw new ArgumentNullException("statistics");
            }
            _readOnly.Assert(false);
            _methodStatistics.Add(statistics);
        }

        public void AddPropertyWeavingStatistics(IPropertyWeavingStatistics statistics)
        {
            if (statistics == null)
            {
                throw new ArgumentNullException("statistics");
            }
            _readOnly.Assert(false);
            _propertyStatistics.Add(statistics);
        }

        public IClassWeavingStatistics ToReadOnly()
        {
            _readOnly.Apply();
            return this;
        }
    }
}
