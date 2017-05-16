/**
 * Description: implementation of ReadOnlyCollectionLookup with fixed id and sort key type
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Common
{
    using System;

    public class StringKeyIntSortKeyReadOnlyCollectionLookup<TR> : ReadOnlyCollectionLookup<TR, string, int> where TR : class, IHasId<string>, IHasSortKey<int>
    {
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
