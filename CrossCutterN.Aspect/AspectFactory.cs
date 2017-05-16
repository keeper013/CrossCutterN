/**
 * Description: aspect factory
 * Author: David Cui
 */

namespace CrossCutterN.Aspect
{
    public static class AspectFactory
    {
        public static ICanSetJoinPointAdvice InitializeAspect()
        {
            return new Aspect();
        }

        public static IPropertyAspect InitializePropertyAspect(IAspect getterAspect, IAspect setterAspect)
        {
            return new PropertyAspect(getterAspect, setterAspect);
        }
    }
}
