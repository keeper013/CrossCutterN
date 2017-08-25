// <copyright file="StringIndexedIntSortedCollection.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Common
{
    using System;

    /// <summary>
    /// Read only collection implementation indexed by string id and sortable by integer key.
    /// </summary>
    /// <typeparam name="TR">Type of collection item.</typeparam>
    public sealed class StringIndexedIntSortedCollection<TR> : IndexedSortedCollection<TR, string, int>
        where TR : class, IHasId<string>, IHasSortKey<int>
    {
        /// <inheritdoc/>
        public override TR Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException();
            }

            return base.Get(id);
        }
    }
}
