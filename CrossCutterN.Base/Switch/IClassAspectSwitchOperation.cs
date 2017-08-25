// <copyright file="IClassAspectSwitchOperation.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Switch
{
    /// <summary>
    /// Operation record for classes that are not loaded yet
    /// </summary>
    internal interface IClassAspectSwitchOperation
    {
        /// <summary>
        /// Switches all aspects injected into this class according to operation parameter input.
        /// </summary>
        /// <param name="operation">Switch operation, <see cref="SwitchOperation"/>.</param>
        void Switch(SwitchOperation operation);

        /// <summary>
        /// Switches all aspects injected into the method of the class with the method signature according to operation parameter input.
        /// </summary>
        /// <param name="methodSignature">Signature of method that aspects are injected into.</param>
        /// <param name="operation">Switch operation, <see cref="SwitchOperation"/>.</param>
        void SwitchMethod(string methodSignature, SwitchOperation operation);

        /// <summary>
        /// Switches all aspects injected into the property of the class which has the same signatures of input getter signagure and setter signature according to operation input.
        /// </summary>
        /// <param name="getterSignature">Getter method signature.</param>
        /// <param name="setterSignature">Setter method signature.</param>
        /// <param name="operation">Switch operation, <see cref="SwitchOperation"/>.</param>
        void SwitchProperty(string getterSignature, string setterSignature, SwitchOperation operation);

        /// <summary>
        /// Switches an aspect injected into the class according to operation parameter input.
        /// </summary>
        /// <param name="aspect">Name of the aspect to be switched.</param>
        /// <param name="operation">Switch operation, <see cref="SwitchOperation"/>.</param>
        void SwitchAspect(string aspect, SwitchOperation operation);

        /// <summary>
        /// Switches an aspect injected into the class according to operation parameter input.
        /// </summary>
        /// <param name="aspect">Name of the aspect to be switched.</param>
        /// <param name="operation">Switch operation, <see cref="SwitchOperation"/>.</param>
        /// <param name="sequence">Sequence of this switch operation, it matters for switch status overwrite calculation.</param>
        void SwitchAspect(string aspect, SwitchOperation operation, int sequence);

        /// <summary>
        /// Switches an aspects injected into a method of the class with the method signature according to operation parameter input.
        /// </summary>
        /// <param name="methodSignature">Signature of the method that the aspect is injected into.</param>
        /// <param name="aspect">Name of the aspect to be switched.</param>
        /// <param name="operation">Switch operation, <see cref="SwitchOperation"/>.</param>
        void SwitchMethodAspect(string methodSignature, string aspect, SwitchOperation operation);

        /// <summary>
        /// Switches an aspect injected into the property of the class which has the same signatures of input getter signagure and setter signature according to operation input.
        /// </summary>
        /// <param name="getterSignature">Getter method signature.</param>
        /// <param name="setterSignature">Setter method signature.</param>
        /// <param name="aspect">Name of the aspect to be switched.</param>
        /// <param name="operation">Switch operation, <see cref="SwitchOperation"/>.</param>
        void SwitchPropertyAspect(string getterSignature, string setterSignature, string aspect, SwitchOperation operation);

        /// <summary>
        /// Gets the switch status value of the given aspect injected to the method which has the input method signature.
        /// </summary>
        /// <param name="value">Default value of switch status.</param>
        /// <param name="methodSignature">Signature of the method which the aspect is injected into.</param>
        /// <param name="aspect">Name of the aspect the switch status of which is to be calculated.</param>
        /// <returns>True if switched on, false elsewise.</returns>
        bool GetSwitchValue(bool value, string methodSignature, string aspect);
    }
}
