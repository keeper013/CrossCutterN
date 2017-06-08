/**
* Description: Read write lock interface
* Author: David Cui
*/

namespace CrossCutterN.Advice.MultiThreading
{
    /// <summary>
    /// Re-entrant, multi-reader, one writer, avoiding missed notification, avoiding spurious wake up
    /// on-going thread has the priority, write requests take priority to read requests
    /// </summary>
    internal interface IReadWriteLock
    {
        void AcquireReadLock();
        void AcquireWriteLock();
        void ReleaseReadLock();
        void ReleaseWriteLock();
    }
}
