// <copyright file="IMethodBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Metadata
{
    using System.Collections.Generic;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Method metadata to be built interface.
    /// </summary>
    public interface IMethodBuilder : IBuilder<IMethod>
    {
        /// <summary>
        /// Sets custom attributes from the class that contains this method.
        /// </summary>
        IReadOnlyCollection<ICustomAttribute> ClassCustomAttributes { set; }

        /// <summary>
        /// Adds a parameter metadata to the method metadata.
        /// </summary>
        /// <param name="parameter">The parameter metadata to be added.</param>
        void AddParameter(IParameter parameter);

        /// <summary>
        /// Adds a custom attribute metadata to the method metadata.
        /// </summary>
        /// <param name="attribute">The custom attribute to be added.</param>
        void AddCustomAttribute(ICustomAttribute attribute);
    }
}
