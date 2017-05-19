/**
 * Description: Name expression collection
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Configuration
{
    using System.Configuration;

    [ConfigurationCollection(typeof(NameExpressionElement), AddItemName = "add")]
    public class NameExpressionCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new NameExpressionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((NameExpressionElement) element).Expression;
        }

        public NameExpressionElement this[int index]
        {
            get { return (NameExpressionElement) BaseGet(index); }
        }
    }
}
