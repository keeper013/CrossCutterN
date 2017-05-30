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
    using Weaver.Utilities;

    class Program
    {
        public const string LogFileName = "Weave_{0}.log";
        public const string SectionName = "crossCutterN";

        static void Main(string[] args)
        {
            if(args.Length < 2 && 3 < args.Length)
            {
                Console.Out.WriteLine("Usage: Weave aspects into .NET assemblies");
                Console.Out.WriteLine("Usage: {0} <input assembly path> <output assembly path> <includeSymbol>(optional, Y/N, default is N)");
                Console.Out.WriteLine("Example: {0} C:\test.dll C:\test_weaved.dll");
                Console.Out.WriteLine("Example: {0} C:\test.dll C:\test_weaved.dll Y");
                return;
            }
            var inputAssembly = args[0];
            var outputAssembly = args[1];
            if (string.Equals(inputAssembly, outputAssembly))
            {
                throw new ArgumentException("It's not encouraged to directly overwrite input assembly with output assembly.");
            }
            var includeSymbol = (args.Length == 3) && args[2].Equals("Y");

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
            var weaver = weaverToBuild.Convert();
            Console.Out.WriteLine("Starting to load {0}, weaving into {1}", inputAssembly, outputAssembly);
            var statistics = weaver.Weave(inputAssembly, outputAssembly, includeSymbol);
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
