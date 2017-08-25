// <copyright file="ICanAddAspect.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Weaver
{
    using System.Collections.Generic;
    using CrossCutterN.Aspect.Aspect;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Weaver to be built up.
    /// </summary>
    /// <typeparam name="T">The Weaver type after built up.</typeparam>
    public interface ICanAddAspect<T> : IBuilder<T>
        where T : class
    {
        /// <summary>
        /// Adds and aspect into the weaver.
        /// </summary>
        /// <param name="aspectName">Name of the aspect, also used as key of the aspect.</param>
        /// <param name="aspect">The aspect to be added.</param>
        /// <param name="sequenceDict">Sequence dictionary for the advices of this aspect.</param>
        void AddAspect(string aspectName, IAspect aspect, IReadOnlyDictionary<JoinPoint, int> sequenceDict);
    }
}
