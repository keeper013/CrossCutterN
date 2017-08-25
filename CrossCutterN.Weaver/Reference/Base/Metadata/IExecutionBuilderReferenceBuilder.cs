// <copyright file="IExecutionBuilderReferenceBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Metadata
{
    using System;
    using System.Reflection;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Metadata.IExecutionBuilder"/> interface to be built up.
    /// </summary>
    internal interface IExecutionBuilderReferenceBuilder : IBuilder<IExecutionBuilderReference>
    {
        /// <summary>
        /// Sets reference to the interface type.
        /// </summary>
        Type TypeReference { set; }

        /// <summary>
        /// Sets reference to the readonly only interface type it can build to.
        /// </summary>
        Type ReadOnlyTypeReference { set; }

        /// <summary>
        /// Sets reference to AddParameter method.
        /// </summary>
        MethodInfo AddParameterMethod { set; }

        /// <summary>
        /// Sets reference to Build method.
        /// </summary>
        MethodInfo BuildMethod { set; }
    }
}
