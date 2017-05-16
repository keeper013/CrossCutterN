/**
 * Description: ICanAddParameter write only reference
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using System;
    using System.Reflection;
    using CrossCutterN.Advice.Common;

    internal interface ICanAddParameterWriteOnlyReference : ICanConvertToReadOnly<ICanAddParameterReference>
    {
        Type TypeReference { set; }
        Type ReadOnlyTypeReference { set; }

        MethodInfo AddParameterMethod { set; }
        MethodInfo ToReadOnlyMethod { set; }
    }
}
