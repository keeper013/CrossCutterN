/**
 * Description: Write only aspect builder
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Builder
{
    using System.Reflection;
    using Advice.Common;

    public interface IWriteOnlyAspectBuilder : ICanConvert<IAspectBuilder>
    {
        /// <summary>
        /// Advice verification will happen in weaver, since this extention point is not under weaver's control
        /// </summary>
        /// <param name="joinPoint">Join point to call advice</param>
        /// <param name="advice">advice method to be called</param>
        void SetAdvice(JoinPoint joinPoint, MethodInfo advice);
    }
}
