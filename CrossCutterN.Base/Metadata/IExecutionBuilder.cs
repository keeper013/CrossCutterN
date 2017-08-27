// <copyright file="IExecutionBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Metadata
{
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Method execution metadata being built up.
    /// </summary>
    public interface IExecutionBuilder : IBuilder<IExecution>
    {
        /// <summary>
        /// Adds a parameter to the method execution.
        /// </summary>
        /// <param name="parameter">Parameter to be added.</param>
        void AddParameter(IParameter parameter);
    }
}
