/**
* Description: Completed class switch container
* Author: David Cui
*/

namespace CrossCutterN.Advice.Switch
{
    using System;
    using System.Collections.Generic;
    using Common;

    internal sealed class ClassAdviceSwitch : IClassAdviceSwitch, IClassAdviceSwitchBuildUp
    {
        private readonly Dictionary<string, Dictionary<string, int>> _propertySwitchDictionary = new Dictionary<string, Dictionary<string, int>>();
        private readonly Dictionary<string, Dictionary<string, int>> _methodSwitchDictionary = new Dictionary<string, Dictionary<string, int>>();
        private readonly Dictionary<string, List<int>> _aspectSwitchDictionary = new Dictionary<string, List<int>>();
        private readonly IDictionary<int, bool> _switchDictionary;
        private readonly IrreversibleOperation _complete = new IrreversibleOperation();

        public ClassAdviceSwitch(IDictionary<int, bool> switchDictionary)
        {
            if(switchDictionary == null)
            {
                throw new ArgumentNullException("switchDictionary");
            }
            _switchDictionary = switchDictionary;
        }

        public bool IsAspectApplied(string aspect)
        {
            if (string.IsNullOrWhiteSpace(aspect))
            {
                throw new ArgumentNullException("aspect");
            }
            _complete.Assert(true);
            return _aspectSwitchDictionary.ContainsKey(aspect);
        }

        #region Switch

        public int Switch(SwitchStatus status)
        {
            _complete.Assert(true);
            var enumerator = _aspectSwitchDictionary.GetEnumerator();
            var count = 0;
            while (enumerator.MoveNext())
            {
                foreach (var id in enumerator.Current.Value)
                {
                    _switchDictionary[id] = Switch(!_switchDictionary[id], status);
                }
                count += enumerator.Current.Value.Count;
            }
            return count;
        }

        public int SwitchAspect(string aspect, SwitchStatus status)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
            if (string.IsNullOrEmpty(aspect))
            {
                throw new ArgumentNullException("aspect");
            }
            _complete.Assert(true);
            if (!_aspectSwitchDictionary.ContainsKey(aspect))
            {
                throw new ArgumentException(
                    string.Format("Aspect {0} isn't applied to any property or method", aspect), "aspect");
            }
#endif
            var ids = _aspectSwitchDictionary[aspect];
            _complete.Assert(true);
            foreach (var id in ids)
            {
                _switchDictionary[id] = Switch(!_switchDictionary[id], status);
            }
            return ids.Count;
        }

        public int SwitchMethod(string methodSignature, SwitchStatus status)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
            if (string.IsNullOrEmpty(methodSignature))
            {
                throw new ArgumentNullException("methodSignature");
            }
            if (!_methodSwitchDictionary.ContainsKey(methodSignature))
            {
                throw new ArgumentException(
                    string.Format("Method {0} isn't applied to any property or method", methodSignature), "methodSignature");
            }
#endif
            _complete.Assert(true);
            var ids = _methodSwitchDictionary[methodSignature].Values;
            _complete.Assert(true);
            foreach (var id in ids)
            {
                _switchDictionary[id] = Switch(_switchDictionary[id], status);
            }
            return ids.Count;
        }

        public int SwitchMethodAspect(string methodSignature, string aspect, SwitchStatus status)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
            if (string.IsNullOrEmpty(methodSignature))
            {
                throw new ArgumentNullException("methodSignature");
            }
            if (string.IsNullOrEmpty(aspect))
            {
                throw new ArgumentNullException("aspect");
            }
            if (!_methodSwitchDictionary.ContainsKey(methodSignature))
            {
                throw new ArgumentException(
                    string.Format("Method {0} isn't applied to any property or method", methodSignature), "methodSignature");
            }
#endif
            _complete.Assert(true);
            var aspectDictionary = _methodSwitchDictionary[methodSignature];
            // apply this exception to avoid user switching off wrong aspect name
            if (!aspectDictionary.ContainsKey(aspect))
            {
                throw new ArgumentException(
                    string.Format("Aspect {0} is not applied to method {1}", aspect, methodSignature), "aspect");
            }
            var id = aspectDictionary[aspect];
            _complete.Assert(true);
            _switchDictionary[id] = Switch(!_switchDictionary[id], status);
            return 1;
        }

        public int SwitchProperty(string propertyName, SwitchStatus status)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }
            if (!_propertySwitchDictionary.ContainsKey(propertyName))
            {
                throw new ArgumentException(
                    string.Format("Property {0} isn't applied to any property or method", propertyName), "propertyName");
            }
