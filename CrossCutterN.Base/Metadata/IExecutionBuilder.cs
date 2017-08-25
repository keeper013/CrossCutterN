// <copyright file="IExecutionBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Metadata
{
    /// <summary>
    /// Method execution metadata being built up.
    /// </summary>
    public interface IExecutionBuilder
    {
        /// <summary>
        /// Adds a parameter to the method execution.
        /// </summary>
        /// <param name="parameter">Parameter to be added.</param>
        void AddParameter(IParameter parameter);

        /// <summary>
        /// Builds to <see cref="IExecution"/>, which is immutable.
        /// </summary>
        /// <returns>The <see cref="IExecution"/> built to.</returns>
        IExecution Build();
    }
}
