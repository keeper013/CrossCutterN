/**
 * Description: concern attribute aspect builder element
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Configuration
{
    using System.Configuration;

    public class ConcernAttributeAspectBuilderElement : AspectBuilderElement
    {
        [ConfigurationProperty("factoryMethod", IsKey = false, IsRequired = true)]
        public ConcernAttributeAspectBuilderConstructorElement FactoryMethod
        {
            get { return (ConcernAttributeAspectBuilderConstructorElement)this["factoryMethod"]; }
            set { this["factoryMethod"] = value; }
        }

        [ConfigurationProperty("options", IsKey = false, IsRequired = false)]
        public ConcernAttributeAspectBuilderOptionsElement Options
        {
            get { return (ConcernAttributeAspectBuilderOptionsElement)this["options"]; }
            set { this["options"] = value; }
        }
    }
}
