// <copyright file="IHasSortKey.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Common
{
    /// <summary>
    /// Class that has a sort key defined.
    /// </summary>
    /// <typeparam name="T">Type of the sort key.</typeparam>
    public interface IHasSortKey<out T>
    {
        /// <summary>
        /// Gets the sort key.
        /// </summary>
        T SortKey { get; }
    }
}
