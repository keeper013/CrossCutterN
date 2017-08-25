// <copyright file="IWeavingRecord.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Statistics
{
    using CrossCutterN.Aspect.Aspect;

    /// <summary>
    /// Detailed weaving record for a join point.
    /// </summary>
    public interface IWeavingRecord
    {
        /// <summary>
        /// Gets join point.
        /// </summary>
        JoinPoint JoinPoint { get; }

        /// <summary>
        /// Gets full name of the method that is weaved.
        /// </summary>
        string MethodFullName { get; }

        /// <summary>
        /// Gets signature of the method that is weaved.
        /// </summary>
        string MethodSignature { get; }

        /// <summary>
        /// Gets sequence of the join point.
        /// </summary>
        int Sequence { get; }

        /// <summary>
        /// Gets name of the aspect, which is also key of the aspect.
        /// </summary>
        string AAspectName { get; }
    }
}
