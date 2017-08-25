// <copyright file="OverwriteTestClassPropertyConcerned.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.OverwriteTest
{
    /// <summary>
    /// Test target for overwrite test that has attribute properties hard coded.
    /// </summary>
    [OverwriteConcernClass(ConcernPropertyGetter = true, ConcernMethod = false, ConcernInternal = true)]
    internal sealed class OverwriteTestClassPropertyConcerned
    {
        /// <summary>
        /// Gets or sets some internal property.
        /// </summary>
        internal int InernalProperty { get; set; }

        /// <summary>
        /// Some not concerned method.
        /// </summary>
        public void NotConcernedMethod()
        {
        }
    }
}
