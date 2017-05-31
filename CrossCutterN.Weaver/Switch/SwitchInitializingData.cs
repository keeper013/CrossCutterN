/**
 * Description: Switch initializing data
 * Author: David Cui
 */

using System;

namespace CrossCutterN.Weaver.Switch
{
    using Mono.Cecil;

    internal class SwitchInitializingData
    {
        public string Property { get; private set; }
        public string Method { get; private set; }
        public string Aspect { get; private set; }
        public FieldReference Field { get; set; }
        public bool Value { get; private set; }

        internal SwitchInitializingData(string property, string method, string aspect, FieldReference field, bool value)
        {
            if (string.IsNullOrWhiteSpace(method))
            {
                throw new ArgumentNullException("method");
            }
            if (string.IsNullOrWhiteSpace(aspect))
            {
                throw new ArgumentNullException("aspect");
            }
            if (field == null)
            {
                throw new ArgumentNullException("field");
            }
            Property = property;
            Method = method;
            Aspect = aspect;
            Field = field;
            Value = value;
        }
    }
}
