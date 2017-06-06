/**
 * Description: static method configuration element extension
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Advice.Parameter;

    public static class AdviceElementExtension
    {
        public static MethodInfo ToMethodInfo(this AdviceElement element)
        {
            var type = element.Type;
            var method = element.Method;
            var parameterPattern = element.ParameterPattern;
            var parameterTypes = new List<Type>();
            if (parameterPattern.NeedExecutionContextParameter())
            {
                parameterTypes.Add(typeof(IExecutionContext));
            }
            if (parameterPattern.NeedExecutionParameter())
            {
                parameterTypes.Add(typeof(IExecution));
            }
            if (parameterPattern.NeedExceptionParameter())
            {
                parameterTypes.Add(typeof(Exception));
            }
            if (parameterPattern.NeedReturnParameter())
            {
                parameterTypes.Add(typeof(IReturn));
            }
            if (parameterPattern.NeedHasExceptionParameter())
            {
                parameterTypes.Add(typeof(bool));
            }
            var methodInfo = type.GetMethod(method, parameterTypes.ToArray());
            if (methodInfo == null)
            {
                var message = string.Format("{0}.{1}(<{2}>)", element.Type.FullName, element.Method,
                                            element.ParameterPattern);
                throw new InvalidOperationException(string.Format("Method {0} doesn't exist", message));
            }
            return methodInfo;
        }
    }
}
