/**
 * Description: parameter implementation
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Concern
{
    using System;
    using System.Collections.Generic;
    using Advice.Common;

    internal sealed class Parameter : IParameter, ICanAddCustomAttribute
    {
        private readonly List<ICustomAttribute> _customAttributes = new List<ICustomAttribute>();
        private readonly IrreversibleOperation _readOnly = new IrreversibleOperation();

        public string Name { get; private set; }
        public string TypeName { get; private set; }
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
                _readOnly.Assert(true);
                return _customAttributes.AsReadOnly();
            }
        }

        internal Parameter(string name, string typeName, int sequence)
        {
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
            Name = name;
            TypeName = typeName;
            Sequence = sequence;
        }

        public void AddCustomAttribute(ICustomAttribute attribute)
        {
            if(attribute == null)
            {
                throw new ArgumentNullException("attribute");
            }
            _readOnly.Assert(false);
            _customAttributes.Add(attribute);
        }

        public IParameter ToReadOnly()
        {
            _readOnly.Apply();
            return this;
        }
    }
}
