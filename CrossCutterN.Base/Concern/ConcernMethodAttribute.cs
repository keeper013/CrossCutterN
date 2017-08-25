// <copyright file="ConcernMethodAttribute.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Concern
{
    using System;

    /// <summary>
    /// Base attribute for concerning method and property getter/setter methods. It is declared to be abstract to force users to use customized attributes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public abstract class ConcernMethodAttribute : Attribute
    {
    }
}
