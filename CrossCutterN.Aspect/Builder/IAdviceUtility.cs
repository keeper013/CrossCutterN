// <copyright file="IAdviceUtility.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Builder
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Interface of utility to store advice assembly contents, including attributes and advices.
    /// </summary>
    public interface IAdviceUtility
    {
        /// <summary>
        /// Gets added attribute from the utility.
        /// </summary>
        /// <param name="assemblyKey">Key of assembly.</param>
        /// <param name="attributeKey">Key of attribute.</param>
        /// <returns>Attribute type retrieved.</returns>
        Type GetAttribute(string assemblyKey, string attributeKey);

        /// <summary>
        /// Gets added advice according to assembly key and advice key.
        /// </summary>
        /// <param name="assemblyKey">Key of assembly.</param>
        /// <param name="adviceKey">Key of advice.</param>
        /// <returns>Advice method.</returns>
        MethodInfo GetMethod(string assemblyKey, string adviceKey);
    }
}
