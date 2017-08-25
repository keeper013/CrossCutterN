// <copyright file="Parameter.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Metadata
{
    using System;
    using System.Collections.Generic;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Parameter metadata implementation.
    /// </summary>
    internal sealed class Parameter : IParameter, ICanAddCustomAttribute
    {
        private readonly List<ICustomAttribute> customAttributes = new List<ICustomAttribute>();
        private readonly IrreversibleOperation readOnly = new IrreversibleOperation();

        /// <summary>
        /// Initializes a new instance of the <see cref="Parameter"/> class.
        /// </summary>
        /// <param name="name">Name of parameter.</param>
        /// <param name="typeName">Type name of parameter.</param>
        /// <param name="sequence">Sequence of parameter in the method.</param>
        internal Parameter(string name, string typeName, int sequence)
        {
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

            Name = name;
            TypeName = typeName;
            Sequence = sequence;
        }

        /// <inheritdoc/>
        public string Name { get; private set; }

        /// <inheritdoc/>
        public string TypeName { get; private set; }

        /// <inheritdoc/>
        public int Sequence { get; private set; }

        /// <inheritdoc/>
        public string Key => Name;

        /// <inheritdoc/>
        public int SortKey => Sequence;

        /// <inheritdoc/>
        public IReadOnlyCollection<ICustomAttribute> CustomAttributes
        {
            get
            {
                readOnly.Assert(true);
                return customAttributes.AsReadOnly();
            }
        }

        /// <inheritdoc/>
        public void AddCustomAttribute(ICustomAttribute attribute)
        {
            if (attribute == null)
            {
                throw new ArgumentNullException("attribute");
            }

            readOnly.Assert(false);
            customAttributes.Add(attribute);
        }

        /// <inheritdoc/>
        public IParameter Build()
        {
            readOnly.Apply();
            return this;
        }
    }
}
