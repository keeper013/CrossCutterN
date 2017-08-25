// <copyright file="IConfigurationProcessor.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Console.Application
{
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Interface of configuration processor
    /// </summary>
    internal interface IConfigurationProcessor
    {
        /// <summary>
        /// Processes configuration section.
        /// </summary>
        /// <param name="section">Configuration section to be configured.</param>
        /// <param name="configurationFullPath">Configuration file full path.</param>
        /// <param name="currentDirectory">Program work directory.</param>
        void Process(IConfigurationSection section, string configurationFullPath, string currentDirectory);
    }
}
