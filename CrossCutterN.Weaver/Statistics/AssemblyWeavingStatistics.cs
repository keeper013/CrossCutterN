/**
* Description: assembly weaving statistics
* Author: David Cui
*/

namespace CrossCutterN.Weaver.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Advice.Common;

    internal sealed class AssemblyWeavingStatistics : IAssemblyWeavingStatistics, IWriteOnlyAssemblyWeavingStatistics
    {
        private readonly List<IModuleWeavingStatistics> _statistics = new List<IModuleWeavingStatistics>();
        private readonly IrreversibleOperation _readOnly = new IrreversibleOperation();
        private Exception _exception;

        public string AssemblyName { get; private set; }
        public Exception Exception
        {
            get
            {
                _readOnly.Assert(true);
                return _exception;
            }
            set
            {
                _readOnly.Assert(false);
                _exception = value;
            }
        }

        public IReadOnlyCollection<IModuleWeavingStatistics> ModuleWeavingStatistics
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
                return _statistics.Sum(statistics => statistics.MethodJoinPointCount);
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
                return _statistics.Sum(statistics => statistics.WeavedClassCount);
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

        public int WeavedModuleCount
        {
            get
            {
                _readOnly.Assert(true);
                return _statistics.Count;
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

        public AssemblyWeavingStatistics(string assemblyName)
        {
            if(string.IsNullOrWhiteSpace(assemblyName))
            {
                throw new ArgumentNullException("assemblyName");
            }
            AssemblyName = assemblyName;
        }

        public void AddModuleWeavingStatistics(IModuleWeavingStatistics statistics)
        {
            _readOnly.Assert(false);
            if (statistics == null)
            {
                throw new ArgumentNullException("statistics");
            }
            _statistics.Add(statistics);
        }

        public IAssemblyWeavingStatistics Convert()
        {
            _readOnly.Apply();
            return this;
        }
    }
}
