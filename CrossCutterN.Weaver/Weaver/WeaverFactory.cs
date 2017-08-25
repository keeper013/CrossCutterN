// <copyright file="WeaverFactory.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Weaver
{
    using System.Reflection;
    using CrossCutterN.Aspect.Aspect;
    using Mono.Cecil;

    /// <summary>
    /// Weaver factory.
    /// </summary>
    public static class WeaverFactory
    {
        /// <summary>
        /// Initializes a new instance of of <see cref="ICanAddAspect{T}"/> which can be built to <see cref="IWeaver"/>.
        /// </summary>
        /// <returns>The <see cref="ICanAddAspect{T}"/> which can be built to <see cref="IWeaver"/> initialized.</returns>
        public static ICanAddAspect<IWeaver> InitializeWeaver() => new Weaver();

        /// <summary>
        /// Initializes a new instance of of <see cref="IAdviceInfo"/>.
        /// </summary>
        /// <param name="method">The advice method.</param>
        /// <param name="aspectName">Name of the aspect, also used as key of aspect.</param>
        /// <param name="parameterFlag">Flag of advice method parameters.</param>
        /// <param name="isSwitchedOn">Default switch status.</param>
        /// <returns>The <see cref="IAdviceInfo"/> initialized.</returns>
        internal static IAdviceInfo InitializeAdviceInfo(MethodInfo method, string aspectName, AdviceParameterFlag parameterFlag, bool? isSwitchedOn) => new AdviceInfo(method, aspectName, parameterFlag, isSwitchedOn);

        /// <summary>
        /// Initializes a new instance of of <see cref="ICanAddJoinPoint"/>.
        /// </summary>
        /// <returns>The <see cref="ICanAddJoinPoint"/> initialized.</returns>
        internal static ICanAddJoinPoint InitializeWeavingPlan() => new WeavingPlan();

        /// <summary>
        /// Initializes a new instance of of <see cref="ICanAddAspect{T}"/> which can be built to <see cref="IWeavingPlanner"/>.
        /// </summary>
        /// <returns>The <see cref="ICanAddAspect{T}"/> which can be built to <see cref="IWeavingPlanner"/> initialized.</returns>
        internal static ICanAddAspect<IWeavingPlanner> InitializeWeavingPlanner() => new WeavingPlanner();

        /// <summary>
        /// Initializes a new instance of of <see cref="IPropertyWeavingPlan"/>.
        /// </summary>
        /// <param name="getterPlan">Weaving plan for getter method.</param>
        /// <param name="setterPlan">Weaving plan for setter method.</param>
        /// <returns>The <see cref="IPropertyWeavingPlan"/> initialized.</returns>
        internal static IPropertyWeavingPlan InitializePropertyWeavingPlan(IWeavingPlan getterPlan, IWeavingPlan setterPlan) => new PropertyWeavingPlan(getterPlan, setterPlan);

        /// <summary>
        /// Initilaizes an instance of <see cref="IWeavingContext"/>.
        /// </summary>
        /// <param name="module">The module that this weaving context is for.</param>
        /// <returns>The <see cref="IWeavingContext"/> initialized.</returns>
        internal static IWeavingContext InitializeMethodWeavingContext(ModuleDefinition module) => new WeavingContext(module);
    }
}
