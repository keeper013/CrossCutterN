// <copyright file="Advice.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Builder
{
    using System.Collections.Generic;

    /// <summary>
    /// Definition of advice method.
    /// </summary>
    public sealed class Advice
    {
        /// <summary>
        /// Gets or sets method name;
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Gets or sets advice parameter types of the advice method.
        /// </summary>
        public List<AdviceParameterType> Parameters { get; set; }
    }
}
