// <copyright file="SwitchFactory.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Switch
{
    using Mono.Cecil;

    /// <summary>
    /// Switch factory.
    /// </summary>
    internal static class SwitchFactory
    {
        /// <summary>
        /// Initializes a new instance of if <see cref="SwitchInitializingData"/>
        /// </summary>
        /// <param name="property">Property that the switch is injected into.</param>
        /// <param name="methodSignature">Signature of method that the switch is injected into.</param>
        /// <param name="aspect">Name of aspect of the switch.</param>
        /// <param name="field">Static field name of the switch.</param>
        /// <param name="value">Value of the switch.</param>
        /// <returns>The <see cref="SwitchInitializingData"/> initialized.</returns>
        public static SwitchInitializingData InitializeSwitchData(string property, string methodSignature, string aspect, FieldReference field, bool value) => new SwitchInitializingData(property, methodSignature, aspect, field, value);

        /// <summary>
        /// Initializes a new instance of of <see cref="ISwitchHandlerBuilder"/>.
        /// </summary>
        /// <param name="type">Class that the switch handler is for.</param>
        /// <param name="reference">Reference to the type field of the aspect switch.</param>
        /// <returns>The <see cref="ISwitchHandlerBuilder"/> initialized.</returns>
        public static ISwitchHandlerBuilder InitializeSwitchHandler(TypeDefinition type, TypeReference reference) => new SwitchHandler(type, reference);

        /// <summary>
        /// Initializes a new instance of of <see cref="ISwitchSet"/>.
        /// </summary>
        /// <returns>The <see cref="ISwitchSet"/> initialized.</returns>
        public static ISwitchSet InitializeSwitchSet() => new SwitchSet();

        /// <summary>
        /// Initializes a new instance of of <see cref="ISwitchableSection"/>.
        /// </summary>
        /// <returns>The <see cref="ISwitchableSection"/> initialized.</returns>
        public static ISwitchableSection InitializeSwitchableSection() => new SwitchableSection();
    }
}
