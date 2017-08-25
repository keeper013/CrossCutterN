// <copyright file="Execution.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Metadata
{
    using System;
    using System.Collections.Generic;
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Method execution metadata implementation.
    /// </summary>
    internal sealed class Execution : IExecution, IExecutionBuilder
    {
        private readonly StringIndexedIntSortedCollection<IParameter> parameters =
            new StringIndexedIntSortedCollection<IParameter>();

        private readonly IrreversibleOperation readOnly = new IrreversibleOperation();

        /// <summary>
        /// Initializes a new instance of the <see cref="Execution"/> class.
        /// </summary>
        /// <param name="assemblyFullName">Full name of assembly this method is defined in.</param>
        /// <param name="nameSpace">Namespace this method is defined in.</param>
        /// <param name="classFullName">Full name of the class this method is defined in.</param>
        /// <param name="className">Name of the class this method is defined in.</param>
        /// <param name="fullName">Full name of the method.</param>
        /// <param name="name">Name of the method.</param>
        /// <param name="returnType">Return type of the method.</param>
        internal Execution(
            string assemblyFullName,
            string nameSpace,
            string classFullName,
            string className,
            string fullName,
            string name,
            string returnType)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
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

            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentNullException("fullName");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }

            if (string.IsNullOrWhiteSpace(returnType))
            {
                throw new ArgumentNullException("returnType");
            }
#endif
            AssemblyFullName = assemblyFullName;
            Namespace = nameSpace;
            ClassFullName = classFullName;
            ClassName = className;
            FullName = fullName;
            Name = name;
            ReturnType = returnType;
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
        public string FullName { get; private set; }

        /// <inheritdoc/>
        public string Name { get; private set; }

        /// <inheritdoc/>
        public string ReturnType { get; private set; }

        /// <inheritdoc/>
        public IReadOnlyCollection<IParameter> Parameters => parameters.All;

        /// <inheritdoc/>
        public void AddParameter(IParameter parameter)
        {
            readOnly.Assert(false);
            parameters.Add(parameter);
        }

        /// <inheritdoc/>
        public IParameter GetParameter(string name) => parameters.Get(name);

        /// <inheritdoc/>
        public bool HasParameter(string name) => parameters.ContainsId(name);

        /// <inheritdoc/>
        public IExecution Build()
        {
            readOnly.Apply();
            return this;
        }
    }
}
