/**
* Description: Operation record for classes that are not loaded yet
* Author: David Cui
*/

namespace CrossCutterN.Advice.Switch
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class ClassAdviceSwitchOperation : IClassAdviceSwitchOperation
    {
        private readonly Dictionary<string, Dictionary<string, SwitchOperation>> _propertyAspectSwitchDictionary = new Dictionary<string, Dictionary<string, SwitchOperation>>();
        private readonly Dictionary<string, Dictionary<string, SwitchOperation>> _methodAspectSwitchDictionary = new Dictionary<string, Dictionary<string, SwitchOperation>>();
        private readonly Dictionary<string, SwitchOperation> _aspectSwitchDictionary = new Dictionary<string, SwitchOperation>();
        private readonly Dictionary<string, SwitchOperation> _methodSwitchDictionary = new Dictionary<string, SwitchOperation>();
        private readonly Dictionary<string, SwitchOperation> _propertySwitchDictionary = new Dictionary<string, SwitchOperation>();
        private readonly IReadOnlyDictionary<string, SwitchOperation> _aspectOperations;
        private readonly SequenceGenerator _sequenceGenerator;

        private SwitchOperation Operation { get; set; }

        public ClassAdviceSwitchOperation(IReadOnlyDictionary<string, SwitchOperation> aspectOperations, SequenceGenerator sequenceGenerator)
        {
            if (aspectOperations == null)
            {
                throw new ArgumentNullException("aspectOperations");
            }
            if (sequenceGenerator == null)
            {
                throw new ArgumentNullException("sequenceGenerator");
            }
            _aspectOperations = aspectOperations;
            _sequenceGenerator = sequenceGenerator;
        }

        #region Switch

        public void Switch(SwitchStatus status)
        {
            if (Operation == null)
            {
                Operation = SwitchFactory.InitializeSwitchOperation(_sequenceGenerator, status);
            }
            else
            {
                var neutralized = Operation.Switch(status);
                if (neutralized)
                {
                    Operation = null;
                }
            }
        }

        public void SwitchMethod(string methodSignature, SwitchStatus status)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
            if (string.IsNullOrEmpty(methodSignature))
            {
                throw new ArgumentNullException("methodSignature");
            }
#endif
            if (_methodSwitchDictionary.ContainsKey(methodSignature))
            {
                var neutralized = _methodSwitchDictionary[methodSignature].Switch(status);
                if (neutralized)
                {
                    _methodSwitchDictionary.Remove(methodSignature);
                }
            }
            else
            {
                _methodSwitchDictionary.Add(methodSignature, SwitchFactory.InitializeSwitchOperation(_sequenceGenerator, status));
            }
        }

        public void SwitchProperty(string propertyName, SwitchStatus status)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }
#endif
            if (_propertySwitchDictionary.ContainsKey(propertyName))
            {
                var neutralized = _propertySwitchDictionary[propertyName].Switch(status);
                if (neutralized)
                {
                    _propertySwitchDictionary.Remove(propertyName);
                }
            }
            else
            {
                _propertySwitchDictionary.Add(propertyName, SwitchFactory.InitializeSwitchOperation(_sequenceGenerator, status));
            }
        }

        public void SwitchAspect(string aspect, SwitchStatus status)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
            if (string.IsNullOrEmpty(aspect))
            {
                throw new ArgumentNullException("aspect");
            }
