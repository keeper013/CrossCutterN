/**
 * Description: ICanAddCustomAttribute write only reference interface
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using System;
    using System.Reflection;
    using CrossCutterN.Advice.Common;

    internal interface ICanAddCustomAttributeWriteOnlyReference : ICanConvert<ICanAddCustomAttributeReference>
    {
        Type TypeReference { set; }
        Type ReadOnlyTypeReference { set; }

        MethodInfo AddCustomAttributeMethod { set; }
        MethodInfo ConvertMethod { set; }
    }
}
