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
        private const int UseGeneratedSequence = -1;
        private readonly Dictionary<string, Dictionary<string, SwitchOperationStatus>> _propertyAspectSwitchDictionary = new Dictionary<string, Dictionary<string, SwitchOperationStatus>>();
        private readonly Dictionary<string, Dictionary<string, SwitchOperationStatus>> _methodAspectSwitchDictionary = new Dictionary<string, Dictionary<string, SwitchOperationStatus>>();
        private readonly Dictionary<string, SwitchOperationStatus> _aspectSwitchDictionary = new Dictionary<string, SwitchOperationStatus>();
        private readonly Dictionary<string, SwitchOperationStatus> _methodSwitchDictionary = new Dictionary<string, SwitchOperationStatus>();
        private readonly SequenceGenerator _sequenceGenerator;

        private SwitchOperationStatus Operation { get; set; }

        public ClassAdviceSwitchOperation(SequenceGenerator sequenceGenerator, IReadOnlyDictionary<string, SwitchOperationStatus> aspectOperations)
        {
            if (sequenceGenerator == null)
            {
                throw new ArgumentNullException("sequenceGenerator");
            }
            if (aspectOperations == null)
            {
                throw new ArgumentNullException("aspectOperations");
            }
            _sequenceGenerator = sequenceGenerator;
            InitializeAspectSwitches(aspectOperations);
        }

        #region Switch

        public void Switch(SwitchOperation operation)
        {
            int sequence;
            if (Operation == null)
            {
                Operation = SwitchFactory.InitializeSwitchOperationStatus(_sequenceGenerator, operation);
                sequence = Operation.Sequence;
            }
            else
            {
                sequence = SwitchExistingClass(operation);
            }
            SwitchAll(operation, sequence);
        }

        public void SwitchAspect(string aspect, SwitchOperation operation)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
            if (string.IsNullOrEmpty(aspect))
            {
                throw new ArgumentNullException("aspect");
            }
#endif
            int sequence;
            if (_aspectSwitchDictionary.ContainsKey(aspect))
            {
                sequence = SwitchExistingAspect(aspect, operation);
            }
            else
            {
                var operationStatus = SwitchFactory.InitializeSwitchOperationStatus(_sequenceGenerator, operation);
                sequence = operationStatus.Sequence;
                _aspectSwitchDictionary.Add(aspect, operationStatus);
            }
            SwitchAllAspect(aspect, operation, sequence);
        }

        public void SwitchAspect(string aspect, SwitchOperation operation, int sequence)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
            if (string.IsNullOrEmpty(aspect))
            {
                throw new ArgumentNullException("aspect");
            }
            if (sequence < 0)
            {
                throw new ArgumentOutOfRangeException("sequence");
            }
#endif
            if (_aspectSwitchDictionary.ContainsKey(aspect))
            {
                SwitchExistingAspect(aspect, operation, sequence);
            }
            else
            {
                var duplicate = SwitchFactory.InitializeSwitchOperationStatus(_sequenceGenerator, operation);
                _aspectSwitchDictionary.Add(aspect, duplicate);
            }
            SwitchAllAspect(aspect, operation, sequence);
        }

        public void SwitchMethod(string methodSignature, SwitchOperation operation)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
            if (string.IsNullOrEmpty(methodSignature))
            {
                throw new ArgumentNullException("methodSignature");
            }
#endif
            SwitchMethodInternal(methodSignature, operation, UseGeneratedSequence);
        }

        public void SwitchProperty(string getterSignature, string setterSignature, SwitchOperation operation)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
            if (string.IsNullOrEmpty(getterSignature) && string.IsNullOrWhiteSpace(setterSignature))
            {
                throw new ArgumentException("Getter and setter can't both be empty");
            }
