// <copyright file="AspectBuilderConfigurationProcessor.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Console.Application
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using CrossCutterN.Aspect.Builder;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Aspect builder configuration processor.
    /// </summary>
    internal sealed class AspectBuilderConfigurationProcessor : IConfigurationProcessor
    {
        private readonly IAspectBuilderUtilityBuilder utility;

        /// <summary>
        /// Initializes a new instance of the <see cref="AspectBuilderConfigurationProcessor"/> class.
        /// </summary>
        /// <param name="utility">aspect utility that contains aspect information.</param>
        public AspectBuilderConfigurationProcessor(IAspectBuilderUtilityBuilder utility) => this.utility = utility ?? throw new ArgumentNullException("utility");

        /// <inheritdoc/>
        public void Process(IConfigurationSection section, string configurationFullPath, string currentDirectory)
        {
            var assemblies = section.Get<Dictionary<string, AspectAssembly>>();
            if (assemblies == null || !assemblies.Any())
            {
                throw new ApplicationException($"Empty aspect assembly in: {configurationFullPath}.");
            }

            Directory.SetCurrentDirectory(Path.GetDirectoryName(configurationFullPath));
            foreach (var assemblyEntry in assemblies)
            {
                utility.Import(assemblyEntry.Key, assemblyEntry.Value);
            }

            Directory.SetCurrentDirectory(currentDirectory);
        }
    }
}
