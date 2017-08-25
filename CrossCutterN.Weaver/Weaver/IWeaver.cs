// <copyright file="IWeaver.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Weaver
{
    using System.IO;
    using CrossCutterN.Weaver.Statistics;

    /// <summary>
    /// Weaver to weave new assemblies with methods injected.
    /// </summary>
    public interface IWeaver
    {
        /// <summary>
        /// Weaves a new assembly according to input assembly and output it according to output path.
        /// </summary>
        /// <param name="inputAssembly">Stream of input assembly.</param>
        /// <param name="outputAssembly">Stream of output assembly.</param>
        /// <param name="includeSymbol">Whether to include symbols (simply known as pdb files).</param>
        /// <param name="strongNameKeyFile">Strong name key file.</param>
        /// <returns>Weaving statistics of the assembly.</returns>
        IAssemblyWeavingStatistics Weave(Stream inputAssembly, Stream outputAssembly, bool includeSymbol, string strongNameKeyFile);
    }
}
