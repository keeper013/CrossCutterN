// <copyright file="Method.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Metadata
{
    using System;
    using System.Collections.Generic;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Method metadata implementation.
    /// </summary>
    internal sealed class Method : IMethod, IMethodBuilder
    {
        private readonly StringIndexedIntSortedCollection<IParameter> parameters =
            new StringIndexedIntSortedCollection<IParameter>();

        private readonly List<ICustomAttribute> customAttributes = new List<ICustomAttribute>();
        private readonly IrreversibleOperation readOnly = new IrreversibleOperation();
        private IReadOnlyCollection<ICustomAttribute> classCustomAttributes;

        /// <summary>
        /// Initializes a new instance of the <see cref="Method"/> class.
        /// </summary>
        /// <param name="assemblyFullName">Full name of assembly this method is defined in.</param>
        /// <param name="nameSpace">Namespace this method is defined in.</param>
        /// <param name="classFullName">Full name of the class this method is defined in.</param>
        /// <param name="className">Name of the class this method is defined in.</param>
        /// <param name="methodFullName">Full name of the method.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="returnType">Return type of the method.</param>
        /// <param name="isInstance">Whether the method is instance.</param>
        /// <param name="isConstructor">Whether the method is constructor.</param>
        /// <param name="accessibility">Accessibility of the method.</param>
        internal Method(
            string assemblyFullName,
            string nameSpace,
            string classFullName,
            string className,
            string methodFullName,
            string methodName,
            string returnType,
            bool isInstance,
            bool isConstructor,
            Accessibility accessibility)
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

            if (string.IsNullOrWhiteSpace(methodFullName))
            {
                throw new ArgumentNullException("methodFullName");
            }

            if (string.IsNullOrWhiteSpace(methodName))
            {
                throw new ArgumentNullException("methodName");
            }

            if (string.IsNullOrWhiteSpace(returnType))
            {
                throw new ArgumentNullException("returnType");
            }

            AssemblyFullName = assemblyFullName;
            Namespace = nameSpace;
            ClassFullName = classFullName;
            ClassName = className;
            MethodFullName = methodFullName;
            MethodName = methodName;
            ReturnType = returnType;
            IsConstructor = isConstructor;
            IsInstance = isInstance;
            Accessibility = accessibility;
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
        public string MethodFullName { get; private set; }

        /// <inheritdoc/>
        public string MethodName { get; private set; }

        /// <inheritdoc/>
        public string ReturnType { get; private set; }

        /// <inheritdoc/>
        public bool IsInstance { get; private set; }

        /// <inheritdoc/>
        public bool IsConstructor { get; private set; }

        /// <inheritdoc/>
        public Accessibility Accessibility { get; private set; }

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
        public IReadOnlyCollection<IParameter> Parameters
        {
            get
            {
                readOnly.Assert(true);
                return parameters.All;
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
        public void AddParameter(IParameter parameter)
        {
            readOnly.Assert(false);
            parameters.Add(parameter);
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
        public IParameter GetParameter(string name)
        {
            readOnly.Assert(true);
            return parameters.Get(name);
        }

        /// <inheritdoc/>
        public bool HasParameter(string name)
        {
            readOnly.Assert(true);
            return parameters.ContainsId(name);
        }

        /// <inheritdoc/>
        public IMethod Build()
        {
            readOnly.Apply();
            return this;
        }
    }
}
