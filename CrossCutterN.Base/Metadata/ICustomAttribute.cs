// <copyright file="ICustomAttribute.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Metadata
{
    using System.Collections.Generic;

    /// <summary>
    /// Custom attribute metadata interface.
    /// </summary>
    public interface ICustomAttribute
    {
        /// <summary>
        /// Gets type name of the custom attribute.
        /// </summary>
        string TypeName { get; }

        /// <summary>
        ///  Gets sequence of the custom attribute in the method or property.
        /// </summary>
        int Sequence { get; }

        /// <summary>
        /// Gets properties of the custom attribute.
        /// </summary>
        IReadOnlyCollection<IAttributeProperty> Properties { get; }

        /// <summary>
        /// Gets an attribute property by it's name.
        /// </summary>
        /// <param name="name">Name of the property.</param>
        /// <returns>Attribute property metadata retrieved.</returns>
        IAttributeProperty GetProperty(string name);

        /// <summary>
        /// Checks whether the custom attribute has the property with the name.
        /// </summary>
        /// <param name="name">Name of attribute property.</param>
        /// <returns>True if the custom attribute has the property with the name, false elsewise.</returns>
        bool HasProperty(string name);
    }
}
