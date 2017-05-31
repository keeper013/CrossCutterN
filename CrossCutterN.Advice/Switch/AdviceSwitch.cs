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

    /// <summary>
    /// Considering this advice switching isn't supposed to happen often, crude lock(this) is used to cater for multithreading 
    /// </summary>
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
        private readonly Dictionary<string, SwitchOperationStatus> _aspectOperations = 
            new Dictionary<string, SwitchOperationStatus>();
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
            lock (this)
            {
                _completed.Add(clazz, _buildingUps[clazz].Convert());
                _buildingUps.Remove(clazz);
                _classOperations.Remove(clazz);
            }
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
            // a significant delay may be expected if we lock this part, so no locking is applied here.
            // the drawback is that when updating multiple switches belong to one aspect or class or method,
            // some switches may be retrieved before all related switches are updated, 
            // so behavior of switches expected to be changed in one switching operation may differ.
            // The assumption is that switching during application shouldn't happen a lot
            // Note that the atomicity of one aspect within one method is still guaranteed by the weaving algorithm 
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
            lock(this)
            {
                var id = _switchList.Count;
                _switchList.Add(GetSwitchValue(value, clazz, method, aspect));
                if (!_buildingUps.ContainsKey(clazz))
                {
                    _buildingUps.Add(clazz, SwitchFactory.InitializeClassAdviceSwitch(_switchList));
                }
                _buildingUps[clazz].RegisterSwitch(id, property, method, aspect);
                return id;
            }
        }

        #region Switch

        public int Switch(PropertyInfo property)
        {
            return Switch(property, SwitchOperation.Switch);
        }

        public int Switch(string aspect)
        {
            return Switch(aspect, SwitchOperation.Switch);
        }

        public int Switch(MethodInfo method)
        {
            return Switch(method, SwitchOperation.Switch);
        }

        public int Switch(Type type)
        {
            return Switch(type, SwitchOperation.Switch);
        }

        public int Switch(PropertyInfo property, string aspect)
        {
            return Switch(property, aspect, SwitchOperation.Switch);
        }

        public int Switch(MethodInfo method, string aspect)
        {
            return Switch(method, aspect, SwitchOperation.Switch);
        }

        public int Switch(Type type, string aspect)
        {
            return Switch(type, aspect, SwitchOperation.Switch);
        }

        #endregion

        #region SwitchOff

        public int SwitchOff(PropertyInfo property)
        {
            return Switch(property, SwitchOperation.Off);
        }

        public int SwitchOff(string aspect)
        {
            return Switch(aspect, SwitchOperation.Off);
        }

        public int SwitchOff(MethodInfo method)
        {
            return Switch(method, SwitchOperation.Off);
        }

        public int SwitchOff(Type type)
        {
            return Switch(type, SwitchOperation.Off);
        }

        public int SwitchOff(PropertyInfo property, string aspect)
        {
            return Switch(property, aspect, SwitchOperation.Off);
        }

        public int SwitchOff(MethodInfo method, string aspect)
        {
            return Switch(method, aspect, SwitchOperation.Off);
        }

        public int SwitchOff(Type type, string aspect)
        {
            return Switch(type, aspect, SwitchOperation.Off);
        }

        #endregion

        #region SwitchOn

        public int SwitchOn(PropertyInfo property)
        {
            return Switch(property, SwitchOperation.On);
        }

        public int SwitchOn(string aspect)
        {
            return Switch(aspect, SwitchOperation.On);
        }

        public int SwitchOn(MethodInfo method)
        {
            return Switch(method, SwitchOperation.On);
        }

        public int SwitchOn(Type type)
        {
            return Switch(type, SwitchOperation.On);
        }

        public int SwitchOn(PropertyInfo property, string aspect)
        {
            return Switch(property, aspect, SwitchOperation.On);
        }

        public int SwitchOn(MethodInfo method, string aspect)
        {
            return Switch(method, aspect, SwitchOperation.On);
        }

        public int SwitchOn(Type type, string aspect)
        {
            return Switch(type, aspect, SwitchOperation.On);
        }

        #endregion

        #region lookup

        public bool? GetSwitchStatus(MethodInfo method, string aspect)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }
            if (string.IsNullOrWhiteSpace(aspect))
            {
                throw new ArgumentNullException("aspect");
            }
            if (method.DeclaringType == null)
            {
                throw new ArgumentException("Method doesn't have a declaring type", "method");
            }
            // for single switch look up, no locking applied
            var clazz = method.DeclaringType.FullName;
            return _completed.ContainsKey(clazz) ? _completed[clazz].LookUp(method.GetSignature(), aspect) : null;
        }

        #endregion

        #region Utilities

        private bool GetSwitchValue(bool value, string clazz, string methodSignature, string aspect)
        {
            if (_classOperations.ContainsKey(clazz))
            {
                return _classOperations[clazz].GetSwitchValue(value, methodSignature, aspect);
            }
            return _aspectOperations.ContainsKey(aspect) ? _aspectOperations[aspect].Switch(value) : value;
        }

        private int Switch(PropertyInfo property, SwitchOperation operation)
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
            lock(this)
            {
                if (_completed.ContainsKey(clazz))
                {
                    // the class is completed
                    return _completed[clazz].SwitchProperty(property.Name, operation);
                }
                if (!_classOperations.ContainsKey(clazz))
                {
                    // the class is not loaded yet
                    _classOperations.Add(clazz, SwitchFactory.InitializeClassAdviceSwitchOperation(_sequenceGenerator, _aspectOperations));
                }
                _classOperations[clazz].SwitchProperty(
                    property.GetMethod == null ? null : property.GetMethod.GetSignature(), 
                    property.SetMethod == null ? null : property.SetMethod.GetSignature(), 
                    operation);
            }
            return -1;
        }

        private int Switch(string aspect, SwitchOperation operation)
        {
            if (string.IsNullOrWhiteSpace(aspect))
            {
                throw new ArgumentNullException("aspect");
            }
            var switched = 0;
            lock (this)
            {
                SwitchOperationStatus operationStatus;
                if (_aspectOperations.ContainsKey(aspect))
                {
                    operationStatus = _aspectOperations[aspect];
                    operationStatus.Switch(operation);
                }
                else
                {
                    operationStatus = SwitchFactory.InitializeSwitchOperationStatus(_sequenceGenerator, operation);
                    _aspectOperations.Add(aspect, operationStatus);
                }
                foreach(var completed in _completed.Values)
                {
                    if (completed.IsAspectApplied(aspect))
                    {
                        switched += completed.SwitchAspect(aspect, operation);
                    }
                }
                foreach (var classOperation in _classOperations.Values)
                {
                    classOperation.SwitchAspect(aspect, operation);
                }
                
            }
            return switched;
        }

        private int Switch(MethodInfo method, SwitchOperation operation)
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
            lock (this)
            {
                if (_completed.ContainsKey(clazz))
                {
                    // the class is completed
                    return _completed[clazz].SwitchMethod(signature, operation);
                }
                if (!_classOperations.ContainsKey(clazz))
                {
                    // the class is not loaded yet
                    _classOperations.Add(clazz, SwitchFactory.InitializeClassAdviceSwitchOperation(_sequenceGenerator, _aspectOperations));
                }
                _classOperations[clazz].SwitchMethod(signature, operation);
            } 
            return -1;
        }

        private int Switch(Type type, SwitchOperation operation)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            var clazz = type.FullName;
            lock (this)
            {
                if (_completed.ContainsKey(clazz))
                {
                    // the class is completed
                    return _completed[clazz].Switch(operation);
                }
                if (!_classOperations.ContainsKey(clazz))
                {
                    // the class is not loaded yet
                    _classOperations.Add(clazz, SwitchFactory.InitializeClassAdviceSwitchOperation(_sequenceGenerator, _aspectOperations));
                }
                _classOperations[clazz].Switch(operation);
            }
            return -1;
        }

        private int Switch(PropertyInfo property, string aspect, SwitchOperation operation)
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
            lock (this)
            {
                if (_completed.ContainsKey(clazz))
                {
                    // the class is completed
                    return _completed[clazz].SwitchPropertyAspect(property.Name, aspect, operation);
                }
                if (!_classOperations.ContainsKey(clazz))
                {
                    // the class is not loaded yet
                    _classOperations.Add(clazz, SwitchFactory.InitializeClassAdviceSwitchOperation(_sequenceGenerator, _aspectOperations));
                }
                _classOperations[clazz].SwitchPropertyAspect(
                    property.GetMethod == null ? null : property.GetMethod.GetSignature(), 
                    property.SetMethod == null ? null : property.SetMethod.GetSignature(), 
                    aspect, 
                    operation);
            }
            return -1;
        }

        private int Switch(MethodInfo method, string aspect, SwitchOperation operation)
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
            lock (this)
            {
                if (_completed.ContainsKey(clazz))
                {
                    // the class is completed
                    return _completed[clazz].SwitchMethodAspect(method.GetSignature(), aspect, operation);
                }
                if (!_classOperations.ContainsKey(clazz))
                {
                    // the class is not loaded yet
                    _classOperations.Add(clazz, SwitchFactory.InitializeClassAdviceSwitchOperation(_sequenceGenerator, _aspectOperations));
                }
                _classOperations[clazz].SwitchMethodAspect(method.GetSignature(), aspect, operation);
            }
            return -1;
        }

        private int Switch(Type type, string aspect, SwitchOperation operation)
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
            lock (this)
            {
                if (_completed.ContainsKey(clazz))
                {
                    // the class is completed
                    return _completed[clazz].SwitchAspect(aspect, operation);
                }
                if (!_classOperations.ContainsKey(clazz))
                {
                    // the class is not loaded yet
                    _classOperations.Add(clazz, SwitchFactory.InitializeClassAdviceSwitchOperation(_sequenceGenerator, _aspectOperations));
                }
                _classOperations[clazz].SwitchAspect(aspect, operation);
            }
            return -1;
        }

        #endregion
    }
}
