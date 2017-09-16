// <copyright file="ICustomAttributeBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Metadata
{
    /// <summary>
    /// Interface for custom attribute metadata being built up.
    /// </summary>
    public interface ICustomAttributeBuilder
    {
        /// <summary>
        /// Adds property metadata to this attribute metadata.
        /// </summary>
        /// <param name="property">Property metadata to be added.</param>
        void AddAttributeProperty(IAttributeProperty property);

        /// <summary>
        /// Builds to <see cref="ICustomAttribute"/>.
        /// </summary>
        /// <returns>Built result.</returns>
        ICustomAttribute Build();
    }
}
