/**
 * Description: method run time flag
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Parameter
{
    public interface IExecutionContext
    {
        bool ExceptionThrown { get; }
        void MarkExceptionThrown();
    }
}
