// <copyright file="IProperty.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Metadata
{
    using System.Collections.Generic;

    /// <summary>
    /// Property metadata interface.
    /// </summary>
    public interface IProperty
    {
        /// <summary>
        /// Gets full name of assembly this property is defined in.
        /// </summary>
        string AssemblyFullName { get; }

        /// <summary>
        /// Gets namespace this property is defined in.
        /// </summary>
        string Namespace { get; }

        /// <summary>
        /// Gets full name of the class this property is defined in.
        /// </summary>
        string ClassFullName { get; }

        /// <summary>
        /// Gets name of the class this property is defined in.
        /// </summary>
        string ClassName { get; }

        /// <summary>
        /// Gets full name of the property.
        /// </summary>
        string PropertyFullName { get; }

        /// <summary>
        /// Gets name of the property.
        /// </summary>
        string PropertyName { get; }

        /// <summary>
        /// Gets type of the property.
        /// </summary>
        string Type { get; }

        /// <summary>
        /// Gets a value indicating whether the property is instance.
        /// </summary>
        bool IsInstance { get; }

        /// <summary>
        /// Gets getter function accessibility.
        /// </summary>
        Accessibility? GetterAccessibility { get; }

        /// <summary>
        /// Gets setter function accessibility.
        /// </summary>
        Accessibility? SetterAccessibility { get; }

        /// <summary>
        /// Gets class custom attributes.
        /// </summary>
        IReadOnlyCollection<ICustomAttribute> ClassCustomAttributes { get; }

        /// <summary>
        /// Gets property custom attributes.
        /// </summary>
        IReadOnlyCollection<ICustomAttribute> CustomAttributes { get; }

        /// <summary>
        /// Gets property getter custom attributes;
        /// </summary>
        IReadOnlyCollection<ICustomAttribute> GetterCustomAttributes { get; }

        /// <summary>
        /// Gets property setter custom attributes;
        /// </summary>
        IReadOnlyCollection<ICustomAttribute> SetterCustomAttributes { get; }
    }
}