#endif
            if (_aspectSwitchDictionary.ContainsKey(aspect))
            {
                var neutralized = _aspectSwitchDictionary[aspect].Switch(status);
                if (neutralized)
                {
                    _aspectSwitchDictionary.Remove(aspect);
                }
            }
            else
            {
                _aspectSwitchDictionary.Add(aspect, SwitchFactory.InitializeSwitchOperation(_sequenceGenerator, status));
            }
        }

        public void SwitchMethodAspect(string methodSignature, string aspect, SwitchStatus status)
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
#endif
            if (_methodAspectSwitchDictionary.ContainsKey(methodSignature))
            {
                if (_methodAspectSwitchDictionary[methodSignature].ContainsKey(aspect))
                {
                    var neutralized = _methodAspectSwitchDictionary[methodSignature][aspect].Switch(status);
                    if (neutralized)
                    {
                        _methodAspectSwitchDictionary[methodSignature].Remove(aspect);
                        if (_methodAspectSwitchDictionary[methodSignature].Count == 0)
                        {
                            _methodAspectSwitchDictionary.Remove(methodSignature);
                        }
                    }
                }
                else
                {
                    _methodAspectSwitchDictionary[methodSignature].Add(aspect, SwitchFactory.InitializeSwitchOperation(_sequenceGenerator, status));
                }
            }
            else
            {
                _methodAspectSwitchDictionary.Add(methodSignature,
                    new Dictionary<string, SwitchOperation> { { aspect, SwitchFactory.InitializeSwitchOperation(_sequenceGenerator, status) } });
            }
        }

        public void SwitchPropertyAspect(string propertyName, string aspect, SwitchStatus status)
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
#endif
            if (_propertyAspectSwitchDictionary.ContainsKey(propertyName))
            {
                if (_propertyAspectSwitchDictionary[propertyName].ContainsKey(aspect))
                {
                    var neutralized = _propertyAspectSwitchDictionary[propertyName][aspect].Switch(status);
                    if (neutralized)
                    {
                        _propertyAspectSwitchDictionary[propertyName].Remove(aspect);
                        if (_propertyAspectSwitchDictionary[propertyName].Count == 0)
                        {
                            _propertyAspectSwitchDictionary.Remove(propertyName);
                        }
                    }
                }
                else
                {
                    _propertyAspectSwitchDictionary[propertyName].Add(aspect, SwitchFactory.InitializeSwitchOperation(_sequenceGenerator, status));
                }
            }
            else
            {
                _propertyAspectSwitchDictionary.Add(propertyName,
                    new Dictionary<string, SwitchOperation> { { aspect, SwitchFactory.InitializeSwitchOperation(_sequenceGenerator, status) } });
            }
        }

        #endregion

        #region GetSwitchValue

        public bool GetSwitchValue(bool value, string propertyName, string methodSignature, string aspect)
        {
            var operations = new SortedDictionary<int, SwitchStatus>();
            if (Operation != null)
            {
                operations.Add(Operation.Sequence, Operation.Status);
            }
            if (_aspectOperations.ContainsKey(aspect))
            {
                var operation = _aspectOperations[aspect];
                operations.Add(operation.Sequence, operation.Status);
            }
            if (_aspectSwitchDictionary.ContainsKey(aspect))
            {
                var operation = _aspectSwitchDictionary[aspect];
                operations.Add(operation.Sequence, operation.Status);
            }
            if (_methodSwitchDictionary.ContainsKey(methodSignature))
            {
                var operation = _methodSwitchDictionary[methodSignature];
                operations.Add(operation.Sequence, operation.Status);
            }
            if (_methodAspectSwitchDictionary.ContainsKey(methodSignature) &&
                _methodAspectSwitchDictionary[methodSignature].ContainsKey(aspect))
            {
                var operation = _methodAspectSwitchDictionary[methodSignature][aspect];
                operations.Add(operation.Sequence, operation.Status);
            }

            if (!string.IsNullOrWhiteSpace(propertyName))
            {
                if (_propertySwitchDictionary.ContainsKey(propertyName))
                {
                    var operation = _propertySwitchDictionary[propertyName];
                    operations.Add(operation.Sequence, operation.Status);
                }
                if (_propertyAspectSwitchDictionary.ContainsKey(propertyName) &&
                    _propertyAspectSwitchDictionary[propertyName].ContainsKey(aspect))
                {
                    var operation = _propertyAspectSwitchDictionary[propertyName][aspect];
                    operations.Add(operation.Sequence, operation.Status);
                }
            }
            
            var statusList = operations.Values.ToList();
            var switchCount = statusList.Count;
            for (var i = statusList.Count - 1; i >= 0; i--)
            {
                if (statusList[i] != SwitchStatus.Switched)
                {
                    value = (statusList[i] == SwitchStatus.On);
                    switchCount = switchCount - i - 1;
                    break;
                }
            }
            value ^= switchCount % 2 == 1;
            return value;
        }

        #endregion
    }
}
