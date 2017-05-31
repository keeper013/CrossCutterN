/**
* Description: Statistics data factory
* Author: David Cui
*/

namespace CrossCutterN.Weaver.Statistics
{
    using Aspect;

    internal static class StatisticsFactory
    {
        public static IWeavingRecord InitializeWeavingRecord(JoinPoint joinPoint,
            string aspectBuilderId, string methodFullName, string methodSignature, int sequence)
        {
            return new WeavingRecord(joinPoint, aspectBuilderId, methodFullName, methodSignature, sequence);
        }

        public static ICanAddMethodWeavingRecord<IMethodWeavingStatistics> InitializeMethodWeavingRecord(string name, string signature)
        {
            return new MethodWeavingStatistics(name, signature);
        }

        public static ICanAddPropertyWeavingRecord InitializePropertyWeavingRecord(string name, string fullName)
        {
            return new PropertyWeavingStatistics(name, fullName);
        }

        public static IWriteOnlyClassWeavingStatistics InitializeClassWeavingRecord(string name, string fullName, string nameSpace)
        {
            return new ClassWeavingStatistics(name, fullName, nameSpace);
        }

        public static ICanAddClassWeavingStatistics InitializeModuleWeavingRecord(string name)
        {
            return new ModuleWeavingStatistics(name);
        }

        public static IWriteOnlyAssemblyWeavingStatistics InitializeAssemblyWeavingRecord(string assemblyName)
        {
            return new AssemblyWeavingStatistics(assemblyName);
        }

        public static ISwitchWeavingRecord InitializeSwitchWeavingRecord(
            string clazz, string property, string method, string aspect, string variable, bool value)
        {
            return new SwitchWeavingRecord(clazz, property, method, aspect, variable, value);
        }
    }
}
