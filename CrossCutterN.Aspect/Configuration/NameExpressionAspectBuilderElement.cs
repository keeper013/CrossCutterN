/**
 * Description: name expression aspect builder element
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Configuration
{
    using System.Configuration;

    public class NameExpressionAspectBuilderElement : AspectBuilderElement
    {
        [ConfigurationProperty("factoryMethod", IsKey = false, IsRequired = true)]
        public NameExpressionAspectBuilderConstructorElement FactoryMethod
        {
            get { return (NameExpressionAspectBuilderConstructorElement)this["factoryMethod"]; }
            set { this["factoryMethod"] = value; }
        }

        [ConfigurationProperty("options", IsKey = false, IsRequired = false)]
        public AspectBuilderOptionsElement Options
        {
            get { return (AspectBuilderOptionsElement)this["options"]; }
            set { this["options"] = value; }
        }
    }
}
