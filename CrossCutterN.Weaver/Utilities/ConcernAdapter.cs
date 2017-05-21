/**
 * Description: converter class between internal data structure to exposed interfaces
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Utilities
{
    using System;
    using System.Collections.Generic;
    using Aspect.Concern;

    internal static class ConcernAdapter
    {
        public static IMethod Convert(this Mono.Cecil.MethodDefinition method, IReadOnlyCollection<ICustomAttribute> classCustomAttributes)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }
            var accessibility = method.GetAccessibility();
            var writableMethod = ConcernFactory.InitializeMethod(
                method.Module.Assembly.FullName, method.DeclaringType.Namespace, method.DeclaringType.FullName, 
                method.DeclaringType.Name, method.FullName, method.Name, method.ReturnType.FullName, !method.IsStatic, 
                method.IsConstructor, accessibility);
            if (method.HasCustomAttributes)
            {
                for(var i = 0; i < method.CustomAttributes.Count; i++)
                {
                    writableMethod.AddCustomAttribute(Convert(method.CustomAttributes[i], i));
                }
            }
            if (method.HasParameters)
            {
                for(var i = 0; i < method.Parameters.Count; i++)
                {
                    writableMethod.AddParameter(Convert(method.Parameters[i], i));
                }
            }
            writableMethod.ClassCustomAttributes = classCustomAttributes;
            return writableMethod.Convert();
        }

        public static IProperty Convert(this Mono.Cecil.PropertyDefinition property, IReadOnlyCollection<ICustomAttribute> classCustomAttributes)
        {
            if(property == null)
            {
                throw new ArgumentNullException("property");
            }
            var getterAccessibility = property.GetMethod == null ? (Accessibility?)null : property.GetMethod.GetAccessibility();
            var setterAccessibility = property.SetMethod == null ? (Accessibility?)null : property.SetMethod.GetAccessibility();
            var writableProperty = ConcernFactory.InitializeProperty(property.Module.Assembly.FullName, property.DeclaringType.Namespace, property.DeclaringType.FullName,
                property.DeclaringType.Name, property.FullName, property.Name, property.PropertyType.FullName, property.HasThis,
                getterAccessibility, setterAccessibility);
            if (property.HasCustomAttributes)
            {
                for (var i = 0; i < property.CustomAttributes.Count; i++)
                {
                    writableProperty.AddCustomAttribute(Convert(property.CustomAttributes[i], i));
                }
            }
            if(property.GetMethod != null && property.GetMethod.HasCustomAttributes)
            {
                for(var i = 0; i < property.GetMethod.CustomAttributes.Count; i++)
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
            return writableProperty.Convert();
        }

        private static IParameter Convert(this Mono.Cecil.ParameterDefinition parameter, int sequence)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException("parameter");
            }
            var writableParameter = ConcernFactory.InitializeParameter(
                parameter.Name, parameter.ParameterType.FullName, sequence);
            if (parameter.HasCustomAttributes)
            {
                for(var i = 0; i < parameter.CustomAttributes.Count; i++)
                {
                    writableParameter.AddCustomAttribute(Convert(parameter.CustomAttributes[i], i));
                }
            }
            return writableParameter.Convert();
        }

        public static ICustomAttribute Convert(this Mono.Cecil.CustomAttribute attribute, int sequence)
        {
            if (attribute == null)
            {
                throw new ArgumentNullException("attribute");
            }
            var customAttribute = ConcernFactory.InitializeCustomAttribute(attribute.AttributeType.FullName, sequence);
            if (attribute.HasProperties)
            {
                for(var i = 0; i < attribute.Properties.Count; i++)
                {
                    customAttribute.AddAttributeProperty(Convert(attribute.Properties[i], i));
                }
            }
            return customAttribute.Convert();
        }

        private static IAttributeProperty Convert(Mono.Cecil.CustomAttributeNamedArgument property, int sequence)
        {
            return ConcernFactory.InitializeAttributeProperty(
                property.Name, property.Argument.Type.FullName, sequence, property.Argument.Value);
        }

        private static Accessibility GetAccessibility(this Mono.Cecil.MethodDefinition method)
        {
            return method.IsPublic ? Accessibility.Public: 
                (method.IsAssembly ? Accessibility.Internal : (method.IsFamily ? Accessibility.Protected : Accessibility.Private));
        }
    }
}
