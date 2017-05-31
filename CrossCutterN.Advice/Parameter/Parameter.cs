/**
 * Description: parameter implementation
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Parameter
{
    using System;
    using System.Collections.Generic;
    using Common;

    internal sealed class Parameter : IParameter, ICanAddCustomAttribute
    {
        private readonly List<ICustomAttribute> _customAttributes = new List<ICustomAttribute>();
        private readonly IrreversibleOperation _readOnly = new IrreversibleOperation();

        public string Name { get; private set; }
        public string TypeName { get; private set; }
        public object Value { get; private set; }
        public int Sequence { get; private set; }

        public string Key
        {
            get { return Name; }
        }
        public int SortKey
        {
            get { return Sequence; }
        }

        public IReadOnlyCollection<ICustomAttribute> CustomAttributes
        {
            get
            {
                return _customAttributes.AsReadOnly();
            }
        }

        internal Parameter(string name, string typeName, int sequence, object value)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }
            if(string.IsNullOrWhiteSpace(typeName))
            {
                throw new ArgumentNullException("typeName");
            }
            if(sequence < 0)
            {
                throw new ArgumentOutOfRangeException("sequence", "Sequence must be non-negative number.");
            }
#endif
            Name = name;
            TypeName = typeName;
            Sequence = sequence;
            Value = value;
        }

        public void AddCustomAttribute(ICustomAttribute attribute)
        {
            _readOnly.Assert(false);
            _customAttributes.Add(attribute);
        }

        public IParameter Convert()
        {
            _readOnly.Apply();
            return this;
        }
    }
}
