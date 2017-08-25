// <copyright file="SwitchHandler.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Switch
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CrossCutterN.Base.Common;
    using Mono.Cecil;

    /// <summary>
    /// Switch handler implementation.
    /// </summary>
    internal sealed class SwitchHandler : ISwitchHandler, ISwitchHandlerBuilder
    {
        private static readonly Random Random = new Random();
        private readonly IrreversibleOperation readOnly = new IrreversibleOperation();
        private readonly IDictionary<string, IDictionary<string, SwitchInfo>> switches
            = new Dictionary<string, IDictionary<string, SwitchInfo>>();

        private readonly TypeDefinition type;
        private readonly TypeReference reference;

        /// <summary>
        /// Initializes a new instance of the <see cref="SwitchHandler"/> class.
        /// </summary>
        /// <param name="type">Class that switchhandler is for.</param>
        /// <param name="reference">Reference to type of the switch field.</param>
        public SwitchHandler(TypeDefinition type, TypeReference reference)
        {
            this.type = type ?? throw new ArgumentNullException("type");
            this.reference = reference ?? throw new ArgumentNullException("reference");
        }

        /// <inheritdoc/>
        public IEnumerable<SwitchInitializingData> GetData()
        {
            readOnly.Assert(true);
            var result = new List<SwitchInitializingData>();
            foreach (var method in switches)
            {
                foreach (var aspect in method.Value)
                {
                    result.Add(SwitchFactory.InitializeSwitchData(aspect.Value.Property, method.Key, aspect.Key, aspect.Value.Field, aspect.Value.Value));
                }
            }

            return result;
        }

        /// <inheritdoc/>
        public FieldReference GetSwitchField(string property, string methodSignature, string aspect, bool value)
        {
#if DEBUG
            if (string.IsNullOrWhiteSpace(methodSignature))
            {
                throw new ArgumentNullException("methodSignature");
            }

            if (string.IsNullOrWhiteSpace(aspect))
            {
                throw new ArgumentNullException("aspect");
            }
#endif
            readOnly.Assert(false);
            if (!switches.ContainsKey(methodSignature))
            {
                var field = GetField(8);
                switches.Add(methodSignature, new Dictionary<string, SwitchInfo> { { aspect, new SwitchInfo(property, field, value) } });
                return field;
            }

            var aspects = switches[methodSignature];
            if (!aspects.ContainsKey(aspect))
            {
                var field = GetField(8);
                aspects.Add(aspect, new SwitchInfo(property, field, value));
                return field;
            }

            return switches[methodSignature][aspect].Field;
        }

        /// <inheritdoc/>
        public ISwitchHandler Build()
        {
            readOnly.Apply();
            return this;
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

        private FieldReference GetField(int length)
        {
            var fieldName = $"_{RandomSwitchFieldName(length)}_";
            while (type.Fields.Any(fld => fld.Name.Equals(fieldName)))
            {
                fieldName = $"_{RandomSwitchFieldName(length)}_";
            }

            var field = new FieldDefinition(fieldName, FieldAttributes.Static | FieldAttributes.Private, reference);
            type.Fields.Add(field);
            return field;
        }

        private class SwitchInfo
        {
            public SwitchInfo(string property, FieldReference field, bool value)
            {
                Property = property;
                Field = field ?? throw new ArgumentNullException("field");
                Value = value;
            }

            public string Property { get; private set; }

            public FieldReference Field { get; private set; }

            public bool Value { get; private set; }
        }
    }
}
