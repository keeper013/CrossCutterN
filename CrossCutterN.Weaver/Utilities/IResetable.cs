// <copyright file="IResetable.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Utilities
{
    /// <summary>
    /// Class that can be reset.
    /// </summary>
    internal interface IResetable
    {
        /// <summary>
        /// Resets the internal state of the class.
        /// </summary>
        void Reset();
    }
}
