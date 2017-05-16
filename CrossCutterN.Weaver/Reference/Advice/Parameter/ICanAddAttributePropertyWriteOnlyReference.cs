/**
 * Description: ICanAddAttributeProperty write only reference
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using System;
    using System.Reflection;
    using CrossCutterN.Advice.Common;

    internal interface ICanAddAttributePropertyWriteOnlyReference : ICanConvertToReadOnly<ICanAddAttributePropertyReference>
    {
        Type TypeReference { set; }
        Type ReadOnlyTypeReference { set; }

        MethodInfo AddAttributePropertyMethod { set; }
        MethodInfo ToReadOnlyMethod { set; }
    }
}
