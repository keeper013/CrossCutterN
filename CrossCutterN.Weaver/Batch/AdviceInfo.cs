/**
* Description: Advice information implementation
* Author: David Cui
*/

namespace CrossCutterN.Weaver.Batch
{
    using System;
    using System.Reflection;
    using Aspect;

    internal sealed class AdviceInfo : IAdviceInfo
    {
        public MethodInfo Advice { get; private set; }

        public string BuilderId { get; private set; }

        public AdviceParameterFlag ParameterFlag { get; private set; }

        public SwitchStatus SwitchStatus { get; private set; }

        public AdviceInfo(MethodInfo advice, string builderId, AdviceParameterFlag parameterFlag, SwitchStatus switchStatus)
        {
            if(advice == null)
            {
                throw new ArgumentNullException("advice");
            }
            if(string.IsNullOrWhiteSpace(builderId))
            {
                throw new ArgumentNullException("builderId");
            }
            Advice = advice;
            BuilderId = builderId;
            ParameterFlag = parameterFlag;
            SwitchStatus = switchStatus;
        }
    }
}
