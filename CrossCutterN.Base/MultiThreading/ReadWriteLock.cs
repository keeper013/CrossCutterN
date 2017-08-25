// <copyright file="ReadWriteLock.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.MultiThreading
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// Read write lock implementation
    /// </summary>
    internal sealed class ReadWriteLock : IReadWriteLock
    {
        private readonly Dictionary<Thread, int> readThreads = new Dictionary<Thread, int>();
        private int writeAccessCount;
        private int writeRequestCount;
        private Thread writerThread;

        private bool IsBeingWritten => writerThread != null;

        private bool HasWriteRequests => writeRequestCount > 0;

        private bool HasReaders => readThreads.Count > 0;

        /// <inheritdoc/>
        public void AcquireReadLock()
        {
            var currentThread = Thread.CurrentThread;
            lock (this)
            {
                while (!MayRead(currentThread))
                {
                    Monitor.Wait(this);
                }

                if (readThreads.ContainsKey(currentThread))
                {
                    readThreads[currentThread] = readThreads[currentThread] + 1;
                }
                else
                {
                    readThreads[currentThread] = 1;
                }
            }
        }

        /// <inheritdoc/>
        public void AcquireWriteLock()
        {
            var currentThread = Thread.CurrentThread;
            lock (this)
            {
                writeRequestCount++;
                while (!MayWrite(currentThread))
                {
                    Monitor.Wait(this);
                }

                writeRequestCount--;
                writeAccessCount++;
                writerThread = currentThread;
            }
        }

        /// <inheritdoc/>
        public void ReleaseReadLock()
        {
            var currentThread = Thread.CurrentThread;
            lock (this)
            {
                if (!readThreads.ContainsKey(currentThread))
                {
                    throw new InvalidOperationException("The current thread doesn't have read lock to release.");
                }

                var value = readThreads[currentThread];
                if (value == 1)
                {
                    readThreads.Remove(currentThread);
                }
                else
                {
                    readThreads[currentThread] = value - 1;
                }

                Monitor.PulseAll(this);
            }
        }

        /// <inheritdoc/>
        public void ReleaseWriteLock()
        {
            var currentThread = Thread.CurrentThread;
            lock (this)
            {
                if (writerThread != currentThread)
                {
                    throw new InvalidOperationException("The current thread doesn't have write lock to release");
                }

                writeAccessCount--;
                if (writeAccessCount == 0)
                {
                    writerThread = null;
                }

                Monitor.PulseAll(this);
            }
        }

        private bool IsWriter(Thread thread) => thread == writerThread;

        private bool IsReader(Thread thread) => readThreads.ContainsKey(thread);

        private bool IsOnlyReader(Thread thread) => readThreads.Count == 1 && readThreads.ContainsKey(thread);

        private bool MayRead(Thread thread)
        {
            if (IsWriter(thread) || IsReader(thread))
            {
                return true;
            }

            return !IsBeingWritten && !HasWriteRequests;
        }

        private bool MayWrite(Thread thread)
        {
            if (IsWriter(thread) || IsOnlyReader(thread))
            {
                return true;
            }

            return !IsBeingWritten && !HasReaders;
        }
    }
}
