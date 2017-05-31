/**
 * Description: Write only switchable advice handler
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Switch
{
    using Mono.Cecil;
    using Advice.Common;

    internal interface IWriteOnlySwitchHandler : ICanConvert<ISwitchHandler>
    {
        string Property { set; }
        FieldReference GetSwitchField(string method, string aspect, bool value);
    }
}
