// <copyright file="IrreversibleOperation.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Common
{
    using System;

    /// <summary>
    /// Irreversible operation class, used as utility.
    /// </summary>
    public sealed class IrreversibleOperation
    {
        private bool applied;

        /// <summary>
        /// Initializes a new instance of the <see cref="IrreversibleOperation"/> class.
        /// </summary>
        public IrreversibleOperation()
        {
            applied = false;
        }

        /// <summary>
        /// Checks if irreversible operation has been applied, throws exception if input expected applied status isn't the same with internal applied status.
        /// </summary>
        /// <param name="applied">Expected applied status.</param>
        public void Assert(bool applied)
        {
            if (this.applied != applied)
            {
                throw new InvalidOperationException($"Irreversible operation validation failed, expecting {this.applied}, got {applied}");
            }
        }

        /// <summary>
        /// Applies the irreversible operation.
        /// </summary>
        public void Apply()
        {
            Assert(false);
            applied = true;
        }
    }
}