#endif
            _complete.Assert(true);
            var ids = _propertySwitchDictionary[propertyName].Values;
            _complete.Assert(true);
            foreach (var id in ids)
            {
                _switchDictionary[id] = Switch(!_switchDictionary[id], status);
            }
            return ids.Count;
        }

        public int SwitchPropertyAspect(string propertyName, string aspect, SwitchStatus status)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }
            if (string.IsNullOrEmpty(aspect))
            {
                throw new ArgumentNullException("aspect");
            }
            if (!_propertySwitchDictionary.ContainsKey(propertyName))
            {
                throw new ArgumentException(
                    string.Format("Invalid property name {0}", propertyName), "propertyName");
            }
#endif
            _complete.Assert(true);
            // apply this exception to avoid user switching off wrong aspect name
            var aspectDictionary = _propertySwitchDictionary[propertyName];
            if (!aspectDictionary.ContainsKey(aspect))
            {
                throw new ArgumentException(
                    string.Format("Aspect {0} is not applied to property {1}", aspect, propertyName), "aspect");
            }
            var id = aspectDictionary[aspect];
            _complete.Assert(true);
            _switchDictionary[id] = Switch(_switchDictionary[id], status);
            return 1;
        }

        #endregion

        #region BuildUp

        public void RegisterSwitch(int id, string propertyName, string methodSignature, string aspect)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
            if (string.IsNullOrWhiteSpace(aspect))
            {
                throw new ArgumentNullException("aspect");
            }
            if (string.IsNullOrWhiteSpace(methodSignature))
            {
                throw new ArgumentException("Method of property can't be empty");
            }
            if (!string.IsNullOrWhiteSpace(methodSignature) && _methodSwitchDictionary.ContainsKey(methodSignature) && _methodSwitchDictionary[methodSignature].ContainsKey(aspect))
            {
                throw new ArgumentException(
                    string.Format("Aspect {0} is added for property method {1} already", aspect, propertyName), "aspect");
            }
            if (_aspectSwitchDictionary.ContainsKey(aspect) && _aspectSwitchDictionary[aspect].Contains(id))
            {
                throw new ArgumentException(string.Format("Id {0} is added to aspect {1} already", id, aspect), "id");
            }
#endif
            _complete.Assert(false);
            if (!string.IsNullOrWhiteSpace(propertyName))
            {
                if (_propertySwitchDictionary.ContainsKey(propertyName))
                {
                    _propertySwitchDictionary[propertyName].Add(aspect, id);
                }
                else
                {
                    _propertySwitchDictionary.Add(propertyName, new Dictionary<string, int> { { aspect, id } });
                }
            }
            if (!string.IsNullOrWhiteSpace(methodSignature))
            {
                if (_methodSwitchDictionary.ContainsKey(methodSignature))
                {
                    _methodSwitchDictionary[methodSignature].Add(aspect, id);
                }
                else
                {
                    _methodSwitchDictionary.Add(methodSignature, new Dictionary<string, int> {{aspect, id}});
                }
            }
            if (_aspectSwitchDictionary.ContainsKey(aspect))
            {
                _aspectSwitchDictionary[aspect].Add(id);
            }
            else
            {
                _aspectSwitchDictionary.Add(aspect, new List<int> { id });
            }
        }

        public IClassAdviceSwitch Convert()
        {
            _complete.Apply();
            return this;
        }

        #endregion

        #region Switch Utilities

        private static bool Switch(bool value, SwitchStatus status)
        {
            switch (status)
            {
                case SwitchStatus.On:
                    return true;
                case SwitchStatus.Off:
                    return false;
                case SwitchStatus.Switched:
                    return !value;
            }
            throw new InvalidOperationException(string.Format("Unexpected switch status: {0}", status));
        }

        #endregion
    }
}
