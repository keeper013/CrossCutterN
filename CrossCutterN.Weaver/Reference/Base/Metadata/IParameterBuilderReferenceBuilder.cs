// <copyright file="IParameterBuilderReferenceBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Metadata
{
    using System;
    using System.Reflection;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Metadata.IParameterBuilder"/> interface to be built up.
    /// </summary>
    internal interface IParameterBuilderReferenceBuilder : IBuilder<IParameterBuilderReference>
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
        /// Sets reference to AddCustomAttribute method.
        /// </summary>
        MethodInfo AddCustomAttributeMethod { set; }

        /// <summary>
        /// Sets reference to Build method.
        /// </summary>
        MethodInfo BuildMethod { set; }
    }
}
