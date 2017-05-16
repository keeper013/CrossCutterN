/**
 * Description: log weaving statistics
 * Author: David Cui
 */

namespace CrossCutterN.Command
{
    using System.IO;
    using Weaver.Statistics;

    public static class LogWeavingStatistics
    {
        public static void Log(this IAssemblyWeavingStatistics statistics, StreamWriter writer)
        {
            if (statistics.Exception != null)
            {
                writer.WriteLine(statistics.Exception.Message);
                writer.Write(statistics.Exception.StackTrace);
                writer.WriteLine();
                writer.WriteLine();
            }
            writer.WriteLine("Assembly Name: {0}", statistics.AssemblyName);
            writer.WriteLine("Weaved: {0} modules, {1} classes, {2} methods, {3} properties.",
                statistics.WeavedModuleCount, statistics.WeavedClassCount, statistics.WeavedMethodCount, statistics.WeavedPropertyCount);
            writer.WriteLine();
            const string moduleIndentation = "\t";
            const string classIndentation = "\t\t";
            const string methodPropertyIndentation = "\t\t\t";
            const string adviceIndentation = "\t\t\t\t";
            foreach (var mStatistics in statistics.ModuleWeavingStatistics)
            {
                writer.WriteLine("{0}Module: {1}", moduleIndentation, mStatistics.Name);
                writer.WriteLine("{0}Weaved {1} classes, {2} methods, {3} properties.",
                    moduleIndentation, mStatistics.WeavedClassCount, mStatistics.WeavedMethodCount, mStatistics.WeavedPropertyCount);
                writer.WriteLine();
                foreach (var cStatistics in mStatistics.ClassWeavingStatistics)
                {
                    writer.WriteLine("{0}Class: {1}", classIndentation, cStatistics.FullName);
                    writer.WriteLine("{0}Weaved {1} methods, {2} properties",
                        classIndentation, cStatistics.WeavedMethodCount, cStatistics.WeavedPropertyCount);
                    writer.WriteLine();
                    foreach (var meStatistics in cStatistics.MethodWeavingStatistics)
                    {
                        writer.WriteLine("{0}Method: {1}", methodPropertyIndentation, meStatistics.Signature);
                        writer.WriteLine("{0}{1} advices weaved", methodPropertyIndentation, meStatistics.JoinPointCount);
                        foreach (var record in meStatistics.Records)
                        {
                            writer.WriteLine("{0}{1} {2} {3} {4}",
                                adviceIndentation, record.JoinPoint, record.Sequence, record.MethodSignature, record.AspectBuilderId);
                        }
                        writer.WriteLine();
                    }

                    foreach (var pStatistics in cStatistics.PropertyWeavingStatistics)
                    {
                        writer.WriteLine("{0}Property: {1}", methodPropertyIndentation, pStatistics.FullName);
                        writer.WriteLine("{0}{1} advices weaved", methodPropertyIndentation, pStatistics.JoinPointCount);
                        writer.WriteLine("{0} Getter:", methodPropertyIndentation);
                        foreach (var record in pStatistics.GetterRecords)
                        {
                            writer.WriteLine("{0}{1} {2} {3} {4}",
                                adviceIndentation, record.JoinPoint, record.Sequence, record.MethodSignature, record.AspectBuilderId);
                        }
                        writer.WriteLine("{0} Setter:", methodPropertyIndentation);
                        foreach (var record in pStatistics.SetterRecords)
                        {
                            writer.WriteLine("{0}{1} {2} {3} {4}",
                                adviceIndentation, record.JoinPoint, record.Sequence, record.MethodSignature, record.AspectBuilderId);
                        }
                        writer.WriteLine();
                    }
                }
            }
        }
    }
}
