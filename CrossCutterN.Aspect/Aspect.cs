/**
 * Description: Injection advice implementation
 * Author: David Cui
 */

namespace CrossCutterN.Aspect
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Advice.Common;

    internal sealed class Aspect : IAspect, ICanSetJoinPointAdvice
    {
        private readonly Dictionary<JoinPoint, MethodInfo> _pointCut = new Dictionary<JoinPoint, MethodInfo>();
        private readonly IrreversibleOperation _readOnly = new IrreversibleOperation();
        private readonly bool? _switch;

        public bool? Switch
        {
            get { return _switch; }
        }

        public IReadOnlyDictionary<JoinPoint, MethodInfo> PointCut
        {
            get
            {
                _readOnly.Assert(true);
                return _pointCut;
            }
        }

        public Aspect(bool? switchValue)
        {
            _switch = switchValue;
        }

        public void SetJoinPointAdvice(JoinPoint joinPoint, MethodInfo advice)
        {
            if(advice == null)
            {
                throw new ArgumentNullException("advice");
            }
            if(_pointCut.ContainsKey(joinPoint))
            {
                throw new InvalidOperationException(string.Format("Join point {0} already exists", joinPoint));
            }
            _readOnly.Assert(false);
            _pointCut.Add(joinPoint, advice);
        }

        public IAspect Convert()
        {
            // Considering aspect generation is extendable, empty aspect is possible by customized aspect builders
            // So no empty aspect checking performed here, only readonly operation
            _readOnly.Apply();
            return this;
        }
    }
}
