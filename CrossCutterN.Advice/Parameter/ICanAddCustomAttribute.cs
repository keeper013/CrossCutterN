/**
 * Description: write only parameter interface
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Parameter
{
    using Common;

    public interface ICanAddCustomAttribute : ICanConvert<IParameter>
    {
        void AddCustomAttribute(ICustomAttribute attribute);
    }
}
