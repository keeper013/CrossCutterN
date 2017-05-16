/**
 * Description: property implementation
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Concern
{
    using System;
    using System.Collections.Generic;
    using Advice.Common;

    internal class Property : IProperty, IWriteOnlyProperty
    {
        private readonly List<ICustomAttribute> _customAttributes = new List<ICustomAttribute>();
        private readonly List<ICustomAttribute> _getterCustomAttributes = new List<ICustomAttribute>();
        private readonly List<ICustomAttribute> _setterCustomAttributes = new List<ICustomAttribute>();
        private IReadOnlyCollection<ICustomAttribute> _classCustomAttributes;
        private readonly IrreversibleOperation _readOnly = new IrreversibleOperation();

        public string AssemblyFullName { get; private set; }
        public string Namespace { get; private set; }
        public string ClassFullName { get; private set; }
        public string ClassName { get; private set; }
        public string PropertyFullName { get; private set; }
        public string PropertyName { get; private set; }
        public string Type { get; private set; }
        public bool IsInstance { get; private set; }
        public Accessibility? GetterAccessibility { get; private set; }
        public Accessibility? SetterAccessibility { get; private set; }

        public IReadOnlyCollection<ICustomAttribute> ClassCustomAttributes
        {
            get
            {
                _readOnly.Assert(true);
                return _classCustomAttributes;
            }
            set
            {
                _readOnly.Assert(false);
                _classCustomAttributes = value;
            }
        }

        public IReadOnlyCollection<ICustomAttribute> CustomAttributes
        {
            get
            {
                _readOnly.Assert(true);
                return _customAttributes.AsReadOnly();
            }
        }

        public IReadOnlyCollection<ICustomAttribute> GetterCustomAttributes
        {
            get
            {
                _readOnly.Assert(true);
                return _getterCustomAttributes.AsReadOnly();
            }
        }

        public IReadOnlyCollection<ICustomAttribute> SetterCustomAttributes
        {
            get
            {
                _readOnly.Assert(true);
                return _setterCustomAttributes.AsReadOnly();
            }
        }

        internal Property(string assemblyFullName, string nameSpace, string classFullName,
            string className, string propertyFullName, string propertyName, string type,
            bool isInstance, Accessibility? getterAccessibility, Accessibility? setterAccessibility)
        {
            if (string.IsNullOrWhiteSpace(assemblyFullName))
            {
                throw new ArgumentNullException("assemblyFullName");
            }
            if (string.IsNullOrWhiteSpace(nameSpace))
            {
                throw new ArgumentNullException("nameSpace");
            }
            if (string.IsNullOrWhiteSpace("classFullName"))
            {
                throw new ArgumentNullException("classFullName");
            }
            if (string.IsNullOrWhiteSpace(className))
            {
                throw new ArgumentNullException("className");
            }
            if (string.IsNullOrWhiteSpace(propertyFullName))
            {
                throw new ArgumentNullException("propertyFullName");
            }
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentNullException("type");
            }
            AssemblyFullName = assemblyFullName;
            Namespace = nameSpace;
            ClassFullName = classFullName;
            ClassName = className;
            PropertyFullName = propertyFullName;
            PropertyName = propertyName;
            Type = type;
            IsInstance = isInstance;
            GetterAccessibility = getterAccessibility;
            SetterAccessibility = setterAccessibility;
        }

        public void AddCustomAttribute(ICustomAttribute attribute)
        {
            if (attribute == null)
            {
                throw new ArgumentNullException("attribute");
            }
            _readOnly.Assert(false);
            _customAttributes.Add(attribute);
        }

        public void AddGetterCustomAttribute(ICustomAttribute attribute)
        {
            if (attribute == null)
            {
                throw new ArgumentNullException("attribute");
            }
            _readOnly.Assert(false);
            _getterCustomAttributes.Add(attribute);
        }

        public void AddSetterCustomAttribute(ICustomAttribute attribute)
        {
            if (attribute == null)
            {
                throw new ArgumentNullException("attribute");
            }
            _readOnly.Assert(false);
            _setterCustomAttributes.Add(attribute);
        }

        public IProperty ToReadOnly()
        {
            _readOnly.Apply();
            return this;
        }
    }
}