#endif
            var sequence = UseGeneratedSequence;
            if (!string.IsNullOrWhiteSpace(getterSignature))
            {
                sequence = SwitchMethodInternal(getterSignature, operation, sequence);
            }
            if (!string.IsNullOrWhiteSpace(setterSignature))
            {
                SwitchMethodInternal(setterSignature, operation, sequence);
            }
            
        }

        public void SwitchMethodAspect(string methodSignature, string aspect, SwitchOperation operation)
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
            SwitchMethodAspectInternal(methodSignature, aspect, operation, UseGeneratedSequence);
        }

        public void SwitchPropertyAspect(string getterSignature, string setterSignature, string aspect, SwitchOperation operation)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
            if (string.IsNullOrEmpty(getterSignature) && string.IsNullOrWhiteSpace(setterSignature))
            {
                throw new ArgumentException("Getter and setter can't both be empty");
            }
            if (string.IsNullOrEmpty(aspect))
            {
                throw new ArgumentNullException("aspect");
            }
#endif
            var sequence = UseGeneratedSequence;
            if (!string.IsNullOrWhiteSpace(getterSignature))
            {
                sequence = SwitchMethodAspectInternal(getterSignature, aspect, operation, sequence);
            }
            if (!string.IsNullOrWhiteSpace(setterSignature))
            {
                SwitchMethodAspectInternal(setterSignature, aspect, operation, sequence);
            }
        }

        #endregion

        #region Utilities

        private int SwitchMethodInternal(string methodSignature, SwitchOperation operation, int sequence)
        {
            if (_methodSwitchDictionary.ContainsKey(methodSignature))
            {
                if (sequence == UseGeneratedSequence)
                {
                    _methodSwitchDictionary[methodSignature].Switch(operation);
                }
                else
                {
                    _methodSwitchDictionary[methodSignature].Switch(operation, sequence);
                }
                sequence = _methodSwitchDictionary[methodSignature].Sequence;
            }
            else
            {
                var operationStatus = SwitchFactory.InitializeSwitchOperationStatus(_sequenceGenerator, operation);
                sequence = operationStatus.Sequence;
                _methodSwitchDictionary.Add(methodSignature, operationStatus);
            }
            SwitchAllExistingMethodAspect(methodSignature, operation, sequence);
            return sequence;
        }

        private int SwitchMethodAspectInternal(string methodSignature, string aspect, SwitchOperation operation, int sequence)
        {
            if (_methodAspectSwitchDictionary.ContainsKey(methodSignature))
            {
                if (_methodAspectSwitchDictionary[methodSignature].ContainsKey(aspect))
                {
                    if (sequence == UseGeneratedSequence)
                    {
                        var operationStatus = _methodAspectSwitchDictionary[methodSignature][aspect];
                        operationStatus.Switch(operation);
                        sequence = operationStatus.Sequence;
                    }
                    else
                    {
                        _methodAspectSwitchDictionary[methodSignature][aspect].Switch(operation, sequence);
                    }
                }
                else
                {
                    var operationStatus = SwitchFactory.InitializeSwitchOperationStatus(_sequenceGenerator, operation);
                    sequence = operationStatus.Sequence;
                    _methodAspectSwitchDictionary[methodSignature].Add(aspect, operationStatus);
                }
            }
            else
            {
                var operationStatus = SwitchFactory.InitializeSwitchOperationStatus(_sequenceGenerator, operation);
                sequence = operationStatus.Sequence;
                _methodAspectSwitchDictionary.Add(methodSignature,
                    new Dictionary<string, SwitchOperationStatus> { { aspect, operationStatus } });
            }
            return sequence;
        }

        private void SwitchAllExistingMethodAspect(string methodSignature, SwitchOperation operation, int sequence)
        {
            if (_methodAspectSwitchDictionary.ContainsKey(methodSignature))
            {
                foreach (var aspectSwitch in _methodAspectSwitchDictionary[methodSignature].Values)
                {
                    aspectSwitch.Switch(operation, sequence);
                }
            }
        }

        private int SwitchExistingAspect(string aspect, SwitchOperation operation)
        {
            _aspectSwitchDictionary[aspect].Switch(operation);
            return _aspectSwitchDictionary[aspect].Sequence;
        }

        private void SwitchExistingAspect(string aspect, SwitchOperation operation, int sequence)
        {
            _aspectSwitchDictionary[aspect].Switch(operation, sequence);
        }

        private void SwitchAllAspect(string aspect, SwitchOperation operation, int sequence)
        {
            foreach (var method in _methodAspectSwitchDictionary.Keys)
            {
                var aspectSwitch = _methodAspectSwitchDictionary[method];
                if (aspectSwitch.ContainsKey(aspect))
                {
                    aspectSwitch[aspect].Switch(operation, sequence);
                }
            }
            foreach (var property in _propertyAspectSwitchDictionary.Keys)
            {
                var aspectSwitch = _propertyAspectSwitchDictionary[property];
                if (aspectSwitch.ContainsKey(aspect))
                {
                    aspectSwitch[aspect].Switch(operation, sequence);
                }
            }
        }

        private int SwitchExistingClass(SwitchOperation operation)
        {
            Operation.Switch(operation);
            return Operation.Sequence;
        }

        private void SwitchAll(SwitchOperation operation, int sequence)
        {
            foreach (var methodSwitch in _methodSwitchDictionary.Values)
            {
                methodSwitch.Switch(operation, sequence);
            }
            foreach (var aspectSwitch in _aspectSwitchDictionary.Values)
            {
                aspectSwitch.Switch(operation, sequence);
            }
            foreach (var methodAspectSwitch in _methodAspectSwitchDictionary.Values)
            {
                foreach (var aspectSwitch in methodAspectSwitch.Values)
                {
                    aspectSwitch.Switch(operation, sequence);
                }
            }
            foreach (var propertyAspectSwitch in _propertyAspectSwitchDictionary.Values)
            {
                foreach (var aspectSwitch in propertyAspectSwitch.Values)
                {
                    aspectSwitch.Switch(operation, sequence);
                }
            }
        }

        private void InitializeAspectSwitches(IReadOnlyDictionary<string, SwitchOperationStatus> aspectOperations)
        {
            foreach (var aspectOperation in aspectOperations)
            {
                var operation = SwitchFactory.InitializeSwitchOperationStatus(_sequenceGenerator, aspectOperation.Value);
                _aspectSwitchDictionary.Add(aspectOperation.Key, operation);
            }
        }

        #endregion

        #region GetSwitchValue

        public bool GetSwitchValue(bool value, string methodSignature, string aspect)
        {
            var operations = new SortedDictionary<int, SwitchStatus>();
            if (Operation != null)
            {
                operations[Operation.Sequence] = Operation.Status;
            }

            if (_aspectSwitchDictionary.ContainsKey(aspect))
            {
                var operation = _aspectSwitchDictionary[aspect];
                operations[operation.Sequence] = operation.Status;
            }

            if (_methodSwitchDictionary.ContainsKey(methodSignature))
            {
                var operation = _methodSwitchDictionary[methodSignature];
                operations[operation.Sequence] = operation.Status;
            }

            if (_methodAspectSwitchDictionary.ContainsKey(methodSignature) &&
                _methodAspectSwitchDictionary[methodSignature].ContainsKey(aspect))
            {
                var operation = _methodAspectSwitchDictionary[methodSignature][aspect];
                operations[operation.Sequence] = operation.Status;
            }

            var switchCount = 0;
            var operationList = operations.Values.ToList();
            for (var i = operationList.Count - 1; i >= 0; i--)
            {
                var operation = operationList[i];
                if (operation == SwitchStatus.Switched)
                {
                    switchCount++;
                }
                else if (operation == SwitchStatus.On)
                {
                    value = true;
                    break;
                }
                else if (operation == SwitchStatus.Off)
                {
                    value = false;
                    break;
                }
            }
            value ^= switchCount%2 == 1;
            return value;
        }

        #endregion
    }
}
