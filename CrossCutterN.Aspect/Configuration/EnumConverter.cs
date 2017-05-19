/**
 * Description: join point enumeration converter
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Configuration
{
    using System.ComponentModel;

    public class EnumConverter<T> : EnumConverter
    {
        public EnumConverter() : base(typeof(T))
        {
        }
    }
}
