// <copyright file="AspectBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Builder
{
    using System;
    using System.Collections.Generic;
    using CrossCutterN.Aspect.Aspect;

    /// <summary>
    /// Aspect configuration.
    /// </summary>
    public abstract class AspectBuilder : IAspectBuilder
    {
        /// <summary>
        /// Gets or sets concern options.
        /// </summary>
        public List<ConcernOption> ConcernOptions { get; set; }

        /// <summary>
        /// Gets or sets advices.
        /// </summary>
        public Dictionary<JoinPoint, AdviceIndex> Advices { get; set; }

        /// <summary>
        /// Gets or sets a value indicating switch status.
        /// </summary>
        public bool? IsSwitchedOn { get; set; }

        /// <inheritdoc/>
        public abstract IAspect Build(IAdviceUtility utility, string defaultAdviceAssemblyKey);

        /// <summary>
        /// builds a switchable aspect builder with default options according to internal configuration.
        /// </summary>
        /// <param name="builder">Switchable aspect builder with default options.</param>
        /// <param name="utility">Advice utility.</param>
        /// <param name="defaultAdviceAssemblyKey">default advice assembly key.</param>
        /// <returns>The built aspect.</returns>
        protected IAspect Build(ISwitchableAspectWithDefaultOptions builder, IAdviceUtility utility, string defaultAdviceAssemblyKey)
        {
            SetAspectOptions(builder);
            SetAspectSwitchStatus(builder);
            AddAdvices(builder, utility, defaultAdviceAssemblyKey);
            return builder.Build();
        }

        private void SetAspectSwitchStatus(CrossCutterN.Aspect.Aspect.IAspectBuilder aspect)
        {
            aspect.IsSwitchedOn = IsSwitchedOn;
        }

        private void SetAspectOptions(IJoinPointDefaultOptionsBuilder aspect)
        {
            if (ConcernOptions != null)
            {
                aspect.ConcernConstructor = ConcernOptions.Contains(ConcernOption.Constructor);
                aspect.ConcernInstance = ConcernOptions.Contains(ConcernOption.Instance);
                aspect.ConcernInternal = ConcernOptions.Contains(ConcernOption.Internal);
                aspect.ConcernMethod = ConcernOptions.Contains(ConcernOption.Method);
                aspect.ConcernPrivate = ConcernOptions.Contains(ConcernOption.Private);
                aspect.ConcernPropertyGetter = ConcernOptions.Contains(ConcernOption.PropertyGetter);
                aspect.ConcernPropertySetter = ConcernOptions.Contains(ConcernOption.PropertySetter);
                aspect.ConcernProtected = ConcernOptions.Contains(ConcernOption.Protected);
                aspect.ConcernPublic = ConcernOptions.Contains(ConcernOption.Public);
                aspect.ConcernStatic = ConcernOptions.Contains(ConcernOption.Static);
            }
        }

        private void AddAdvices(CrossCutterN.Aspect.Aspect.IAspectBuilder aspect, IAdviceUtility utility, string defaultAdviceAssemblyKey)
        {
            foreach (var adviceEntry in Advices)
            {
                var index = adviceEntry.Value;
                var adviceAssemblyKey = index.AdviceAssemblyKey;
                if (string.IsNullOrWhiteSpace(adviceAssemblyKey))
                {
                    adviceAssemblyKey = defaultAdviceAssemblyKey;
                }

                if (string.IsNullOrWhiteSpace(adviceAssemblyKey))
                {
                    throw new ApplicationException($"{adviceEntry.Key}: advice assembly key is empty");
                }

                if (string.IsNullOrWhiteSpace(index.MethodKey))
                {
                    throw new ApplicationException($"{adviceEntry.Key}: method key is empty");
                }

                var methodInfo = utility.GetMethod(adviceAssemblyKey, index.MethodKey);
                if (methodInfo == null)
                {
                    throw new ApplicationException($"Method doesn't exist: assembly key = {adviceAssemblyKey}, method key = {index.MethodKey}");
                }

                aspect.SetAdvice(adviceEntry.Key, methodInfo);
            }
        }
    }
}
