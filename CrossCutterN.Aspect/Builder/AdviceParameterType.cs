// <copyright file="AdviceParameterType.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Builder
{
    /// <summary>
    /// Definition of advice parameter type.
    /// </summary>
    public enum AdviceParameterType
    {
        /// <summary>
        /// Context parameter.
        /// </summary>
        Context,

        /// <summary>
        /// Method execution parameter.
        /// </summary>
        Execution,

        /// <summary>
        /// Exception parameter.
        /// </summary>
        Exception,

        /// <summary>
        /// Return parameter.
        /// </summary>
        Return,

        /// <summary>
        /// Has exception parameter.
        /// </summary>
        HasException,
    }
}
