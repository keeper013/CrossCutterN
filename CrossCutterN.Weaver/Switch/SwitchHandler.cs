/**
 * Description: Switchable advice handler implementation
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Switch
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Mono.Cecil;
    using Advice.Common;

    internal class SwitchHandler : ISwitchHandler, IWriteOnlySwitchHandler
    {
        private static readonly Random Random = new Random();
        private readonly IrreversibleOperation _readOnly = new IrreversibleOperation();
        private string _property;
        private readonly IDictionary<string, IDictionary<string, SwitchInfo>> _switches 
            = new Dictionary<string, IDictionary<string, SwitchInfo>>();

        private readonly TypeDefinition _type;
        private readonly TypeReference _reference;

        public string Property
        {
            private get { return _property; }
            set
            {
                _readOnly.Assert(false);
                _property = value;
            }
        }

        public SwitchHandler(TypeDefinition type, TypeReference reference)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            if (reference == null)
            {
                throw new ArgumentNullException("reference");
            }
            _type = type;
            _reference = reference;
        }

        public IEnumerable<SwitchInitializingData> GetData()
        {
            _readOnly.Assert(true);
            var result = new List<SwitchInitializingData>();
            foreach (var method in _switches)
            {
                foreach (var aspect in method.Value)
                {
                    result.Add(SwitchFactory.InitializeSwitchData(aspect.Value.Property, method.Key, aspect.Key, aspect.Value.Field, aspect.Value.Value));
                }
            }
            return result;
        }

        public FieldReference GetSwitchField(string method, string aspect, bool value)
        {
            _readOnly.Assert(false);
            if (string.IsNullOrWhiteSpace(method))
            {
                throw new ArgumentNullException("method");
            }
            if (string.IsNullOrWhiteSpace(aspect))
            {
                throw new ArgumentNullException("aspect");
            }
            if (!_switches.ContainsKey(method))
            {
                var field = GetField(8);
                _switches.Add(method, new Dictionary<string, SwitchInfo> { { aspect, new SwitchInfo(Property, field, value) } });
                return field;
            }
            var aspects = _switches[method];
            if (!aspects.ContainsKey(aspect))
            {
                var field = GetField(8);
                aspects.Add(aspect, new SwitchInfo(Property, field, value));
                return field;
            }
            return _switches[method][aspect].Field;
        }

        public ISwitchHandler Convert()
        {
            _readOnly.Apply();
            return this;
        }

        private FieldReference GetField(int length)
        {
            var fieldName = string.Format("_{0}_", RandomSwitchFieldName(length));
            while (_type.Fields.Any(fld => fld.Name.Equals(fieldName)))
            {
                fieldName = string.Format("_{0}_", RandomSwitchFieldName(length));
            }
            var field = new FieldDefinition(fieldName, FieldAttributes.Static | FieldAttributes.Private, _reference);
            _type.Fields.Add(field);
            return field;
        }

        private static string RandomSwitchFieldName(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            const int charsLength = 62;
            var strb = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                strb.Append(chars[Random.Next(charsLength)]);
            }
            return strb.ToString();
        }

        private class SwitchInfo
        {
            public string Property { get; private set; }
            public FieldReference Field { get; private set; }
            public bool Value { get; private set; }

            public SwitchInfo(string property, FieldReference field, bool value)
            {
                if (field == null)
                {
                    throw new ArgumentNullException("field");
                }
                Property = property;
                Field = field;
                Value = value;
            }
        }
    }
}
