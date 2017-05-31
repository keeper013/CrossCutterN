/**
 * Description: Switchable aspect builder element
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Configuration
{
    using System.Configuration;

    public class SwitchableAspectBuilderElement : AspectBuilderElement
    {
        [ConfigurationProperty("switch", IsKey = false, IsRequired = false)]
        public SwitchElement Switch
        {
            get { return (SwitchElement)this["switch"]; }
            set { this["switch"] = value; }
        }
    }
}
