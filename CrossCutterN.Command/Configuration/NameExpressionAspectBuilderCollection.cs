/**
 * Description: Name expression aspect builder collection
 * Author: David Cui
 */

namespace CrossCutterN.Command.Configuration
{
    using System.Configuration;

    [ConfigurationCollection(typeof(NameExpressionAspectBuilderElement), AddItemName = "add")]
    public class NameExpressionAspectBuilderCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new NameExpressionAspectBuilderElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((NameExpressionAspectBuilderElement) element).Id;
        }

        public NameExpressionAspectBuilderElement this[int index]
        {
            get { return (NameExpressionAspectBuilderElement) BaseGet(index); }
        }
    }
}
