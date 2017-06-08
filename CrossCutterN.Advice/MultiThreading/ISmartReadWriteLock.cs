/**
* Description: smart read write lock interface
* Author: David Cui
*/

namespace CrossCutterN.Advice.MultiThreading
{
    internal interface ISmartReadWriteLock
    {
        ISmartLock ReadLock { get; }
        ISmartLock WriteLock { get; }
    }
}
