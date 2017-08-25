// <copyright file="ICustomAttributeBuilderReferenceBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Metadata
{
    using System;
    using System.Reflection;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Metadata.ICustomAttributeBuilder"/> interface to be built up.
    /// </summary>
    internal interface ICustomAttributeBuilderReferenceBuilder : IBuilder<ICustomAttributeBuilderReference>
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
        /// Sets reference to AddAttributeProperty method.
        /// </summary>
        MethodInfo AddAttributePropertyMethod { set; }

        /// <summary>
        /// Sets reference to Build method.
        /// </summary>
        MethodInfo BuildMethod { set; }
    }
}
