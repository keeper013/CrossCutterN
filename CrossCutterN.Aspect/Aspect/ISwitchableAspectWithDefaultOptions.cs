// <copyright file="ISwitchableAspectWithDefaultOptions.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Aspect.Aspect
{
    /// <summary>
    /// Interface of switchable aspect with default concern options.
    /// </summary>
    public interface ISwitchableAspectWithDefaultOptions : IJoinPointDefaultOptionsBuilder, IAspectBuilder
    {
    }
}
