/**
* Description: Completed class switch container
* Author: David Cui
*/

namespace CrossCutterN.Advice.Switch
{
    using System;
    using System.Collections.Generic;
    using Common;
    using MultiThreading;

    internal sealed class ClassAdviceSwitch : IClassAdviceSwitch, IClassAdviceSwitchBuildUp
    {
        private readonly Dictionary<string, Dictionary<string, PropertySwitches>> _propertySwitchDictionary = new Dictionary<string, Dictionary<string, PropertySwitches>>();
        private readonly Dictionary<string, Dictionary<string, int>> _methodSwitchDictionary = new Dictionary<string, Dictionary<string, int>>();
        private readonly Dictionary<string, List<int>> _aspectSwitchDictionary = new Dictionary<string, List<int>>();
        private readonly IList<bool> _switchList;
        private readonly ISmartReadWriteLock _switchLock;
        private readonly IrreversibleOperation _complete = new IrreversibleOperation();

        public ClassAdviceSwitch(IList<bool> switchList, ISmartReadWriteLock lck)
        {
            if (switchList == null)
            {
                throw new ArgumentNullException("switchList");
            }
            if (lck == null)
            {
                throw new ArgumentNullException("lck");
            }
            _switchLock = lck;
            _switchList = switchList;
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

        public int Switch(SwitchOperation operation)
        {
            _complete.Assert(true);
            var count = 0;
            var enumerator = _aspectSwitchDictionary.GetEnumerator();
            while (enumerator.MoveNext())
            {
                count += Switch(enumerator.Current.Value, operation);
            }
            return count;
        }

        public int SwitchAspect(string aspect, SwitchOperation operation)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
            if (string.IsNullOrEmpty(aspect))
            {
                throw new ArgumentNullException("aspect");
            }
            if (!_aspectSwitchDictionary.ContainsKey(aspect))
            {
                throw new ArgumentException(
                    string.Format("Aspect {0} isn't applied to any property or method", aspect), "aspect");
            }
#endif
            _complete.Assert(true);
            return Switch(_aspectSwitchDictionary[aspect], operation);
        }

        public int SwitchMethod(string methodSignature, SwitchOperation operation)
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
            return Switch(_methodSwitchDictionary[methodSignature].Values, operation);
        }

