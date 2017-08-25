// <copyright file="Return.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Metadata
{
    using System;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Return value metadata implementation.
    /// </summary>
    internal sealed class Return : IReturn, IReturnBuilder
    {
        private readonly IrreversibleOperation readOnly = new IrreversibleOperation();
        private bool hasReturn;
        private object value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Return"/> class.
        /// </summary>
        /// <param name="typeName">Type name of the return value.</param>
        internal Return(string typeName)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
            if (string.IsNullOrWhiteSpace(typeName))
            {
                throw new ArgumentNullException("typeName");
            }
#endif
            TypeName = typeName;
        }

        /// <inheritdoc/>
        public bool HasReturn
        {
            get => hasReturn;

            set
            {
                readOnly.Assert(false);
                hasReturn = value;
            }
        }

        /// <inheritdoc/>
        public string TypeName { get; private set; }

        /// <inheritdoc/>
        public object Value
        {
            get => value;

            set
            {
                readOnly.Assert(false);
                this.value = value;
            }
        }

        /// <inheritdoc/>
        public IReturn Build()
        {
            if (string.IsNullOrWhiteSpace(TypeName))
            {
                throw new InvalidOperationException("Type full name not set.");
            }

            readOnly.Apply();
            return this;
        }
    }
}
