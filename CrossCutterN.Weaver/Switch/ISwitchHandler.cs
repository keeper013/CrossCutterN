// <copyright file="ISwitchHandler.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Switch
{
    using System.Collections.Generic;

    /// <summary>
    /// Switch handler for a class.
    /// </summary>
    internal interface ISwitchHandler
    {
        /// <summary>
        /// Gets all necessary data to initialize all switches in a weaved class.
        /// </summary>
        /// <returns>All necessary initialization data.</returns>
        IEnumerable<SwitchInitializingData> GetData();
    }
}
