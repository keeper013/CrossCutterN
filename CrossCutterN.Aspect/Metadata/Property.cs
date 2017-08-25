// <copyright file="Property.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Metadata
{
    using System;
    using System.Collections.Generic;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Prperty metadata implementation.
    /// </summary>
    internal sealed class Property : IProperty, IPropertyBuilder
    {
        private readonly IrreversibleOperation readOnly = new IrreversibleOperation();
        private readonly List<ICustomAttribute> customAttributes = new List<ICustomAttribute>();
        private readonly List<ICustomAttribute> getterCustomAttributes = new List<ICustomAttribute>();
        private readonly List<ICustomAttribute> setterCustomAttributes = new List<ICustomAttribute>();
        private IReadOnlyCollection<ICustomAttribute> classCustomAttributes;

        /// <summary>
        /// Initializes a new instance of the <see cref="Property"/> class.
        /// </summary>
        /// <param name="assemblyFullName">Full name of assembly this property is defined in.</param>
        /// <param name="nameSpace">Namespace this property is defined in.</param>
        /// <param name="classFullName">Full name of the class this property is defined in.</param>
        /// <param name="className">Name of the class this property is defined in.</param>
        /// <param name="propertyFullName">Full name of the property.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="type">Type of the property.</param>
        /// <param name="isInstance">Whether the property is instance.</param>
        /// <param name="getterAccessibility">Getter function accessibility.</param>
        /// <param name="setterAccessibility">Setter function accessibility.</param>
        internal Property(
            string assemblyFullName,
            string nameSpace,
            string classFullName,
            string className,
            string propertyFullName,
            string propertyName,
            string type,
            bool isInstance,
            Accessibility? getterAccessibility,
            Accessibility? setterAccessibility)
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

        /// <inheritdoc/>
        public string AssemblyFullName { get; private set; }

        /// <inheritdoc/>
        public string Namespace { get; private set; }

        /// <inheritdoc/>
        public string ClassFullName { get; private set; }

        /// <inheritdoc/>
        public string ClassName { get; private set; }

        /// <inheritdoc/>
        public string PropertyFullName { get; private set; }

        /// <inheritdoc/>
        public string PropertyName { get; private set; }

        /// <inheritdoc/>
        public string Type { get; private set; }

        /// <inheritdoc/>
        public bool IsInstance { get; private set; }

        /// <inheritdoc/>
        public Accessibility? GetterAccessibility { get; private set; }

        /// <inheritdoc/>
        public Accessibility? SetterAccessibility { get; private set; }

        /// <inheritdoc/>
        public IReadOnlyCollection<ICustomAttribute> ClassCustomAttributes
        {
            get
            {
                readOnly.Assert(true);
                return classCustomAttributes;
            }

            set
            {
                readOnly.Assert(false);
                classCustomAttributes = value;
            }
        }

        /// <inheritdoc/>
        public IReadOnlyCollection<ICustomAttribute> CustomAttributes
        {
            get
            {
                readOnly.Assert(true);
                return customAttributes.AsReadOnly();
            }
        }

        /// <inheritdoc/>
        public IReadOnlyCollection<ICustomAttribute> GetterCustomAttributes
        {
            get
            {
                readOnly.Assert(true);
                return getterCustomAttributes.AsReadOnly();
            }
        }

        /// <inheritdoc/>
        public IReadOnlyCollection<ICustomAttribute> SetterCustomAttributes
        {
            get
            {
                readOnly.Assert(true);
                return setterCustomAttributes.AsReadOnly();
            }
        }

        /// <inheritdoc/>
        public void AddCustomAttribute(ICustomAttribute attribute)
        {
            if (attribute == null)
            {
                throw new ArgumentNullException("attribute");
            }

            readOnly.Assert(false);
            customAttributes.Add(attribute);
        }

        /// <inheritdoc/>
        public void AddGetterCustomAttribute(ICustomAttribute attribute)
        {
            if (attribute == null)
            {
                throw new ArgumentNullException("attribute");
            }

            readOnly.Assert(false);
            getterCustomAttributes.Add(attribute);
        }

        /// <inheritdoc/>
        public void AddSetterCustomAttribute(ICustomAttribute attribute)
        {
            if (attribute == null)
            {
                throw new ArgumentNullException("attribute");
            }

            readOnly.Assert(false);
            setterCustomAttributes.Add(attribute);
        }

        /// <inheritdoc/>
        public IProperty Build()
        {
            readOnly.Apply();
            return this;
        }
    }
}
