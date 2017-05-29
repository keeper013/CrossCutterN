/**
 * Description: Switch field equality comparor
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Utilities
{
    using System.Collections.Generic;
    using Mono.Cecil;

    internal class FieldReferenceComparer : IEqualityComparer<FieldReference>
    {
        public bool Equals(FieldReference x, FieldReference y)
        {
            return x.Name.Equals(y.Name);
        }

        public int GetHashCode(FieldReference obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
