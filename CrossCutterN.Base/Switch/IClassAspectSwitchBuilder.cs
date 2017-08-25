// <copyright file="IClassAspectSwitchBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Switch
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface of a class indexer of switch list being built up.
    /// </summary>
    internal interface IClassAspectSwitchBuilder
    {
        /// <summary>
        /// Registers a switch.
        /// </summary>
        /// <param name="id">Id of the switch.</param>
        /// <param name="propertyName">Name of the property injected.</param>
        /// <param name="methodSignature">Signature of method injected.</param>
        /// <param name="aspect">Name of aspect to be switched.</param>
        void RegisterSwitch(int id, string propertyName, string methodSignature, string aspect);

        /// <summary>
        /// Builds to interface <see cref="IClassAspectSwitch"/>.
        /// </summary>
        /// <param name="clazz">Name of the class.</param>
        /// <param name="classOperations">Operation records before the class is loaded of the class</param>
        /// <param name="aspectOperations">Operation records before the class is loaded of the aspect.</param>
        /// <returns>Built class indexer of switch list.</returns>
        IClassAspectSwitch Build(string clazz, IClassAspectSwitchOperation classOperations, Dictionary<string, SwitchOperationStatus> aspectOperations);
    }
}
