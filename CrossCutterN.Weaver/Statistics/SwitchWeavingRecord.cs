/**
* Description: Switch weaving record implementation
* Author: David Cui
*/

namespace CrossCutterN.Weaver.Statistics
{
    using System;

    internal class SwitchWeavingRecord : ISwitchWeavingRecord
    {
        public string Class { get; private set; }
        public string Property { get; private set; }
        public string Method { get; private set; }
        public string Aspect { get; private set; }
        public string StaticVariableName { get; private set; }
        public bool Value { get; private set; }

        public SwitchWeavingRecord(string clazz, string property, string method, string aspect, string variable, bool value)
        {
            if (string.IsNullOrWhiteSpace(clazz))
            {
                throw new ArgumentNullException("clazz");
            }
            if (string.IsNullOrWhiteSpace(method))
            {
                throw new ArgumentNullException("method");
            }
            if (string.IsNullOrWhiteSpace(aspect))
            {
                throw new ArgumentNullException("aspect");
            }
            if (string.IsNullOrWhiteSpace(variable))
            {
                throw new ArgumentNullException("variable");
            }
            Class = clazz;
            Property = property;
            Method = method;
            Aspect = aspect;
            StaticVariableName = variable;
            Value = value;
        }
    }
}
