/**
 * Description: data factory for exposed interfaces
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Parameter
{
    using System;

    public static class ParameterFactory
    {
        public static ICanAddParameter InitializeExecution(string assemblyFullName, string nameSpace, 
            string classFullName, string className, string fullName, string name, string returnType)
        {
            return new MethodExecution(assemblyFullName, nameSpace, classFullName, className, fullName, name, returnType);
        }

        public static IExecutionContext InitializeExecutionContext()
        {
            return new ExecutionContext();
        }

        public static ICanAddCustomAttribute InitializeParameter(string name, string typeName, int sequence, object value)
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

        public static ICanAddAttributeProperty InitializeCustomAttribute(string typeName, int sequence)
        {
            return new CustomAttribute(typeName, sequence);
        }

        public static IAttributeProperty InitializeAttributeProperty(string name, string typeName, int sequence, object value)
        {
            return new AttributeProperty(name, typeName, sequence, value);
        }

        public static IWriteOnlyReturn InitializeReturn(string typeName)
        {
            return new Return(typeName);
        }
    }
}