/**
 * Description: custom attribute data for AOP injector
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Parameter
{
    using System;
    using System.Collections.Generic;
    using Common;

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
                return _properties.GetAll();
            }
        }

        internal CustomAttribute(string typeName, int sequence)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
            if (string.IsNullOrWhiteSpace(typeName))
            {
                throw new ArgumentNullException("typeName");
            }
            if (sequence < 0)
            {
                throw new ArgumentOutOfRangeException("sequence", "Sequence must be non-negative number.");
            }
#endif
            TypeName = typeName;
            Sequence = sequence;
        }

        public IAttributeProperty GetProperty(string name)
        {
            return _properties.Get(name);
        }

        public bool HasProperty(string name)
        {
            return _properties.Has(name);
        }

        public void AddAttributeProperty(IAttributeProperty property)
        {
            _readOnly.Assert(false);
            _properties.Add(property);
        }

        public ICustomAttribute Convert()
        {
            _readOnly.Apply();
            return this;
        }
    }
}
