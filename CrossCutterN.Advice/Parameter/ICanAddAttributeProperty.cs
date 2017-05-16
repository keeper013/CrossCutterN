/**
 * Description: write only custom attribute interface
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Parameter
{
    using Common;

    public interface ICanAddAttributeProperty : ICanConvertToReadOnly<ICustomAttribute>
    {
        void AddAttributeProperty(IAttributeProperty property);
    }
}
