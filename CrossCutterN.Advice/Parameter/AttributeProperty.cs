/**
 * Description: attribute property data for AOP injector
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Parameter
{
    using System;

    internal sealed class AttributeProperty : IAttributeProperty
    {
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

        internal AttributeProperty(string name, string typeName, int sequence, object value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }
            if (string.IsNullOrWhiteSpace(typeName))
            {
                throw new ArgumentNullException("typeName");
            }
            if (sequence < 0)
            {
                throw new ArgumentOutOfRangeException("sequence", "Sequence must be non-negative number.");
            }
            Name = name;
            TypeName = typeName;
            Sequence = sequence;
            Value = value;
        }
    }
}
