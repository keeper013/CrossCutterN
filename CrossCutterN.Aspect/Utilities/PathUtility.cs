// <copyright file="PathUtility.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Utilities
{
    using System.IO;

    /// <summary>
    /// Utility class to handle path related features.
    /// </summary>
    public static class PathUtility
    {
        /// <summary>
        /// Returns absolute path if input is relevant path.
        /// </summary>
        /// <param name="path">Path to be processed.</param>
        /// <returns>Absolute path.</returns>
        public static string ProcessPath(string path) => Path.IsPathRooted(path) ? path : $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}{path}";
    }
}
