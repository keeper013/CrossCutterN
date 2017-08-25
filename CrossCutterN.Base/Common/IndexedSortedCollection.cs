// <copyright file="IndexedSortedCollection.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Read only collection implementation indexed by id and sortable by a key.
    /// </summary>
    /// <typeparam name="TR">Type of collection item.</typeparam>
    /// <typeparam name="TI">Type of look up Id.</typeparam>
    /// <typeparam name="TS">Type of sort key.</typeparam>
    public class IndexedSortedCollection<TR, TI, TS>
        where TR : class, IHasId<TI>, IHasSortKey<TS>
    {
        private readonly IDictionary<TI, TR> dictionary = new Dictionary<TI, TR>();

        /// <summary>
        /// Gets all added contents in the collection.
        /// </summary>
        /// <returns>All stored values sorted.</returns>
        public virtual IReadOnlyCollection<TR> All => dictionary.Values.OrderBy(tr => tr.SortKey).ToList().AsReadOnly();

        /// <summary>
        /// Adds a value into the collection.
        /// </summary>
        /// <param name="w">Value to be added to the collection.</param>
        public virtual void Add(TR w)
        {
            if (w == null)
            {
                throw new ArgumentNullException();
            }

            if (dictionary.ContainsKey(w.Key))
            {
                throw new ArgumentException("Key of IndexedSortedCollection item already exists in container.");
            }

            dictionary.Add(w.Key, w);
        }

        /// <summary>
        /// Checks if the collection contains an item with the given Id.
        /// </summary>
        /// <param name="id">Id of an item.</param>
        /// <returns>True if the collection contains an item with the given Id, false elsewise.</returns>
        public virtual bool ContainsId(TI id)
        {
            return dictionary.ContainsKey(id);
        }

        /// <summary>
        /// Gets item with the given Id.
        /// </summary>
        /// <param name="id">Id of an item.</param>
        /// <returns>The item with the given Id.</returns>
        public virtual TR Get(TI id)
        {
            if (!dictionary.ContainsKey(id))
            {
                throw new ArgumentException();
            }

            return dictionary[id];
        }
    }
}
