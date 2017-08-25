// <copyright file="IReturnBuilderReferenceBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Metadata
{
    using System;
    using System.Reflection;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Metadata.IReturnBuilder"/> interface to be built up.
    /// </summary>
    internal interface IReturnBuilderReferenceBuilder : IBuilder<IReturnBuilderReference>
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
        /// Sets reference to setter method of HasReturn property.
        /// </summary>
        MethodInfo HasReturnSetter { set; }

        /// <summary>
        /// Sets reference to setter method of Value property.
        /// </summary>
        MethodInfo ValueSetter { set; }

        /// <summary>
        /// Sets reference to Build method.
        /// </summary>
        MethodInfo BuildMethod { set; }
    }
}
