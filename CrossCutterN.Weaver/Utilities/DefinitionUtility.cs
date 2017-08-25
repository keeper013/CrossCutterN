// <copyright file="DefinitionUtility.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Utilities
{
    using System;
    using System.Linq;
    using System.Text;
    using Mono.Cecil;

    /// <summary>
    /// Utility to retrieve assembly content information from Mono.Cecil form.
    /// </summary>
    internal static class DefinitionUtility
    {
        /// <summary>
        /// Gets method signature which is generally recognized and used in CrossCutterN.
        /// </summary>
        /// <param name="method">Method definition.</param>
        /// <returns>Signature of the method which will be recognized and used in CrossCutterN.</returns>
        public static string GetSignature(this MethodDefinition method)
        {
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

        /// <summary>
        /// Gets full name of a class which will be generally recognized and used in CrossCutterN.
        /// </summary>
        /// <param name="type">The class full name of which will be retrieved.</param>
        /// <returns>Full name of a class which will be generally recognized and used in CrossCutterN</returns>
        public static string GetFullName(this TypeDefinition type) => $"{type.Namespace}.{type.Name}";

        /// <summary>
        /// Finds out if a method is of void return type.
        /// </summary>
        /// <param name="method">The method to be checked.</param>
        /// <returns>True if the method does have void return type, false elsewise.</returns>
        public static bool IsVoidReturn(this MethodDefinition method) => method.ReturnType.FullName.Equals(typeof(void).FullName);
    }
}
