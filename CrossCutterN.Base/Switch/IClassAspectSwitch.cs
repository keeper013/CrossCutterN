// <copyright file="IClassAspectSwitch.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Switch
{
    /// <summary>
    /// Indexer for easily switching AOP method calls via class, member methods and properties.
    /// </summary>
    internal interface IClassAspectSwitch
    {
        /// <summary>
        /// Check if an aspect has been applied to this class.
        /// </summary>
        /// <param name="aspect">Name of the aspect.</param>
        /// <returns>True if the aspect is applied to this class, false if not.</returns>
        bool IsAspectApplied(string aspect);

        /// <summary>
        /// Switches all aspects injected into this class according to operation parameter input.
        /// </summary>
        /// <param name="operation">Switch operation, <see cref="SwitchOperation"/>.</param>
        /// <returns>Numer of aspects affected.</returns>
        int Switch(SwitchOperation operation);

        /// <summary>
        /// Switches all aspects injected into the method of the class with the method signature according to operation parameter input.
        /// </summary>
        /// <param name="methodSignature">Signature of method that aspects are injected into.</param>
        /// <param name="operation">Switch operation, <see cref="SwitchOperation"/>.</param>
        /// <returns>Numer of aspects affected.</returns>
        int SwitchMethod(string methodSignature, SwitchOperation operation);

        /// <summary>
        /// Switches all aspects injected into the property of the class with the property name according to operation parameter input.
        /// </summary>
        /// <param name="propertyName">Name or property that aspects are injected into.</param>
        /// <param name="operation">Switch operation, <see cref="SwitchOperation"/>.</param>
        /// <returns>Numer of aspects affected.</returns>
        int SwitchProperty(string propertyName, SwitchOperation operation);

        /// <summary>
        /// Switches an aspect injected into the class according to operation parameter input.
        /// </summary>
        /// <param name="aspect">Name of the aspect to be switched.</param>
        /// <param name="operation">Switch operation, <see cref="SwitchOperation"/>.</param>
        /// <returns>Numer of aspects affected.</returns>
        int SwitchAspect(string aspect, SwitchOperation operation);

        /// <summary>
        /// Switches an aspects injected into a method of the class with the method signature according to operation parameter input.
        /// </summary>
        /// <param name="methodSignature">Signature of the method that the aspect is injected into.</param>
        /// <param name="aspect">Name of the aspect to be switched.</param>
        /// <param name="operation">Switch operation, <see cref="SwitchOperation"/>.</param>
        /// <returns>Numer of aspects affected.</returns>
        int SwitchMethodAspect(string methodSignature, string aspect, SwitchOperation operation);

        /// <summary>
        /// Switches an aspects injected into a property of the class with the property name according to operation parameter input.
        /// </summary>
        /// <param name="propertyName">Name of the property that the aspect is injected into.</param>
        /// <param name="aspect">Name of the aspect to be switched.</param>
        /// <param name="operation">Switch operation, <see cref="SwitchOperation"/>.</param>
        /// <returns>Numer of aspects affected.</returns>
        int SwitchPropertyAspect(string propertyName, string aspect, SwitchOperation operation);

        /// <summary>
        /// Looks up a aspect switch status in this indexer
        /// </summary>
        /// <param name="methodSignature">Signature of the method that the aspect is injected into.</param>
        /// <param name="aspect">Name of the aspect to be looked up.</param>
        /// <returns>
        ///     Null for the aspect isn't injected into the method or the aspect injected into the method is not switchable.
        ///     True if the aspect injected to the method is switched on, false elsewise.
        /// </returns>
        bool? Lookup(string methodSignature, string aspect);
    }
}
