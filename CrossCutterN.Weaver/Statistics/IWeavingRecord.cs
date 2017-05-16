/**
* Description: Weaving record
* Author: David Cui
*/

namespace CrossCutterN.Weaver.Statistics
{
    using Aspect;

    public interface IWeavingRecord
    {
        JoinPoint JoinPoint { get; }
        string MethodFullName { get; }
        string MethodSignature { get; }
        int Sequence { get; }
        string AspectBuilderId { get; }
    }
}
