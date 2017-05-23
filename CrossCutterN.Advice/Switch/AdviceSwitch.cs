/**
* Description: Advice switch implementation
* Author: David Cui
*/

namespace CrossCutterN.Advice.Switch
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Common;

    internal sealed class AdviceSwitch : IAdviceSwitch, IAdviceSwitchBuildUp, IAdviceSwitchLookUp
    {
        private readonly IList<bool> _switchList = new List<bool>();
        private readonly IDictionary<string, IClassAdviceSwitchBuildUp> _buildingUps =
            new Dictionary<string, IClassAdviceSwitchBuildUp>();
        private readonly IDictionary<string, IClassAdviceSwitch> _completed =
            new Dictionary<string, IClassAdviceSwitch>();
        private readonly Dictionary<string, IClassAdviceSwitchOperation> _classOperations =
            new Dictionary<string, IClassAdviceSwitchOperation>(); 
        // we never know when will all class be loaded, so this aspect operation dictionary is necessary
        private readonly Dictionary<string, SwitchOperation> _aspectOperations = 
            new Dictionary<string, SwitchOperation>();
        private readonly SequenceGenerator _sequenceGenerator = new SequenceGenerator();

        public void Complete(string clazz)
        {
#if DEBUG
            if (string.IsNullOrWhiteSpace(clazz))
            {
                throw new ArgumentNullException("clazz");
            }
            if (_completed.ContainsKey(clazz))
            {
                throw new ArgumentException(string.Format("{0} is completed for switch registration already.", clazz));
            }
            if (!_buildingUps.ContainsKey(clazz))
            {
                throw new ArgumentException(string.Format("{0} is not built up at all.", clazz));
            }
#endif
            _completed.Add(clazz, _buildingUps[clazz].Convert());
            _buildingUps.Remove(clazz);
            _classOperations.Remove(clazz);
        }

        public bool IsOn(int id)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
            if(_switchList.Count <= id)
            {
                throw new InvalidOperationException(string.Format("Switch for id {0} is not found", id));
            }
#endif
            return _switchList[id];
        }

        public int RegisterSwitch(string clazz, string property, string method, string aspect, bool value)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
            if (string.IsNullOrEmpty(clazz))
            {
                throw new ArgumentNullException("clazz");
            }
            if (_completed.ContainsKey(clazz))
            {
                throw new ArgumentException(string.Format("{0} is completed for switch registration already.", clazz));
            }
