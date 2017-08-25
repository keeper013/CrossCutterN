// <copyright file="CustomAttribute.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Metadata
{
    using System;
    using System.Collections.Generic;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Custom attribute metadata implementation.
    /// </summary>
    internal sealed class CustomAttribute : ICustomAttribute, ICustomAttributeBuilder
    {
        private readonly StringIndexedIntSortedCollection<IAttributeProperty> properties =
            new StringIndexedIntSortedCollection<IAttributeProperty>();

        private readonly IrreversibleOperation readOnly = new IrreversibleOperation();

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAttribute"/> class.
        /// </summary>
        /// <param name="typeName">Type name of the custom attribute</param>
        /// <param name="sequence">Sequence of the custom attribute in the parameter</param>
        internal CustomAttribute(string typeName, int sequence)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
            if (string.IsNullOrWhiteSpace(typeName))
            {
                throw new ArgumentNullException("typeName");
            }

            if (sequence < 0)
            {
                throw new ArgumentOutOfRangeException("sequence", "Sequence must be non-negative number.");
            }
#endif
            TypeName = typeName;
            Sequence = sequence;
        }

        /// <inheritdoc/>
        public string TypeName { get; private set; }

        /// <inheritdoc/>
        public int Sequence { get; private set; }

        /// <inheritdoc/>
        public IReadOnlyCollection<IAttributeProperty> Properties => properties.All;

        /// <inheritdoc/>
        public IAttributeProperty GetProperty(string name) => properties.Get(name);

        /// <inheritdoc/>
        public bool HasProperty(string name) => properties.ContainsId(name);

        /// <inheritdoc/>
        public void AddAttributeProperty(IAttributeProperty property)
        {
            readOnly.Assert(false);
            properties.Add(property);
        }

        /// <inheritdoc/>
        public ICustomAttribute Build()
        {
            readOnly.Apply();
            return this;
        }
    }
}
