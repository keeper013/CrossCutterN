// <copyright file="OverwriteTestClassNotMarked.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.OverwriteTest
{
    using System;

    /// <summary>
    /// Class not concerned, but methods and properties are concerned.
    /// </summary>
    internal sealed class OverwriteTestClassNotMarked
    {
        /// <summary>
        /// Gets or sets some internal property.
        /// </summary>
        [OverwriteConcernProperty(ConcernGetter = true, ConcernSetter = false)]
        internal int InternalProperty { get; set; }

        /// <summary>
        /// Some internal method.
        /// </summary>
        [OverwriteConcernMethod]
        internal void InternalMethodConceredByAttribute()
        {
            Console.Out.WriteLine(InternalProperty);
        }

        /// <summary>
        /// Some empty method.
        /// </summary>
        [OverwriteConcernMethod]
        internal void EntryExit()
        {
        }
    }
}
