// <copyright file="StatisticsFactory.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Statistics
{
    using CrossCutterN.Aspect.Aspect;

    /// <summary>
    /// Statistics factory.
    /// </summary>
    internal static class StatisticsFactory
    {
        /// <summary>
        /// Initializes a new instance of of <see cref="IWeavingRecord"/>.
        /// </summary>
        /// <param name="joinPoint">Join point</param>
        /// <param name="aspectName">Name of the aspect, which is also the key of aspect which is involved in the weaving process.</param>
        /// <param name="methodFullName">Full name of the method weaved.</param>
        /// <param name="methodSignature">Signature of the method weaved.</param>
        /// <param name="sequence">Sequence of the join point.</param>
        /// <returns>The <see cref="IWeavingRecord"/> initialized.</returns>
        public static IWeavingRecord InitializeWeavingRecord(
            JoinPoint joinPoint,
            string aspectName,
            string methodFullName,
            string methodSignature,
            int sequence)
        {
            return new WeavingRecord(joinPoint, aspectName, methodFullName, methodSignature, sequence);
        }

        /// <summary>
        /// Initializes a new instance of of <see cref="ICanAddMethodWeavingRecord"/>.
        /// </summary>
        /// <param name="name">Name of the method.</param>
        /// <param name="signature">Signature of the method.</param>
        /// <returns>The <see cref="ICanAddMethodWeavingRecord"/> initialized.</returns>
        public static ICanAddMethodWeavingRecord<IMethodWeavingStatistics> InitializeMethodWeavingRecord(string name, string signature) => new MethodWeavingStatistics(name, signature);

        /// <summary>
        /// Initializes a new instance of of <see cref="ICanAddPropertyWeavingRecord"/>.
        /// </summary>
        /// <param name="name">Name of the property.</param>
        /// <param name="fullName">Full name of the property.</param>
        /// <returns>The <see cref="ICanAddPropertyWeavingRecord"/> initialized.</returns>
        public static ICanAddPropertyWeavingRecord InitializePropertyWeavingRecord(string name, string fullName) => new PropertyWeavingStatistics(name, fullName);

        /// <summary>
        /// Initializes a new instance of of <see cref="IClassWeavingStatisticsBuilder"/>.
        /// </summary>
        /// <param name="name">Name of the class.</param>
        /// <param name="fullName">Full name of the class.</param>
        /// <param name="nameSpace">namespace of the class.</param>
        /// <returns>The <see cref="IClassWeavingStatisticsBuilder"/> initialized.</returns>
        public static IClassWeavingStatisticsBuilder InitializeClassWeavingRecord(string name, string fullName, string nameSpace) => new ClassWeavingStatistics(name, fullName, nameSpace);

        /// <summary>
        /// Initializes a new instance of of <see cref="IModuleWeavingStatisticsBuilder"/>.
        /// </summary>
        /// <param name="name">Name of the module.</param>
        /// <returns>The <see cref="IModuleWeavingStatisticsBuilder"/> initialized.</returns>
        public static IModuleWeavingStatisticsBuilder InitializeModuleWeavingRecord(string name) => new ModuleWeavingStatistics(name);

        /// <summary>
        /// Initializes a new instance of of <see cref="IAssemblyWeavingStatisticsBuilder"/>
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <returns>The <see cref="IAssemblyWeavingStatisticsBuilder"/> initialized.</returns>
        public static IAssemblyWeavingStatisticsBuilder InitializeAssemblyWeavingRecord(string assemblyName) => new AssemblyWeavingStatistics(assemblyName);

        /// <summary>
        /// Initializes a new instance of of <see cref="ISwitchWeavingRecord"/>.
        /// </summary>
        /// <param name="clazz">Class that the aspect switch is in.</param>
        /// <param name="property">Property that the aspect switch is injected in.</param>
        /// <param name="methodSignature">Signature of method that the aspect switch is injected in.</param>
        /// <param name="aspect">Name of the aspect of the switch.</param>
        /// <param name="variable">Static variable name for the aspect switch.</param>
        /// <param name="value">Value of the aspect switch.</param>
        /// <returns>The <see cref="ISwitchWeavingRecord"/> initialized.</returns>
        public static ISwitchWeavingRecord InitializeSwitchWeavingRecord(
            string clazz, string property, string methodSignature, string aspect, string variable, bool value)
        {
            return new SwitchWeavingRecord(clazz, property, methodSignature, aspect, variable, value);
        }
    }
}
