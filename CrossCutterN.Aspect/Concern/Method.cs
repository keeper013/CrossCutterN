/**
 * Description: method execution information implementation
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Concern
{
    using System;
    using System.Collections.Generic;
    using Advice.Common;

    internal sealed class Method : IMethod, IWriteOnlyMethod
    {
        private readonly StringKeyIntSortKeyReadOnlyCollectionLookup<IParameter> _parameters =
            new StringKeyIntSortKeyReadOnlyCollectionLookup<IParameter>();
        private readonly List<ICustomAttribute> _customAttributes = new List<ICustomAttribute>();
        private IReadOnlyCollection<ICustomAttribute> _classCustomAttributes;
        private readonly IrreversibleOperation _readOnly = new IrreversibleOperation();

        public string AssemblyFullName { get; private set; }
        public string Namespace { get; private set; }
        public string ClassFullName { get; private set; }
        public string ClassName { get; private set; }
        public string MethodFullName { get; private set; }
        public string MethodName { get; private set; }
        public string ReturnType { get; private set; }
        public bool IsInstance { get; private set; }
        public bool IsConstructor { get; private set; }
        public Accessibility Accessibility { get; private set; }

        public IReadOnlyCollection<ICustomAttribute> ClassCustomAttributes
        {
            get
            {
                _readOnly.Assert(true);
                return _classCustomAttributes;
            }
            set
            {
                _readOnly.Assert(false);
                _classCustomAttributes = value;
            }
        }

        public IReadOnlyCollection<IParameter> Parameters
        {
            get
            {
                _readOnly.Assert(true);
                return _parameters.GetAll();
            }
        }

        public IReadOnlyCollection<ICustomAttribute> CustomAttributes
        {
            get
            {
                _readOnly.Assert(true);
                return _customAttributes.AsReadOnly();
            }
        }

        internal Method(string assemblyFullName, string nameSpace, string classFullName, 
            string className, string methodFullName, string methodName, string returnType,
            bool isInstance, bool isConstructor, Accessibility accessibility)
        {
            if(string.IsNullOrWhiteSpace(assemblyFullName))
            {
                throw new ArgumentNullException("assemblyFullName");
            }
            if(string.IsNullOrWhiteSpace(nameSpace))
            {
                throw new ArgumentNullException("nameSpace");
            }
            if (string.IsNullOrWhiteSpace("classFullName"))
            {
                throw new ArgumentNullException("classFullName");
            }
            if(string.IsNullOrWhiteSpace(className))
            {
                throw new ArgumentNullException("className");
            }
            if(string.IsNullOrWhiteSpace(methodFullName))
            {
                throw new ArgumentNullException("methodFullName");
            }
            if(string.IsNullOrWhiteSpace(methodName))
            {
                throw new ArgumentNullException("methodName");
            }
            if(string.IsNullOrWhiteSpace(returnType))
            {
                throw new ArgumentNullException("returnType");
            }
            AssemblyFullName = assemblyFullName;
            Namespace = nameSpace;
            ClassFullName = classFullName;
            ClassName = className;
            MethodFullName = methodFullName;
            MethodName = methodName;
            ReturnType = returnType;
            IsConstructor = isConstructor;
            IsInstance = isInstance;
            Accessibility = accessibility;
        }

        public void AddParameter(IParameter parameter)
        {
            _readOnly.Assert(false);
            _parameters.Add(parameter);
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

        public IParameter GetParameter(string name)
        {
            _readOnly.Assert(true);
            return _parameters.Get(name);
        }

        public bool HasParameter(string name)
        {
            _readOnly.Assert(true);
            return _parameters.Has(name);
        }

        public IMethod ToReadOnly()
        {
            _readOnly.Apply();
            return this;
        }
    }

}
