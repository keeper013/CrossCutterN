/**
 * Description: Base static method element
 * Author: David Cui
 */

namespace CrossCutterN.Command.Configuration
{
    using System;
    using System.ComponentModel;
    using System.Configuration;

    public class BaseStaticMethodElement : ConfigurationElement
    {
        [ConfigurationProperty("type", IsKey = false, IsRequired = true)]
        [TypeConverter(typeof(TypeNameConverter))]
        public Type Type
        {
            get { return (Type)this["type"]; }
            set { this["type"] = value; }
        }

        [ConfigurationProperty("method", IsKey = false, IsRequired = true)]
        [StringValidator(InvalidCharacters = "!@#$%^&*()-+={}[]\\|;:\"'<>,.?/~`")]
        public string Method
        {
            get { return (string)this["method"]; }
            set { this["method"] = value; }
        }
    }
}
