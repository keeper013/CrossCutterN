/**
 * Description: Aspect builder with default options interface
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Builder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Advice.Common;
    using Concern;

    public abstract class SwitchableAspectBuilderWithDefaultOptions : IAspectBuilder, IWriteOnlyJoinPointDefaultOptions, ISwitchableAspectBuilder
    {
        private bool _concernConstructor;
        private bool _concernInstance;
        private bool _concernInternal;
        private bool _concernMethod;
        private bool _concernPropertyGetter;
        private bool _concernPropertySetter;
        private bool _concernPrivate;
        private bool _concernProtected;
        private bool _concernPublic;
        private bool _concernStatic;
        private SwitchStatus _switchStatus;
        
        private readonly Dictionary<JoinPoint, MethodInfo> _pointCut = new Dictionary<JoinPoint, MethodInfo>();

        protected readonly IrreversibleOperation ReadOnly = new IrreversibleOperation();

        public bool ConcernConstructor
        {
            protected get
            {
                return _concernConstructor;
            }
            set
            {
                ReadOnly.Assert(false);
                _concernConstructor = value;
            }
        }

        public bool ConcernInstance
        {
            protected get
            {
                return _concernInstance;
            }
            set
            {
                ReadOnly.Assert(false);
                _concernInstance = value;
            }
        }

        public bool ConcernInternal
        {
            protected get
            {
                return _concernInternal;
            }
            set
            {
                ReadOnly.Assert(false);
                _concernInternal = value;
            }
        }

        public bool ConcernMethod
        {
            protected get
            {
                return _concernMethod;
            }
            set
            {
                ReadOnly.Assert(false);
                _concernMethod = value;
            }
        }

        public bool ConcernPropertyGetter
        {
            protected get
            {
                return _concernPropertyGetter;
            }
            set
            {
                ReadOnly.Assert(false);
                _concernPropertyGetter = value;
            }
        }

        public bool ConcernPropertySetter
        {
            protected get
            {
                return _concernPropertySetter;
            }
            set
            {
                ReadOnly.Assert(false);
                _concernPropertySetter = value;
            }
        }

        public bool ConcernPrivate
        {
            protected get
            {
                return _concernPrivate;
            }
            set
            {
                ReadOnly.Assert(false);
                _concernPrivate = value;
            }
        }

        public bool ConcernProtected
        {
            protected get
            {
                return _concernProtected;
            }
            set
            {
                ReadOnly.Assert(false);
                _concernProtected = value;
            }
        }
        public bool ConcernPublic
        {
            protected get
            {
                return _concernPublic;
            }
            set
            {
                ReadOnly.Assert(false);
                _concernPublic = value;
            }
        }

        public bool ConcernStatic
        {
            protected get
            {
                return _concernStatic;
            }
            set
            {
                ReadOnly.Assert(false);
                _concernStatic = value;
            }
        }

        public IReadOnlyCollection<JoinPoint> PointCut
        {
            get
            {
                ReadOnly.Assert(true);
                return _pointCut.Keys.ToList().AsReadOnly();
            }
        }

        public SwitchStatus SwitchStatus
        {
            protected get { return _switchStatus; }
            set
            {
                ReadOnly.Assert(false);
                _switchStatus = value;
            }
        }

        protected SwitchableAspectBuilderWithDefaultOptions()
        {
            // by default: concern instance, static, public, method
            ConcernInstance = true;
            ConcernStatic = true;
            ConcernPublic = true;
            ConcernMethod = true;
            // by default, not concern constructor, internal, private, protected, property getter or setter
            ConcernConstructor = false;
            ConcernInternal = false;
            ConcernPropertyGetter = false;
            ConcernPropertySetter = false;
            ConcernPrivate = false;
            ConcernProtected = false;
        }

        public void SetAdvice(JoinPoint joinPoint, MethodInfo advice)
        {
            if (advice == null)
            {
                throw new ArgumentNullException("advice");
            }
            if (_pointCut.ContainsKey(joinPoint))
            {
                throw new ArgumentException(string.Format("Join point {0} has been set in this aspect builder", joinPoint), "joinPoint");
            }
            ReadOnly.Assert(false);
            _pointCut.Add(joinPoint, advice);
        }

        public IAspectBuilder Convert()
        {
            if (!_pointCut.Any())
            {
                throw new InvalidCastException("Aspect builder doesn't have any advice set yet.");
            }
            ReadOnly.Apply();
            return this;
        }

        public MethodInfo GetAdvice(JoinPoint joinPoint)
        {
            ReadOnly.Assert(true);
            if (!_pointCut.ContainsKey(joinPoint))
            {
                throw new ArgumentException(
                    string.Format("Join point {0} doesn't exists in aspect builder", joinPoint), "joinPoint");
            }
            return _pointCut[joinPoint];
        }

        protected bool ContainsJoinPoint(JoinPoint joinPoint)
        {
            return _pointCut.ContainsKey(joinPoint);
        }

        public abstract IAspect GetAspect(IMethod method);
        public abstract IPropertyAspect GetAspect(IProperty property);
    }
}
