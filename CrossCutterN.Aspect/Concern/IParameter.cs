/**
 * Description: read only parameter implementation
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Concern
{
    using System.Collections.Generic;
    using Advice.Common;

    public interface IParameter : IHasId<string>, IHasSortKey<int>
    {
        string Name { get; }
        string TypeName { get; }
        int Sequence { get; }
        IReadOnlyCollection<ICustomAttribute> CustomAttributes { get; }
    }
}
