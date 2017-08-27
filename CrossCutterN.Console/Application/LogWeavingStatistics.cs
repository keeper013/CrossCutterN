// <copyright file="LogWeavingStatistics.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Console.Application
{
    using System.IO;
    using CrossCutterN.Weaver.Statistics;

    /// <summary>
    /// Weaving statistics logger.
    /// </summary>
    internal static class LogWeavingStatistics
    {
        /// <summary>
        /// Logs weaving statistics of an assembly to a text writer.
        /// </summary>
        /// <param name="statistics">Weaving statistics of an assembly.</param>
        /// <param name="writer">Text writer.</param>
        public static void Log(this IAssemblyWeavingStatistics statistics, TextWriter writer)
        {
            if (statistics.Exception != null)
            {
                writer.WriteLine(statistics.Exception.Message);
                writer.WriteLine(statistics.Exception.StackTrace);
                writer.WriteLine();
            }

            writer.WriteLine($"Assembly Name: {statistics.AssemblyName}");
            writer.WriteLine($"Weaved: {statistics.WeavedModuleCount} modules, {statistics.WeavedClassCount} classes, {statistics.WeavedMethodCount} methods, {statistics.WeavedPropertyCount} properties, {statistics.WeavedSwitchCount} switches.");
            writer.WriteLine();
            const string moduleIndentation = "\t";
            const string classIndentation = "\t\t";
            const string methodPropertyIndentation = "\t\t\t";
            const string adviceIndentation = "\t\t\t\t";
            foreach (var mStatistics in statistics.ModuleWeavingStatistics)
            {
                writer.WriteLine($"{moduleIndentation}Module: {mStatistics.Name}");
                writer.WriteLine($"{moduleIndentation}Added {mStatistics.AddedAssemblyReferenceCount} assembly references:");
                foreach (var assembly in mStatistics.AddedAssemblyReferences)
                {
                    writer.WriteLine($"{classIndentation}{assembly}");
                }

                writer.WriteLine();
                writer.WriteLine($"{moduleIndentation}Weaved {mStatistics.WeavedClassCount} classes, {mStatistics.WeavedMethodCount} methods, {mStatistics.WeavedPropertyCount} properties, {mStatistics.WeavedSwitchCount} switches.");
                writer.WriteLine();
                foreach (var cStatistics in mStatistics.ClassWeavingStatistics)
                {
                    writer.WriteLine($"{classIndentation}Class: {cStatistics.FullName}");
                    writer.WriteLine($"{classIndentation}Weaved {cStatistics.WeavedMethodCount} methods, {cStatistics.WeavedPropertyCount} properties, {cStatistics.WeavedSwitchCount} switches");
                    writer.WriteLine();
                    foreach (var meStatistics in cStatistics.MethodWeavingStatistics)
                    {
                        writer.WriteLine($"{methodPropertyIndentation}Method: {meStatistics.Signature}");
                        writer.WriteLine($"{methodPropertyIndentation}{meStatistics.JoinPointCount} advices weaved");
                        foreach (var record in meStatistics.Records)
                        {
                            writer.WriteLine($"{adviceIndentation}{record.JoinPoint} {record.Sequence} {record.MethodSignature} {record.AAspectName}");
                        }

                        writer.WriteLine();
                    }

                    foreach (var pStatistics in cStatistics.PropertyWeavingStatistics)
                    {
                        writer.WriteLine($"{methodPropertyIndentation}Property: {pStatistics.FullName}");
                        writer.WriteLine($"{methodPropertyIndentation}{pStatistics.JoinPointCount} advices weaved");
                        writer.WriteLine($"{methodPropertyIndentation} Getter:");
                        foreach (var record in pStatistics.GetterRecords)
                        {
                            writer.WriteLine($"{adviceIndentation}{record.JoinPoint} {record.Sequence} {record.MethodSignature} {record.AAspectName}");
                        }

                        writer.WriteLine($"{methodPropertyIndentation} Setter:");
                        foreach (var record in pStatistics.SetterRecords)
                        {
                            writer.WriteLine($"{adviceIndentation}{record.JoinPoint} {record.Sequence} {record.MethodSignature} {record.AAspectName}");
                        }

                        writer.WriteLine();
                    }

                    if (cStatistics.SwitchWeavingRecords.Count > 0)
                    {
                        writer.WriteLine($"{methodPropertyIndentation}Switches:");
                        foreach (var record in cStatistics.SwitchWeavingRecords)
                        {
                            writer.WriteLine($"{adviceIndentation}{record.StaticFieldName}: {record.Class} {record.Property} {record.MethodSignature} {record.Aspect} {record.Value}");
                        }

                        writer.WriteLine();
                    }
                }
            }
        }
    }
}
