/**
* Description: write only weaving batch
* Author: David Cui
*/

namespace CrossCutterN.Weaver.Batch
{
    using System.Collections.Generic;
    using Advice.Common;
    using Aspect.Builder;
    using Aspect;

    internal interface ICanAddAspectBuilder : ICanConvertToReadOnly<IWeavingBatch>
    {
        void AddAspectBuilder(string id, IAspectBuilder builder, IReadOnlyDictionary<JoinPoint, int> sequenceDict);
    }
}
