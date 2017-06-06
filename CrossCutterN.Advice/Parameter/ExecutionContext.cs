/**
* Description: method run time context implementation
* Author: David Cui
*/

namespace CrossCutterN.Advice.Parameter
{
    using System;
    using System.Collections.Generic;

    internal sealed class ExecutionContext : IExecutionContext
    {
        private readonly Dictionary<object, object> _dictionary = new Dictionary<object, object>();

        public void Set(object key, object value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            _dictionary[key] = value;
        }

        public bool Remove(object key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            return _dictionary.Remove(key);
        }

        public object Get(object key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (!_dictionary.ContainsKey(key))
            {
                throw new ArgumentException(string.Format("Valus is not set for key {0}", key), "key");
            }
            return _dictionary[key];
        }

        public bool Exist(object key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            return _dictionary.ContainsKey(key);
        }
    }
}
