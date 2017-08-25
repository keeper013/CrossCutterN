// <copyright file="ICustomAttribute.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Metadata
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
        /// Gets sequence of the custom attribute in the parameter.
        /// </summary>
        int Sequence { get; }

        /// <summary>
        /// Gets properties of the custom attribute.
        /// </summary>
        IReadOnlyCollection<IAttributeProperty> Properties { get; }

        /// <summary>
        /// Gets a property according to the property name.
        /// </summary>
        /// <param name="name">Name of the property to be retrieved.</param>
        /// <returns>The <see cref="IAttributeProperty"/> retrieved.</returns>
        IAttributeProperty GetProperty(string name);

        /// <summary>
        /// Gets a value indicating whether the custom attribute contains a property with the given name.
        /// </summary>
        /// <param name="name">Name of the property.</param>
        /// <returns>True if the custom attribute has the proeprty, false elsewise.</returns>
        bool HasProperty(string name);
    }
}
