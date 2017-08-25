// <copyright file="OverwriteTestTarget.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.OverwriteTest
{
    using System;

    /// <summary>
    /// General overwrite test target.
    /// </summary>
    [OverwriteConcernClass]
    internal sealed class OverwriteTestTarget
    {
        private static int value;

        /// <summary>
        /// Initializes a new instance of the <see cref="OverwriteTestTarget"/> class.
        /// </summary>
        internal OverwriteTestTarget()
        {
            OverwriteByPropertyConcern = 1;
        }

        /// <summary>
        /// Gets or sets property to be weaved for concern method attribute though by default should be weaved by concern class attribute on the class already.
        /// </summary>
        [OverwriteConcernProperty(ConcernGetter = true, ConcernSetter = true)]
        private static int OverwriteByPropertyConcern
        {
            get => value;

            [OverwriteNoConcern]
            set { OverwriteTestTarget.value = value; }
        }

        /// <summary>
        /// Method to be weaved for concern method attribute though by default should be weaved by concern class attribute on the class already.
        /// </summary>
        [OverwriteConcernMethod]
        public static void OverwriteByMethodConcern()
        {
            Console.Out.WriteLine(OverwriteByPropertyConcern);
        }

        /// <summary>
        /// Method not weaved for no concern attribute.
        /// </summary>
        [OverwriteNoConcern]
        public void NoConcernMethod()
        {
            Console.Out.WriteLine("1");
        }

        /// <summary>
        /// Concerned method which throws exceptions.
        /// </summary>
        [OverwriteConcernMethod]
        public void ThrowException()
        {
            throw new Exception();
        }
    }
}
