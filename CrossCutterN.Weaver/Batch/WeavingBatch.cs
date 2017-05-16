/**
* Description: Weaving batch implementation
* Author: David Cui
*/

namespace CrossCutterN.Weaver.Batch
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Advice.Common;
    using Aspect;
    using Aspect.Builder;
    using Aspect.Concern;
    using Utilities;

    internal sealed class WeavingBatch : ICanAddAspectBuilder, IWeavingBatch
    {
        private readonly Dictionary<string, IAspectBuilder> _builders = new Dictionary<string, IAspectBuilder>();
        private readonly Dictionary<JoinPoint, Dictionary<string, BuilderJoinPointInfo>> _joinPoints = 
            new Dictionary<JoinPoint, Dictionary<string, BuilderJoinPointInfo>>();
        private readonly IrreversibleOperation _readOnly = new IrreversibleOperation();

        public void AddAspectBuilder(string id, IAspectBuilder builder, IReadOnlyDictionary<JoinPoint, int> sequenceDict)
        {
            if(string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("id");
            }
            if(builder == null)
            {
                throw new ArgumentNullException("builder");
            }
            if(sequenceDict == null || !sequenceDict.Any())
            {
                throw new ArgumentNullException("sequenceDict");
            }
            if(_builders.ContainsKey(id))
            {
                throw new ArgumentException(string.Format("Aspect builder with id {0} already exists.", id), "id");
            }
            var pointCut = builder.PointCut;
            if(pointCut == null || !pointCut.Any())
            {
                throw new ArgumentException("Aspect builder doesn't contain any join point", "builder");
            }
            if (!Comparer.PointCutEquals(pointCut, sequenceDict.Keys.ToList().AsReadOnly()))
            {
                throw new ArgumentException("Sequence dictionary doesn't match join points of aspect builder", "sequenceDict");
            }
            foreach(var sequenceItem in sequenceDict)
            {
                var key = sequenceItem.Key;
                if(_joinPoints.ContainsKey(key) && _joinPoints[key].Values.Any(i => i.Sequence == sequenceItem.Value))
                {
                    throw new ArgumentException(
                        string.Format("Sequence {0} of join point {1} has been occupied already.", sequenceItem.Value, key), "sequenceDict");
                }
            }
            _readOnly.Assert(false);
            var builderJoinPointDict = new Dictionary<JoinPoint, BuilderJoinPointInfo>();
            foreach(var joinPoint in pointCut)
            {
                var flag = AdviceValidator.Validate(joinPoint, builder.GetAdvice(joinPoint));
                builderJoinPointDict.Add(joinPoint, new BuilderJoinPointInfo { Sequence = sequenceDict[joinPoint], ParameterFlag = flag });
            }
            _builders.Add(id, builder);
            foreach(var joinPoint in pointCut)
            {
                if(!_joinPoints.ContainsKey(joinPoint))
                {
                    _joinPoints.Add(joinPoint, new Dictionary<string, BuilderJoinPointInfo>());
                }
                _joinPoints[joinPoint].Add(id, builderJoinPointDict[joinPoint]);
            }
        }

        public IWeavingPlan BuildPlan(IMethod method)
        {
            _readOnly.Assert(true);
            var plan = BatchFactory.InitializeWeavingPlan();
            foreach(var builder in _builders)
            {
                var aspect = builder.Value.GetAspect(method);
                foreach(var jp in aspect.PointCut)
                {
                    var joinPoint = jp.Key;
                    var advice = jp.Value;
                    var id = builder.Key;
                    var info = _joinPoints[joinPoint][id];
                    plan.AddJoinPoint(joinPoint, id, advice, info.Sequence, info.ParameterFlag);
                }
            }
            return plan.ToReadOnly();
        }

        public IPropertyWeavingPlan BuildPlan(IProperty property)
        {
            _readOnly.Assert(true);
            var getterPlan = BatchFactory.InitializeWeavingPlan();
            var setterPlan = BatchFactory.InitializeWeavingPlan();
            foreach (var builder in _builders)
            {
                var aspect = builder.Value.GetAspect(property);
                foreach (var jp in aspect.GetterAspect.PointCut)
                {
                    var joinPoint = jp.Key;
                    var advice = jp.Value;
                    var id = builder.Key;
                    var info = _joinPoints[joinPoint][id];
                    getterPlan.AddJoinPoint(joinPoint, id, advice, info.Sequence, info.ParameterFlag);
                }
                foreach (var jp in aspect.SetterAspect.PointCut)
                {
                    var joinPoint = jp.Key;
                    var advice = jp.Value;
                    var id = builder.Key;
                    var info = _joinPoints[joinPoint][id];
                    setterPlan.AddJoinPoint(joinPoint, id, advice, info.Sequence, info.ParameterFlag);
                }
            }
            return BatchFactory.InitializePropertyWeavingPlan(getterPlan.ToReadOnly(), setterPlan.ToReadOnly());
        }

        public IWeavingBatch ToReadOnly()
        {
            if(!_builders.Any())
            {
                throw new InvalidCastException("No aspect builder has been added");
            }
            _readOnly.Apply();
            return this;
        }

        private class BuilderJoinPointInfo
        {
            public int Sequence { get; set; }
            public AdviceParameterFlag ParameterFlag { get; set; }
        }
    }
}
