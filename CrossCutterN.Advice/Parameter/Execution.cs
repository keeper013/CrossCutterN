/**
 * Description: method execution information implementation
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Parameter
{
    using System;
    using System.Collections.Generic;
    using Common;

    internal sealed class MethodExecution : IExecution, ICanAddParameter
    {
        private readonly StringKeyIntSortKeyReadOnlyCollectionLookup<IParameter> _parameters =
            new StringKeyIntSortKeyReadOnlyCollectionLookup<IParameter>();
        private readonly IrreversibleOperation _readOnly = new IrreversibleOperation();

        public string AssemblyFullName { get; private set; }
        public string Namespace { get; private set; }
        public string ClassFullName { get; private set; }
        public string ClassName { get; private set; }
        public string FullName { get; private set; }
        public string Name { get; private set; }
        public string ReturnType { get; private set; }

        public IReadOnlyCollection<IParameter> Parameters
        {
            get
            {
                return _parameters.GetAll();
            }
        }

        internal MethodExecution(string assemblyFullName, string nameSpace, string classFullName, 
            string className, string fullName, string name, string returnType)
        {
#if DEBUG
            // the code will be called in client assembly, so reducing unnecessary validations for performance consideration
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
            if(string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentNullException("fullName");
            }
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }
            if(string.IsNullOrWhiteSpace(returnType))
            {
                throw new ArgumentNullException("returnType");
            }
#endif
            AssemblyFullName = assemblyFullName;
            Namespace = nameSpace;
            ClassFullName = classFullName;
            ClassName = className;
            FullName = fullName;
            Name = name;
            ReturnType = returnType;
        }

        public void AddParameter(IParameter parameter)
        {
            _readOnly.Assert(false);
            _parameters.Add(parameter);
        }

        public IParameter GetParameter(string name)
        {
            return _parameters.Get(name);
        }

        public bool HasParameter(string name)
        {
            return _parameters.Has(name);
        }

        public IExecution Convert()
        {
            _readOnly.Apply();
            return this;
        }
    }

}
