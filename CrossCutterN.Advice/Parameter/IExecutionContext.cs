/**
 * Description: method run time flag
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Parameter
{
    public interface IExecutionContext
    {
        void Set(object key, object value);
        bool Remove(object key);
        object Get(object key);
        bool Exist(object key);
    }
}
