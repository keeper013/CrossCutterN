// <copyright file="SwitchWeavingRecord.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Statistics
{
    using System;

    /// <summary>
    /// Switch weaving record implementation.
    /// </summary>
    internal sealed class SwitchWeavingRecord : ISwitchWeavingRecord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SwitchWeavingRecord"/> class.
        /// </summary>
        /// <param name="clazz">Class that the aspect switch is injected in.</param>
        /// <param name="property">Property that the aspect switch is injected in.</param>
        /// <param name="methodSignature">Signature of method that the aspect switch is injected in.</param>
        /// <param name="aspect">Name of aspect of the switch.</param>
        /// <param name="field">Static field name of the aspect switch.</param>
        /// <param name="value">Value of the aspect switch.</param>
        public SwitchWeavingRecord(string clazz, string property, string methodSignature, string aspect, string field, bool value)
        {
#if DEBUG
            if (string.IsNullOrWhiteSpace(clazz))
            {
                throw new ArgumentNullException("clazz");
            }

            if (string.IsNullOrWhiteSpace(methodSignature))
            {
                throw new ArgumentNullException("methodSignature");
            }

            if (string.IsNullOrWhiteSpace(aspect))
            {
                throw new ArgumentNullException("aspect");
            }

            if (string.IsNullOrWhiteSpace(field))
            {
                throw new ArgumentNullException("field");
            }
#endif
            Class = clazz;
            Property = property;
            MethodSignature = methodSignature;
            Aspect = aspect;
            StaticFieldName = field;
            Value = value;
        }

        /// <inheritdoc/>
        public string Class { get; private set; }

        /// <inheritdoc/>
        public string Property { get; private set; }

        /// <inheritdoc/>
        public string MethodSignature { get; private set; }

        /// <inheritdoc/>
        public string Aspect { get; private set; }

        /// <inheritdoc/>
        public string StaticFieldName { get; private set; }

        /// <inheritdoc/>
        public bool Value { get; private set; }
    }
}
