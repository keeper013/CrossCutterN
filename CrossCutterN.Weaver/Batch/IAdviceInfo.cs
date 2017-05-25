/**
 * Description: Advice information interface
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Batch
{
    using System.Reflection;

    internal interface IAdviceInfo
    {
        MethodInfo Advice { get; }
        string BuilderId { get; }
        AdviceParameterFlag ParameterFlag { get; }
        bool? Switch { get; }
    }
}
