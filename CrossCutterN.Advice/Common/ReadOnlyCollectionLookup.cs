/**
 * Description: generic container for adding element, outputing elements as readonly list and look up elements by ids
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ReadOnlyCollectionLookup<TR, TI, TS> where TR: class, IHasId<TI>, IHasSortKey<TS>
    {
        private readonly IDictionary<TI, TR> _dictionary = new Dictionary<TI, TR>();

        public virtual void Add(TR w)
        {
            if (w == null)
            {
                throw new ArgumentNullException();
            }
            if (_dictionary.ContainsKey(w.Key))
            {
                throw new ArgumentException("Key of lookup item already exists in container.");
            }

            _dictionary.Add(w.Key, w);
        }

        public virtual IReadOnlyCollection<TR> GetAll()
        {
            return _dictionary.Values.OrderBy(tr => tr.SortKey).ToList().AsReadOnly();
        }

        public virtual bool Has(TI id)
        {
            return _dictionary.ContainsKey(id);
        }

        public virtual TR Get(TI id)
        {
            if (!_dictionary.ContainsKey(id))
            {
                throw new ArgumentException();
            }
            return _dictionary[id];
        }
    }
}
