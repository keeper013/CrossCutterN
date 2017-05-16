/**
 * Description: data factory for exposed interfaces
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Concern
{
    public static class ConcernFactory
    {
        public static IWriteOnlyMethod InitializeMethod(string assemblyFullName, string nameSpace, 
            string classFullName, string className, string methodFullName, string methodName, 
            string returnType, bool isInstance, bool isConstructor, Accessibility accessibility)
        {
            return new Method(assemblyFullName, nameSpace, classFullName, className, methodFullName, 
                methodName, returnType, isInstance, isConstructor, accessibility);
        }

        public static ICanAddCustomAttribute InitializeParameter(string name, string typeName, int sequence)
        {
            return new Parameter(name, typeName, sequence);
        }

        public static ICanAddAttributeProperty InitializeCustomAttribute(string typeName, int sequence)
        {
            return new CustomAttribute(typeName, sequence);
        }

        public static IAttributeProperty InitializeAttributeProperty(string name, string typeName, int sequence, object value)
        {
            return new AttributeProperty(name, typeName, sequence, value);
        }

        public static IWriteOnlyProperty InitializeProperty(string assemblyFullName, string nameSpace,
            string classFullName, string className, string propertyFullName, string propertyName,
            string type, bool isInstance, Accessibility? getterAccessibility, Accessibility? setterAccessibility)
        {
            return new Property(assemblyFullName, nameSpace, classFullName, className, propertyFullName,
                propertyName, type, isInstance, getterAccessibility, setterAccessibility);
        }
    }
}