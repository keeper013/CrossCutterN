// <copyright file="IPropertyBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Metadata
{
    using System.Collections.Generic;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Property metadata to be built.
    /// </summary>
    public interface IPropertyBuilder : IBuilder<IProperty>
    {
        /// <summary>
        /// Sets custom attributes from class.
        /// </summary>
        IReadOnlyCollection<ICustomAttribute> ClassCustomAttributes { set; }

        /// <summary>
        /// Adds a custom attribute to the property.
        /// </summary>
        /// <param name="attribute">Custom attribute to be added.</param>
        void AddCustomAttribute(ICustomAttribute attribute);

        /// <summary>
        /// Adds a custom attribute to the property getter.
        /// </summary>
        /// <param name="attribute">Custom attribute to be added.</param>
        void AddGetterCustomAttribute(ICustomAttribute attribute);

        /// <summary>
        /// Adds a custom attribute to the property setter.
        /// </summary>
        /// <param name="attribute">Custom attribute to be added.</param>
        void AddSetterCustomAttribute(ICustomAttribute attribute);
    }
}
