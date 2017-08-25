// <copyright file="AdviceInfo.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Weaver
{
    using System;
    using System.Reflection;
    using CrossCutterN.Aspect.Aspect;

    /// <summary>
    /// Advice information in weaving plan.
    /// </summary>
    internal sealed class AdviceInfo : IAdviceInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdviceInfo"/> class.
        /// </summary>
        /// <param name="advice">Advice method.</param>
        /// <param name="aspectName">Name of the aspect, also serves as key of aspect.</param>
        /// <param name="parameterFlag">Flag of parameters of this advice method.</param>
        /// <param name="isSwitchedOn">Default switch status for this aspect.</param>
        public AdviceInfo(MethodInfo advice, string aspectName, AdviceParameterFlag parameterFlag, bool? isSwitchedOn)
        {
#if DEBUG
            if (string.IsNullOrWhiteSpace(aspectName))
            {
                throw new ArgumentNullException("aspectName");
            }
#endif
            Advice = advice;
            AspectName = aspectName;
            ParameterFlag = parameterFlag;
            IsSwitchedOn = isSwitchedOn;
        }

        /// <inheritdoc/>
        public MethodInfo Advice { get; private set; }

        /// <inheritdoc/>
        public string AspectName { get; private set; }

        /// <inheritdoc/>
        public AdviceParameterFlag ParameterFlag { get; private set; }

        /// <inheritdoc/>
        public bool? IsSwitchedOn { get; private set; }
    }
}
