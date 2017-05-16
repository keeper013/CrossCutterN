/**
* Description: return value implementation
* Author: David Cui
*/
namespace CrossCutterN.Advice.Parameter
{
    using System;
    using Common;

    internal sealed class Return : IReturn, IWriteOnlyReturn
    {
        private readonly IrreversibleOperation _readOnly = new IrreversibleOperation();
        private bool _hasReturn;
        private object _value;

        public bool HasReturn
        {
            get
            {
                return _hasReturn;
            }
            set
            {
                _readOnly.Assert(false);
                _hasReturn = value;
            }
        }

        public string TypeName { get; private set; }

        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                _readOnly.Assert(false);
                _value = value;
            }
        }

        internal Return(bool hasReturn, string typeName)
        {
            if(string.IsNullOrWhiteSpace(typeName))
            {
                throw new ArgumentNullException("typeName");
            }
            HasReturn = hasReturn;
            TypeName = typeName;
        }

        public IReturn ToReadOnly()
        {
            if(string.IsNullOrWhiteSpace(TypeName))
            {
                throw new InvalidOperationException("Type full name not set.");
            }
            _readOnly.Apply();
            return this;
        }
    }
}
