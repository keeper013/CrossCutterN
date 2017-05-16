/**
 * Description: IExecutionContext refernece
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using System;
    using System.Reflection;

    internal interface IExecutionContextWriteOnlyReference
    {
        Type TypeReference { set; }

        MethodInfo ExceptionThrownGetter { set; }
        MethodInfo MarkExceptionThrownMethod { set; }
    }
}