#endif
            var id = _switchList.Count;
            _switchList.Add(GetSwitchValue(value, clazz, property, method, aspect));
            if (!_buildingUps.ContainsKey(clazz))
            {
                _buildingUps.Add(clazz, SwitchFactory.InitializeClassAdviceSwitch(_switchList));
            }
            _buildingUps[clazz].RegisterSwitch(id, property, method, aspect);
            return id;
        }

        #region Switch

        public int Switch(PropertyInfo property)
        {
            return Switch(property, SwitchStatus.Switched);
        }

        public int Switch(string aspect)
        {
            return Switch(aspect, SwitchStatus.Switched);
        }

        public int Switch(MethodInfo method)
        {
            return Switch(method, SwitchStatus.Switched);
        }

        public int Switch(Type type)
        {
            return Switch(type, SwitchStatus.Switched);
        }

        public int Switch(PropertyInfo property, string aspect)
        {
            return Switch(property, aspect, SwitchStatus.Switched);
        }

        public int Switch(MethodInfo method, string aspect)
        {
            return Switch(method, aspect, SwitchStatus.Switched);
        }

        public int Switch(Type type, string aspect)
        {
            return Switch(type, aspect, SwitchStatus.Switched);
        }

        #endregion

        #region SwitchOff

        public int SwitchOff(PropertyInfo property)
        {
            return Switch(property, SwitchStatus.Off);
        }

        public int SwitchOff(string aspect)
        {
            return Switch(aspect, SwitchStatus.Off);
        }

        public int SwitchOff(MethodInfo method)
        {
            return Switch(method, SwitchStatus.Off);
        }

        public int SwitchOff(Type type)
        {
            return Switch(type, SwitchStatus.Off);
        }

        public int SwitchOff(PropertyInfo property, string aspect)
        {
            return Switch(property, aspect, SwitchStatus.Off);
        }

        public int SwitchOff(MethodInfo method, string aspect)
        {
            return Switch(method, aspect, SwitchStatus.Off);
        }

        public int SwitchOff(Type type, string aspect)
        {
            return Switch(type, aspect, SwitchStatus.Off);
        }

        #endregion

        #region SwitchOn

        public int SwitchOn(PropertyInfo property)
        {
            return Switch(property, SwitchStatus.On);
        }

        public int SwitchOn(string aspect)
        {
            return Switch(aspect, SwitchStatus.On);
        }

        public int SwitchOn(MethodInfo method)
        {
            return Switch(method, SwitchStatus.On);
        }

        public int SwitchOn(Type type)
        {
            return Switch(type, SwitchStatus.On);
        }

        public int SwitchOn(PropertyInfo property, string aspect)
        {
            return Switch(property, aspect, SwitchStatus.On);
        }

        public int SwitchOn(MethodInfo method, string aspect)
        {
            return Switch(method, aspect, SwitchStatus.On);
        }

        public int SwitchOn(Type type, string aspect)
        {
            return Switch(type, aspect, SwitchStatus.On);
        }

        #endregion

        #region Utilities

        private bool GetSwitchValue(bool value, string clazz, string propertyName, string methodSignature, string aspect)
        {
            return _classOperations.ContainsKey(clazz) ? _classOperations[clazz].GetSwitchValue(value, propertyName, methodSignature, aspect) : value;
        }

        private int Switch(PropertyInfo property, SwitchStatus status)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }
            if (property.DeclaringType == null)
            {
                throw new ArgumentException(string.Format("Property {0} doesn't have declaring type.", property.Name));
            }
            var clazz = property.DeclaringType.FullName;
            if (_completed.ContainsKey(clazz))
            {
                // the class is completed
                return _completed[clazz].SwitchProperty(property.Name, status);
            }
            if (!_classOperations.ContainsKey(clazz))
            {
                // the class is not loaded yet
                _classOperations.Add(clazz, SwitchFactory.InitializeClassAdviceSwitchOperation(_aspectOperations, _sequenceGenerator));
            }
            _classOperations[clazz].SwitchProperty(property.Name, status);
            return -1;
        }

        private int Switch(string aspect, SwitchStatus status)
        {
            if (string.IsNullOrWhiteSpace(aspect))
            {
                throw new ArgumentNullException("aspect");
            }
            if (_aspectOperations.ContainsKey(aspect))
            {
                var neutralized = _aspectOperations[aspect].Switch(status);
                if (neutralized)
                {
                    _aspectOperations.Remove(aspect);
                }
            }
            else
            {
                _aspectOperations.Add(aspect, SwitchFactory.InitializeSwitchOperation(_sequenceGenerator, status));
            }
            return 1;
        }

        private int Switch(MethodInfo method, SwitchStatus status)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }
            var signature = method.GetSignature();
            if (method.DeclaringType == null)
            {
                throw new ArgumentException(string.Format("Method {0} doesn't have declaring type.", signature));
            }
            var clazz = method.DeclaringType.FullName;
            if (_completed.ContainsKey(clazz))
            {
                // the class is completed
                return _completed[clazz].SwitchMethod(signature, status);
            }
            if (!_classOperations.ContainsKey(clazz))
            {
                // the class is not loaded yet
                _classOperations.Add(clazz, SwitchFactory.InitializeClassAdviceSwitchOperation(_aspectOperations, _sequenceGenerator));
            }
            _classOperations[clazz].SwitchMethod(signature, status);
            return -1;
        }

        private int Switch(Type type, SwitchStatus status)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            var clazz = type.FullName;
            if (_completed.ContainsKey(clazz))
            {
                // the class is completed
                return _completed[clazz].Switch(status);
            }
            if (!_classOperations.ContainsKey(clazz))
            {
                // the class is not loaded yet
                _classOperations.Add(clazz, SwitchFactory.InitializeClassAdviceSwitchOperation(_aspectOperations, _sequenceGenerator));
            }
            _classOperations[clazz].Switch(status);
            return -1;
        }

        private int Switch(PropertyInfo property, string aspect, SwitchStatus status)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }
            if (property.DeclaringType == null)
            {
                throw new ArgumentException(string.Format("Property {0} doesn't have declaring type.", property.Name));
            }
            if (string.IsNullOrWhiteSpace(aspect))
            {
                throw new ArgumentNullException("aspect");
            }
            var clazz = property.DeclaringType.FullName;
            if (_completed.ContainsKey(clazz))
            {
                // the class is completed
                return _completed[clazz].SwitchPropertyAspect(property.Name, aspect, status);
            }
            if (!_classOperations.ContainsKey(clazz))
            {
                // the class is not loaded yet
                _classOperations.Add(clazz, SwitchFactory.InitializeClassAdviceSwitchOperation(_aspectOperations, _sequenceGenerator));
            }
            _classOperations[clazz].SwitchPropertyAspect(property.Name, aspect, status);
            return -1;
        }

        private int Switch(MethodInfo method, string aspect, SwitchStatus status)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }
            if (method.DeclaringType == null)
            {
                throw new ArgumentException(string.Format("Method {0} doesn't have declaring type.", method.GetSignature()));
            }
            if (string.IsNullOrWhiteSpace(aspect))
            {
                throw new ArgumentNullException("aspect");
            }
            var clazz = method.DeclaringType.FullName;
            if (_completed.ContainsKey(clazz))
            {
                // the class is completed
                return _completed[clazz].SwitchMethodAspect(method.GetSignature(), aspect, status);
            }
            if (!_classOperations.ContainsKey(clazz))
            {
                // the class is not loaded yet
                _classOperations.Add(clazz, SwitchFactory.InitializeClassAdviceSwitchOperation(_aspectOperations, _sequenceGenerator));
            }
            _classOperations[clazz].SwitchMethodAspect(method.GetSignature(), aspect, status);
            return -1;
        }

        private int Switch(Type type, string aspect, SwitchStatus status)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            if (string.IsNullOrWhiteSpace(aspect))
            {
                throw new ArgumentNullException("aspect");
            }
            var clazz = type.FullName;
            if (_completed.ContainsKey(clazz))
            {
                // the class is completed
                return _completed[clazz].SwitchAspect(aspect, status);
            }
            if (!_classOperations.ContainsKey(clazz))
            {
                // the class is not loaded yet
                _classOperations.Add(clazz, SwitchFactory.InitializeClassAdviceSwitchOperation(_aspectOperations, _sequenceGenerator));
            }
            _classOperations[clazz].SwitchAspect(aspect, status);
            return -1;
        }

        #endregion
    }
}
