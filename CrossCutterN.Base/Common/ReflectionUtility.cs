// <copyright file="ReflectionUtility.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Common
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Reflection utility class.
    /// </summary>
    public static class ReflectionUtility
    {
        /// <summary>
        /// Gets the formatted full name of a class, the format is generally used by CrossCutterN.
        /// </summary>
        /// <param name="type">Class the full name of which will be </param>
        /// <returns>Full name of a class used by CrossCutterN.</returns>
        public static string GetFullName(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return type.FullName;
        }

        /// <summary>
        /// Gets the formatted method signature of a method, the format is generally used by CrossCutterN
        /// </summary>
        /// <param name="method">Method whose signature will be retrieved.</param>
        /// <returns>Signature of the method.</returns>
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

        /// <summary>
        /// Gets the formatted method full name, the format is generally used by CrossCutterN
        /// </summary>
        /// <param name="method">Method whose signature will be retrieved.</param>
        /// <returns>Signature of the method.</returns>
        public static string GetFullName(this MethodInfo method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            return method.DeclaringType == null
                       ? method.Name
                       : $"{method.DeclaringType.FullName}.{method.Name}";
        }
    }
}
