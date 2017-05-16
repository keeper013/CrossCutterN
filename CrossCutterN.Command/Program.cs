/**
 * Description: Command line tool for CrossCutterN
 * Author: David Cui
 */

namespace CrossCutterN.Command
{
    using System;
    using System.Configuration;
    using System.IO;
    using Configuration;
    using Weaver;

    class Program
    {
        public const string LogFileName = "Weave_{0}.log";
        public const string SectionName = "crossCutterN";

        static void Main(string[] args)
        {
            if(args.Length != 2)
            {
                Console.Out.WriteLine("Usage: Weave aspects into .NET assemblies");
                Console.Out.WriteLine("Usage: {0} <input assembly path> <output assembly path>");
                Console.Out.WriteLine("Example: {0} C:\test.dll test_weaved.dll");
                return;
            }
            var config = (CrossCutterNSection)ConfigurationManager.GetSection("crossCutterN");
            var weaverToBuild = WeaverFactory.InitializeWeaver();
            var concernAttributeAspectBuilderCount = config.ConcernAttributeAspectBuilders.Count;
            for (var i = 0; i < concernAttributeAspectBuilderCount; i++)
            {
                weaverToBuild.AddBuilder(config.ConcernAttributeAspectBuilders[i]);
            }
            var nameExpressionAspectBuilderCount = config.NameExpressionAspectBuilders.Count;
            for (var i = 0; i < nameExpressionAspectBuilderCount; i++)
            {
                weaverToBuild.AddBuilder(config.NameExpressionAspectBuilders[i]);
            }
            var weaver = weaverToBuild.ToReadOnly();
            var inputAssembly = args[0];
            var outputAssembly = args[1];
            Console.Out.WriteLine("Starting to load {0}, weaving into {1}", inputAssembly, outputAssembly);
            var statistics = weaver.Weave(inputAssembly, outputAssembly);
            var fileName = string.Format(LogFileName, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff"));
            using (var file = new StreamWriter(fileName, true))
            {
                statistics.Log(file);
                file.Flush();
                file.Close();
            }
            Console.Out.WriteLine("Weaving finished, statistics logged into {0}", fileName);
        }
    }
}
