// <copyright file="IReadWriteLock.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.MultiThreading
{
    /// <summary>
    /// Re-entrant, multi-reader, one writer, avoiding missed notification, avoiding spurious wake up
    /// on-going thread has the priority, write requests take priority to read requests
    /// </summary>
    internal interface IReadWriteLock
    {
        /// <summary>
        /// Acquires read lock, wait if the read lock can't be acquired currently
        /// </summary>
        void AcquireReadLock();

        /// <summary>
        /// Acquires write lock, wait if the write lock can't be acquired currently
        /// </summary>
        void AcquireWriteLock();

        /// <summary>
        /// Releases read lock, only if a read lock has been acquired by the same thread previously
        /// </summary>
        void ReleaseReadLock();

        /// <summary>
        /// Releases write lock, only if a write lock has been acquired by the same thread previously
        /// </summary>
        void ReleaseWriteLock();
    }
}
