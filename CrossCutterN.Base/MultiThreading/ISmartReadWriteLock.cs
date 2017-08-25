// <copyright file="ISmartReadWriteLock.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.MultiThreading
{
    /// <summary>
    /// Smart read write lock interface
    /// </summary>
    internal interface ISmartReadWriteLock
    {
        /// <summary>
        /// Gets the read lock
        /// </summary>
        ISmartLock ReadLock { get; }

        /// <summary>
        /// Gets the write lock
        /// </summary>
        ISmartLock WriteLock { get; }
    }
}
