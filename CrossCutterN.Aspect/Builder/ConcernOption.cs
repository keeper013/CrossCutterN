// <copyright file="ConcernOption.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Builder
{
    /// <summary>
    /// Concern option enumeration.
    /// </summary>
    public enum ConcernOption
    {
        /// <summary>
        /// Concerns constructors.
        /// </summary>
        Constructor,

        /// <summary>
        /// Concerns instance methods and properties.
        /// </summary>
        Instance,

        /// <summary>
        /// Concerns internal methods and properties.
        /// </summary>
        Internal,

        /// <summary>
        /// Concerns methods.
        /// </summary>
        Method,

        /// <summary>
        /// Concerns instance property getters.
        /// </summary>
        PropertyGetter,

        /// <summary>
        /// Concerns instance property setters.
        /// </summary>
        PropertySetter,

        /// <summary>
        /// Concerns private methods and properties.
        /// </summary>
        Private,

        /// <summary>
        /// Concerns protected methods and properties.
        /// </summary>
        Protected,

        /// <summary>
        /// Concerns public methods and properties.
        /// </summary>
        Public,

        /// <summary>
        /// Concerns static methods and properties.
        /// </summary>
        Static,
    }
}