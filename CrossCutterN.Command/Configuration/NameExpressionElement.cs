/**
 * Description: Name expression element
 * Author: David Cui
 */

namespace CrossCutterN.Command.Configuration
{
    using System.Configuration;

    public class NameExpressionElement : ConfigurationElement
    {
        [ConfigurationProperty("expression", IsKey = false, IsRequired = true)]
        [StringValidator(InvalidCharacters = "!@#$%^&()-+={}[]\\|;:\"'<>,?/~`")]
        public string Expression
        {
            get { return (string)this["expression"]; }
            set { this["expression"] = value; }
        }
    }
}
