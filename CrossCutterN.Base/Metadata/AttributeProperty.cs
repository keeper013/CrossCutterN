// <copyright file="AttributeProperty.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Metadata
{
    using System;

    /// <summary>
    /// Attribute property metadata implementation for properties of attributes.
    /// </summary>
    internal sealed class AttributeProperty : IAttributeProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeProperty"/> class.
        /// </summary>
        /// <param name="name">Name of the attribute property.</param>
        /// <param name="typeName">Type name of the attribute property.</param>
        /// <param name="sequence">Sequence of the attribute property in the attribute.</param>
        /// <param name="value">Value of the attribute property.</param>
        internal AttributeProperty(string name, string typeName, int sequence, object value)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }

            if (string.IsNullOrWhiteSpace(typeName))
            {
                throw new ArgumentNullException("typeName");
            }

            if (sequence < 0)
            {
                throw new ArgumentOutOfRangeException("sequence", "Sequence must be non-negative number.");
            }
#endif
            Name = name;
            TypeName = typeName;
            Sequence = sequence;
            Value = value;
        }

        /// <inheritdoc/>
        public string Name { get; private set; }

        /// <inheritdoc/>
        public string TypeName { get; private set; }

        /// <inheritdoc/>
        public object Value { get; private set; }

        /// <inheritdoc/>
        public int Sequence { get; private set; }

        /// <inheritdoc/>
        public string Key
        {
            get { return Name; }
        }

        /// <inheritdoc/>
        public int SortKey
        {
            get { return Sequence; }
        }
    }
}
