// <copyright file="WeavingPlan.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Weaver
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using CrossCutterN.Aspect.Aspect;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Weaving plan implementation.
    /// </summary>
    internal sealed class WeavingPlan : IWeavingPlan, ICanAddJoinPoint
    {
        private readonly SortedDictionary<JoinPoint, SortedDictionary<int, IAdviceInfo>> plan =
            new SortedDictionary<JoinPoint, SortedDictionary<int, IAdviceInfo>>();

        private readonly IrreversibleOperation readOnly = new IrreversibleOperation();

        /// <summary>
        /// Initializes a new instance of the <see cref="WeavingPlan"/> class.
        /// </summary>
        public WeavingPlan() => ParameterFlag = AdviceParameterFlag.None;

        /// <inheritdoc/>
        public AdviceParameterFlag ParameterFlag { get; private set; }

        /// <inheritdoc/>
        public IReadOnlyCollection<JoinPoint> PointCut
        {
            get
            {
                readOnly.Assert(true);
                return plan.Keys.ToList().AsReadOnly();
            }
        }

        /// <inheritdoc/>
        public void AddJoinPoint(JoinPoint joinPoint, string aspectName, MethodInfo advice, int sequence, AdviceParameterFlag parameterFlag, bool? isSwitchedOn)
        {
#if DEBUG
            if (string.IsNullOrWhiteSpace(aspectName))
            {
                throw new ArgumentNullException("aspectName");
            }

            if (advice == null)
            {
                throw new ArgumentNullException("advice");
            }
#endif
            readOnly.Assert(false);
            if (plan.ContainsKey(joinPoint))
            {
                var sequenceSetting = plan[joinPoint];
                if (sequenceSetting.ContainsKey(sequence))
                {
                    throw new ArgumentException($"Sequence {sequence} for join point {joinPoint} has been used already", "sequence");
                }

                sequenceSetting.Add(sequence, WeaverFactory.InitializeAdviceInfo(advice, aspectName, parameterFlag, isSwitchedOn));
            }
            else
            {
                var sequenceSetting = new SortedDictionary<int, IAdviceInfo> { { sequence, WeaverFactory.InitializeAdviceInfo(advice, aspectName, parameterFlag, isSwitchedOn) } };
                plan.Add(joinPoint, sequenceSetting);
            }

            ParameterFlag |= parameterFlag;
        }

        /// <inheritdoc/>
        public IReadOnlyCollection<IAdviceInfo> GetAdvices(JoinPoint joinPoint)
        {
            readOnly.Assert(true);
            if (!plan.ContainsKey(joinPoint))
            {
                return null;
            }

            return plan[joinPoint].Values.ToList().AsReadOnly();
        }

        /// <inheritdoc/>
        public IWeavingPlan Build()
        {
            // with possibility to be empty, plan may be empty if the population process isn't monitored
            // and the code looks dirty to monitor weaving plan population
            // so allow plan to be empty, handle empty plan at weaving phase
            // no empty checking here
            readOnly.Apply();
            return this;
        }
    }
}
