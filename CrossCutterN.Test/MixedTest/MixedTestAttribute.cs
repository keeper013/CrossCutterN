/**
 * Description: mixed test attributes
 * Author: David Cui
 */

namespace CrossCutterN.Test.MixedTest
{
    using Advice.Concern;

    internal sealed class MixedConcernMethod1Attribute : MethodConcernAttribute
    {
    }

    internal sealed class MixedConcernMethod2Attribute : MethodConcernAttribute
    {
    }

    internal sealed class MixedConcernClass3Attribute : ClassConcernAttribute
    {
    }
}
