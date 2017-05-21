/**
 * Description: IAttributeProperty write only reference
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Parameter
{
    using System;
    using CrossCutterN.Advice.Common;

    internal interface IAttributePropertyWriteOnlyReference : ICanConvert<IAttributePropertyReference>
    {
        Type TypeReference { set; }
    }
}
