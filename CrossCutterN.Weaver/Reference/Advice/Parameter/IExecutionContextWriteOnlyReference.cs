/**
 * Description: IExecutionContext refernece
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using System;
    using CrossCutterN.Advice.Common;

    internal interface IExecutionContextWriteOnlyReference : ICanConvert<IExecutionContextReference>
    {
        Type TypeReference { set; }
    }
}
