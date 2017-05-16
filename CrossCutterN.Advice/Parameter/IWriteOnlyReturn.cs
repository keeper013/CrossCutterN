/**
* Description: write only return value interface
* Author: David Cui
*/
namespace CrossCutterN.Advice.Parameter
{
    using Common;

    public interface IWriteOnlyReturn : ICanConvertToReadOnly<IReturn>
    {
        bool HasReturn { set; }
        object Value { set; }
    }
}
