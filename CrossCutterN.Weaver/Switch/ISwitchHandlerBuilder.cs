// <copyright file="ISwitchHandlerBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Switch
{
    using CrossCutterN.Base.Common;
    using Mono.Cecil;

    /// <summary>
    /// Switch handler to be built up.
    /// </summary>
    internal interface ISwitchHandlerBuilder : IBuilder<ISwitchHandler>
    {
        /// <summary>
        /// Gets the field reference of the switch, the reference will be created if it doesn't exist.
        /// </summary>
        /// <param name="property">Property that the aspect switch is in.</param>
        /// <param name="methodSignature">Signature of method that the aspect switch is in.</param>
        /// <param name="aspect">Name of the aspect of the switch.</param>
        /// <param name="value">Value of the switch, if the switch doesn't exist, this value will only be applied if the reference doesn't exist.</param>
        /// <returns>The field reference reteieved.</returns>
        FieldReference GetSwitchField(string property, string methodSignature, string aspect, bool value);
    }
}
