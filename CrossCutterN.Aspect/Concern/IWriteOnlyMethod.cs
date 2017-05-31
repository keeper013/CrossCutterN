/**
 * Description: add parameter interface
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Concern
{
    using System.Collections.Generic;
    using Advice.Common;

    public interface IWriteOnlyMethod : ICanConvert<IMethod>
    {
        void AddParameter(IParameter parameter);
        void AddCustomAttribute(ICustomAttribute property);
        IReadOnlyCollection<ICustomAttribute> ClassCustomAttributes { set; }
    }
}
