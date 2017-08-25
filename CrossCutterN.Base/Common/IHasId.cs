// <copyright file="IHasId.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Common
{
    /// <summary>
    /// Interface for a class which has an Id.
    /// </summary>
    /// <typeparam name="T">Type of Id.</typeparam>
    public interface IHasId<out T>
    {
        /// <summary>
        /// Gets Id of the item.
        /// </summary>
        T Key { get; }
    }
}
