// <copyright file="IReturn.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Metadata
{
    /// <summary>
    /// Return value metadata interface.
    /// </summary>
    public interface IReturn
    {
        /// <summary>
        /// Gets a value indicating whether there is a return value, if the return type is void or an uncaught exception happened during the execution, this value will be false.
        /// </summary>
        bool HasReturn { get; }

        /// <summary>
        /// Gets type name of the return value.
        /// </summary>
        string TypeName { get; }

        /// <summary>
        /// Gets value of the return value.
        /// </summary>
        object Value { get; }
    }
}
