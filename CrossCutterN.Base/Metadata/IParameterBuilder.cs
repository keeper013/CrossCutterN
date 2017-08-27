// <copyright file="IParameterBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Metadata
{
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Parameter metadata being built up.
    /// </summary>
    public interface IParameterBuilder : IBuilder<IParameter>
    {
        /// <summary>
        /// Adds a custom attribute metadata.
        /// </summary>
        /// <param name="attribute">Custom attribute metadata to be added.</param>
        void AddCustomAttribute(ICustomAttribute attribute);
    }
}
