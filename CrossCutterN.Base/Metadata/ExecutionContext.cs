// <copyright file="ExecutionContext.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Metadata
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Execution context implementation used to pass objects among AOP methods
    /// </summary>
    internal sealed class ExecutionContext : IExecutionContext
    {
        private readonly Dictionary<object, object> dictionary = new Dictionary<object, object>();

        /// <inheritdoc/>
        public void Set(object key, object value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            dictionary[key] = value;
        }

        /// <inheritdoc/>
        public bool Remove(object key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            return dictionary.Remove(key);
        }

        /// <inheritdoc/>
        public object Get(object key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            if (!dictionary.ContainsKey(key))
            {
                throw new ArgumentException($"Valus is not set for key {key}", "key");
            }

            return dictionary[key];
        }

        /// <inheritdoc/>
        public bool Exist(object key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            return dictionary.ContainsKey(key);
        }
    }
}
