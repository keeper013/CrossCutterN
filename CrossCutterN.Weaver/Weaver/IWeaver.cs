// <copyright file="IWeaver.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Weaver
{
    using CrossCutterN.Weaver.Statistics;

    /// <summary>
    /// Weaver to weave new assemblies with methods injected.
    /// </summary>
    public interface IWeaver
    {
        /// <summary>
        /// Weaves a new assembly according to input assembly and output it according to output path.
        /// Currently Mono.Cecil implementation isn't complete, that we can't totally use stream to replace file name.
        /// So we use file name for interface parameter types.
        /// </summary>
        /// <param name="inputAssemblyPath">Path of input assembly.</param>
        /// <param name="includeSymbol">Whether to include symbol file (simply known as pdb files).</param>
        /// <param name="outputAssemblyPath">Output assembly path</param>
        /// <param name="strongNameKeyFile">Strong name key file.</param>
        /// <returns>Weaving statistics of the assembly.</returns>
        IAssemblyWeavingStatistics Weave(string inputAssemblyPath, bool includeSymbol, string outputAssemblyPath, string strongNameKeyFile);
    }
}
