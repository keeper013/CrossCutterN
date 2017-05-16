/**
 * Description: Name expression aspect builder constructor element
 * Author: David Cui
 */

namespace CrossCutterN.Command.Configuration
{
    using System.Configuration;

    public class NameExpressionAspectBuilderConstructorElement : BaseStaticMethodElement
    {
        [ConfigurationProperty("includes", IsDefaultCollection = false, IsRequired = true)]
        public NameExpressionCollection Includes
        {
            get { return (NameExpressionCollection)this["includes"]; }
            set { this["includes"] = value; }
        }

        [ConfigurationProperty("excludes", IsDefaultCollection = false)]
        public NameExpressionCollection Excludes
        {
            get { return (NameExpressionCollection)this["excludes"]; }
            set { this["excludes"] = value; }
        }
    }
}
