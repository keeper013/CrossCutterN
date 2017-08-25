// <copyright file="AspectSwitch.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Switch
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using CrossCutterN.Base.Common;
    using CrossCutterN.Base.MultiThreading;

    /// <summary>
    /// Considering this aspect switching isn't supposed to happen often, crude lock(this) is used to cater for multithreading.
    /// </summary>
    internal sealed class AspectSwitch : IAspectSwitch, IAspectSwitchBuilder, IAspectSwitchGlancer
    {
        private readonly IList<bool> switchList = new List<bool>();
        private readonly IDictionary<string, IClassAspectSwitchBuilder> buildingUps =
            new Dictionary<string, IClassAspectSwitchBuilder>();

        private readonly IDictionary<string, IClassAspectSwitch> completed =
            new Dictionary<string, IClassAspectSwitch>();

        private readonly Dictionary<string, IClassAspectSwitchOperation> classOperations =
            new Dictionary<string, IClassAspectSwitchOperation>();

        // we never know when will all class be loaded, so this aspect operation dictionary is necessary
        private readonly Dictionary<string, SwitchOperationStatus> aspectOperations =
            new Dictionary<string, SwitchOperationStatus>();

        private readonly SequenceGenerator sequenceGenerator = new SequenceGenerator();

        private readonly ISmartReadWriteLock builderLock = LockFactory.GetSmartReadWriteLock();
        private readonly ISmartReadWriteLock completedLock = LockFactory.GetSmartReadWriteLock();
        private readonly ISmartReadWriteLock operationLock = LockFactory.GetSmartReadWriteLock();
        private readonly ISmartReadWriteLock switchLock = LockFactory.GetSmartReadWriteLock();

        /// <inheritdoc/>
        public void Complete(string clazz)
        {
#if DEBUG
            if (string.IsNullOrWhiteSpace(clazz))
            {
                throw new ArgumentNullException("clazz");
            }

            if (completed.ContainsKey(clazz))
            {
                throw new ArgumentException($"{clazz} is completed for switch registration already.");
            }

            if (!buildingUps.ContainsKey(clazz))
            {
                throw new ArgumentException($"{clazz} is not built up at all.");
            }
#endif
            IClassAspectSwitchOperation classOperations = null;
            using (operationLock.ReadLock)
            {
                if (this.classOperations.ContainsKey(clazz))
                {
                    classOperations = this.classOperations[clazz];
                }
            }

            using (builderLock.WriteLock)
            using (completedLock.WriteLock)
            using (operationLock.WriteLock)
            {
                completed.Add(clazz, buildingUps[clazz].Build(clazz, classOperations, aspectOperations));
                buildingUps.Remove(clazz);
                this.classOperations.Remove(clazz);
            }
        }

        /// <inheritdoc/>
        public bool IsOn(int id)
        {
            using (switchLock.ReadLock)
            {
#if DEBUG
                // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
                if (switchList.Count <= id)
                {
                    throw new InvalidOperationException($"Switch for id {id} is not found");
                }
#endif

                // Considering List is not thread safe, locking is necessary here
                return switchList[id];
            }
        }

        /// <inheritdoc/>
        public int RegisterSwitch(string clazz, string property, string methodSignature, string aspect, bool value)
        {
            using (builderLock.WriteLock)
#if DEBUG
            using (completedLock.ReadLock)
#endif
            using (switchLock.WriteLock)
            {
#if DEBUG
                // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
                if (string.IsNullOrEmpty(clazz))
                {
                    throw new ArgumentNullException("clazz");
                }

                if (completed.ContainsKey(clazz))
                {
                    throw new ArgumentException($"{clazz} is completed for switch registration already.");
                }
#endif
                var id = switchList.Count;
                switchList.Add(value);
                if (!buildingUps.ContainsKey(clazz))
                {
                    buildingUps.Add(clazz, SwitchFactory.InitializeClassAspectSwitch(switchList, switchLock));
                }

                buildingUps[clazz].RegisterSwitch(id, property, methodSignature, aspect);
                return id;
            }
        }

        /// <inheritdoc/>
        public int Switch(PropertyInfo property) => Switch(property, SwitchOperation.Switch);

        /// <inheritdoc/>
        public int Switch(string aspect) => Switch(aspect, SwitchOperation.Switch);

        /// <inheritdoc/>
        public int Switch(MethodInfo method) => Switch(method, SwitchOperation.Switch);

        /// <inheritdoc/>
        public int Switch(Type type) => Switch(type, SwitchOperation.Switch);

        /// <inheritdoc/>
        public int Switch(PropertyInfo property, string aspect) => Switch(property, aspect, SwitchOperation.Switch);

        /// <inheritdoc/>
        public int Switch(MethodInfo method, string aspect) => Switch(method, aspect, SwitchOperation.Switch);

        /// <inheritdoc/>
        public int Switch(Type type, string aspect) => Switch(type, aspect, SwitchOperation.Switch);

        /// <inheritdoc/>
        public int SwitchOff(PropertyInfo property) => Switch(property, SwitchOperation.Off);

        /// <inheritdoc/>
        public int SwitchOff(string aspect) => Switch(aspect, SwitchOperation.Off);

        /// <inheritdoc/>
        public int SwitchOff(MethodInfo method) => Switch(method, SwitchOperation.Off);

        /// <inheritdoc/>
        public int SwitchOff(Type type) => Switch(type, SwitchOperation.Off);

        /// <inheritdoc/>
        public int SwitchOff(PropertyInfo property, string aspect) => Switch(property, aspect, SwitchOperation.Off);

        /// <inheritdoc/>
        public int SwitchOff(MethodInfo method, string aspect) => Switch(method, aspect, SwitchOperation.Off);

        /// <inheritdoc/>
        public int SwitchOff(Type type, string aspect) => Switch(type, aspect, SwitchOperation.Off);

        /// <inheritdoc/>
        public int SwitchOn(PropertyInfo property) => Switch(property, SwitchOperation.On);

        /// <inheritdoc/>
        public int SwitchOn(string aspect) => Switch(aspect, SwitchOperation.On);

        /// <inheritdoc/>
        public int SwitchOn(MethodInfo method) => Switch(method, SwitchOperation.On);

        /// <inheritdoc/>
        public int SwitchOn(Type type) => Switch(type, SwitchOperation.On);

        /// <inheritdoc/>
        public int SwitchOn(PropertyInfo property, string aspect) => Switch(property, aspect, SwitchOperation.On);

        /// <inheritdoc/>
        public int SwitchOn(MethodInfo method, string aspect) => Switch(method, aspect, SwitchOperation.On);

        /// <inheritdoc/>
        public int SwitchOn(Type type, string aspect) => Switch(type, aspect, SwitchOperation.On);

        /// <inheritdoc/>
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

            var clazz = method.DeclaringType.FullName;

            using (completedLock.ReadLock)
            {
                return completed.ContainsKey(clazz) ? completed[clazz].Lookup(method.GetSignature(), aspect) : null;
            }
        }

        private int Switch(PropertyInfo property, SwitchOperation operation)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }

            if (property.DeclaringType == null)
            {
                throw new ArgumentException($"Property {property.Name} doesn't have declaring type.");
            }

            var clazz = property.DeclaringType.FullName;

            using (completedLock.ReadLock)
            {
                if (completed.ContainsKey(clazz))
                {
                    // the class is completed
                    return completed[clazz].SwitchProperty(property.Name, operation);
                }
            }

            IClassAspectSwitchOperation op;
            using (operationLock.ReadLock)
            {
                if (!classOperations.ContainsKey(clazz))
                {
                    // the class is not loaded yet
                    op = SwitchFactory.InitializeClassAspectSwitchOperation(sequenceGenerator, aspectOperations);
                    using (operationLock.WriteLock)
                    {
                        classOperations.Add(clazz, op);
                    }
                }
                else
                {
                    op = classOperations[clazz];
                }
            }

            op.SwitchProperty(
                property.GetMethod?.GetSignature(),
                property.SetMethod?.GetSignature(),
                operation);
            return -1;
        }

        private int Switch(string aspect, SwitchOperation operation)
        {
            if (string.IsNullOrWhiteSpace(aspect))
            {
                throw new ArgumentNullException("aspect");
            }

            var switched = 0;
            using (completedLock.ReadLock)
            using (operationLock.ReadLock)
            {
                foreach (var completed in completed.Values)
                {
                    if (completed.IsAspectApplied(aspect))
                    {
                        switched += completed.SwitchAspect(aspect, operation);
                    }
                }

                SwitchOperationStatus operationStatus;
                if (aspectOperations.ContainsKey(aspect))
                {
                    operationStatus = aspectOperations[aspect];
                    operationStatus.Switch(operation);
                }
                else
                {
                    operationStatus = SwitchFactory.InitializeSwitchOperationStatus(sequenceGenerator, operation);
                    using (operationLock.WriteLock)
                    {
                        aspectOperations.Add(aspect, operationStatus);
                    }
                }

                foreach (var classOperation in classOperations.Values)
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
                throw new ArgumentException($"Method {signature} doesn't have declaring type.");
            }

            var clazz = method.DeclaringType.FullName;
            using (completedLock.ReadLock)
            {
                if (completed.ContainsKey(clazz))
                {
                    // the class is completed
                    return completed[clazz].SwitchMethod(signature, operation);
                }
            }

            IClassAspectSwitchOperation op;
            using (operationLock.ReadLock)
            {
                if (!classOperations.ContainsKey(clazz))
                {
                    // the class is not loaded yet
                    op = SwitchFactory.InitializeClassAspectSwitchOperation(sequenceGenerator, aspectOperations);
                    using (operationLock.WriteLock)
                    {
                        classOperations.Add(clazz, op);
                    }
                }
                else
                {
                    op = classOperations[clazz];
                }
            }

            op.SwitchMethod(signature, operation);
            return -1;
        }

        private int Switch(Type type, SwitchOperation operation)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            var clazz = type.FullName;
            using (completedLock.ReadLock)
            {
                if (completed.ContainsKey(clazz))
                {
                    // the class is completed
                    return completed[clazz].Switch(operation);
                }
            }

            IClassAspectSwitchOperation op;
            using (operationLock.ReadLock)
            {
                if (!classOperations.ContainsKey(clazz))
                {
                    // the class is not loaded yet
                    op = SwitchFactory.InitializeClassAspectSwitchOperation(sequenceGenerator, aspectOperations);
                    using (operationLock.WriteLock)
                    {
                        classOperations.Add(clazz, op);
                    }
                }
                else
                {
                    op = classOperations[clazz];
                }
            }

            op.Switch(operation);
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
                throw new ArgumentException($"Property {property.Name} doesn't have declaring type.");
            }

            if (string.IsNullOrWhiteSpace(aspect))
            {
                throw new ArgumentNullException("aspect");
            }

            var clazz = property.DeclaringType.FullName;
            using (completedLock.ReadLock)
            {
                if (completed.ContainsKey(clazz))
                {
                    // the class is completed
                    return completed[clazz].SwitchPropertyAspect(property.Name, aspect, operation);
                }
            }

            IClassAspectSwitchOperation op;
            using (operationLock.ReadLock)
            {
                if (!classOperations.ContainsKey(clazz))
                {
                    // the class is not loaded yet
                    op = SwitchFactory.InitializeClassAspectSwitchOperation(sequenceGenerator, aspectOperations);
                    using (operationLock.WriteLock)
                    {
                        classOperations.Add(clazz, op);
                    }
                }
                else
                {
                    op = classOperations[clazz];
                }
            }

            op.SwitchPropertyAspect(property.GetMethod?.GetSignature(), property.SetMethod?.GetSignature(), aspect, operation);
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
                throw new ArgumentException($"Method {method.GetSignature()} doesn't have declaring type.");
            }

            if (string.IsNullOrWhiteSpace(aspect))
            {
                throw new ArgumentNullException("aspect");
            }

            var clazz = method.DeclaringType.FullName;

            using (completedLock.ReadLock)
            {
                if (completed.ContainsKey(clazz))
                {
                    // the class is completed
                    return completed[clazz].SwitchMethodAspect(method.GetSignature(), aspect, operation);
                }
            }

            IClassAspectSwitchOperation op;
            using (operationLock.ReadLock)
            {
                if (!classOperations.ContainsKey(clazz))
                {
                    // the class is not loaded yet
                    op = SwitchFactory.InitializeClassAspectSwitchOperation(sequenceGenerator, aspectOperations);
                    using (operationLock.WriteLock)
                    {
                        classOperations.Add(clazz, op);
                    }
                }
                else
                {
                    op = classOperations[clazz];
                }
            }

            op.SwitchMethodAspect(method.GetSignature(), aspect, operation);
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

            using (completedLock.ReadLock)
            {
                if (completed.ContainsKey(clazz))
                {
                    // the class is completed
                    return completed[clazz].SwitchAspect(aspect, operation);
                }
            }

            IClassAspectSwitchOperation op;
            using (operationLock.ReadLock)
            {
                if (!classOperations.ContainsKey(clazz))
                {
                    // the class is not loaded yet
                    op = SwitchFactory.InitializeClassAspectSwitchOperation(sequenceGenerator, aspectOperations);
                    using (operationLock.WriteLock)
                    {
                        classOperations.Add(clazz, op);
                    }
                }
                else
                {
                    op = classOperations[clazz];
                }
            }

            op.SwitchAspect(aspect, operation);
            return -1;
        }
    }
}
