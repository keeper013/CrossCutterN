/**
* Description: advices for a single property from a single aspect builder
* Author: David Cui
*/

namespace CrossCutterN.Aspect
{
    public interface IPropertyAspect
    {
        IAspect GetterAspect { get; }
        IAspect SetterAspect { get; }
    }
}
