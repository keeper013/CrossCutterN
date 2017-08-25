// <copyright file="LockFactory.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.MultiThreading
{
    /// <summary>
    /// Lock factory
    /// </summary>
    internal static class LockFactory
    {
        /// <summary>
        /// Gets an instnce of smart read write lock
        /// </summary>
        /// <returns>Smart read write lock interface</returns>
        public static ISmartReadWriteLock GetSmartReadWriteLock() => new SmartReadWriteLock(new ReadWriteLock());
    }
}
