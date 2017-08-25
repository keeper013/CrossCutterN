// <copyright file="IReturnBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Metadata
{
    /// <summary>
    /// Return value metadata being built.
    /// </summary>
    public interface IReturnBuilder
    {
        /// <summary>
        /// Sets a value indicating whether there is a return value, if the return type is void or an uncaught exception happened during the execution, this value will be false.
        /// </summary>
        bool HasReturn { set; }

        /// <summary>
        /// Sets value of the return value.
        /// </summary>
        object Value { set; }

        /// <summary>
        /// Builds to <see cref="IReturn"/>, which is immutable.
        /// </summary>
        /// <returns>The <see cref="IReturn"/> built to.</returns>
        IReturn Build();
    }
}
