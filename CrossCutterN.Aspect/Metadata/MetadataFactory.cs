// <copyright file="MetadataFactory.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Metadata
{
    /// <summary>
    /// Concern factory.
    /// </summary>
    public static class MetadataFactory
    {
        /// <summary>
        /// Initializes a new instance of of <see cref="IMethodBuilder"/>.
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
        /// <returns>The <see cref="IMethodBuilder"/> initialized.</returns>
        public static IMethodBuilder InitializeMethod(
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
            return new Method(
                assemblyFullName,
                nameSpace,
                classFullName,
                className,
                methodFullName,
                methodName,
                returnType,
                isInstance,
                isConstructor,
                accessibility);
        }

        /// <summary>
        /// Initializes a new instance of of <see cref="ICanAddCustomAttribute"/>.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <param name="typeName">Type name of the parameter.</param>
        /// <param name="sequence">Sequence of the parameter in the method.</param>
        /// <returns>The <see cref="ICanAddCustomAttribute"/> initialized.</returns>
        public static ICanAddCustomAttribute InitializeParameter(string name, string typeName, int sequence) => new Parameter(name, typeName, sequence);

        /// <summary>
        /// Initializes a new instance of of <see cref="ICanAddAttributeProperty"/>.
        /// </summary>
        /// <param name="typeName">Type name of the custom attribute.</param>
        /// <param name="sequence">Sequence of the custom attribute in the parameter.</param>
        /// <returns>The <see cref="ICanAddAttributeProperty"/> initialized.</returns>
        public static ICanAddAttributeProperty InitializeCustomAttribute(string typeName, int sequence) => new CustomAttribute(typeName, sequence);

        /// <summary>
        /// Initializes a new instance of of <see cref="IAttributeProperty"/>.
        /// </summary>
        /// <param name="name">Name of the attribute property.</param>
        /// <param name="typeName">Type name of the attribute property.</param>
        /// <param name="sequence">Sequence of the attribute property in the custom attribute.</param>
        /// <param name="value">Value of the attribute property.</param>
        /// <returns>The <see cref="IAttributeProperty"/> initialized.</returns>
        public static IAttributeProperty InitializeAttributeProperty(string name, string typeName, int sequence, object value) => new AttributeProperty(name, typeName, sequence, value);

        /// <summary>
        /// Initializes a new instance of of <see cref="IPropertyBuilder"/>.
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
        /// <returns>The <see cref="IPropertyBuilder"/> initialized.</returns>
        public static IPropertyBuilder InitializeProperty(
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
            return new Property(
                assemblyFullName,
                nameSpace,
                classFullName,
                className,
                propertyFullName,
                propertyName,
                type,
                isInstance,
                getterAccessibility,
                setterAccessibility);
        }
    }
}