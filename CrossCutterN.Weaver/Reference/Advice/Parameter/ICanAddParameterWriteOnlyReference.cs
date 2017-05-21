/**
 * Description: ICanAddParameter write only reference
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using System;
    using System.Reflection;
    using CrossCutterN.Advice.Common;

    internal interface ICanAddParameterWriteOnlyReference : ICanConvert<ICanAddParameterReference>
    {
        Type TypeReference { set; }
        Type ReadOnlyTypeReference { set; }

        MethodInfo AddParameterMethod { set; }
        MethodInfo ConvertMethod { set; }
    }
}
