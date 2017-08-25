// <copyright file="SwitchableAspectWithDefaultOptions.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Aspect
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using CrossCutterN.Aspect.Metadata;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Switchable aspect with default options, supposed to be base class for all aspects.
    /// </summary>
    public abstract class SwitchableAspectWithDefaultOptions : IAspect, ISwitchableAspectWithDefaultOptions
    {
        private readonly Dictionary<JoinPoint, MethodInfo> pointCut = new Dictionary<JoinPoint, MethodInfo>();
        private bool concernConstructor;
        private bool concernInstance;
        private bool concernInternal;
        private bool concernMethod;
        private bool concernPropertyGetter;
        private bool concernPropertySetter;
        private bool concernPrivate;
        private bool concernProtected;
        private bool concernPublic;
        private bool concernStatic;
        private bool? switchStatus;

        /// <summary>
        /// Initializes a new instance of the <see cref="SwitchableAspectWithDefaultOptions"/> class.
        /// </summary>
        protected SwitchableAspectWithDefaultOptions()
        {
            // by default: concern instance, static, public, method
            concernInstance = true;
            concernStatic = true;
            concernPublic = true;
            concernMethod = true;

            // by default, not concern constructor, internal, private, protected, property getter or setter
            concernConstructor = false;
            concernInternal = false;
            concernPropertyGetter = false;
            concernPropertySetter = false;
            concernPrivate = false;
            concernProtected = false;
        }

        /// <inheritdoc/>
        public virtual bool ConcernConstructor
        {
            protected get => concernConstructor;

            set
            {
                ReadOnly.Assert(false);
                concernConstructor = value;
            }
        }

        /// <inheritdoc/>
        public virtual bool ConcernInstance
        {
            protected get => concernInstance;

            set
            {
                ReadOnly.Assert(false);
                concernInstance = value;
            }
        }

        /// <inheritdoc/>
        public virtual bool ConcernInternal
        {
            protected get => concernInternal;

            set
            {
                ReadOnly.Assert(false);
                concernInternal = value;
            }
        }

        /// <inheritdoc/>
        public virtual bool ConcernMethod
        {
            protected get => concernMethod;

            set
            {
                ReadOnly.Assert(false);
                concernMethod = value;
            }
        }

        /// <inheritdoc/>
        public virtual bool ConcernPropertyGetter
        {
            protected get => concernPropertyGetter;

            set
            {
                ReadOnly.Assert(false);
                concernPropertyGetter = value;
            }
        }

        /// <inheritdoc/>
        public virtual bool ConcernPropertySetter
        {
            protected get => concernPropertySetter;

            set
            {
                ReadOnly.Assert(false);
                concernPropertySetter = value;
            }
        }

        /// <inheritdoc/>
        public virtual bool ConcernPrivate
        {
            protected get => concernPrivate;

            set
            {
                ReadOnly.Assert(false);
                concernPrivate = value;
            }
        }

        /// <inheritdoc/>
        public virtual bool ConcernProtected
        {
            protected get => concernProtected;

            set
            {
                ReadOnly.Assert(false);
                concernProtected = value;
            }
        }

        /// <inheritdoc/>
        public virtual bool ConcernPublic
        {
            protected get => concernPublic;

            set
            {
                ReadOnly.Assert(false);
                concernPublic = value;
            }
        }

        /// <inheritdoc/>
        public virtual bool ConcernStatic
        {
            protected get => concernStatic;

            set
            {
                ReadOnly.Assert(false);
                concernStatic = value;
            }
        }

        /// <inheritdoc/>
        public IReadOnlyCollection<JoinPoint> PointCut
        {
            get
            {
                ReadOnly.Assert(true);
                return pointCut.Keys.ToList().AsReadOnly();
            }
        }

        /// <inheritdoc/>
        public bool? IsSwitchedOn
        {
            get => switchStatus;

            set
            {
                ReadOnly.Assert(false);
                switchStatus = value;
            }
        }

        /// <summary>
        /// Gets irreversible operation for readonly convertion.
        /// </summary>
        protected IrreversibleOperation ReadOnly { get; } = new IrreversibleOperation();

        /// <inheritdoc/>
        public void SetAdvice(JoinPoint joinPoint, MethodInfo advice)
        {
            if (advice == null)
            {
                throw new ArgumentNullException("advice");
            }

            if (pointCut.ContainsKey(joinPoint))
            {
                throw new ArgumentException($"Join point {joinPoint} has been set in this aspect", "joinPoint");
            }

            ReadOnly.Assert(false);
            pointCut.Add(joinPoint, advice);
        }

        /// <inheritdoc/>
        public IAspect Build()
        {
            if (!pointCut.Any())
            {
                throw new InvalidCastException("Aspect doesn't have any advice set yet.");
            }

            ReadOnly.Apply();
            return this;
        }

        /// <inheritdoc/>
        public MethodInfo GetAdvice(JoinPoint joinPoint)
        {
            ReadOnly.Assert(true);
            if (!pointCut.ContainsKey(joinPoint))
            {
                throw new ArgumentException($"Join point {joinPoint} doesn't exists in aspect", "joinPoint");
            }

            return pointCut[joinPoint];
        }

        /// <inheritdoc/>
        public abstract bool CanApplyTo(IMethod method);

        /// <inheritdoc/>
        public abstract PropertyConcern CanApplyTo(IProperty property);
    }
}
