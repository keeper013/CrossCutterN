// <copyright file="AdviceConfigurationProcessor.cs" company="Cui Ziqiang">
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
    /// Advice assembly configuration processor.
    /// </summary>
    internal sealed class AdviceConfigurationProcessor : IConfigurationProcessor
    {
        private readonly IAdviceUtilityBuilder utility;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdviceConfigurationProcessor"/> class.
        /// </summary>
        /// <param name="utility">Advice utility that contains advice information.</param>
        public AdviceConfigurationProcessor(IAdviceUtilityBuilder utility) => this.utility = utility ?? throw new ArgumentNullException("utility");

        /// <inheritdoc/>
        public void Process(IConfigurationSection section, string configurationFullPath, string currentDirectory)
        {
            var assemblies = section.Get<Dictionary<string, AdviceAssembly>>();
            if (assemblies == null || !assemblies.Any())
            {
                throw new ApplicationException($"Empty advice assembly in: {configurationFullPath}.");
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
