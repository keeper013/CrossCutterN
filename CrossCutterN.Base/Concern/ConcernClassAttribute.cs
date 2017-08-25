// <copyright file="ConcernClassAttribute.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Concern
{
    using System;

    /// <summary>
    /// Base attribute used for class concern. It is declared to be abstract to force users to use customized attributes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public abstract class ConcernClassAttribute : Attribute
    {
        /// <summary>
        /// Property name for concerning property getters
        /// </summary>
        public static readonly string ConcernPropertyGetterPropertyName = "ConcernPropertyGetter";

        /// <summary>
        /// Property name for concerning property setters
        /// </summary>
        public static readonly string ConcernPropertySetterPropertyName = "ConcernPropertySetter";

        /// <summary>
        /// Property name for concerning method
        /// </summary>
        public static readonly string ConcernMethodPropertyName = "ConcernMethod";

        /// <summary>
        /// Property name for concerning constructor
        /// </summary>
        public static readonly string ConcernConstructorPropertyName = "ConcernConstructor";

        /// <summary>
        /// Property name for concerning public methods and properties
        /// </summary>
        public static readonly string ConcernPublicPropertyName = "ConcernPublic";

        /// <summary>
        /// Property name for concerning private methods and properties
        /// </summary>
        public static readonly string ConcernPrivatePropertyName = "ConcernPrivate";

        /// <summary>
        /// Property name for concerning protected methods and properties
        /// </summary>
        public static readonly string ConcernProtectedPropertyName = "ConcernProtected";

        /// <summary>
        /// Property name for concerning internal methods and properties
        /// </summary>
        public static readonly string ConcernInternalPropertyName = "ConcernInternal";

        /// <summary>
        /// Property name for concerning static methods and properties
        /// </summary>
        public static readonly string ConcernStaticPropertyName = "ConcernStatic";

        /// <summary>
        /// Property name for concerning instance methods and properties
        /// </summary>
        public static readonly string ConcernInstancePropertyName = "ConcernInstance";

        /// <summary>
        /// Gets or sets a value indicating whether property getters are concerned
        /// </summary>
        public bool ConcernPropertyGetter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether property setters are concerned
        /// </summary>
        public bool ConcernPropertySetter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether methods are concerned
        /// </summary>
        public bool ConcernMethod { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether constructors are concerned
        /// </summary>
        public bool ConcernConstructor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether public methods and properties are concerned
        /// </summary>
        public bool ConcernPublic { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether private methods and properties are concerned
        /// </summary>
        public bool ConcernPrivate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether protected methods and properties are concerned
        /// </summary>
        public bool ConcernProtected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether internal methods and properties are concerned
        /// </summary>
        public bool ConcernInternal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether static methods and properties are concerned
        /// </summary>
        public bool ConcernStatic { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether instance methods and properties are concerned
        /// </summary>
        public bool ConcernInstance { get; set; }
    }
}
