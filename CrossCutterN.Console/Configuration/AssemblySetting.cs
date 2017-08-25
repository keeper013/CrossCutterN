// <copyright file="AssemblySetting.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Console.Configuration
{
    /// <summary>
    /// Assembly setting configuration, for target assembly configuration.
    /// </summary>
    public sealed class AssemblySetting
    {
        /// <summary>
        /// Gets or sets input assembly path.
        /// </summary>
        public string Input { get; set; }

        /// <summary>
        /// Gets or sets output assembly path.
        /// </summary>
        public string Output { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether symbol file (pdb) should be included.
        /// </summary>
        public bool IncludeSymbol { get; set; }

        /// <summary>
        /// Gets or sets path of strong name key file.
        /// </summary>
        public string StrongNameKeyFile { get; set; }
    }
}
