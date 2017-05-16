/**
 * Description: concern attribute aspect builder constructor element
 * Author: David Cui
 */

namespace CrossCutterN.Command.Configuration
{
    using System;
    using System.ComponentModel;
    using System.Configuration;

    public class ConcernAttributeAspectBuilderConstructorElement : BaseStaticMethodElement
    {
        [ConfigurationProperty("classConcernAttribute", IsKey = false, IsRequired = false)]
        [TypeConverter(typeof(TypeNameConverter))]
        public Type ClassConcernAttributeType
        {
            get { return (Type)this["classConcernAttribute"]; }
            set { this["classConcernAttribute"] = value; }
        }

        [ConfigurationProperty("methodConcernAttribute", IsKey = false, IsRequired = false)]
        [TypeConverter(typeof(TypeNameConverter))]
        public Type MethodConcernAttributeType
        {
            get { return (Type)this["methodConcernAttribute"]; }
            set { this["methodConcernAttribute"] = value; }
        }

        [ConfigurationProperty("propertyConcernAttribute", IsKey = false, IsRequired = false)]
        [TypeConverter(typeof(TypeNameConverter))]
        public Type PropertyConcernAttributeType
        {
            get { return (Type)this["propertyConcernAttribute"]; }
            set { this["propertyConcernAttribute"] = value; }
        }

        [ConfigurationProperty("noConcernAttribute", IsKey = false, IsRequired = false)]
        [TypeConverter(typeof(TypeNameConverter))]
        public Type NoConcernAttributeType
        {
            get { return (Type)this["noConcernAttribute"]; }
            set { this["noConcernAttribute"] = value; }
        }
    }
}
