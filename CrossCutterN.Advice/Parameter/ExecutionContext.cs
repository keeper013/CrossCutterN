/**
* Description: method run time flags implementation
* Author: David Cui
*/

namespace CrossCutterN.Advice.Parameter
{
    internal sealed class ExecutionContext : IExecutionContext
    {
        public bool ExceptionThrown { get; private set; }

        internal ExecutionContext()
        {
            ExceptionThrown = false;
        }

        public void MarkExceptionThrown()
        {
            ExceptionThrown = true;
        }
    }
}
