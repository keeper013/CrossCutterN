/**
* Description: Completed class switch container
* Author: David Cui
*/

namespace CrossCutterN.Advice.Switch
{
    using System;
    using System.Collections.Generic;

    internal class ClassAdviceSwitch : IClassAdviceSwitch
    {
        private Dictionary<string, Dictionary<string, int>> _propertySwitchDictionary = new Dictionary<string, Dictionary<string, int>>();
        private Dictionary<string, Dictionary<string, int>> _methodSwitchDictionary = new Dictionary<string, Dictionary<string, int>>();
        private Dictionary<string, List<int>> _aspectSwitchDictionary = new Dictionary<string, List<int>>();
        private readonly IDictionary<int, bool> _switchDictionary;
        private readonly string _className;

        public ClassAdviceSwitch(string className, IDictionary<int, bool> switchDictionary)
        {
            if(string.IsNullOrWhiteSpace(className))
            {
                throw new ArgumentException("className");
            }
            if(switchDictionary == null)
            {
                throw new ArgumentNullException("switchDictionary");
            }
            _className = className;
            _switchDictionary = switchDictionary;
        }

        public bool IsAspectApplied(string aspect)
        {
            if (string.IsNullOrWhiteSpace(aspect))
            {
                throw new ArgumentNullException("aspect");
            }
            return _aspectSwitchDictionary.ContainsKey(aspect);
        }

        public int SwitchPropertyAspect(string propertyName, string aspect)
        {
            var id = LocatePropertyAspectSwitch(propertyName, aspect);
            _switchDictionary[id] = !_switchDictionary[id];
            return 1;
        }

        public int SwitchAspect(string aspect)
        {
            var ids = LocateAspectSwitches(aspect);
            foreach (var id in ids)
            {
                _switchDictionary[id] = !_switchDictionary[id];
            }
            return ids.Count;
        }

        public int SwitchMethod(string methodSignature)
        {
            var ids = LocateMethodSwitches(methodSignature);
            foreach (var id in ids)
            {
                _switchDictionary[id] = !_switchDictionary[id];
            }
            return ids.Count;
        }

        public int SwitchMethodAspect(string methodSignature, string aspect)
        {
            var id = LocateMethodAspectSwitch(methodSignature, aspect);
            _switchDictionary[id] = !_switchDictionary[id];
            return 1;
        }

        public int SwitchProperty(string propertyName)
        {
            var ids = LocatePropertySwitches(propertyName);
            foreach (var id in ids)
            {
                _switchDictionary[id] = !_switchDictionary[id];
            }
            return ids.Count;
        }

        public int SwitchOffPropertyAspect(string propertyName, string aspect)
        {
            var id = LocatePropertyAspectSwitch(propertyName, aspect);
            _switchDictionary[id] = false;
            return 1;
        }

        public int SwitchOffAspect(string aspect)
        {
            var ids = LocateAspectSwitches(aspect);
            foreach (var id in ids)
            {
                _switchDictionary[id] = false;
            }
            return ids.Count;
        }

        public int SwitchOffMethod(string methodSignature)
        {
            var ids = LocateMethodSwitches(methodSignature);
            foreach (var id in ids)
            {
                _switchDictionary[id] = false;
            }
            return ids.Count;
        }

        public int SwitchOffMethodAspect(string methodSignature, string aspect)
        {
            var id = LocateMethodAspectSwitch(methodSignature, aspect);
            _switchDictionary[id] = false;
            return 1;
        }

        public int SwitchOffProperty(string propertyName)
        {
            var ids = LocatePropertySwitches(propertyName);
            foreach (var id in ids)
            {
                _switchDictionary[id] = false;
            }
            return ids.Count;
        }

        public int SwitchOnPropertyAspect(string propertyName, string aspect)
        {
            var id = LocatePropertyAspectSwitch(propertyName, aspect);
            _switchDictionary[id] = true;
            return 1;
        }

        public int SwitchOnAspect(string aspect)
        {
            var ids = LocateAspectSwitches(aspect);
            foreach (var id in ids)
            {
                _switchDictionary[id] = true;
            }
            return ids.Count;
        }

        public int SwitchOnMethod(string methodSignature)
        {
            var ids = LocateMethodSwitches(methodSignature);
            foreach (var id in ids)
            {
                _switchDictionary[id] = true;
            }
            return ids.Count;
        }

        public int SwitchOnMethodAspect(string methodSignature, string aspect)
        {
            var id = LocateMethodAspectSwitch(methodSignature, aspect);
            _switchDictionary[id] = true;
            return 1;
        }

        public int SwitchOnProperty(string propertyName)
        {
            var ids = LocatePropertySwitches(propertyName);
            foreach (var id in ids)
            {
                _switchDictionary[id] = true;
            }
            return ids.Count;
        }

        private int LocatePropertyAspectSwitch(string propertyName, string aspect)
        {
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
                    string.Format("Invalid property name {0} in class {1}", propertyName, _className), "propertyName");
            }
            var aspectDictionary = _propertySwitchDictionary[propertyName];
            // apply this exception to avoid user switching off wrong aspect name
            if (!aspectDictionary.ContainsKey(aspect))
            {
                throw new ArgumentException(
                    string.Format("Aspect {0} is not applied to property {1} of class {2}", aspect, propertyName, _className), "aspect");
            }
            return aspectDictionary[aspect];
        }

        private ICollection<int> LocateAspectSwitches(string aspect)
        {
            if (string.IsNullOrEmpty(aspect))
            {
                throw new ArgumentNullException("aspect");
            }
            // apply this exception to avoid user switching off wrong aspect name
            if (!_aspectSwitchDictionary.ContainsKey(aspect))
            {
                throw new ArgumentException(
                    string.Format("Aspect {0} isn't applied to any property or method in class {1}", aspect, _className), "aspect");
            }
            return _aspectSwitchDictionary[aspect];
        }

        private ICollection<int> LocateMethodSwitches(string methodSignature)
        {
            if (string.IsNullOrEmpty(methodSignature))
            {
                throw new ArgumentNullException("methodSignature");
            }
            if (!_methodSwitchDictionary.ContainsKey(methodSignature))
            {
                throw new ArgumentException(
                    string.Format("Method {0} isn't applied to any property or method in class {1}", methodSignature, _className), "methodSignature");
            }
            return _methodSwitchDictionary[methodSignature].Values;
        }

        private int LocateMethodAspectSwitch(string methodSignature, string aspect)
        {
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
                    string.Format("Method {0} isn't applied to any property or method in class {1}", methodSignature, _className), "methodSignature");
            }
            var aspectDictionary = _methodSwitchDictionary[methodSignature];
            // apply this exception to avoid user switching off wrong aspect name
            if (!aspectDictionary.ContainsKey(aspect))
            {
                throw new ArgumentException(
                    string.Format("Aspect {0} is not applied to method {1} of class {2}", aspect, methodSignature, _className), "aspect");
            }
            return aspectDictionary[aspect];
        }

        private ICollection<int> LocatePropertySwitches(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }
            if (!_propertySwitchDictionary.ContainsKey(propertyName))
            {
                throw new ArgumentException(
                    string.Format("Property {0} isn't applied to any property or method in class {1}", propertyName, _className), "propertyName");
            }
            return _propertySwitchDictionary[propertyName].Values;
        }
    }
}
