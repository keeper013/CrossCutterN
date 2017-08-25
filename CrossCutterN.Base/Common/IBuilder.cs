// <copyright file="IBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Common
{
    /// <summary>
    /// Interface that can build to another type.
    /// </summary>
    /// <typeparam name="T">Type to be built to.</typeparam>
    public interface IBuilder<out T>
        where T : class
    {
        /// <summary>
        /// Builds to another class T.
        /// </summary>
        /// <returns>Built result.</returns>
        T Build();
    }
}
