// <copyright file="WeavingPlanner.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Weaver
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CrossCutterN.Aspect.Aspect;
    using CrossCutterN.Aspect.Metadata;
    using CrossCutterN.Base.Common;
    using CrossCutterN.Weaver.Utilities;

    /// <summary>
    /// Weaving planner implementation.
    /// </summary>
    internal sealed class WeavingPlanner : ICanAddAspect<IWeavingPlanner>, IWeavingPlanner
    {
        private readonly Dictionary<string, IAspect> aspects = new Dictionary<string, IAspect>();
        private readonly Dictionary<JoinPoint, Dictionary<string, JoinPointInfo>> joinPoints =
            new Dictionary<JoinPoint, Dictionary<string, JoinPointInfo>>();

        private readonly IrreversibleOperation readOnly = new IrreversibleOperation();

        /// <inheritdoc/>
        public void AddAspect(string aspectName, IAspect aspect, IReadOnlyDictionary<JoinPoint, int> sequenceDict)
        {
#if DEBUG
            if (string.IsNullOrWhiteSpace(aspectName))
            {
                throw new ArgumentNullException("aspectName");
            }

            if (aspect == null)
            {
                throw new ArgumentNullException("aspect");
            }

            if (sequenceDict == null || !sequenceDict.Any())
            {
                throw new ArgumentNullException("sequenceDict");
            }
#endif
            if (aspects.ContainsKey(aspectName))
            {
                throw new ArgumentException($"aspect with id {aspectName} already exists.", "aspectName");
            }

            var pointCut = aspect.PointCut;
            if (pointCut == null || !pointCut.Any())
            {
                throw new ArgumentException($"aspect {aspectName} doesn't contain any join point", "aspect");
            }

            if (!JoinPointCollectionComparer.PointCutEquals(pointCut, sequenceDict.Keys.ToList().AsReadOnly()))
            {
                throw new ArgumentException($"Sequence dictionary doesn't match join points of aspect {aspectName}", "sequenceDict");
            }

            foreach (var sequenceItem in sequenceDict)
            {
                var key = sequenceItem.Key;
                if (joinPoints.ContainsKey(key) && joinPoints[key].Values.Any(i => i.Sequence == sequenceItem.Value))
                {
                    throw new ArgumentException($"Sequence {sequenceItem.Value} of join point {key} has been occupied already.", "sequenceDict");
                }
            }

            readOnly.Assert(false);
            var joinPointDict = new Dictionary<JoinPoint, JoinPointInfo>();
            foreach (var joinPoint in pointCut)
            {
                var flag = AdviceValidator.Validate(joinPoint, aspect.GetAdvice(joinPoint));
                joinPointDict.Add(joinPoint, new JoinPointInfo { Sequence = sequenceDict[joinPoint], ParameterFlag = flag });
            }

            aspects.Add(aspectName, aspect);

            foreach (var joinPoint in pointCut)
            {
                if (!joinPoints.ContainsKey(joinPoint))
                {
                    joinPoints.Add(joinPoint, new Dictionary<string, JoinPointInfo>());
                }

                joinPoints[joinPoint].Add(aspectName, joinPointDict[joinPoint]);
            }
        }

        /// <inheritdoc/>
        public IWeavingPlan MakePlan(IMethod method)
        {
            readOnly.Assert(true);
            var plan = WeaverFactory.InitializeWeavingPlan();
            foreach (var aspectEntry in aspects)
            {
                var aspect = aspectEntry.Value;
                if (aspect.CanApplyTo(method))
                {
                    foreach (var joinPoint in aspect.PointCut)
                    {
                        var advice = aspect.GetAdvice(joinPoint);
                        var id = aspectEntry.Key;
                        var info = joinPoints[joinPoint][id];
                        plan.AddJoinPoint(joinPoint, id, advice, info.Sequence, info.ParameterFlag, aspect.IsSwitchedOn);
                    }
                }
            }

            return plan.Build();
        }

        /// <inheritdoc/>
        public IPropertyWeavingPlan MakePlan(IProperty property)
        {
            readOnly.Assert(true);
            var getterPlan = WeaverFactory.InitializeWeavingPlan();
            var setterPlan = WeaverFactory.InitializeWeavingPlan();
            foreach (var aspectEntry in aspects)
            {
                var aspect = aspectEntry.Value;
                var propertyConcern = aspect.CanApplyTo(property);
                if (propertyConcern.IsConcerned())
                {
                    foreach (var joinPoint in aspect.PointCut)
                    {
                        var advice = aspect.GetAdvice(joinPoint);
                        var aspectName = aspectEntry.Key;
                        var info = joinPoints[joinPoint][aspectName];
                        if (propertyConcern.IsGetterConcerned())
                        {
                            getterPlan.AddJoinPoint(joinPoint, aspectName, advice, info.Sequence, info.ParameterFlag, aspect.IsSwitchedOn);
                        }

                        if (propertyConcern.IsSetterConcerned())
                        {
                            setterPlan.AddJoinPoint(joinPoint, aspectName, advice, info.Sequence, info.ParameterFlag, aspect.IsSwitchedOn);
                        }
                    }
                }
            }

            return WeaverFactory.InitializePropertyWeavingPlan(getterPlan.Build(), setterPlan.Build());
        }

        /// <inheritdoc/>
        public IWeavingPlanner Build()
        {
            if (!aspects.Any())
            {
                throw new InvalidCastException("No aspect has been added");
            }

            readOnly.Apply();
            return this;
        }

        private class JoinPointInfo
        {
            public int Sequence { get; set; }

            public AdviceParameterFlag ParameterFlag { get; set; }
        }
    }
}
