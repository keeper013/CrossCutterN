// <copyright file="ConcernAttributeAspectBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Builder
{
    using System;
    using CrossCutterN.Aspect.Aspect;

    /// <summary>
    /// Definition for aspect that identifies target methods and assemblies by custom attributes.
    /// </summary>
    public sealed class ConcernAttributeAspectBuilder : AspectBuilder
    {
        /// <summary>
        /// Gets or sets type of class concern attribute.
        /// </summary>
        public AdviceAssemblyTypeIndex ConcernClassAttributeType { get; set; }

        /// <summary>
        /// Gets or sets type of method concern attribute.
        /// </summary>
        public AdviceAssemblyTypeIndex ConcernMethodAttributeType { get; set; }

        /// <summary>
        /// Gets or sets type of property concern attribute.
        /// </summary>
        public AdviceAssemblyTypeIndex ConcernPropertyAttributeType { get; set; }

        /// <summary>
        /// Gets or sets type of no concern attribute.
        /// </summary>
        public AdviceAssemblyTypeIndex NoConcernAttributeType { get; set; }

        /// <inheritdoc/>
        public override IAspect Build(IAdviceUtility utility, string defaultAdviceAssemblyKey)
        {
            var concernClass = GetAttribute(ConcernClassAttributeType, utility, defaultAdviceAssemblyKey);
            var concernMethod = GetAttribute(ConcernMethodAttributeType, utility, defaultAdviceAssemblyKey);
            var concernProperty = GetAttribute(ConcernPropertyAttributeType, utility, defaultAdviceAssemblyKey);
            var noConcern = GetAttribute(NoConcernAttributeType, utility, defaultAdviceAssemblyKey);
            var aspect = AspectFactory.InitializeConcernAttributeAspect(concernClass, concernMethod, concernProperty, noConcern);
            return Build(aspect, utility, defaultAdviceAssemblyKey);
        }

        private static Type GetAttribute(AdviceAssemblyTypeIndex index, IAdviceUtility utility, string defaultAdviceAssemblyKey)
        {
            Type concernAttribute = null;
            if (index != null)
            {
                var classConcernAttributeAssemblyKey = index.AdviceAssemblyKey;
                if (string.IsNullOrWhiteSpace(classConcernAttributeAssemblyKey))
                {
                    classConcernAttributeAssemblyKey = defaultAdviceAssemblyKey;
                }

                concernAttribute = utility.GetAttribute(classConcernAttributeAssemblyKey, index.TypeKey) ??
                    throw new ApplicationException($"Type index {classConcernAttributeAssemblyKey}, {index.TypeKey} not found.");
            }

            return concernAttribute;
        }
    }
}
