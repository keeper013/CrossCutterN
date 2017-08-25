// <copyright file="ISwitchWeavingRecord.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Statistics
{
    /// <summary>
    /// Detailed weaving record for switches.
    /// </summary>
    public interface ISwitchWeavingRecord
    {
        /// <summary>
        /// Gets class that the switch is in.
        /// </summary>
        string Class { get; }

        /// <summary>
        /// Gets property that the switch is in.
        /// </summary>
        string Property { get; }

        /// <summary>
        /// Gets method that the switch is in.
        /// </summary>
        string MethodSignature { get; }

        /// <summary>
        /// Gets aspect that the switch belongs to.
        /// </summary>
        string Aspect { get; }

        /// <summary>
        /// Gets the static field name of the switch.
        /// </summary>
        string StaticFieldName { get; }

        /// <summary>
        /// Gets a value indicating whether the switch is on or off.
        /// </summary>
        bool Value { get; }
    }
}
