/**
* Description: property advice implementation
* Author: David Cui
*/

namespace CrossCutterN.Aspect
{
    using System;

    public class PropertyAspect : IPropertyAspect
    {
        public IAspect GetterAspect { get; private set; }
        public IAspect SetterAspect { get; private set; }

        public PropertyAspect(IAspect getterAspect, IAspect setterAspect)
        {
            if (getterAspect == null)
            {
                throw new ArgumentNullException("getterAspect");
            }
            if (setterAspect == null)
            {
                throw new ArgumentNullException("setterAspect");
            }
            // Considering aspect generation is extendable, empty aspect is possible by customized aspect builders
            // So no empty aspect checking performed here
            GetterAspect = getterAspect;
            SetterAspect = setterAspect;
        }
    }
}
