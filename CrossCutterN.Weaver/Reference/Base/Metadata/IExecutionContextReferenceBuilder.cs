// <copyright file="IExecutionContextReferenceBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Metadata
{
    using System;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Metadata.IExecutionContext"/> interface to be built up.
    /// </summary>
    internal interface IExecutionContextReferenceBuilder : IBuilder<IExecutionContextReference>
    {
        /// <summary>
        /// Sets reference to the readonly only interface type it can build to.
        /// </summary>
        Type TypeReference { set; }
    }
}
