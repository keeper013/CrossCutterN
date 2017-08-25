// <copyright file="ReferenceBase.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using CrossCutterN.Base.Common;
    using Mono.Cecil;

    /// <summary>
    /// Base class for references.
    /// </summary>
    internal abstract class ReferenceBase
    {
        private readonly ModuleDefinition module;
        private readonly IrreversibleOperation readOnly = new IrreversibleOperation();
        private readonly List<MethodReference> methods = new List<MethodReference>();
        private readonly List<TypeReference> types = new List<TypeReference>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceBase"/> class.
        /// </summary>
        /// <param name="module">The module that this reference is declared for.</param>
        protected ReferenceBase(ModuleDefinition module) => this.module = module ?? throw new ArgumentNullException("module");

        /// <summary>
        /// Gets type reference according to key.
        /// </summary>
        /// <param name="internalId">Internal Id of the type..</param>
        /// <returns>The type reference retrieved.</returns>
        protected TypeReference GetType(int internalId)
        {
#if DEBUG
            if (internalId < 0 || types.Count <= internalId)
            {
                throw new ArgumentOutOfRangeException("internalId");
            }
#endif
            readOnly.Assert(true);

            return types[internalId];
        }

        /// <summary>
        /// Sets type reference and its key.
        /// </summary>
        /// <param name="type">The type reference.</param>
        /// <returns>Internal id of the added type reference.</returns>
        protected int AddType(Type type)
        {
#if DEBUG
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
#endif
            readOnly.Assert(false);

            types.Add(module.ImportReference(type));
            return types.Count - 1;
        }

        /// <summary>
        /// Gets method reference according to key.
        /// </summary>
        /// <param name="internalId">Internal id of the method.</param>
        /// <returns>The method reference.</returns>
        protected MethodReference GetMethod(int internalId)
        {
#if DEBUG
            if (internalId < 0 || methods.Count <= internalId)
            {
                throw new ArgumentOutOfRangeException("internalId");
            }
#endif
            readOnly.Assert(true);

            return methods[internalId];
        }

        /// <summary>
        /// Adds a method reference.
        /// </summary>
        /// <param name="method">The method reference.</param>
        /// <returns>Internal Id of the added method reference.</returns>
        protected int AddMethod(MethodInfo method)
        {
#if DEBUG
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }
#endif
            readOnly.Assert(false);

            methods.Add(module.ImportReference(method));
            return methods.Count - 1;
        }

        /// <summary>
        /// Complete adding operations. After calling this method, no adding method or type can be done.
        /// </summary>
        protected void CompleteAdding() => readOnly.Apply();
    }
}
