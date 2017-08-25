// <copyright file="ConcernPropertyAttribute.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Concern
{
    using System;

    /// <summary>
    /// Base attribute for concerning properties. It is declared to be abstract to force users to use customized attributes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public abstract class ConcernPropertyAttribute : Attribute
    {
        /// <summary>
        /// Property name for concerning getter of the property
        /// </summary>
        public static readonly string ConcernGetterPropertyName = "ConcernGetter";

        /// <summary>
        /// Property name for concerning setter of the property
        /// </summary>
        public static readonly string ConcernSetterPropertyName = "ConcernSetter";

        /// <summary>
        /// Gets or sets a value indicating whether getter of the property is concerned
        /// </summary>
        public bool ConcernGetter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether setter of the property is concerned
        /// </summary>
        public bool ConcernSetter { get; set; }
    }
}
