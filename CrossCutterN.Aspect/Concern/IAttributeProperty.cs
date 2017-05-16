/**
 * Description: readonly attribute property interface
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Concern
{
    using Advice.Common;

    public interface IAttributeProperty : IHasId<string>, IHasSortKey<int>
    {
        string Name { get; }
        string TypeName { get; }
        int Sequence { get; }
        object Value { get; }
    }
}