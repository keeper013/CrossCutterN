/**
 * Description: Aspect builder element
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Configuration
{
    using System.Configuration;

    public class AspectBuilderElement : ConfigurationElement
    {
        [ConfigurationProperty("id", IsKey = true, IsRequired = true)]
        public string Id
        {
            get { return (string)this["id"]; }
            set { this["id"] = value; }
        }

        [ConfigurationCollection(typeof(JoinPointSettingElement))]
        [ConfigurationProperty("pointcut", IsDefaultCollection = false, IsRequired = true)]
        public JoinPointSettingsCollection PointCut
        {
            get { return (JoinPointSettingsCollection)this["pointcut"]; }
            set { this["pointcut"] = value; }
        }
    }
}
