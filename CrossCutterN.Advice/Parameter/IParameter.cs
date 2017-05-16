/**
 * Description: read only parameter implementation
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Parameter
{
    using System.Collections.Generic;
    using Common;

    public interface IParameter : IHasId<string>, IHasSortKey<int>
    {
        string Name { get; }
        string TypeName { get; }
        object Value { get; }
        int Sequence { get; }
        IReadOnlyCollection<ICustomAttribute> CustomAttributes { get; }
    }
}
