/**
* Description: Weaving record implementation
* Author: David Cui
*/

namespace CrossCutterN.Weaver.Statistics
{
    using Aspect;
    using System;

    internal sealed class WeavingRecord : IWeavingRecord
    {
        public JoinPoint JoinPoint { get; private set; }
        public string AspectBuilderId { get; private set; }
        public string MethodFullName { get; private set; }
        public string MethodSignature { get; private set; }
        public int Sequence { get; private set; }

        internal WeavingRecord(JoinPoint joinPoint, string aspectBuilderId, 
            string methodFullName, string methodSignature, int sequence)
        {
            if(string.IsNullOrWhiteSpace(aspectBuilderId))
            {
                throw new ArgumentNullException("aspectBuilderId");
            }
            if(string.IsNullOrWhiteSpace(methodFullName))
            {
                throw new ArgumentNullException("methodFullName");
            }
            if(string.IsNullOrWhiteSpace(methodSignature))
            {
                throw new ArgumentNullException("methodSignature");
            }
            if(sequence < 0)
            {
                throw new ArgumentOutOfRangeException("sequence");
            }
            JoinPoint = joinPoint;
            AspectBuilderId = aspectBuilderId;
            MethodFullName = methodFullName;
            MethodSignature = methodSignature;
            Sequence = sequence;
        }
    }
}
