// <copyright file="ClassAspectSwitch.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Switch
{
    using System;
    using System.Collections.Generic;
    using CrossCutterN.Base.Common;
    using CrossCutterN.Base.MultiThreading;

    /// <summary>
    /// Class aspect index on registered switch list
    /// </summary>
    internal sealed class ClassAspectSwitch : IClassAspectSwitch, IClassAspectSwitchBuilder
    {
        private readonly Dictionary<string, Dictionary<string, PropertySwitches>> propertySwitchDictionary = new Dictionary<string, Dictionary<string, PropertySwitches>>();
        private readonly Dictionary<string, Dictionary<string, int>> methodSwitchDictionary = new Dictionary<string, Dictionary<string, int>>();
        private readonly Dictionary<string, List<int>> aspectSwitchDictionary = new Dictionary<string, List<int>>();
        private readonly IList<bool> switchList;
        private readonly ISmartReadWriteLock switchLock;
        private readonly IrreversibleOperation complete = new IrreversibleOperation();

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassAspectSwitch"/> class.
        /// </summary>
        /// <param name="switchList">List of all registered switches</param>
        /// <param name="lck">Lock of registered switch list</param>
        public ClassAspectSwitch(IList<bool> switchList, ISmartReadWriteLock lck)
        {
            switchLock = lck ?? throw new ArgumentNullException("lck");
            this.switchList = switchList ?? throw new ArgumentNullException("switchList");
        }

        /// <inheritdoc/>
        public bool IsAspectApplied(string aspect)
        {
            if (string.IsNullOrWhiteSpace(aspect))
            {
                throw new ArgumentNullException("aspect");
            }

            complete.Assert(true);
            return aspectSwitchDictionary.ContainsKey(aspect);
        }

        /// <inheritdoc/>
        public int Switch(SwitchOperation operation)
        {
            complete.Assert(true);
            var count = 0;
            var enumerator = aspectSwitchDictionary.GetEnumerator();
            while (enumerator.MoveNext())
            {
                count += Switch(enumerator.Current.Value, operation);
            }

            return count;
        }

        /// <inheritdoc/>
        public int SwitchAspect(string aspect, SwitchOperation operation)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
            if (string.IsNullOrEmpty(aspect))
            {
                throw new ArgumentNullException("aspect");
            }

            if (!aspectSwitchDictionary.ContainsKey(aspect))
            {
                throw new ArgumentException($"Aspect {aspect} isn't applied to any property or method", "aspect");
            }
#endif
            complete.Assert(true);
            return Switch(aspectSwitchDictionary[aspect], operation);
        }

        /// <inheritdoc/>
        public int SwitchMethod(string methodSignature, SwitchOperation operation)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
            if (string.IsNullOrEmpty(methodSignature))
            {
                throw new ArgumentNullException("methodSignature");
            }

            if (!methodSwitchDictionary.ContainsKey(methodSignature))
            {
                throw new ArgumentException($"Method {methodSignature} isn't applied to any property or method", "methodSignature");
            }
#endif
            complete.Assert(true);
            return Switch(methodSwitchDictionary[methodSignature].Values, operation);
        }

        /// <inheritdoc/>
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

            if (!methodSwitchDictionary.ContainsKey(methodSignature))
            {
                throw new ArgumentException($"Method {methodSignature} isn't applied to any property or method", "methodSignature");
            }
#endif
            complete.Assert(true);
            var aspectDictionary = methodSwitchDictionary[methodSignature];

            // apply this exception to avoid user switching off wrong aspect name
            if (!aspectDictionary.ContainsKey(aspect))
            {
                throw new ArgumentException($"Aspect {aspect} is not applied to method {methodSignature}", "aspect");
            }

            return Switch(aspectDictionary[aspect], operation);
        }

        /// <inheritdoc/>
        public int SwitchProperty(string propertyName, SwitchOperation operation)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }

            if (!propertySwitchDictionary.ContainsKey(propertyName))
            {
                throw new ArgumentException($"Property {propertyName} isn't applied to any property or method", "propertyName");
            }
#endif
            complete.Assert(true);
            return Switch(propertySwitchDictionary[propertyName].Values, operation);
        }

        /// <inheritdoc/>
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

            if (!propertySwitchDictionary.ContainsKey(propertyName))
            {
                throw new ArgumentException($"Invalid property name {propertyName}", "propertyName");
            }
#endif
            complete.Assert(true);

            // apply this exception to avoid user switching off wrong aspect name
            var aspectDictionary = propertySwitchDictionary[propertyName];
            if (!aspectDictionary.ContainsKey(aspect))
            {
                throw new ArgumentException($"Aspect {aspect} is not applied to property {propertyName}", "aspect");
            }

            using (switchLock.ReadLock)
            {
                return Switch(aspectDictionary[aspect], operation);
            }
        }

        /// <inheritdoc/>
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

            if (!string.IsNullOrWhiteSpace(methodSignature) && methodSwitchDictionary.ContainsKey(methodSignature) && methodSwitchDictionary[methodSignature].ContainsKey(aspect))
            {
                throw new ArgumentException($"Aspect {aspect} is added for property method {propertyName} already", "aspect");
            }

            if (aspectSwitchDictionary.ContainsKey(aspect) && aspectSwitchDictionary[aspect].Contains(id))
            {
                throw new ArgumentException($"Id {id} is added to aspect {aspect} already", "id");
            }
