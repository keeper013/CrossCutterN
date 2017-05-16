/**
 * Description: readonly attribute property interface
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Parameter
{
    using Common;

    public interface IAttributeProperty : IHasId<string>, IHasSortKey<int>
    {
        string Name { get; }
        string TypeName { get; }
        int Sequence { get; }
        object Value { get; }
    }
}