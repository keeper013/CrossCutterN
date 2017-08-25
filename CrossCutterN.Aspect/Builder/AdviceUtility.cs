// <copyright file="AdviceUtility.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Builder
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Utility class to store advice assembly contents, including attributes and advices.
    /// </summary>
    internal sealed class AdviceUtility : IAdviceUtility, IAdviceUtilityBuilder
    {
        private static readonly Type AttributeType = typeof(Attribute);

        private readonly Dictionary<string, AssemblyContent> adviceAssemblies = new Dictionary<string, AssemblyContent>();
        private readonly IrreversibleOperation readOnly = new IrreversibleOperation();

        /// <inheritdoc/>
        public void AddAttribute(string assemblyKey, string attributeKey, Type attribute)
        {
            if (string.IsNullOrWhiteSpace(assemblyKey))
            {
                throw new ArgumentNullException("assemblyKey");
            }

            if (string.IsNullOrWhiteSpace(attributeKey))
            {
                throw new ArgumentNullException("attributeKey");
            }

            if (attribute == null)
            {
                throw new ArgumentNullException("type");
            }

            readOnly.Assert(false);
            if (!attribute.IsSubclassOf(AttributeType))
            {
                throw new ApplicationException($"Type {attribute.FullName} is not an attribute.");
            }

            if (adviceAssemblies.ContainsKey(assemblyKey))
            {
                var types = adviceAssemblies[assemblyKey].TypeDictionary;
                if (types.ContainsKey(attributeKey))
                {
                    throw new ApplicationException($"Type key {attributeKey} already exists under assembly {assemblyKey}.");
                }

                types[attributeKey] = attribute;
            }
            else
            {
                var assemblyContent = new AssemblyContent();
                assemblyContent.TypeDictionary[attributeKey] = attribute;
                adviceAssemblies[assemblyKey] = assemblyContent;
            }
        }

        /// <inheritdoc/>
        public void AddAdvice(string assemblyKey, string adviceKey, MethodInfo advice)
        {
            if (string.IsNullOrWhiteSpace(assemblyKey))
            {
                throw new ArgumentNullException("assemblyKey");
            }

            if (string.IsNullOrWhiteSpace(adviceKey))
            {
                throw new ArgumentNullException("adviceKey");
            }

            if (advice == null)
            {
                throw new ArgumentNullException("advice");
            }

            readOnly.Assert(false);
            if (adviceAssemblies.ContainsKey(assemblyKey))
            {
                var methods = adviceAssemblies[assemblyKey].MethodDictionary;
                if (methods.ContainsKey(adviceKey))
                {
                    throw new ApplicationException($"Method key {adviceKey} already exists under assembly {assemblyKey}.");
                }

                methods[adviceKey] = advice;
            }
            else
            {
                var assemblyContent = new AssemblyContent();
                assemblyContent.MethodDictionary[adviceKey] = advice;
                adviceAssemblies[assemblyKey] = assemblyContent;
            }
        }

        /// <inheritdoc/>
        public MethodInfo GetMethod(string assemblyKey, string adviceKey)
        {
            if (string.IsNullOrWhiteSpace(assemblyKey))
            {
                throw new ArgumentNullException("assemblyKey");
            }

            if (string.IsNullOrWhiteSpace(adviceKey))
            {
                throw new ArgumentNullException("adviceKey");
            }

            readOnly.Assert(true);
            return !adviceAssemblies.ContainsKey(assemblyKey) || !adviceAssemblies[assemblyKey].MethodDictionary.ContainsKey(adviceKey) ?
                null : adviceAssemblies[assemblyKey].MethodDictionary[adviceKey];
        }

        /// <inheritdoc/>
        public Type GetAttribute(string assemblyKey, string attributeKey)
        {
            if (string.IsNullOrWhiteSpace(assemblyKey))
            {
                throw new ArgumentNullException("assemblyKey");
            }

            if (string.IsNullOrWhiteSpace(attributeKey))
            {
                throw new ArgumentNullException("attributeKey");
            }

            readOnly.Assert(true);
            return !adviceAssemblies.ContainsKey(assemblyKey) || !adviceAssemblies[assemblyKey].TypeDictionary.ContainsKey(attributeKey) ?
                null : adviceAssemblies[assemblyKey].TypeDictionary[attributeKey];
        }

        /// <inheritdoc/>
        public IAdviceUtility Build()
        {
            readOnly.Apply();
            return this;
        }

        private class AssemblyContent
        {
            public Dictionary<string, Type> TypeDictionary { get; } = new Dictionary<string, Type>();

            public Dictionary<string, MethodInfo> MethodDictionary { get; } = new Dictionary<string, MethodInfo>();
        }
    }
}
