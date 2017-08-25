// <copyright file="AspectUtilityExtension.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Builder
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using CrossCutterN.Aspect.Utilities;

    /// <summary>
    /// Extensions of aspect utility.
    /// </summary>
    public static class AspectUtilityExtension
    {
        /// <summary>
        /// Imports an advice assembly content into this advice utility according to it's configuration.
        /// </summary>
        /// <param name="utility">The aspect utility.</param>
        /// <param name="assemblyKey">Key of the assembly.</param>
        /// <param name="configuration">Configuration of advice assembly.</param>
        public static void Import(this IAspectBuilderUtilityBuilder utility, string assemblyKey, AspectAssembly configuration)
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

            Assembly assembly;
            string assemblyString;
            if (string.IsNullOrWhiteSpace(configuration.AssemblyPath))
            {
                throw new ApplicationException("Assembly path should be set.");
            }

            assemblyString = configuration.AssemblyPath;
            assembly = Assembly.Load(File.ReadAllBytes(PathUtility.ProcessPath(configuration.AssemblyPath)));

            if (assembly == null)
            {
                throw new ApplicationException($"Assembly {assemblyString} is not found.");
            }

            if (configuration.AspectBuilders == null || !configuration.AspectBuilders.Any())
            {
                throw new ApplicationException($"Empty aspect collection for {assemblyString}.");
            }

            foreach (var typeEntry in configuration.AspectBuilders)
            {
                var type = assembly.GetType(typeEntry.Value);
                if (type == null)
                {
                    throw new ApplicationException($"Invalid type {type} in assembly {assemblyString}");
                }

                string aspectName = typeEntry.Key;
                var dynamicMethod = new DynamicMethod(aspectName, type, new Type[0]);
                var ilGenerator = dynamicMethod.GetILGenerator();
                ilGenerator.Emit(OpCodes.Newobj, type.GetConstructor(new Type[0]));
                ilGenerator.Emit(OpCodes.Ret);
                var constructor = (Func<AspectBuilder>)dynamicMethod.CreateDelegate(typeof(Func<AspectBuilder>));
                utility.AddAspectBuilderConstructor(assemblyKey, aspectName, constructor);
            }
        }
    }
}
