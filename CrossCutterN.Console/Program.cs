// <copyright file="Program.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Console
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using CrossCutterN.Console.Application;

    /// <summary>
    /// Main program.
    /// </summary>
    internal static class Program
    {
        private const string AspectPrefix = "/s:";
        private const string AdvicePrefix = "/d:";
        private const string TargetPrefix = "/t:";

        /// <summary>
        /// Main method.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        private static void Main(string[] args)
        {
            int length = args.Length;
            if (args.Length < 2)
            {
                var name = Path.GetFileName(Assembly.GetExecutingAssembly().CodeBase);
                Console.Out.WriteLine("*************************************************************");
                Console.Out.WriteLine("Usage: Weave aspects into .NET assemblies");
                Console.Out.WriteLine($"Usage: {name} {AdvicePrefix}<advice configuration file> {TargetPrefix}<target configuration file>");
                Console.Out.WriteLine($"Example: {name} {AdvicePrefix}advice.json {TargetPrefix}target.json");
                Console.Out.WriteLine($"To apply customized aspect builders, refer to the content below:");
                Console.Out.WriteLine($"Usage: {name} {AspectPrefix}<aspect configuration file> {AdvicePrefix}<advice configuration file> {TargetPrefix}<target configuration file>");
                Console.Out.WriteLine($"Example: {name} {AspectPrefix}Aspect.json {AdvicePrefix}advice.json {TargetPrefix}target.json");
                Console.Out.WriteLine($"Note: there can be multiple {AspectPrefix}, {AdvicePrefix} and {TargetPrefix} targets, but at least one for each");
                Console.Out.WriteLine("*************************************************************");
                return;
            }

            var aspectConfigFiles = new List<string>();
            var adviceConfigFiles = new List<string>();
            var targetConfigFiles = new List<string>();
            ProcessArguments(args, aspectConfigFiles, adviceConfigFiles, targetConfigFiles);
            ConsoleApplication.Process(aspectConfigFiles.ToArray(), adviceConfigFiles.ToArray(), targetConfigFiles.ToArray());
        }

        private static void ProcessArguments(
            string[] args,
            List<string> aspectConfigurations,
            List<string> adviceConfigurations,
            List<string> targetConfigurations)
        {
            foreach (var arg in args)
            {
                if (arg.StartsWith(AspectPrefix) && arg.Length > 3)
                {
                    aspectConfigurations.Add(arg.Substring(3));
                }
                else if (arg.StartsWith(AdvicePrefix) && arg.Length > 3)
                {
                    adviceConfigurations.Add(arg.Substring(3));
                }
                else if (arg.StartsWith(TargetPrefix) && arg.Length > 3)
                {
                    targetConfigurations.Add(arg.Substring(3));
                }
                else
                {
                    throw new ApplicationException($"invalid argument format {arg}");
                }
            }
        }
    }
}
