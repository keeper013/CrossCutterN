// <copyright file="ICanAddAttributeProperty.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Metadata
{
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Interface for custom attribute metadata to be built.
    /// </summary>
    public interface ICanAddAttributeProperty : IBuilder<ICustomAttribute>
    {
        /// <summary>
        /// Adds an attribute property metadata to the custom attribute.
        /// </summary>
        /// <param name="property">The custom attribute metadata to be added.</param>
        void AddAttributeProperty(IAttributeProperty property);
    }
}
