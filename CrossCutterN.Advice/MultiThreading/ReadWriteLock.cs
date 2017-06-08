/**
* Description: Read write lock implementation
* Author: David Cui
*/

using System;

namespace CrossCutterN.Advice.MultiThreading
{
    using System.Collections.Generic;
    using System.Threading;

    internal sealed class ReadWriteLock : IReadWriteLock
    {
        private int _writeAccessCount;
        private int _writeRequestCount;
        private Thread _writerThread;
        private readonly Dictionary<Thread, int> _readThreads = new Dictionary<Thread, int>();

        private bool IsBeingWritten
        {
            get { return _writerThread != null; }
        }

        private bool HasWriteRequests
        {
            get { return _writeRequestCount > 0; }
        }

        private bool HasReaders
        {
            get { return _readThreads.Count > 0; }
        }

        public void AcquireReadLock()
        {
            var currentThread = Thread.CurrentThread;
            lock (this)
            {
                while (!MayRead(currentThread))
                {
                    Monitor.Wait(this);
                }
                if (_readThreads.ContainsKey(currentThread))
                {
                    _readThreads[currentThread] = _readThreads[currentThread] + 1;
                }
                else
                {
                    _readThreads[currentThread] = 1;
                }
            }
        }

        public void AcquireWriteLock()
        {
            var currentThread = Thread.CurrentThread;
            lock (this)
            {
                _writeRequestCount++;
                while (!MayWrite(currentThread))
                {
                    Monitor.Wait(this);
                }
                _writeRequestCount--;
                _writeAccessCount++;
                _writerThread = currentThread;
            }
        }

        public void ReleaseReadLock()
        {
            var currentThread = Thread.CurrentThread;
            lock (this)
            {
                if (!_readThreads.ContainsKey(currentThread))
                {
                    throw new InvalidOperationException("The current thread doesn't have read lock to release.");
                }
                var value = _readThreads[currentThread];
                if (value == 1)
                {
                    _readThreads.Remove(currentThread);
                }
                else
                {
                    _readThreads[currentThread] = value - 1;
                }
                Monitor.PulseAll(this);
            }
        }

        public void ReleaseWriteLock()
        {
            var currentThread = Thread.CurrentThread;
            lock (this)
            {
                if (_writerThread != currentThread)
                {
                    throw new InvalidOperationException("The current thread doesn't have write lock to release");
                }
                _writeAccessCount--;
                if (_writeAccessCount == 0)
                {
                    _writerThread = null;
                }
                Monitor.PulseAll(this);
            }
        }

        private bool IsWriter(Thread thread)
        {
            return thread == _writerThread;
        }

        private bool IsReader(Thread thread)
        {
            return _readThreads.ContainsKey(thread);
        }

        private bool IsOnlyReader(Thread thread)
        {
            return _readThreads.Count == 1 && _readThreads.ContainsKey(thread);
        }

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
