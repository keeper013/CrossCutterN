// <copyright file="PropertyConcern.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Aspect
{
    using System;

    /// <summary>
    /// Concern status for getter and setter of a property
    /// </summary>
    [Flags]
    public enum PropertyConcern
    {
        /// <summary>
        /// Not concerning getter or setter.
        /// </summary>
        None = 0,

        /// <summary>
        /// Concerns getter method.
        /// </summary>
        Getter = 1,

        /// <summary>
        /// Concerns setter method.
        /// </summary>
        Setter = 2,
    }
}
