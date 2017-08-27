// <copyright file="TargetConfigurationProcessor.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Console.Application
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using CrossCutterN.Aspect.Aspect;
    using CrossCutterN.Aspect.Builder;
    using CrossCutterN.Aspect.Utilities;
    using CrossCutterN.Console.Configuration;
    using CrossCutterN.Weaver.Statistics;
    using CrossCutterN.Weaver.Weaver;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Target configuration processor.
    /// </summary>
    internal sealed class TargetConfigurationProcessor : IConfigurationProcessor
    {
        private const string AspectBuildersSectionName = "AspectBuilders";
        private const string TargetsSectionName = "Targets";
        private const string DefaultAspectAssemblyKey = "CrossCutterN.Aspect";

        private readonly IAspectBuilderUtility aspectUtility;
        private readonly IAdviceUtility adviceUtility;
        private readonly IList<string> processed;

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetConfigurationProcessor"/> class.
        /// </summary>
        /// <param name="aspectUtility">Aspect utility that contains all aspect information.</param>
        /// <param name="adviceUtility">Advice utility that contains all advice information.</param>
        /// <param name="processed">Processed assembly list.</param>
        public TargetConfigurationProcessor(IAspectBuilderUtility aspectUtility, IAdviceUtility adviceUtility, IList<string> processed)
        {
            this.aspectUtility = aspectUtility ?? throw new ArgumentNullException("AspectUtility");
            this.adviceUtility = adviceUtility ?? throw new ArgumentNullException("adviceUtility");
            this.processed = processed ?? throw new ArgumentNullException("processed");
        }

        /// <inheritdoc/>
        public void Process(IConfigurationSection section, string configurationFullPath, string currentDirectory)
        {
            var targets = new TargetAssemblies();
            section.Bind(targets);
            if (targets.AspectBuilders == null || !targets.AspectBuilders.Any())
            {
                throw new ApplicationException($"Empty aspect references in {configurationFullPath}.");
            }

            if (targets.Targets == null || !targets.Targets.Any())
            {
                throw new ApplicationException($"Empty targets in {configurationFullPath}.");
            }

            var aspects = InitializeAspects(targets.AspectBuilders, DefaultAspectAssemblyKey, targets.DefaultAdviceAssemblyKey, section, configurationFullPath);
            var sequences = InitializeSequences(targets.Order, configurationFullPath);
            var weaver = BuildWeaver(aspects, sequences, configurationFullPath);
            Weave(weaver, targets.Targets, configurationFullPath, currentDirectory);
        }

        private static Dictionary<string, Dictionary<JoinPoint, int>> InitializeSequences(
            Dictionary<JoinPoint, List<string>> orders, string configurationFullPath)
        {
            var sequenceDict = new Dictionary<string, Dictionary<JoinPoint, int>>();
            if (orders != null && orders.Any())
            {
                foreach (var item in orders)
                {
                    var joinPoint = item.Key;
                    var seq = 1;
                    foreach (var aspect in item.Value)
                    {
                        if (sequenceDict.ContainsKey(aspect))
                        {
                            if (sequenceDict[aspect].ContainsKey(joinPoint))
                            {
                                throw new ApplicationException($"Repeated aspect {aspect} and join point {joinPoint} in sequence configuration in {configurationFullPath}.");
                            }

                            sequenceDict[aspect][joinPoint] = seq++;
                        }
                        else
                        {
                            sequenceDict[aspect] = new Dictionary<JoinPoint, int> { { joinPoint, seq++ } };
                        }
                    }
                }
            }

            return sequenceDict;
        }

        private static IWeaver BuildWeaver(
            Dictionary<string, IAspect> aspects, Dictionary<string, Dictionary<JoinPoint, int>> sequences, string configurationFullPath)
        {
            var weaverToBuild = WeaverFactory.InitializeWeaver();

            // if there is only one aspect, sequence configuration section can be ignored
            if (aspects.Count == 1 && (sequences == null || !sequences.Any()))
            {
                var aspect = aspects.First();
                var sequence = new Dictionary<JoinPoint, int>();
                foreach (var joinPoint in aspect.Value.PointCut)
                {
                    sequence.Add(joinPoint, 1);
                }

                weaverToBuild.AddAspect(aspect.Key, aspect.Value, sequence);
            }
            else
            {
                foreach (var aspectEntry in aspects)
                {
                    var aspectName = aspectEntry.Key;
                    if (!sequences.ContainsKey(aspectName))
                    {
                        throw new ApplicationException($"Sequence not specified for aspect {aspectName} in {configurationFullPath}.");
                    }

                    weaverToBuild.AddAspect(aspectName, aspectEntry.Value, sequences[aspectName]);
                }
            }

            return weaverToBuild.Build();
        }

        private static string ProcessAssemblyName(string name)
        {
            var index = name.IndexOf(',');
            return index == -1 ? name : name.Substring(0, index);
        }

        private Dictionary<string, IAspect> InitializeAspects(
            Dictionary<string, AspectBuilderReference> targets,
            string defaultAspectAssemblyKey,
            string defaultAdviceAssemblyKey,
            IConfigurationSection section,
            string configurationFullPath)
        {
            var aspects = new Dictionary<string, IAspect>();
            var aspectsSection = section.GetSection(AspectBuildersSectionName);
            foreach (var entry in targets)
            {
                var aspectName = entry.Key;
                if (aspects.ContainsKey(aspectName))
                {
                    throw new ApplicationException($"Aspect name {aspectName} is repeated in {configurationFullPath}");
                }

                var assemblyKey = entry.Value.AspectAssemblyKey;
                if (string.IsNullOrWhiteSpace(assemblyKey))
                {
                    assemblyKey = defaultAspectAssemblyKey;
                }

                var aspectBuilderKey = entry.Value.AspectBuilderKey;
                var constructor = aspectUtility.GetAspectConstructor(assemblyKey, aspectBuilderKey);
                if (constructor == null)
                {
                    throw new ApplicationException($"No aspect exists with assembly key {assemblyKey} and aspect key {aspectBuilderKey}.");
                }

                var builder = constructor();
                var aspectSection = aspectsSection.GetSection(aspectName);
                aspectSection.Bind(builder);
                var aspect = builder.Build(adviceUtility, defaultAdviceAssemblyKey);
                aspects.Add(aspectName, aspect);
            }

            return aspects;
        }

        private void Weave(IWeaver weaver, Dictionary<string, AssemblySetting> targets, string configurationFullPath, string currentDirectory)
        {
            var configurationDirectory = Path.GetDirectoryName(configurationFullPath);
            foreach (var target in targets)
            {
                Directory.SetCurrentDirectory(configurationDirectory);
                var inputAssembly = target.Key;
                var inputPath = PathUtility.ProcessPath(inputAssembly);
                var settings = target.Value;
                var snkFilePath = string.IsNullOrWhiteSpace(settings.StrongNameKeyFile) ? null : PathUtility.ProcessPath(settings.StrongNameKeyFile);
                var outputPath = string.IsNullOrWhiteSpace(settings.Output) ? inputAssembly : settings.Output;
                Console.Out.WriteLine($"Starting to load {inputAssembly}, weaving into {outputPath}");
                outputPath = PathUtility.ProcessPath(outputPath);
                var statistics = weaver.Weave(inputPath, settings.IncludeSymbol, outputPath, snkFilePath);
                Directory.SetCurrentDirectory(currentDirectory);
                var assemblyName = ProcessAssemblyName(statistics.AssemblyName);
                var fileName = $"{assemblyName}_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.log";
                using (var output = new StreamWriter(fileName, true))
                {
                    statistics.Log(output);
                    output.Close();
                }

                processed.Add(inputPath);
                Console.Out.WriteLine($"Weaving of assembly {statistics.AssemblyName} in {inputAssembly} finished, output as {outputPath}");
            }

            Console.Out.WriteLine("Weaving finished");
        }
    }
}
