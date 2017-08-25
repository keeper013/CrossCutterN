// <copyright file="ConsoleApplication.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Console.Application
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using CrossCutterN.Aspect.Builder;
    using CrossCutterN.Aspect.Utilities;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Console application implementation
    /// </summary>
    public static class ConsoleApplication
    {
        private const string SectionName = "CrossCutterN";

        /// <summary>
        /// Processes configuration files.
        /// </summary>
        /// <param name="aspectConfigFiles">Aspect configuration files.</param>
        /// <param name="adviceConfigFiles">Advice configuration files</param>
        /// <param name="targetConfigFiles">Target configuration files</param>
        /// <returns>List of Processed assemblies.</returns>
        internal static List<string> Process(string[] aspectConfigFiles, string[] adviceConfigFiles, string[] targetConfigFiles)
        {
            if (adviceConfigFiles == null || !adviceConfigFiles.Any())
            {
                throw new ArgumentNullException("adviceConfigFiles");
            }

            if (targetConfigFiles == null || !targetConfigFiles.Any())
            {
                throw new ArgumentNullException("targetConfigFiles");
            }

            var result = new List<string>();
            try
            {
                var aspectUtility = GetAspectUtility(aspectConfigFiles);
                var adviceUtility = GetAdviceUtility(adviceConfigFiles);
                var targetProcessor = new TargetConfigurationProcessor(aspectUtility, adviceUtility, result);
                Process(targetConfigFiles, targetProcessor);
            }
            catch (Exception e)
            {
                while (e != null)
                {
                    Console.Out.WriteLine(e.Message);
                    Console.Out.WriteLine(e.StackTrace);
                    e = e.InnerException;
                }
            }

            return result;
        }

        private static IAspectBuilderUtility GetAspectUtility(string[] aspectConfigurations)
        {
            var aspectBuilderUtility = AspectBuilderFactory.InitializeAspectBuilderUtility();
            var processor = new AspectBuilderConfigurationProcessor(aspectBuilderUtility);
            Process(aspectConfigurations, processor);
            return aspectBuilderUtility.Build();
        }

        private static IAdviceUtility GetAdviceUtility(string[] adviceConfigurations)
        {
            var adviceUtility = AspectBuilderFactory.InitializeAdviceUtility();
            var processor = new AdviceConfigurationProcessor(adviceUtility);
            Process(adviceConfigurations, processor);
            return adviceUtility.Build();
        }

        private static void Process(string[] paths, IConfigurationProcessor processor)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var l = paths.Length;
            for (var i = 0; i < l; i++)
            {
                var configurationPath = paths[i];
                var configurationfullPath = PathUtility.ProcessPath(configurationPath);
                var builder = new ConfigurationBuilder().AddJsonFile(configurationfullPath).Build();
                var section = builder.GetSection(SectionName);
                processor.Process(section, configurationfullPath, currentDirectory);
            }
        }
    }
}