#endif

            // This method is not supposed to be called with multithread style, so no locking applied
            complete.Assert(false);

            if (!string.IsNullOrWhiteSpace(propertyName))
            {
                if (propertySwitchDictionary.ContainsKey(propertyName))
                {
                    var aspectDictionary = propertySwitchDictionary[propertyName];
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
                    propertySwitchDictionary.Add(propertyName, new Dictionary<string, PropertySwitches> { { aspect, propertySwitches } });
                }
            }

            if (!string.IsNullOrWhiteSpace(methodSignature))
            {
                if (methodSwitchDictionary.ContainsKey(methodSignature))
                {
                    methodSwitchDictionary[methodSignature].Add(aspect, id);
                }
                else
                {
                    methodSwitchDictionary.Add(methodSignature, new Dictionary<string, int> { { aspect, id } });
                }
            }

            if (aspectSwitchDictionary.ContainsKey(aspect))
            {
                aspectSwitchDictionary[aspect].Add(id);
            }
            else
            {
                aspectSwitchDictionary.Add(aspect, new List<int> { id });
            }
        }

        /// <inheritdoc/>
        public IClassAspectSwitch Build(string clazz, IClassAspectSwitchOperation classOperations, Dictionary<string, SwitchOperationStatus> aspectOperations)
        {
            // This method is not supposed to be called with multithread style, so no locking applied
            using (switchLock.ReadLock)
            {
                foreach (var methodSwitch in methodSwitchDictionary)
                {
                    var methodSignature = methodSwitch.Key;
                    var aspectSwitch = methodSwitch.Value;
                    foreach (var value in aspectSwitch)
                    {
                        var id = value.Value;
                        switchList[id] = GetSwitchValue(switchList[id], clazz, methodSignature, value.Key, classOperations, aspectOperations);
                    }
                }
            }

            complete.Apply();
            return this;
        }

        /// <inheritdoc/>
        public bool? Lookup(string methodSignature, string aspect)
        {
            if (string.IsNullOrWhiteSpace(methodSignature))
            {
                throw new ArgumentNullException("methodSignature");
            }

            if (string.IsNullOrWhiteSpace(aspect))
            {
                throw new ArgumentNullException("aspect");
            }

            if (methodSwitchDictionary.ContainsKey(methodSignature))
            {
                var aspectDictionary = methodSwitchDictionary[methodSignature];
                if (aspectDictionary.ContainsKey(aspect))
                {
                    using (switchLock.ReadLock)
                    {
                        return switchList[aspectDictionary[aspect]];
                    }
                }
            }

            return null;
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

            throw new InvalidOperationException($"Unexpected switch operation: {operation}");
        }

        private static void SetPropertySwitches(PropertySwitches switches, string method, int id)
        {
            const string getterPrefix = "get_";
            const string setterPrefix = "set_";
            if (method.StartsWith(getterPrefix))
            {
                if (switches.Getter >= 0)
                {
                    throw new ArgumentException($"Getter method {method} has been set");
                }

                switches.Getter = id;
            }
            else if (method.StartsWith(setterPrefix))
            {
                if (switches.Setter >= 0)
                {
                    throw new ArgumentException($"Setter method {method} has been set");
                }

                switches.Setter = id;
            }
            else
            {
                throw new ArgumentException($"Invalid getter or setter method name: {method}");
            }
        }

        private bool GetSwitchValue(bool value, string clazz, string methodSignature, string aspect, IClassAspectSwitchOperation classOperations, Dictionary<string, SwitchOperationStatus> aspectOperations)
        {
            if (classOperations != null)
            {
                return classOperations.GetSwitchValue(value, methodSignature, aspect);
            }

            return aspectOperations.ContainsKey(aspect) ? aspectOperations[aspect].Switch(value) : value;
        }

        private int Switch(int id, SwitchOperation operation)
        {
            using (switchLock.ReadLock)
            {
                switchList[id] = Switch(switchList[id], operation);
            }

            return 1;
        }

        private int Switch(PropertySwitches propertySwitches, SwitchOperation operation)
        {
            var result = 0;
            var getter = propertySwitches.Getter;
            if (getter >= 0)
            {
                switchList[getter] = Switch(switchList[getter], operation);
                result++;
            }

            var setter = propertySwitches.Setter;
            if (setter >= 0)
            {
                switchList[setter] = Switch(switchList[setter], operation);
                result++;
            }

            return result;
        }

        private int Switch(ICollection<int> ids, SwitchOperation operation)
        {
            using (switchLock.ReadLock)
            {
                foreach (var id in ids)
                {
                    switchList[id] = Switch(switchList[id], operation);
                }
            }

            return ids.Count;
        }

        private int Switch(IEnumerable<PropertySwitches> switches, SwitchOperation operation)
        {
            var result = 0;
            using (switchLock.ReadLock)
            {
                foreach (var propertySwitches in switches)
                {
                    result += Switch(propertySwitches, operation);
                }
            }

            return result;
        }

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
