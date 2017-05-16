/**
 * Description: Configuration section
 * Author: David Cui
 */

namespace CrossCutterN.Command.Configuration
{
    using System.Configuration;

    public class CrossCutterNSection : ConfigurationSection
    {
        [ConfigurationProperty("concernAttributeAspectBuilders", IsDefaultCollection = false)]
        public ConcernAttributeAspectBuilderCollection ConcernAttributeAspectBuilders
        {
            get { return (ConcernAttributeAspectBuilderCollection)this["concernAttributeAspectBuilders"]; }
            set { this["concernAttributeAspectBuilders"] = value; }
        }

        [ConfigurationProperty("nameExpressionAspectBuilders", IsDefaultCollection = false)]
        public NameExpressionAspectBuilderCollection NameExpressionAspectBuilders
        {
            get { return (NameExpressionAspectBuilderCollection)this["nameExpressionAspectBuilders"]; }
            set { this["nameExpressionAspectBuilders"] = value; }
        }
    }
}
