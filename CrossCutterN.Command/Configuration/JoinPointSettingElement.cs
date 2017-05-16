/**
 * Description: join point sequence configuration
 * Author: David Cui
 */

namespace CrossCutterN.Command.Configuration
{
    using Aspect;
    using System.ComponentModel;
    using System.Configuration;

    public class JoinPointSettingElement : AdviceElement
    {
        [ConfigurationProperty("joinPoint", IsKey = false, IsRequired = true)]
        [TypeConverter(typeof(EnumConverter<JoinPoint>))]
        public JoinPoint JoinPoint
        {
            get { return (JoinPoint)this["joinPoint"]; }
            set { this["joinPoint"] = value; }
        }

        [ConfigurationProperty("sequence", IsKey = false, IsRequired = true)]
        public int Sequence
        {
            get { return (int)this["sequence"]; }
            set { this["sequence"] = value; }
        }
    }
}
