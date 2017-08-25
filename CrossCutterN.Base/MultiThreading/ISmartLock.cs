// <copyright file="ISmartLock.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.MultiThreading
{
    using System;

    /// <summary>
    /// Smart lock implementation, supposed to be working with using statement
    /// </summary>
    internal interface ISmartLock : IDisposable
    {
    }
}
