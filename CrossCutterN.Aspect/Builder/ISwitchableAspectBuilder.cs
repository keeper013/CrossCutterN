/**
 * Description: aspect builder that supports switchable advices interface
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Builder
{
    public interface ISwitchableAspectBuilder
    {
        SwitchStatus SwitchStatus { set; }
    }
}
