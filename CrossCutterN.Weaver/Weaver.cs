/**
* Description: Weaver implementation
* Author: David Cui
*/

namespace CrossCutterN.Weaver
{
    using System;
    using System.Collections.Generic;
    using Aspect;
    using Aspect.Builder;
    using Statistics;
    using Batch;
    using AssemblyHandler;

    internal sealed class Weaver : ICanAddAspectBuilder, IWeaver
    {
        private Batch.ICanAddAspectBuilder _writeOnlyBatch = BatchFactory.InitializeBatch();
        private IWeavingBatch _batch;

        public void AddAspectBuilder(string id, IAspectBuilder builder, IReadOnlyDictionary<JoinPoint, int> sequenceDict)
        {
            if(_writeOnlyBatch == null)
            {
                throw new InvalidOperationException("The weaver can't accept aspect build any more.");
            }
            _writeOnlyBatch.AddAspectBuilder(id, builder, sequenceDict);
        }

        public IAssemblyWeavingStatistics Weave(string inputAssemblyPath, string outputAssemblyPath, bool includeSymbol)
        {
            if(_batch == null)
            {
                throw new InvalidOperationException("The weaver isn't ready to weave yet.");
            }
            if(string.IsNullOrWhiteSpace(inputAssemblyPath))
            {
                throw new ArgumentNullException("inputAssemblyPath");
            }
            if (string.IsNullOrWhiteSpace(outputAssemblyPath))
            {
                throw new ArgumentNullException("outputAssemblyPath");
            }
            return Processor.Weave(inputAssemblyPath, outputAssemblyPath, _batch, includeSymbol);
        }

        public IWeaver Convert()
        {
            if (_writeOnlyBatch == null)
            {
                throw new InvalidOperationException("The weaver has been set to read-only already.");
            }
            _batch = _writeOnlyBatch.Convert();
            _writeOnlyBatch = null;
            return this;
        }
    }
}
