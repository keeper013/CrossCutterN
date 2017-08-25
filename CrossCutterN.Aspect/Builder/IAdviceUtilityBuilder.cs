// <copyright file="IAdviceUtilityBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Builder
{
    using System;
    using System.Reflection;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Interface for utility that contains advice information to be built up.
    /// </summary>
    public interface IAdviceUtilityBuilder : IBuilder<IAdviceUtility>
    {
        /// <summary>
        /// Adds an attribute from advice assembly.
        /// </summary>
        /// <param name="assemblyKey">Key of advice assembly.</param>
        /// <param name="attributeKey">Key of the attribute class.</param>
        /// <param name="attribute">Type of the attribute.</param>
        void AddAttribute(string assemblyKey, string attributeKey, Type attribute);

        /// <summary>
        /// Adds an advice in advice assembly to the utility.
        /// </summary>
        /// <param name="assemblyKey">Key of assembly.</param>
        /// <param name="adviceKey">Key of advice method.</param>
        /// <param name="advice">Advice method.</param>
        void AddAdvice(string assemblyKey, string adviceKey, MethodInfo advice);
    }
}
