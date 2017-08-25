// <copyright file="IAspectSwitchBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Switch
{
    /// <summary>
    /// Interface of class indexer of switch list being built up.
    /// </summary>
    public interface IAspectSwitchBuilder
    {
        /// <summary>
        /// Registers an aspect switch in switch list.
        /// </summary>
        /// <param name="clazz">Class that the switched aspect injected in.</param>
        /// <param name="property">Property that the switched aspect injected in.</param>
        /// <param name="methodSignature">Signature of method that the switched aspect injected in.</param>
        /// <param name="aspect">Name of the aspect injected and to be switched.</param>
        /// <param name="value">Default switch status value.</param>
        /// <returns>Id of the registered switch.</returns>
        int RegisterSwitch(string clazz, string property, string methodSignature, string aspect, bool value);

        /// <summary>
        /// Completes switch registration for a class, after the completion, no switch can be registered for the class.
        /// </summary>
        /// <param name="clazz">Name of the class to complete switch registration.</param>
        void Complete(string clazz);
    }
}
