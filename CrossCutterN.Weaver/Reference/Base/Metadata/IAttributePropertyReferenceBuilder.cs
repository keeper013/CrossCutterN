// <copyright file="IAttributePropertyReferenceBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Metadata
{
    using System;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Metadata.IAttributeProperty"/> interface to be built up.
    /// </summary>
    internal interface IAttributePropertyReferenceBuilder : IBuilder<IAttributePropertyReference>
    {
        /// <summary>
        /// Sets reference to the interface type.
        /// </summary>
        Type TypeReference { set; }
    }
}
