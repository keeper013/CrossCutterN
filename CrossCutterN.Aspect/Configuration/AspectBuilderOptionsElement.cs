/**
 * Description: aspect builder options elements
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Configuration
{
    using System;
    using System.ComponentModel;
    using System.Configuration;

    public class AspectBuilderOptionsElement : ConfigurationElement
    {
        [ConfigurationProperty("concernConstructor", IsKey = false, IsRequired = false, DefaultValue = false)]
        [TypeConverter(typeof(BooleanConverter))]
        public bool ConcernConstructor
        {
            get { return (Boolean)this["concernConstructor"]; }
            set { this["concernConstructor"] = value; }
        }

        [ConfigurationProperty("concernInstance", IsKey = false, IsRequired = false, DefaultValue = true)]
        [TypeConverter(typeof(BooleanConverter))]
        public bool ConcernInstance
        {
            get { return (Boolean)this["concernInstance"]; }
            set { this["concernInstance"] = value; }
        }

        [ConfigurationProperty("concernInternal", IsKey = false, IsRequired = false, DefaultValue = false)]
        [TypeConverter(typeof(BooleanConverter))]
        public bool ConcernInternal
        {
            get { return (Boolean)this["concernInternal"]; }
            set { this["concernInternal"] = value; }
        }


        [ConfigurationProperty("concernMethod", IsKey = false, IsRequired = false, DefaultValue = true)]
        [TypeConverter(typeof(BooleanConverter))]
        public bool ConcernMethod
        {
            get { return (Boolean)this["concernMethod"]; }
            set { this["concernMethod"] = value; }
        }

        [ConfigurationProperty("concernPropertyGetter", IsKey = false, IsRequired = false, DefaultValue = false)]
        [TypeConverter(typeof(BooleanConverter))]
        public bool ConcernPropertyGetter
        {
            get { return (Boolean)this["concernPropertyGetter"]; }
            set { this["concernPropertyGetter"] = value; }
        }

        [ConfigurationProperty("concernPropertySetter", IsKey = false, IsRequired = false, DefaultValue = false)]
        [TypeConverter(typeof(BooleanConverter))]
        public bool ConcernPropertySetter
        {
            get { return (Boolean)this["concernPropertySetter"]; }
            set { this["concernPropertySetter"] = value; }
        }

        [ConfigurationProperty("concernPrivate", IsKey = false, IsRequired = false, DefaultValue = false)]
        [TypeConverter(typeof(BooleanConverter))]
        public bool ConcernPrivate
        {
            get { return (Boolean)this["concernPrivate"]; }
            set { this["concernPrivate"] = value; }
        }

        [ConfigurationProperty("concernProtected", IsKey = false, IsRequired = false, DefaultValue = false)]
        [TypeConverter(typeof(BooleanConverter))]
        public bool ConcernProtected
        {
            get { return (Boolean)this["concernProtected"]; }
            set { this["concernProtected"] = value; }
        }

        [ConfigurationProperty("concernPublic", IsKey = false, IsRequired = false, DefaultValue = true)]
        [TypeConverter(typeof(BooleanConverter))]
        public bool ConcernPublic
        {
            get { return (Boolean)this["concernPublic"]; }
            set { this["concernPublic"] = value; }
        }

        [ConfigurationProperty("concernStatic", IsKey = false, IsRequired = false, DefaultValue = true)]
        [TypeConverter(typeof(BooleanConverter))]
        public bool ConcernStatic
        {
            get { return (Boolean)this["concernStatic"]; }
            set { this["concernStatic"] = value; }
        }
    }
}
