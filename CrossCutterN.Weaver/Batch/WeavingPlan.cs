/**
* Description: Weaving plan implementation
* Author: David Cui
*/

namespace CrossCutterN.Weaver.Batch
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Advice.Common;
    using Aspect;

    internal sealed class WeavingPlan : IWeavingPlan, ICanAddJoinPoint
    {
        private readonly SortedDictionary<JoinPoint, SortedDictionary<int, IAdviceInfo>> _plan = 
            new SortedDictionary<JoinPoint, SortedDictionary<int, IAdviceInfo>>();
        private readonly IrreversibleOperation _readOnly = new IrreversibleOperation();

        public AdviceParameterFlag ParameterFlag { get; private set; }

        public IReadOnlyCollection<JoinPoint> PointCut
        {
            get
            {
                _readOnly.Assert(true);
                return _plan.Keys.ToList().AsReadOnly();
            }
        }

        public WeavingPlan()
        {
            ParameterFlag = AdviceParameterFlag.None;
        }

        public void AddJoinPoint(JoinPoint joinPoint, string builderId, MethodInfo advice, int sequence, AdviceParameterFlag parameterFlag)
        {
            if(string.IsNullOrWhiteSpace(builderId))
            {
                throw new ArgumentNullException("builderId");
            }
            if(advice == null)
            {
                throw new ArgumentNullException("advice");
            }
            _readOnly.Assert(false);
            if (_plan.ContainsKey(joinPoint))
            {
                var sequenceSetting = _plan[joinPoint];
                if (sequenceSetting.ContainsKey(sequence))
                {
                    throw new ArgumentException(string.Format("Sequence {0} for join point {1} has been used already", sequence, joinPoint),
                        "sequence");
                }
                sequenceSetting.Add(sequence, BatchFactory.InitializeAdviceInfo(advice, builderId, parameterFlag));
            }
            else
            {
                var sequenceSetting = new SortedDictionary<int, IAdviceInfo> { { sequence, BatchFactory.InitializeAdviceInfo(advice, builderId, parameterFlag) } };
                _plan.Add(joinPoint, sequenceSetting);
            }
            ParameterFlag |= parameterFlag;
        }

        public IReadOnlyCollection<IAdviceInfo> GetAdvices(JoinPoint joinPoint)
        {
            _readOnly.Assert(true);
            if(!_plan.ContainsKey(joinPoint))
            {
                return null;
            }
            return _plan[joinPoint].Values.ToList().AsReadOnly();
        }

        public IWeavingPlan ToReadOnly()
        {
            // with possibility to be empty, plan may be empty if the population process isn't monitored
            // and the code looks dirty to monitor weaving plan population
            // so allow plan to be empty, handle empty plan at weaving phase
            // no empty checking here
            _readOnly.Apply();
            return this;
        }
    }
}
