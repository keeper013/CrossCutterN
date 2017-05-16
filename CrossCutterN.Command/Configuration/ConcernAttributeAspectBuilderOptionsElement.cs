/**
 * Description: concern attribute aspect builder options elements
 * Author: David Cui
 */

namespace CrossCutterN.Command.Configuration
{
    using System;
    using System.ComponentModel;
    using System.Configuration;

    public class ConcernAttributeAspectBuilderOptionsElement : AspectBuilderOptionsElement
    {
        [ConfigurationProperty("pointCutAtEntry", IsKey = false, IsRequired = false, DefaultValue = true)]
        [TypeConverter(typeof(BooleanConverter))]
        public bool PointCutAtEntry
        {
            get { return (Boolean)this["pointCutAtEntry"]; }
            set { this["pointCutAtEntry"] = value; }
        }

        [ConfigurationProperty("pointCutAtException", IsKey = false, IsRequired = false, DefaultValue = true)]
        [TypeConverter(typeof(BooleanConverter))]
        public bool PointCutAtException
        {
            get { return (Boolean)this["pointCutAtException"]; }
            set { this["pointCutAtException"] = value; }
        }

        [ConfigurationProperty("pointCutAtExit", IsKey = false, IsRequired = false, DefaultValue = true)]
        [TypeConverter(typeof(BooleanConverter))]
        public bool PointCutAtExit
        {
            get { return (Boolean)this["pointCutAtExit"]; }
            set { this["pointCutAtExit"] = value; }
        }
    }
}
