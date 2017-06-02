/**
 * Description: Switchable advice handler
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Switch
{
    using Mono.Cecil;

    internal static class SwitchFactory
    {
        public static SwitchInitializingData InitializeSwitchData(string property, string method, string aspect, FieldReference field, bool value)
        {
            return new SwitchInitializingData(property, method, aspect, field, value);
        }

        public static IWriteOnlySwitchHandler InitializeSwitchHandler(TypeDefinition type, TypeReference reference)
        {
            return new SwitchHandler(type, reference);
        }

        public static ISwitchSet InitializeSwitchSet()
        {
            return new SwitchSet();
        }

        public static ISwitchableSection InitializeSwitchableSection()
        {
            return new SwitchableSection();
        }
    }
}
