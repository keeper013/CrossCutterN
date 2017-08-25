// <copyright file="IMethod.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Metadata
{
    using System.Collections.Generic;

    /// <summary>
    /// Method metadata interface.
    /// </summary>
    public interface IMethod
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
        string MethodFullName { get; }

        /// <summary>
        /// Gets name of the method.
        /// </summary>
        string MethodName { get; }

        /// <summary>
        /// Gets return type of the method.
        /// </summary>
        string ReturnType { get; }

        /// <summary>
        /// Gets a value indicating whether the method is instance method.
        /// </summary>
        bool IsInstance { get; }

        /// <summary>
        /// Gets a value indicating whether the method is a constructor.
        /// </summary>
        bool IsConstructor { get; }

        /// <summary>
        /// Gets accessibility of the method.
        /// </summary>
        Accessibility Accessibility { get; }

        /// <summary>
        /// Gets custom attributes of the class that contains the method.
        /// </summary>
        IReadOnlyCollection<ICustomAttribute> ClassCustomAttributes { get; }

        /// <summary>
        /// Gets custom attributes of this method.
        /// </summary>
        IReadOnlyCollection<ICustomAttribute> CustomAttributes { get; }

        /// <summary>
        /// Gets parameters of this method.
        /// </summary>
        IReadOnlyCollection<IParameter> Parameters { get; }

        /// <summary>
        /// Gets a parameter of the name given from this method.
        /// </summary>
        /// <param name="name">Name of the method.</param>
        /// <returns>The <see cref="IParameter"/> retrieved.</returns>
        IParameter GetParameter(string name);

        /// <summary>
        /// Gets a value indicating whether this method contains a parameter with the given name.
        /// </summary>
        /// <param name="name">Name of parameter.</param>
        /// <returns>True if the method contains a parameter with the name, false elsewise.</returns>
        bool HasParameter(string name);
    }
}
