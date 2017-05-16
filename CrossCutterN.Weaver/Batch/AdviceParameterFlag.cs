/**
 * Description: Advice parameter flag enumeration and extension
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Batch
{
    using System;

    [Flags]
    internal enum AdviceParameterFlag
    {
        None = 0,
        Execution = 1,
        Exception = 2,
        Return = 4,
        HasException = 8
    }
}
