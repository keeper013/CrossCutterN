/**
 * Description: advice element
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Configuration
{
    using System;
    using System.ComponentModel;
    using System.Configuration;

    public class AdviceElement : ConfigurationElement
    {
        [ConfigurationProperty("classType", IsKey = false, IsRequired = true)]
        [TypeConverter(typeof(TypeNameConverter))]
        public Type Type
        {
            get { return (Type)this["classType"]; }
            set { this["classType"] = value; }
        }

        [ConfigurationProperty("method", IsKey = false, IsRequired = true)]
        [StringValidator(InvalidCharacters = "!@#$%^&*()-+={}[]\\|;:\"'<>,.?/~`")]
        public string Method
        {
            get { return (string)this["method"]; }
            set { this["method"] = value; }
        }

        [ConfigurationProperty("parameterPattern", IsKey = false, IsRequired = true)]
        [TypeConverter(typeof(EnumConverter<AdviceParameterPattern>))]
        public AdviceParameterPattern ParameterPattern
        {
            get { return (AdviceParameterPattern) this["parameterPattern"]; }
            set { this["parameterPattern"] = value; }
        }
    }
}
