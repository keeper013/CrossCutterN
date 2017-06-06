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
        Context = 1,
        Execution = 2,
        Exception = 4,
        Return = 8,
        HasException = 16
    }
}
