/**
* Description: Must add aspect builder interface
* Author: David Cui
*/

namespace CrossCutterN.Weaver
{
    using System.Collections.Generic;
    using Advice.Common;
    using Aspect;
    using Aspect.Builder;

    public interface ICanAddAspectBuilder : ICanConvertToReadOnly<IWeaver>
    {
        void AddAspectBuilder(string id, IAspectBuilder builder, IReadOnlyDictionary<JoinPoint, int> sequenceDict);
    }
}
