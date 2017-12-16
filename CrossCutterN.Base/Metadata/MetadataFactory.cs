// <copyright file="MetadataFactory.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Metadata
{
    using System;

    /// <summary>
    /// Metadata factory.
    /// </summary>
    public static class MetadataFactory
    {
        /// <summary>
        /// Initializes a new instance of of <see cref="IExecutionBuilder"/>.
        /// </summary>
        /// <param name="assemblyFullName">Full name of the assembly in which the method is defined.</param>
        /// <param name="nameSpace">Namespace in which the method is defined.</param>
        /// <param name="classFullName">Full name of the class in which the method is defined.</param>
        /// <param name="className">Name of the class in which the method is defined.</param>
        /// <param name="hashCode">Hash code of the class the injected method belongs to, if the method is not static.</param>
        /// <param name="fullName">Full name of the method</param>
        /// <param name="name">Name of the method.</param>
        /// <param name="returnType">Return type of the method.</param>
        /// <returns>The initialized <see cref="IExecutionBuilder"/>.</returns>
        public static IExecutionBuilder InitializeExecution(
            string assemblyFullName,
            string nameSpace,
            string classFullName,
            string className,
            int hashCode,
            string fullName,
            string name,
            string returnType)
        {
            return new Execution(assemblyFullName, nameSpace, classFullName, className, hashCode, fullName, name, returnType);
        }

        /// <summary>
        /// Initializes a new instance of of <see cref="IExecutionContext"/>.
        /// </summary>
        /// <returns>The initialized <see cref="IExecutionContext"/>.</returns>
        public static IExecutionContext InitializeExecutionContext() => new ExecutionContext();

        /// <summary>
        /// Initializes a new instance of of <see cref="IParameterBuilder"/>.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <param name="typeName">Type name of the parameter.</param>
        /// <param name="sequence">Sequence of the parameter.</param>
        /// <param name="value">Value of the parameter.</param>
        /// <returns>The initialized <see cref="IParameterBuilder"/>.</returns>
        public static IParameterBuilder InitializeParameter(string name, string typeName, int sequence, object value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }

            if (string.IsNullOrWhiteSpace(typeName))
            {
                throw new ArgumentNullException("typeName");
            }

            if (sequence < 0)
            {
                throw new ArgumentOutOfRangeException("sequence", "sequence must be non-negative number");
            }

            return new Parameter(name, typeName, sequence, value);
        }

        /// <summary>
        /// Initializes a new instance of of <see cref="ICustomAttributeBuilder"/>.
        /// </summary>
        /// <param name="typeName">Type name of the custom attribute.</param>
        /// <param name="sequence">Sequence of the custom attribute in the parameter.</param>
        /// <returns>The initialized <see cref="ICustomAttributeBuilder"/>.</returns>
        public static ICustomAttributeBuilder InitializeCustomAttribute(string typeName, int sequence) => new CustomAttribute(typeName, sequence);

        /// <summary>
        /// Initializes a new instance of of <see cref="IAttributeProperty"/>.
        /// </summary>
        /// <param name="name">Name of the attribute property.</param>
        /// <param name="typeName">Type name of the attribute property.</param>
        /// <param name="sequence">Sequence of the attribute property in the custom attribute.</param>
        /// <param name="value">Value fo the attribute property.</param>
        /// <returns>The initialized <see cref="IAttributeProperty"/>.</returns>
        public static IAttributeProperty InitializeAttributeProperty(string name, string typeName, int sequence, object value) => new AttributeProperty(name, typeName, sequence, value);

        /// <summary>
        /// Initializes a new instance of of <see cref="IReturnBuilder"/>.
        /// </summary>
        /// <param name="typeName">Type name of the return value.</param>
        /// <returns>The initialized <see cref="IReturnBuilder"/>.</returns>
        public static IReturnBuilder InitializeReturn(string typeName) => new Return(typeName);
    }
}