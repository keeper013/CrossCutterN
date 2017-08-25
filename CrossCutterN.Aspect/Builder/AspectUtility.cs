// <copyright file="AspectUtility.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Builder
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Utility to store available aspects.
    /// </summary>
    internal sealed class AspectUtility : IAspectBuilderUtility, IAspectBuilderUtilityBuilder
    {
        private static readonly Type AspectType = typeof(IAspectBuilder);
        private readonly IrreversibleOperation readOnly = new IrreversibleOperation();
        private readonly Dictionary<string, Dictionary<string, Func<IAspectBuilder>>> aspectConstructors =
            new Dictionary<string, Dictionary<string, Func<IAspectBuilder>>>();

        /// <inheritdoc/>
        public void AddAspectBuilderConstructor(string assemblyKey, string aspectName, Func<IAspectBuilder> constructor)
        {
            if (string.IsNullOrWhiteSpace(assemblyKey))
            {
                throw new ArgumentNullException("assemblyKey");
            }

            if (string.IsNullOrWhiteSpace(aspectName))
            {
                throw new ArgumentNullException("aspectName");
            }

            if (constructor == null)
            {
                throw new ArgumentNullException("constructor");
            }

            readOnly.Assert(false);
            if (constructor.GetMethodInfo().GetParameters().Length != 0)
            {
                throw new ApplicationException($"Function {constructor.GetMethodInfo().GetSignature()} should be a parameterless constructor.");
            }

            var constructedType = constructor.GetMethodInfo().ReturnType;
            if (!AspectType.IsAssignableFrom(constructedType))
            {
                throw new ApplicationException(
                    $"Function {constructor.GetMethodInfo().GetSignature()} does't return a type that is compatible with {AspectType.FullName}");
            }

            if (aspectConstructors.ContainsKey(assemblyKey))
            {
                var constructorDict = aspectConstructors[assemblyKey];
                if (constructorDict.ContainsKey(aspectName))
                {
                    throw new ApplicationException($"Aspect key {aspectName} is taken already.");
                }

                constructorDict[aspectName] = constructor;
            }
            else
            {
                aspectConstructors[assemblyKey] = new Dictionary<string, Func<IAspectBuilder>> { { aspectName, constructor } };
            }
        }

        /// <inheritdoc/>
        public IAspectBuilderUtility Build()
        {
            readOnly.Apply();
            return this;
        }

        /// <inheritdoc/>
        public Func<IAspectBuilder> GetAspectConstructor(string assemblyKey, string aspectName)
        {
            if (string.IsNullOrWhiteSpace(assemblyKey))
            {
                throw new ArgumentNullException("assemblyKey");
            }

            if (string.IsNullOrWhiteSpace(aspectName))
            {
                throw new ArgumentNullException("aspectName");
            }

            readOnly.Assert(true);
            if (!aspectConstructors.ContainsKey(assemblyKey))
            {
                return null;
            }

            var constructorDict = aspectConstructors[assemblyKey];
            return constructorDict.ContainsKey(aspectName) ?
                constructorDict[aspectName] : null;
        }
    }
}
