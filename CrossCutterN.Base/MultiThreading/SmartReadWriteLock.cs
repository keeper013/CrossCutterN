// <copyright file="SmartReadWriteLock.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.MultiThreading
{
    using System;

    /// <summary>
    /// Smart read write lock implementation.
    /// This is a wrapper class for <see cref="IReadWriteLock"/> interface, the read locks and write locks acquired can be automatically disposed by using statement, so it helps to keep the code clean.
    /// </summary>
    internal sealed class SmartReadWriteLock : ISmartReadWriteLock
    {
        private readonly SmartReadLock readLock;
        private readonly SmartWriteLock writeLock;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartReadWriteLock"/> class.
        /// </summary>
        /// <param name="lck">Read write lock </param>
        public SmartReadWriteLock(IReadWriteLock lck)
        {
            if (lck == null)
            {
                throw new ArgumentNullException("lck");
            }

            readLock = new SmartReadLock(lck);
            writeLock = new SmartWriteLock(lck);
        }

        /// <inheritdoc/>
        public ISmartLock ReadLock => readLock.Refer();

        /// <inheritdoc/>
        public ISmartLock WriteLock => writeLock.Refer();

        private class SmartReadLock : ISmartLock
        {
            private readonly IReadWriteLock @lock;

            public SmartReadLock(IReadWriteLock lck) => @lock = lck;

            public ISmartLock Refer()
            {
                @lock.AcquireReadLock();
                return this;
            }

            public void Dispose() => @lock.ReleaseReadLock();
        }

        private class SmartWriteLock : ISmartLock
        {
            private readonly IReadWriteLock @lock;

            public SmartWriteLock(IReadWriteLock lck) => @lock = lck;

            public ISmartLock Refer()
            {
                @lock.AcquireWriteLock();
                return this;
            }

            public void Dispose() => @lock.ReleaseWriteLock();
        }
    }
}
