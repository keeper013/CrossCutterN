/**
 * Description: Reference default implementation
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Mono.Cecil;
    using CrossCutterN.Advice.Common;

    internal class ReferenceBase
    {
        private readonly ModuleDefinition _module;
        private readonly bool _setReadOnly;
        private readonly IrreversibleOperation _readOnly = new IrreversibleOperation();
        private readonly IDictionary<string, MethodReference> _methods = new Dictionary<string, MethodReference>();
        private readonly IDictionary<string, TypeReference> _types = new Dictionary<string, TypeReference>();

        protected ReferenceBase(ModuleDefinition module, bool setReadOnly)
        {
            if (module == null)
            {
                throw new ArgumentNullException("module");
            }
            _module = module;
            _setReadOnly = setReadOnly;
        }

        protected TypeReference GetType(string key)
        {
            if (_setReadOnly)
            {
                _readOnly.Assert(true);
            }
            return _types[key];
        }

        protected void SetType(string key, Type value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (_setReadOnly)
            {
                _readOnly.Assert(false);
            }
            _types[key] = _module.Import(value);
        }

        protected MethodReference GetMethod(string key)
        {
            if (_setReadOnly)
            {
                _readOnly.Assert(true);
            }
            return _methods[key];
        }

        protected void SetMethod(string key, MethodInfo value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (_setReadOnly)
            {
                _readOnly.Assert(false);
            }
            _methods[key] = _module.Import(value);
        }

        protected void ValidateConvert(params string[] keys)
        {
            foreach (var key in keys)
            {
                if ((!_methods.ContainsKey(key) || _methods[key] == null) && (!_types.ContainsKey(key) || _types[key] == null))
                {
                    throw new InvalidOperationException(string.Format("Necessary reference {0} is not set yet", key));
                }
            }
            if (_setReadOnly)
            {
                _readOnly.Apply();
            }
            
        }
    }
}
