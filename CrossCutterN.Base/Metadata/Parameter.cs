// <copyright file="Parameter.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Metadata
{
    using System;
    using System.Collections.Generic;
    using Common;

    /// <summary>
    /// Parameter metadata implementation
    /// </summary>
    internal sealed class Parameter : IParameter, IParameterBuilder
    {
        private readonly List<ICustomAttribute> customAttributes = new List<ICustomAttribute>();
        private readonly IrreversibleOperation readOnly = new IrreversibleOperation();

        /// <summary>
        /// Initializes a new instance of the <see cref="Parameter"/> class.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <param name="typeName">Type name of the parameter.</param>
        /// <param name="sequence">Sequence of the parameter in method.</param>
        /// <param name="value">Value of the parameter.</param>
        internal Parameter(string name, string typeName, int sequence, object value)
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
        public string Key => Name;

        /// <inheritdoc/>
        public int SortKey => Sequence;

        /// <inheritdoc/>
        public IReadOnlyCollection<ICustomAttribute> CustomAttributes => customAttributes.AsReadOnly();

        /// <inheritdoc/>
        public void AddCustomAttribute(ICustomAttribute attribute)
        {
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
