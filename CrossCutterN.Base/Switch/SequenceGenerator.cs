// <copyright file="SequenceGenerator.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Switch
{
    /// <summary>
    /// Switch operation sequence generator
    /// </summary>
    internal sealed class SequenceGenerator
    {
        private int value;

        /// <summary>
        /// Gets next sequence number.
        /// </summary>
        /// <returns>Next sequence number.</returns>
        public int Next => ++value;
    }
}
