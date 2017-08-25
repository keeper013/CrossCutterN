// <copyright file="SwitchInitializingData.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Switch
{
    using System;
    using Mono.Cecil;

    /// <summary>
    /// Data used to initialize switch information.
    /// </summary>
    internal sealed class SwitchInitializingData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SwitchInitializingData"/> class.
        /// </summary>
        /// <param name="property">Property that the aspect switch is injected to.</param>
        /// <param name="methodSignature">Signature of method that the aspect switch is injected to.</param>
        /// <param name="aspect">Aspect name of the swtich.</param>
        /// <param name="field">Field name of the aspect switch.</param>
        /// <param name="value">Value of the switch.</param>
        internal SwitchInitializingData(string property, string methodSignature, string aspect, FieldReference field, bool value)
        {
#if DEBUG
            if (string.IsNullOrWhiteSpace(methodSignature))
            {
                throw new ArgumentNullException("method");
            }

            if (string.IsNullOrWhiteSpace(aspect))
            {
                throw new ArgumentNullException("aspect");
            }
#endif
            Property = property;
            MethodSignature = methodSignature;
            Aspect = aspect;
            Field = field ?? throw new ArgumentNullException("field");
            Value = value;
        }

        /// <summary>
        /// Gets property that the aspect switch is injected to.
        /// </summary>
        public string Property { get; private set; }

        /// <summary>
        /// Gets signature of method that the aspect switch is injected to.
        /// </summary>
        public string MethodSignature { get; private set; }

        /// <summary>
        /// Gets aspect name of the switch.
        /// </summary>
        public string Aspect { get; private set; }

        /// <summary>
        /// Gets field name of the aspect switch.
        /// </summary>
        public FieldReference Field { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the aspect switch is turned on.
        /// </summary>
        public bool Value { get; private set; }
    }
}