        public int SwitchMethodAspect(string methodSignature, string aspect, SwitchOperation operation)
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
            return Switch(aspectDictionary[aspect], operation);
        }

        public int SwitchProperty(string propertyName, SwitchOperation operation)
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
            return Switch(_propertySwitchDictionary[propertyName].Values, operation);
        }

        public int SwitchPropertyAspect(string propertyName, string aspect, SwitchOperation operation)
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
            using (_switchLock.ReadLock)
            {
                return Switch(aspectDictionary[aspect], operation);
            }
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
            // This method is not supposed to be called with multithread style, so no locking applied
            _complete.Assert(false);
            
            if (!string.IsNullOrWhiteSpace(propertyName))
            {
                if (_propertySwitchDictionary.ContainsKey(propertyName))
                {
                    var aspectDictionary = _propertySwitchDictionary[propertyName];
                    if (aspectDictionary.ContainsKey(aspect))
                    {
                        SetPropertySwitches(aspectDictionary[aspect], methodSignature, id);
                    }
                    else
                    {
                        var propertySwitches = new PropertySwitches();
                        SetPropertySwitches(propertySwitches, methodSignature, id);
                        aspectDictionary.Add(aspect, propertySwitches);
                    }
                }
                else
                {
                    var propertySwitches = new PropertySwitches();
                    SetPropertySwitches(propertySwitches, methodSignature, id);
                    _propertySwitchDictionary.Add(propertyName, new Dictionary<string, PropertySwitches> { { aspect, propertySwitches } });
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

        public IClassAdviceSwitch Convert(string clazz, IClassAdviceSwitchOperation classOperations, Dictionary<string, SwitchOperationStatus> aspectOperations)
        {
            // This method is not supposed to be called with multithread style, so no locking applied
            using (_switchLock.ReadLock)
            {
                foreach (var methodSwitch in _methodSwitchDictionary)
                {
                    var methodSignature = methodSwitch.Key;
                    var aspectSwitch = methodSwitch.Value;
                    foreach(var value in aspectSwitch) {
                        var id = value.Value;
                        _switchList[id] = GetSwitchValue(_switchList[id], clazz, methodSignature, value.Key, classOperations, aspectOperations);
                    }
                }
            }
            _complete.Apply();
            return this;
        }

        private bool GetSwitchValue(bool value, string clazz, string methodSignature, string aspect, 
            IClassAdviceSwitchOperation classOperations, Dictionary<string, SwitchOperationStatus> aspectOperations)
        {
            if (classOperations != null)
            {
                return classOperations.GetSwitchValue(value, methodSignature, aspect);
            }
            return aspectOperations.ContainsKey(aspect) ? aspectOperations[aspect].Switch(value) : value;
        }

        #endregion

        #region Lookup

        public bool? LookUp(string methodSignature, string aspect)
        {
            if (string.IsNullOrWhiteSpace(methodSignature))
            {
                throw new ArgumentNullException("methodSignature");
            }
            if (string.IsNullOrWhiteSpace(aspect))
            {
                throw new ArgumentNullException("aspect");
            }
            if (_methodSwitchDictionary.ContainsKey(methodSignature))
            {
                var aspectDictionary = _methodSwitchDictionary[methodSignature];
                if (aspectDictionary.ContainsKey(aspect))
                {
                    using (_switchLock.ReadLock)
                    {
                        return _switchList[aspectDictionary[aspect]];
                    }
                }
            }
            return null;
        }

        #endregion

        #region Switch Utilities

        private int Switch(int id, SwitchOperation operation)
        {
            using (_switchLock.ReadLock)
            {
                _switchList[id] = Switch(_switchList[id], operation);
            }
            return 1;
        }

        private int Switch(PropertySwitches propertySwitches, SwitchOperation operation)
        {
            var result = 0;
            var getter = propertySwitches.Getter;
            if (getter >= 0)
            {
                _switchList[getter] = Switch(_switchList[getter], operation);
                result++;
            }
            var setter = propertySwitches.Setter;
            if (setter >= 0)
            {
                _switchList[setter] = Switch(_switchList[setter], operation);
                result++;
            }
            return result;
        }

        private int Switch(ICollection<int> ids, SwitchOperation operation)
        {
            using (_switchLock.ReadLock)
            {
                foreach (var id in ids)
                {
                    _switchList[id] = Switch(_switchList[id], operation);
                }
            }
            return ids.Count;
        }

        private int Switch(IEnumerable<PropertySwitches> switches, SwitchOperation operation)
        {
            var result = 0;
            using (_switchLock.ReadLock)
            {
                foreach (var propertySwitches in switches)
                {
                    result += Switch(propertySwitches, operation);
                }
            }
            return result;
        }

        private static bool Switch(bool value, SwitchOperation operation)
        {
            switch (operation)
            {
                case SwitchOperation.On:
                    return true;
                case SwitchOperation.Off:
                    return false;
                case SwitchOperation.Switch:
                    return !value;
            }
            throw new InvalidOperationException(string.Format("Unexpected switch operation: {0}", operation));
        }

        private static void SetPropertySwitches(PropertySwitches switches, string method, int id)
        {
            const string getterPrefix = "get_";
            const string setterPrefix = "set_";
            if (method.StartsWith(getterPrefix))
            {
                if (switches.Getter >= 0)
                {
                    throw new ArgumentException(string.Format("Getter method {0} has been set", method));
                }
                switches.Getter = id;
            }
            else if (method.StartsWith(setterPrefix))
            {
                if (switches.Setter >= 0)
                {
                    throw new ArgumentException(string.Format("Setter method {0} has been set", method));
                }
                switches.Setter = id;
            }
            else
            {
                throw new ArgumentException("Invalid getter or setter method name: {0}", method);
            }
        }

        #endregion

        private class PropertySwitches
        {
            public PropertySwitches()
            {
                Getter = -1;
                Setter = -1;
            }

            public int Getter { get; set; }
            public int Setter { get; set; }
        }
    }
}
