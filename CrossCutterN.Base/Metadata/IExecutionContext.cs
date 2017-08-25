// <copyright file="IExecutionContext.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Metadata
{
    /// <summary>
    /// Interface of execution context.
    /// </summary>
    public interface IExecutionContext
    {
        /// <summary>
        /// Set key value pair.
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        void Set(object key, object value);

        /// <summary>
        /// Remove value by key.
        /// </summary>
        /// <param name="key">Key of the value.</param>
        /// <returns>True if a value is removed, otherwise false.</returns>
        bool Remove(object key);

        /// <summary>
        /// Gets value according to its key.
        /// </summary>
        /// <param name="key">Key of the value.</param>
        /// <returns>Retrieved value.</returns>
        object Get(object key);

        /// <summary>
        /// Checks whether a key is set in the context.
        /// </summary>
        /// <param name="key">Key of value.</param>
        /// <returns>True if the key is set in the context, false elsewise.</returns>
        bool Exist(object key);
    }
}
