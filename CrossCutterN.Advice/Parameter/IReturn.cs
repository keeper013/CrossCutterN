/**
 * Description: read only return value
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Parameter
{
    public interface IReturn
    {
        bool HasReturn { get; }
        string TypeName { get; }
        object Value { get; }
    }
}
