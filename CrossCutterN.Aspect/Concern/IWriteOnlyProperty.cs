/**
 * Description: writeonly property interface
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Concern
{
    using System.Collections.Generic;
    using Advice.Common;

    public interface IWriteOnlyProperty : ICanConvertToReadOnly<IProperty>
    {
        void AddCustomAttribute(ICustomAttribute attribute);
        void AddGetterCustomAttribute(ICustomAttribute attribute);
        void AddSetterCustomAttribute(ICustomAttribute attribute);
        IReadOnlyCollection<ICustomAttribute> ClassCustomAttributes { set; }
    }
}
