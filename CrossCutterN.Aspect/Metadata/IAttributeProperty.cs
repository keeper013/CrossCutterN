// <copyright file="IAttributeProperty.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Metadata
{
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Attribute property metadata interface.
    /// </summary>
    public interface IAttributeProperty : IHasId<string>, IHasSortKey<int>
    {
        /// <summary>
        /// Gets name of the attribute property.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets type name of the attribute property.
        /// </summary>
        string TypeName { get; }

        /// <summary>
        /// Gets sequence of the attribute property in the attribute.
        /// </summary>
        int Sequence { get; }

        /// <summary>
        /// Gets value of the attribute property.
        /// </summary>
        object Value { get; }
    }
}