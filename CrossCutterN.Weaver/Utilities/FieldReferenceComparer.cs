// <copyright file="FieldReferenceComparer.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Utilities
{
    using System.Collections.Generic;
    using Mono.Cecil;

    /// <summary>
    /// Comparer utility for field reference.
    /// </summary>
    internal sealed class FieldReferenceComparer : IEqualityComparer<FieldReference>
    {
        /// <inheritdoc/>
        public bool Equals(FieldReference x, FieldReference y) => x.Name.Equals(y.Name);

        /// <inheritdoc/>
        public int GetHashCode(FieldReference obj) => obj.Name.GetHashCode();
    }
}
