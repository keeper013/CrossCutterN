/**
 * Description: write only custom attribute interface
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Concern
{
    using Advice.Common;

    public interface ICanAddAttributeProperty : ICanConvertToReadOnly<ICustomAttribute>
    {
        void AddAttributeProperty(IAttributeProperty property);
    }
}
