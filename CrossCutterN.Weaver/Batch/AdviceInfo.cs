/**
* Description: Advice information implementation
* Author: David Cui
*/

namespace CrossCutterN.Weaver.Batch
{
    using System;
    using System.Reflection;

    internal sealed class AdviceInfo : IAdviceInfo
    {
        public MethodInfo Advice { get; private set; }

        public string BuilderId { get; private set; }

        public AdviceParameterFlag ParameterFlag { get; private set; }

        public AdviceInfo(MethodInfo advice, string builderId, AdviceParameterFlag parameterFlag)
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
        }
    }
}
