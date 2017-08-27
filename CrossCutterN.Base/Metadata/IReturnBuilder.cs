// <copyright file="IReturnBuilder.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Metadata
{
    using CrossCutterN.Base.Common;

    /// <summary>
    /// Return value metadata being built.
    /// </summary>
    public interface IReturnBuilder : IBuilder<IReturn>
    {
        /// <summary>
        /// Sets a value indicating whether there is a return value, if the return type is void or an uncaught exception happened during the execution, this value will be false.
        /// </summary>
        bool HasReturn { set; }

        /// <summary>
        /// Sets value of the return value.
        /// </summary>
        object Value { set; }
    }
}
