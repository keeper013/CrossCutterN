/**
 * Description: custom attribute data for AOP injector
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Concern
{
    using System;
    using System.Collections.Generic;
    using Advice.Common;

    internal sealed class CustomAttribute : ICustomAttribute, ICanAddAttributeProperty
    {
        private readonly StringKeyIntSortKeyReadOnlyCollectionLookup<IAttributeProperty> _properties =
            new StringKeyIntSortKeyReadOnlyCollectionLookup<IAttributeProperty>();
        private readonly IrreversibleOperation _readOnly = new IrreversibleOperation();

        public string TypeName { get; private set; }
        public int Sequence { get; private set; }

        public IReadOnlyCollection<IAttributeProperty> Properties
        {
            get
            {
                _readOnly.Assert(true);
                return _properties.GetAll();
            }
        }

        internal CustomAttribute(string typeName, int sequence)
        {
            if (string.IsNullOrWhiteSpace(typeName))
            {
                throw new ArgumentNullException("typeName");
            }
            if (sequence < 0)
            {
                throw new ArgumentOutOfRangeException("sequence", "Sequence must be non-negative number.");
            }
            TypeName = typeName;
            Sequence = sequence;
        }

        public IAttributeProperty GetProperty(string name)
        {
            _readOnly.Assert(true);
            return _properties.Get(name);
        }

        public bool HasProperty(string name)
        {
            _readOnly.Assert(true);
            return _properties.Has(name);
        }

        public void AddAttributeProperty(IAttributeProperty property)
        {
            _readOnly.Assert(false);
            _properties.Add(property);
        }

        public ICustomAttribute ToReadOnly()
        {
            _readOnly.Apply();
            return this;
        }
    }
}
