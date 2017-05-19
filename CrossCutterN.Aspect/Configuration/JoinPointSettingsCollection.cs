/**
 * Description: join point sequence collection configuration
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Configuration
{
    using System.Configuration;

    [ConfigurationCollection(typeof(JoinPointSettingElement), AddItemName = "add")]
    public class JoinPointSettingsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new JoinPointSettingElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((JoinPointSettingElement)element).JoinPoint;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }

        public JoinPointSettingElement this[int index]
        {
            get { return (JoinPointSettingElement) BaseGet(index); }
        }
    }
}
