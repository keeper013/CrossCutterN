// <copyright file="WeavingRecord.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Statistics
{
    using System;
    using CrossCutterN.Aspect.Aspect;

    /// <summary>
    /// Weaving record implementation.
    /// </summary>
    internal sealed class WeavingRecord : IWeavingRecord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeavingRecord"/> class.
        /// </summary>
        /// <param name="joinPoint">Join point.</param>
        /// <param name="aspectName">Name of the aspect, also serves as key of the aspect.</param>
        /// <param name="methodFullName">Full name of the method weaved.</param>
        /// <param name="methodSignature">Signature of the method weaved.</param>
        /// <param name="sequence">Sequence of the join point.</param>
        internal WeavingRecord(JoinPoint joinPoint, string aspectName, string methodFullName, string methodSignature, int sequence)
        {
#if DEBUG
            if (string.IsNullOrWhiteSpace(aspectName))
            {
                throw new ArgumentNullException("aspectName");
            }

            if (string.IsNullOrWhiteSpace(methodFullName))
            {
                throw new ArgumentNullException("methodFullName");
            }

            if (string.IsNullOrWhiteSpace(methodSignature))
            {
                throw new ArgumentNullException("methodSignature");
            }
#endif
            JoinPoint = joinPoint;
            AAspectName = aspectName;
            MethodFullName = methodFullName;
            MethodSignature = methodSignature;
            Sequence = sequence;
        }

        /// <inheritdoc/>
        public JoinPoint JoinPoint { get; private set; }

        /// <inheritdoc/>
        public string AAspectName { get; private set; }

        /// <inheritdoc/>
        public string MethodFullName { get; private set; }

        /// <inheritdoc/>
        public string MethodSignature { get; private set; }

        /// <inheritdoc/>
        public int Sequence { get; private set; }
    }
}
