/**
 * Description: Context test attributes
 * Author: David Cui
 */

namespace CrossCutterN.Test.ContextTest
{
    using Advice.Concern;

    internal sealed class ContextEntryExceptionConcernMethodAttribute : MethodConcernAttribute
    {
    }

    internal sealed class ContextEntryExitConcernMethodAttribute : MethodConcernAttribute
    {
    }

    internal sealed class ContextExceptionExitConcernMethodAttribute : MethodConcernAttribute
    {
    }

    internal sealed class ContextConcernMethodAttribute : MethodConcernAttribute
    {
    }
}
