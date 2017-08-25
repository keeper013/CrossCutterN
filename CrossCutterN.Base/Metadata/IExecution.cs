// <copyright file="IExecution.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Metadata
{
    using System.Collections.Generic;

    /// <summary>
    /// Method execution metadata interface.
    /// </summary>
    public interface IExecution
    {
        /// <summary>
        /// Gets full name of assembly this method is defined in.
        /// </summary>
        string AssemblyFullName { get; }

        /// <summary>
        /// Gets namespace this method is defined in.
        /// </summary>
        string Namespace { get; }

        /// <summary>
        /// Gets full name of the class this method is defined in.
        /// </summary>
        string ClassFullName { get; }

        /// <summary>
        /// Gets name of the class this method is defined in.
        /// </summary>
        string ClassName { get; }

        /// <summary>
        /// Gets full name of the method.
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Gets name of the method.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets return type of the method.
        /// </summary>
        string ReturnType { get; }

        /// <summary>
        /// Gets metadata of all parameters of the method.
        /// </summary>
        IReadOnlyCollection<IParameter> Parameters { get; }

        /// <summary>
        /// Gets metadata of a parameter based on the parameter name.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <returns>Metadata of the parameter.</returns>
        IParameter GetParameter(string name);

        /// <summary>
        /// Checks if the method has a parameter with the name.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <returns>True if the method has the parameter with the name, false elsewise.</returns>
        bool HasParameter(string name);
    }
}
