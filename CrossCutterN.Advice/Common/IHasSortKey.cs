/**
 * Description: has sort key interface
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Common
{
    public interface IHasSortKey<out T>
    {
       T SortKey { get; }
    }
}
