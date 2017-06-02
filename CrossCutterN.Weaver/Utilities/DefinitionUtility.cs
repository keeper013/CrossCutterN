/**
 * Description: Mono.Cecil definition key getter
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Utilities
{
    using System;
    using System.Linq;
    using System.Text;
    using Mono.Cecil;

    public static class DefinitionUtility
    {
        public static string GetSignature(this MethodDefinition method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }
            var buffer = new StringBuilder();
            buffer.Append(method.Name);
            buffer.Append('(');
            var parameters = method.Parameters;
            if (parameters.Any())
            {
                foreach (var parameterInfo in parameters)
                {
                    buffer.Append(parameterInfo.ParameterType.FullName);
                    buffer.Append(',');
                }
                buffer.Remove(buffer.Length - 1, 1);
            }
            buffer.Append(")");
            return buffer.ToString();
        }

        public static string GetFullName(this TypeDefinition type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            return string.Format("{0}.{1}", type.Namespace, type.Name);
        }

        public static bool IsVoidReturn(this MethodDefinition method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }
            return method.ReturnType.FullName.Equals(typeof (void).FullName);
        }
    }
}
