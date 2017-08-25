// <copyright file="ClassAspectSwitchOperation.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Switch
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Operation record for not loaded class implementation
    /// </summary>
    internal sealed class ClassAspectSwitchOperation : IClassAspectSwitchOperation
    {
        private const int UseGeneratedSequence = -1;
        private readonly Dictionary<string, Dictionary<string, SwitchOperationStatus>> propertyAspectSwitchDictionary = new Dictionary<string, Dictionary<string, SwitchOperationStatus>>();
        private readonly Dictionary<string, Dictionary<string, SwitchOperationStatus>> methodAspectSwitchDictionary = new Dictionary<string, Dictionary<string, SwitchOperationStatus>>();
        private readonly Dictionary<string, SwitchOperationStatus> aspectSwitchDictionary = new Dictionary<string, SwitchOperationStatus>();
        private readonly Dictionary<string, SwitchOperationStatus> methodSwitchDictionary = new Dictionary<string, SwitchOperationStatus>();
        private readonly SequenceGenerator sequenceGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassAspectSwitchOperation"/> class.
        /// </summary>
        /// <param name="sequenceGenerator">Sequence generator for operations.</param>
        /// <param name="aspectOperations">Operations on aspects.</param>
        public ClassAspectSwitchOperation(SequenceGenerator sequenceGenerator, IReadOnlyDictionary<string, SwitchOperationStatus> aspectOperations)
        {
            if (aspectOperations == null)
            {
                throw new ArgumentNullException("aspectOperations");
            }

            this.sequenceGenerator = sequenceGenerator ?? throw new ArgumentNullException("sequenceGenerator");
            InitializeAspectSwitches(aspectOperations);
        }

        private SwitchOperationStatus Operation { get; set; }

        /// <inheritdoc/>
        public void Switch(SwitchOperation operation)
        {
            int sequence;
            if (Operation == null)
            {
                Operation = SwitchFactory.InitializeSwitchOperationStatus(sequenceGenerator, operation);
                sequence = Operation.Sequence;
            }
            else
            {
                sequence = SwitchExistingClass(operation);
            }

            SwitchAll(operation, sequence);
        }

        /// <inheritdoc/>
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
            if (aspectSwitchDictionary.ContainsKey(aspect))
            {
                sequence = SwitchExistingAspect(aspect, operation);
            }
            else
            {
                var operationStatus = SwitchFactory.InitializeSwitchOperationStatus(sequenceGenerator, operation);
                sequence = operationStatus.Sequence;
                aspectSwitchDictionary.Add(aspect, operationStatus);
            }

            SwitchAllAspect(aspect, operation, sequence);
        }

        /// <inheritdoc/>
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
            if (aspectSwitchDictionary.ContainsKey(aspect))
            {
                SwitchExistingAspect(aspect, operation, sequence);
            }
            else
            {
                var duplicate = SwitchFactory.InitializeSwitchOperationStatus(sequenceGenerator, operation);
                aspectSwitchDictionary.Add(aspect, duplicate);
            }

            SwitchAllAspect(aspect, operation, sequence);
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public bool GetSwitchValue(bool value, string methodSignature, string aspect)
        {
            var operations = new SortedDictionary<int, SwitchStatus>();
            if (Operation != null)
            {
                operations[Operation.Sequence] = Operation.Status;
            }

            if (aspectSwitchDictionary.ContainsKey(aspect))
            {
                var operation = aspectSwitchDictionary[aspect];
                operations[operation.Sequence] = operation.Status;
            }

            if (methodSwitchDictionary.ContainsKey(methodSignature))
            {
                var operation = methodSwitchDictionary[methodSignature];
                operations[operation.Sequence] = operation.Status;
            }

            if (methodAspectSwitchDictionary.ContainsKey(methodSignature) &&
                methodAspectSwitchDictionary[methodSignature].ContainsKey(aspect))
            {
                var operation = methodAspectSwitchDictionary[methodSignature][aspect];
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

            value ^= switchCount % 2 == 1;
            return value;
        }

        private int SwitchMethodInternal(string methodSignature, SwitchOperation operation, int sequence)
        {
            if (methodSwitchDictionary.ContainsKey(methodSignature))
            {
                if (sequence == UseGeneratedSequence)
                {
                    methodSwitchDictionary[methodSignature].Switch(operation);
                }
                else
                {
                    methodSwitchDictionary[methodSignature].Switch(operation, sequence);
                }

                sequence = methodSwitchDictionary[methodSignature].Sequence;
            }
            else
            {
                var operationStatus = SwitchFactory.InitializeSwitchOperationStatus(sequenceGenerator, operation);
                sequence = operationStatus.Sequence;
                methodSwitchDictionary.Add(methodSignature, operationStatus);
            }

            SwitchAllExistingMethodAspect(methodSignature, operation, sequence);
            return sequence;
        }

        private int SwitchMethodAspectInternal(string methodSignature, string aspect, SwitchOperation operation, int sequence)
        {
            if (methodAspectSwitchDictionary.ContainsKey(methodSignature))
            {
                if (methodAspectSwitchDictionary[methodSignature].ContainsKey(aspect))
                {
                    if (sequence == UseGeneratedSequence)
                    {
                        var operationStatus = methodAspectSwitchDictionary[methodSignature][aspect];
                        operationStatus.Switch(operation);
                        sequence = operationStatus.Sequence;
                    }
                    else
                    {
                        methodAspectSwitchDictionary[methodSignature][aspect].Switch(operation, sequence);
                    }
                }
                else
                {
                    var operationStatus = SwitchFactory.InitializeSwitchOperationStatus(sequenceGenerator, operation);
                    sequence = operationStatus.Sequence;
                    methodAspectSwitchDictionary[methodSignature].Add(aspect, operationStatus);
                }
            }
            else
            {
                var operationStatus = SwitchFactory.InitializeSwitchOperationStatus(sequenceGenerator, operation);
                sequence = operationStatus.Sequence;
                methodAspectSwitchDictionary.Add(methodSignature, new Dictionary<string, SwitchOperationStatus> { { aspect, operationStatus } });
            }

            return sequence;
        }

        private void SwitchAllExistingMethodAspect(string methodSignature, SwitchOperation operation, int sequence)
        {
            if (methodAspectSwitchDictionary.ContainsKey(methodSignature))
            {
                foreach (var aspectSwitch in methodAspectSwitchDictionary[methodSignature].Values)
                {
                    aspectSwitch.Switch(operation, sequence);
                }
            }
        }

        private int SwitchExistingAspect(string aspect, SwitchOperation operation)
        {
            aspectSwitchDictionary[aspect].Switch(operation);
            return aspectSwitchDictionary[aspect].Sequence;
        }

        private void SwitchExistingAspect(string aspect, SwitchOperation operation, int sequence)
        {
            aspectSwitchDictionary[aspect].Switch(operation, sequence);
        }

        private void SwitchAllAspect(string aspect, SwitchOperation operation, int sequence)
        {
            foreach (var method in methodAspectSwitchDictionary.Keys)
            {
                var aspectSwitch = methodAspectSwitchDictionary[method];
                if (aspectSwitch.ContainsKey(aspect))
                {
                    aspectSwitch[aspect].Switch(operation, sequence);
                }
            }

            foreach (var property in propertyAspectSwitchDictionary.Keys)
            {
                var aspectSwitch = propertyAspectSwitchDictionary[property];
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
            foreach (var methodSwitch in methodSwitchDictionary.Values)
            {
                methodSwitch.Switch(operation, sequence);
            }

            foreach (var aspectSwitch in aspectSwitchDictionary.Values)
            {
                aspectSwitch.Switch(operation, sequence);
            }

            foreach (var methodAspectSwitch in methodAspectSwitchDictionary.Values)
            {
                foreach (var aspectSwitch in methodAspectSwitch.Values)
                {
                    aspectSwitch.Switch(operation, sequence);
                }
            }

            foreach (var propertyAspectSwitch in propertyAspectSwitchDictionary.Values)
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
                var operation = SwitchFactory.InitializeSwitchOperationStatus(sequenceGenerator, aspectOperation.Value);
                aspectSwitchDictionary.Add(aspectOperation.Key, operation);
            }
        }
    }
}
