// <copyright file="NoConcernAttribute.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Concern
{
    using System;

    /// <summary>
    /// Base attribute for marking methods/properties/properties getter methods/property setter methods not to be concerned. It is declared to be abstract to force users to use customized attributes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public abstract class NoConcernAttribute : Attribute
    {
    }
}
