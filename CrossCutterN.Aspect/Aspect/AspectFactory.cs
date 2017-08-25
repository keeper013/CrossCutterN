// <copyright file="AspectFactory.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Aspect
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Aspect factory.
    /// </summary>
    public static class AspectFactory
    {
        /// <summary>
        /// Initializes a new instance of of aspect which identifies target methods and properties using customized attributes.
        /// </summary>
        /// <param name="classConcernAttributeType">Type of class concern attribute.</param>
        /// <param name="methodConcernAttributeType">Type of method concern attribute.</param>
        /// <param name="propertyConcernAttributeType">Type of property concern attribute.</param>
        /// <param name="noConcernAttributeType">Type of no concern attribute.</param>
        /// <returns>The <see cref="ISwitchableAspectWithDefaultOptions"/> initialized.</returns>
        public static ISwitchableAspectWithDefaultOptions InitializeConcernAttributeAspect(
            Type classConcernAttributeType,
            Type methodConcernAttributeType,
            Type propertyConcernAttributeType,
            Type noConcernAttributeType)
        {
            return new ConcernAttributeAspect(
                classConcernAttributeType,
                methodConcernAttributeType,
                propertyConcernAttributeType,
                noConcernAttributeType);
        }

        /// <summary>
        /// Initializes a new instance of of aspect which identifies target methods and properties using method/property name expression.
        /// </summary>
        /// <param name="includes">Patterns used to include target methods/properties.</param>
        /// <param name="excludes">Patterns used to exclude target methods/properties.</param>
        /// <returns>The <see cref="ISwitchableAspectWithDefaultOptions"/> initialized.</returns>
        public static ISwitchableAspectWithDefaultOptions InitializeNameExpressionAspect(
            ICollection<string> includes,
            ICollection<string> excludes)
        {
            return new NameExpressionAspect(includes, excludes);
        }
    }
}
