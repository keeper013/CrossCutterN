/**
 * Description: write only parameter interface
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Concern
{
    using Advice.Common;

    public interface ICanAddCustomAttribute : ICanConvertToReadOnly<IParameter>
    {
        void AddCustomAttribute(ICustomAttribute attribute);
    }
}
