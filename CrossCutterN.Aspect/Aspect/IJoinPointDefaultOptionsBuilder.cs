// <copyright file="IJoinPointDefaultOptionsBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Aspect
{
    /// <summary>
    /// These flags are default behaviors which may be overwritten by implementation
    /// </summary>
    public interface IJoinPointDefaultOptionsBuilder
    {
        /// <summary>
        /// Sets a value indicating whether constructors are concerned.
        /// </summary>
        bool ConcernConstructor { set; }

        /// <summary>
        /// Sets a value indicating whether instance methods and properties are concerned.
        /// </summary>
        bool ConcernInstance { set; }

        /// <summary>
        /// Sets a value indicating whether internal methods and properties are concerned.
        /// </summary>
        bool ConcernInternal { set; }

        /// <summary>
        /// Sets a value indicating whether methods are concerned.
        /// </summary>
        bool ConcernMethod { set; }

        /// <summary>
        /// Sets a value indicating whether property getters are concerned.
        /// </summary>
        bool ConcernPropertyGetter { set; }

        /// <summary>
        /// Sets a value indicating whether property setters are concerned.
        /// </summary>
        bool ConcernPropertySetter { set; }

        /// <summary>
        /// Sets a value indicating whether private methods and properties are concerned.
        /// </summary>
        bool ConcernPrivate { set; }

        /// <summary>
        /// Sets a value indicating whether protected methods and properties are concerned.
        /// </summary>
        bool ConcernProtected { set; }

        /// <summary>
        /// Sets a value indicating whether public methods and properties are concerned.
        /// </summary>
        bool ConcernPublic { set; }

        /// <summary>
        /// Sets a value indicating whether static methods and properties are concerned.
        /// </summary>
        bool ConcernStatic { set; }
    }
}
