/**
 * Description: IWriteOnlyReturn write only reference
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using System;
    using System.Reflection;
    using CrossCutterN.Advice.Common;

    internal interface IWriteOnlyReturnWriteOnlyReference : ICanConvert<IWriteOnlyReturnReference>
    {
        Type TypeReference { set; }
        Type ReadOnlyTypeReference { set; }

        MethodInfo HasReturnSetter { set; }
        MethodInfo ValueSetter { set; }
        MethodInfo ConvertMethod { set; }
    }
}
