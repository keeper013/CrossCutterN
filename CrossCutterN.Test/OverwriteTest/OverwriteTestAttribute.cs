/**
 * Description: Overwrite test attributes
 * Author: David Cui
 */

namespace CrossCutterN.Test.OverwriteTest
{
    using Advice.Concern;

    internal sealed class OverwriteConcernClassAttribute : ClassConcernAttribute
    {
    }

    internal sealed class OverwriteConcernMethodAttribute : MethodConcernAttribute
    {
    }

    internal sealed class OverwriteConcernPropertyAttribute : PropertyConcernAttribute
    {
    }

    internal sealed class OverwriteNoConcernAttribute : NoConcernAttribute
    {
    }
}
