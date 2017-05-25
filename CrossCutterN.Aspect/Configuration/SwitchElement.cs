/**
 * Description: switchable element
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Configuration
{
    using System;
    using System.ComponentModel;
    using System.Configuration;

    public class SwitchElement : ConfigurationElement
    {
        [ConfigurationProperty("defaultStatus", IsKey = false, IsRequired = true, DefaultValue = true)]
        [TypeConverter(typeof(BooleanConverter))]
        public bool DefaultStatus
        {
            get { return (Boolean)this["defaultStatus"]; }
            set { this["defaultStatus"] = value; }
        }
    }
}
