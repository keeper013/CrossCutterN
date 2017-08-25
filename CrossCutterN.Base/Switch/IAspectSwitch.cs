// <copyright file="IAspectSwitch.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Switch
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Switch interface for CrossCutterN user to switch aspects
    /// </summary>
    public interface IAspectSwitch
    {
        /// <summary>
        /// Switches all aspects injected in a class, change switch status to Off if it's On, and change switch status to On if it's off.
        /// </summary>
        /// <param name="type">The class that aspects are injected in.</param>
        /// <returns>Number of aspects affected.</returns>
        int Switch(Type type);

        /// <summary>
        /// Switches all aspects injected in a method, change switch status to Off if it's On, and change switch status to On if it's off.
        /// </summary>
        /// <param name="method">The method that aspects are injected in.</param>
        /// <returns>Number of aspects affected.</returns>
        int Switch(MethodInfo method);

        /// <summary>
        /// Switches all aspects injected in a property, change switch status to Off if it's On, and change switch status to On if it's off.
        /// </summary>
        /// <param name="property">The property that aspects are injected in.</param>
        /// <returns>Number of aspects affected.</returns>
        int Switch(PropertyInfo property);

        /// <summary>
        /// Switches an aspect injected in all classes, change switch status to Off if it's On, and change switch status to On if it's off.
        /// </summary>
        /// <param name="aspect">Name of the aspect to be switched.</param>
        /// <returns>Number of aspects affected.</returns>
        int Switch(string aspect);

        /// <summary>
        /// Switches an aspect injected in a class, change switch status to Off if it's On, and change switch status to On if it's off.
        /// </summary>
        /// <param name="type">The class that the aspect is injected in.</param>
        /// <param name="aspect">Name of the aspect to be switched.</param>
        /// <returns>Number of aspects affected.</returns>
        int Switch(Type type, string aspect);

        /// <summary>
        /// Switches an aspect injected in a method, change switch status to Off if it's On, and change switch status to On if it's off.
        /// </summary>
        /// <param name="method">The method that the aspect is injected in.</param>
        /// <param name="aspect">Name of the aspect to be switched.</param>
        /// <returns>Number of aspects affected.</returns>
        int Switch(MethodInfo method, string aspect);

        /// <summary>
        /// Switches an aspect injected in a property, change switch status to Off if it's On, and change switch status to On if it's off.
        /// </summary>
        /// <param name="property">The property that the aspect is injected in.</param>
        /// <param name="aspect">Name of the aspect to be switched.</param>
        /// <returns>Number of aspects affected.</returns>
        int Switch(PropertyInfo property, string aspect);

        /// <summary>
        /// Switches on all aspects injected in a class, change switch status to On no matter what it is.
        /// </summary>
        /// <param name="type">The class that aspects are injected in.</param>
        /// <returns>Number of aspects affected.</returns>
        int SwitchOn(Type type);

        /// <summary>
        /// Switches on all aspects injected in a method, change switch status to On no matter what it is.
        /// </summary>
        /// <param name="method">The method that aspects are injected in.</param>
        /// <returns>Number of aspects affected.</returns>
        int SwitchOn(MethodInfo method);

        /// <summary>
        /// Switches on all aspects injected in a property, change switch status to On no matter what it is.
        /// </summary>
        /// <param name="property">The property that aspects are injected in.</param>
        /// <returns>Number of aspects affected.</returns>
        int SwitchOn(PropertyInfo property);

        /// <summary>
        /// Switches on an aspect injected in all classes, change switch status to On if it's On no matter what it is.
        /// </summary>
        /// <param name="aspect">Name of the aspect to be switched on.</param>
        /// <returns>Number of aspects affected.</returns>
        int SwitchOn(string aspect);

        /// <summary>
        /// Switches on an aspect injected in a class, change switch status to On no matter what it is.
        /// </summary>
        /// <param name="type">The class that the aspect is injected in.</param>
        /// <param name="aspect">Name of the aspect to be switched on.</param>
        /// <returns>Number of aspects affected.</returns>
        int SwitchOn(Type type, string aspect);

        /// <summary>
        /// Switch on an aspect injected in a method, change switch status to On no matter what it is.
        /// </summary>
        /// <param name="method">The method that the aspect is injected in.</param>
        /// <param name="aspect">Name of the aspect to be switched on.</param>
        /// <returns>Number of aspects affected.</returns>
        int SwitchOn(MethodInfo method, string aspect);

        /// <summary>
        /// Switches on an aspect injected in a property, change switch status to On no matter what it is.
        /// </summary>
        /// <param name="property">The property that the aspect is injected in.</param>
        /// <param name="aspect">Name of the aspect to be switched on.</param>
        /// <returns>Number of aspects affected.</returns>
        int SwitchOn(PropertyInfo property, string aspect);

        /// <summary>
        /// Switches off all aspects injected in a class, change switch status to Off no matter what it is.
        /// </summary>
        /// <param name="type">The class that aspects are injected in.</param>
        /// <returns>Number of aspects affected.</returns>
        int SwitchOff(Type type);

        /// <summary>
        /// Switches off all aspects injected in a class, change switch status to Off no matter what it is.
        /// </summary>
        /// <param name="method">The method that aspects are injected in.</param>
        /// <returns>Number of aspects affected.</returns>
        int SwitchOff(MethodInfo method);

        /// <summary>
        /// Switches off all aspects injected in a property, change switch status to Off no matter what it is.
        /// </summary>
        /// <param name="property">The property that aspects are injected in.</param>
        /// <returns>Number of aspects affected.</returns>
        int SwitchOff(PropertyInfo property);

        /// <summary>
        /// Switches off an aspect injected in all classes, change switch status to Off if it's On no matter what it is.
        /// </summary>
        /// <param name="aspect">Name of the aspect to be switched.</param>
        /// <returns>Number of aspects affected.</returns>
        int SwitchOff(string aspect);

        /// <summary>
        /// Switch off an aspect injected in a class, change switch status to Off no matter what it is.
        /// </summary>
        /// <param name="type">The class that the aspect is injected in.</param>
        /// <param name="aspect">Name of the aspect to be switched off.</param>
        /// <returns>Number of aspects affected.</returns>
        int SwitchOff(Type type, string aspect);

        /// <summary>
        /// Switches off an aspect injected in a class, change switch status to Off no matter what it is.
        /// </summary>
        /// <param name="method">The method that the aspect is injected in.</param>
        /// <param name="aspect">Name of the aspect to be switched off.</param>
        /// <returns>Number of aspects affected.</returns>
        int SwitchOff(MethodInfo method, string aspect);

        /// <summary>
        /// Switches off an aspect injected in a method, change switch status to Off no matter what it is.
        /// </summary>
        /// <param name="property">The property that the aspect is injected in.</param>
        /// <param name="aspect">Name of the aspect to be switched off.</param>
        /// <returns>Number of aspects affected.</returns>
        int SwitchOff(PropertyInfo property, string aspect);

        /// <summary>
        /// Gets switch status of an aspect in a method.
        /// Without clear requirements, this version doesn't support massive switch status lookup.
        /// This is the only lookup interface for user to check switch status for an aspect in a method.
        /// </summary>
        /// <param name="method">Method that aspect is injected to.</param>
        /// <param name="aspect">Name of aspect to be looked up.</param>
        /// <returns>Null if aspect isn't injected into method, or aspect is not switchable. True if aspect is switched on, false elsewise.</returns>
        bool? GetSwitchStatus(MethodInfo method, string aspect);
    }
}
