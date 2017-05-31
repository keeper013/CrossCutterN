/**
 * Description: write only injection advice
 * Author: David Cui
 */

namespace CrossCutterN.Aspect
{
    using System.Reflection;
    using Advice.Common;

    public interface ICanSetJoinPointAdvice : ICanConvert<IAspect>
    {
        void SetJoinPointAdvice(JoinPoint joinPoint, MethodInfo advice);
    }
}
