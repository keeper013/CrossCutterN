/**
* Description: Lock factory
* Author: David Cui
*/

namespace CrossCutterN.Advice.MultiThreading
{
    internal static class LockFactory
    {
        public static IReadWriteLock InitializeReadWriteLock()
        {
            return new ReadWriteLock();
        }

        public static ISmartReadWriteLock InitializeSmartReadWriteLock()
        {
            return new SmartReadWriteLock(new ReadWriteLock());
        }
    }
}
