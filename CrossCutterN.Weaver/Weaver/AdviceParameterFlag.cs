// <copyright file="AdviceParameterFlag.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Weaver
{
    using System;

    /// <summary>
    /// Flags to represent possible parameter types of CrossCutterN advices.
    /// </summary>
    [Flags]
    internal enum AdviceParameterFlag
    {
        /// <summary>
        /// No parameter
        /// </summary>
        None = 0,

        /// <summary>
        /// Parameter type of <see cref="CrossCutterN.Base.Metadata.IExecutionContext"/>.
        /// </summary>
        Context = 1,

        /// <summary>
        /// Parameter type of <see cref="CrossCutterN.Base.Metadata.IExecution"/>.
        /// </summary>
        Execution = 2,

        /// <summary>
        /// Parameter type of System.Exception.
        /// </summary>
        Exception = 4,

        /// <summary>
        /// Parameter type of <see cref="CrossCutterN.Base.Metadata.IReturn"/>.
        /// </summary>
        Return = 8,

        /// <summary>
        /// Parameter type of bool, used in finally block and indicates whether exception has occured.
        /// </summary>
        HasException = 16,
    }
}
