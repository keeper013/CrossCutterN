/**
 * Description: has id interface
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Common
{
    public interface IHasId<out T>
    {
        T Key { get; }
    }
}
