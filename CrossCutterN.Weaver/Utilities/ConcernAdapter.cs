// <copyright file="ConcernAdapter.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Utilities
{
    using System.Collections.Generic;
    using CrossCutterN.Aspect.Metadata;

    /// <summary>
    /// Utility that converts assembly content metadata from Mono.Cecil form to CrossCutterN form.
    /// </summary>
    internal static class ConcernAdapter
    {
        /// <summary>
        /// Convert method from Mono.Cecil.MethodDefinition to <see cref="IMethod"/>.
        /// </summary>
        /// <param name="method">The method to be converted.</param>
        /// <param name="classCustomAttributes">Custom attributes applied to the class which contains the method.</param>
        /// <returns>The <see cref="IMethod"/> converted to.</returns>
        public static IMethod Convert(this Mono.Cecil.MethodDefinition method, IReadOnlyCollection<ICustomAttribute> classCustomAttributes)
        {
            var accessibility = method.GetAccessibility();
            var writableMethod = MetadataFactory.InitializeMethod(
                method.Module.Assembly.FullName,
                method.DeclaringType.Namespace,
                method.DeclaringType.FullName,
                method.DeclaringType.Name,
                method.FullName,
                method.Name,
                method.ReturnType.FullName,
                !method.IsStatic,
                method.IsConstructor,
                accessibility);
            if (method.HasCustomAttributes)
            {
                for (var i = 0; i < method.CustomAttributes.Count; i++)
                {
                    writableMethod.AddCustomAttribute(Convert(method.CustomAttributes[i], i));
                }
            }

            if (method.HasParameters)
            {
                for (var i = 0; i < method.Parameters.Count; i++)
                {
                    writableMethod.AddParameter(Convert(method.Parameters[i], i));
                }
            }

            writableMethod.ClassCustomAttributes = classCustomAttributes;
            return writableMethod.Build();
        }

        /// <summary>
        /// Convert property from Mono.Cecil.PropertyDefinition to <see cref="IProperty"/>
        /// </summary>
        /// <param name="property">The property to be converted.</param>
        /// <param name="classCustomAttributes">Custom attributes applied to the class which contains the property.</param>
        /// <returns>The <see cref="IProperty"/> converted to.</returns>
        public static IProperty Convert(this Mono.Cecil.PropertyDefinition property, IReadOnlyCollection<ICustomAttribute> classCustomAttributes)
        {
            var getterAccessibility = property.GetMethod == null ? (Accessibility?)null : property.GetMethod.GetAccessibility();
            var setterAccessibility = property.SetMethod == null ? (Accessibility?)null : property.SetMethod.GetAccessibility();
            var writableProperty = MetadataFactory.InitializeProperty(
                property.Module.Assembly.FullName,
                property.DeclaringType.Namespace,
                property.DeclaringType.FullName,
                property.DeclaringType.Name,
                property.FullName,
                property.Name,
                property.PropertyType.FullName,
                property.HasThis,
                getterAccessibility,
                setterAccessibility);
            if (property.HasCustomAttributes)
            {
                for (var i = 0; i < property.CustomAttributes.Count; i++)
                {
                    writableProperty.AddCustomAttribute(Convert(property.CustomAttributes[i], i));
                }
            }

            if (property.GetMethod != null && property.GetMethod.HasCustomAttributes)
            {
                for (var i = 0; i < property.GetMethod.CustomAttributes.Count; i++)
                {
                    writableProperty.AddGetterCustomAttribute(Convert(property.GetMethod.CustomAttributes[i], i));
                }
            }

            if (property.SetMethod != null && property.SetMethod.HasCustomAttributes)
            {
                for (var i = 0; i < property.SetMethod.CustomAttributes.Count; i++)
                {
                    writableProperty.AddSetterCustomAttribute(Convert(property.SetMethod.CustomAttributes[i], i));
                }
            }

            writableProperty.ClassCustomAttributes = classCustomAttributes;
            return writableProperty.Build();
        }

        /// <summary>
        /// Convert Mono.Cecil.CustomAttribute to <see cref="ICustomAttribute"/>.
        /// </summary>
        /// <param name="attribute">The custom attribute to be converted.</param>
        /// <param name="sequence">Sequence of the attribute in parameter, method, property or class.</param>
        /// <returns>The <see cref="ICustomAttribute"/> converted to.</returns>
        public static ICustomAttribute Convert(this Mono.Cecil.CustomAttribute attribute, int sequence)
        {
            var customAttribute = MetadataFactory.InitializeCustomAttribute(attribute.AttributeType.FullName, sequence);
            if (attribute.HasProperties)
            {
                for (var i = 0; i < attribute.Properties.Count; i++)
                {
                    customAttribute.AddAttributeProperty(Convert(attribute.Properties[i], i));
                }
            }

            return customAttribute.Build();
        }

        private static IParameter Convert(this Mono.Cecil.ParameterDefinition parameter, int sequence)
        {
            var writableParameter = MetadataFactory.InitializeParameter(
                parameter.Name, parameter.ParameterType.FullName, sequence);
            if (parameter.HasCustomAttributes)
            {
                for (var i = 0; i < parameter.CustomAttributes.Count; i++)
                {
                    writableParameter.AddCustomAttribute(Convert(parameter.CustomAttributes[i], i));
                }
            }

            return writableParameter.Build();
        }

        private static IAttributeProperty Convert(Mono.Cecil.CustomAttributeNamedArgument property, int sequence)
        {
            return MetadataFactory.InitializeAttributeProperty(
                property.Name, property.Argument.Type.FullName, sequence, property.Argument.Value);
        }

        private static Accessibility GetAccessibility(this Mono.Cecil.MethodDefinition method)
        {
            return method.IsPublic ? Accessibility.Public :
                (method.IsAssembly ? Accessibility.Internal : (method.IsFamily ? Accessibility.Protected : Accessibility.Private));
        }
    }
}
