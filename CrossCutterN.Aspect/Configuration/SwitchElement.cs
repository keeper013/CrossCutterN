/**
 * Description: switchable element
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Configuration
{
    using System.ComponentModel;
    using System.Configuration;

    public class SwitchElement : ConfigurationElement
    {
        [ConfigurationProperty("status", IsKey = false, IsRequired = true, DefaultValue = SwitchStatus.NotSwitchable)]
        [TypeConverter(typeof(EnumConverter<SwitchStatus>))]
        public SwitchStatus Status
        {
            get { return (SwitchStatus)this["status"]; }
            set { this["status"] = value; }
        }
    }
}
