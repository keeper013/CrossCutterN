// <copyright file="IMetadataFactoryReference.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference.Base.Metadata
{
    using Mono.Cecil;

    /// <summary>
    /// Reference to <see cref="CrossCutterN.Base.Metadata.MetadataFactory"/> class.
    /// </summary>
    internal interface IMetadataFactoryReference
    {
        /// <summary>
        /// Gets reference to InitializeExecution method.
        /// </summary>
        MethodReference InitializeExecutionMethod { get; }

        /// <summary>
        /// Gets reference to InitializeExecutionContext method.
        /// </summary>
        MethodReference InitializeExecutionContextMethod { get; }

        /// <summary>
        /// Gets reference to InitializeParameter method.
        /// </summary>
        MethodReference InitializeParameterMethod { get; }

        /// <summary>
        /// Gets reference to InitializeCustomAttribute method.
        /// </summary>
        MethodReference InitializeCustomAttributeMethod { get; }

        /// <summary>
        /// Gets reference to InitializeAttributeProperty method.
        /// </summary>
        MethodReference InitializeAttributePropertyMethod { get; }

        /// <summary>
        /// Gets reference to InitializeReturn method.
        /// </summary>
        MethodReference InitializeReturnMethod { get; }
    }
}
