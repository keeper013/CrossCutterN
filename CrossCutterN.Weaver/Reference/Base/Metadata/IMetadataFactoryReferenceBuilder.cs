// <copyright file="IMetadataFactoryReferenceBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Metadata
{
    using System.Reflection;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Metadata.MetadataFactory"/> class to be built up.
    /// </summary>
    internal interface IMetadataFactoryReferenceBuilder : IBuilder<IMetadataFactoryReference>
    {
        /// <summary>
        /// Sets reference to InitializeExecution method.
        /// </summary>
        MethodInfo InitializeExecutionMethod { set; }

        /// <summary>
        /// Sets reference to InitializeExecutionContext method.
        /// </summary>
        MethodInfo InitializeExecutionContextMethod { set; }

        /// <summary>
        /// Sets reference to InitializeParameter method.
        /// </summary>
        MethodInfo InitializeParameterMethod { set; }

        /// <summary>
        /// Sets reference to InitializeCustomAttribute method.
        /// </summary>
        MethodInfo InitializeCustomAttributeMethod { set; }

        /// <summary>
        /// Sets reference to InitializeAttributeProperty method.
        /// </summary>
        MethodInfo InitializeAttributePropertyMethod { set; }

        /// <summary>
        /// Sets reference to InitializeReturn method.
        /// </summary>
        MethodInfo InitializeReturnMethod { set; }
    }
}
