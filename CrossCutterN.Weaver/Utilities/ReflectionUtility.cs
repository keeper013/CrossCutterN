// <copyright file="ReflectionUtility.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Utilities
{
    using System;
    using System.Reflection;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Utility to handle reflection related features.
    /// </summary>
    internal static class ReflectionUtility
    {
        private static readonly string VoidTypeName = typeof(void).FullName;

        /// <summary>
        /// Checks if a method is of void return.
        /// </summary>
        /// <param name="method">The method to be checked.</param>
        /// <returns>True if the method is of void return, false elsewise.</returns>
        public static bool IsVoidReturn(this MethodInfo method)
        {
#if DEBUG
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }
#endif
            return method.ReturnType.FullName.Equals(VoidTypeName);
        }

        /// <summary>
        /// Gets the formatted method signature with full class name of a method, the format is generally used by CrossCutterN
        /// </summary>
        /// <param name="method">Method whose signature will be retrieved.</param>
        /// <returns>Signature of the method.</returns>
        public static string GetSignatureWithTypeFullName(this MethodInfo method)
        {
#if DEBUG
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }
#endif
            return $"{method.DeclaringType.GetFullName()}.{method.GetSignature()}";
        }
    }
}
