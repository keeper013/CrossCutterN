/**
 * Description: Concern attribute aspect builder collection
 * Author: David Cui
 */

namespace CrossCutterN.Command.Configuration
{
    using System.Configuration;

    [ConfigurationCollection(typeof(ConcernAttributeAspectBuilderElement), AddItemName = "add")]
    public class ConcernAttributeAspectBuilderCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ConcernAttributeAspectBuilderElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConcernAttributeAspectBuilderElement)element).Id;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }

        public ConcernAttributeAspectBuilderElement this[int index]
        {
            get { return (ConcernAttributeAspectBuilderElement)BaseGet(index); }
        }
    }
}
