/**
 * Description: add parameter interface
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Parameter
{
    using Common;

    public interface ICanAddParameter : ICanConvert<IExecution>
    {
        void AddParameter(IParameter parameter);
    }
}
