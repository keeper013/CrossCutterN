// <copyright file="AdviceUtilityExtension.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Builder
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using CrossCutterN.Aspect.Utilities;
    using CrossCutterN.Base.Metadata;

    /// <summary>
    /// Extension to <see cref="AdviceUtility"/>.
    /// </summary>
    public static class AdviceUtilityExtension
    {
        /// <summary>
        /// Imports an advice assembly content into this advice utility according to it's configuration.
        /// </summary>
        /// <param name="utility">The <see cref="IAdviceUtilityBuilder"/>.</param>
        /// <param name="assemblyKey">Key of the advice assembly.</param>
        /// <param name="configuration">Configuration of advice assembly.</param>
        public static void Import(this IAdviceUtilityBuilder utility, string assemblyKey, AdviceAssembly configuration)
        {
            if (utility == null)
            {
                throw new ArgumentNullException("utility");
            }

            if (string.IsNullOrWhiteSpace(assemblyKey))
            {
                throw new ArgumentNullException("assemblyKey");
            }

            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            var assembly = Assembly.Load(File.ReadAllBytes(PathUtility.ProcessPath(configuration.AssemblyPath)));
            if (assembly == null)
            {
                throw new ApplicationException($"Invalid assembly path: {configuration.AssemblyPath}.");
            }

            var hasAttributes = configuration.Attributes != null && configuration.Attributes.Any();
            var hasAdvices = configuration.Advices != null && configuration.Advices.Any();
            if (!hasAttributes && !hasAdvices)
            {
                throw new ApplicationException($"Assembly {configuration.AssemblyPath} doesn't contain attributes or advices.");
            }

            if (hasAttributes)
            {
                foreach (var attributeEntry in configuration.Attributes)
                {
                    var type = assembly.GetType(attributeEntry.Value);
                    if (type == null)
                    {
                        throw new ApplicationException($"Invalid type: {attributeEntry.Value} in assembly: {configuration.AssemblyPath}");
                    }

                    utility.AddAttribute(assemblyKey, attributeEntry.Key, type);
                }
            }

            if (hasAdvices)
            {
                var typeCache = new Dictionary<string, Type>();
                foreach (var methodEntry in configuration.Advices)
                {
                    var typeName = methodEntry.Key;
                    Type type;
                    if (typeCache.ContainsKey(typeName))
                    {
                        type = typeCache[typeName];
                    }
                    else
                    {
                        type = assembly.GetType(typeName) ?? throw new ApplicationException($"Invalid type: {typeName} in assembly: {configuration.AssemblyPath}");
                        typeCache[typeName] = type;
                    }

                    foreach (var adviceEntry in methodEntry.Value)
                    {
                        var key = adviceEntry.Key;
                        var adviceInfo = adviceEntry.Value;
                        var parameterTypes = new List<Type>();
                        var parameters = adviceInfo.Parameters;
                        if (parameters != null && parameters.Any())
                        {
                            if (parameters.Contains(AdviceParameterType.Context))
                            {
                                parameterTypes.Add(typeof(IExecutionContext));
                            }

                            if (parameters.Contains(AdviceParameterType.Execution))
                            {
                                parameterTypes.Add(typeof(IExecution));
                            }

                            if (parameters.Contains(AdviceParameterType.Exception))
                            {
                                parameterTypes.Add(typeof(Exception));
                            }

                            if (parameters.Contains(AdviceParameterType.Return))
                            {
                                parameterTypes.Add(typeof(IReturn));
                            }

                            if (parameters.Contains(AdviceParameterType.HasException))
                            {
                                parameterTypes.Add(typeof(bool));
                            }
                        }

                        var methodInfo = type.GetMethod(adviceInfo.MethodName, parameterTypes.ToArray());
                        if (methodInfo == null)
                        {
                            var parameterList = parameters == null || !parameters.Any() ? string.Empty : string.Join(",", parameters);
                            throw new ApplicationException(
                                $"Method {adviceInfo.MethodName}({parameterList}) doesn't exist in type {type} in assembly {configuration.AssemblyPath}");
                        }

                        utility.AddAdvice(assemblyKey, key, methodInfo);
                    }
                }
            }
        }
    }
}
