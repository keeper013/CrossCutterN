/**
* Description: smart read write lock
* Author: David Cui
*/

namespace CrossCutterN.Advice.MultiThreading
{
    using System;

    internal class SmartReadWriteLock : ISmartReadWriteLock
    {
        private readonly SmartReadLock _readLock;
        private readonly SmartWriteLock _writeLock;

        public SmartReadWriteLock(IReadWriteLock lck)
        {
            if (lck == null)
            {
                throw new ArgumentNullException("lck");
            }
            _readLock = new SmartReadLock(lck);
            _writeLock = new SmartWriteLock(lck);
        }

        public ISmartLock ReadLock
        {
            get { return _readLock.Refer(); }
        }
        public ISmartLock WriteLock
        {
            get { return _writeLock.Refer(); }
        }

        private class SmartReadLock : ISmartLock
        {
            private readonly IReadWriteLock _lock;

            public SmartReadLock(IReadWriteLock lck)
            {
                _lock = lck;
            }

            public ISmartLock Refer()
            {
                _lock.AcquireReadLock();
                return this;
            }

            public void Dispose()
            {
                _lock.ReleaseReadLock();
            }
        }

        private class SmartWriteLock : ISmartLock
        {
            private readonly IReadWriteLock _lock;

            public SmartWriteLock(IReadWriteLock lck)
            {
                _lock = lck;
            }

            public ISmartLock Refer()
            {
                _lock.AcquireWriteLock();
                return this;
            }

            public void Dispose()
            {
                _lock.ReleaseWriteLock();
            }
        }
    }
}
