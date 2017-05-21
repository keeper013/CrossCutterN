/**
 * Description: internal reflection key builder
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Utilities
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public static class ReflectionUtility
    {
        public static string GetFullName(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            return type.FullName;
        }

        public static string GetSignatureWithTypeFullName(this MethodInfo method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }
            return string.Format("{0}.{1}", method.DeclaringType.GetFullName(), method.GetSignature());
        }

        public static string GetSignature(this MethodInfo method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }
            var buffer = new StringBuilder();
            buffer.Append(method.Name);
            buffer.Append('(');
            var parameters = method.GetParameters();
            if (parameters.Any())
            {
                foreach (var parameterInfo in parameters)
                {
                    buffer.Append(GetFullName(parameterInfo.ParameterType));
                    buffer.Append(',');
                }
                buffer.Remove(buffer.Length - 1, 1);
            }
            buffer.Append(")");
            return buffer.ToString();
        }

        public static string GetFullName(this MethodInfo method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }
            return method.DeclaringType == null
                       ? method.Name
                       : string.Format("{0}.{1}", method.DeclaringType.FullName, method.Name);
        }
    }
}
