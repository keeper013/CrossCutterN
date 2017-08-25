// <copyright file="SwitchFactory.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Switch
{
    using System.Collections.Generic;
    using CrossCutterN.Base.MultiThreading;

    /// <summary>
    /// Switch factory.
    /// </summary>
    internal static class SwitchFactory
    {
        /// <summary>
        /// Initializes a new instance of of <see cref="IClassAspectSwitchBuilder"/>.
        /// </summary>
        /// <param name="switchList">Switch list to be indexed on.</param>
        /// <param name="lck">Lock for the switch list.</param>
        /// <returns>The initialized <see cref="IClassAspectSwitchBuilder"/>.</returns>
        public static IClassAspectSwitchBuilder InitializeClassAspectSwitch(IList<bool> switchList, ISmartReadWriteLock lck)
        {
            return new ClassAspectSwitch(switchList, lck);
        }

        /// <summary>
        /// Initializes a new instance of of <see cref="IClassAspectSwitchOperation"/>.
        /// </summary>
        /// <param name="sequenceGenerator">Sequence generator used to generate sequence number.</param>
        /// <param name="aspectOperations">Operation records of aspects.</param>
        /// <returns>The initialized <see cref="IClassAspectSwitchOperation"/>.</returns>
        public static IClassAspectSwitchOperation InitializeClassAspectSwitchOperation(
            SequenceGenerator sequenceGenerator,
            IReadOnlyDictionary<string, SwitchOperationStatus> aspectOperations)
        {
            return new ClassAspectSwitchOperation(sequenceGenerator, aspectOperations);
        }

        /// <summary>
        /// Initializes a new instance of of <see cref="SwitchOperationStatus"/>. Switch operation status will be calculated based on Default status and operation.
        /// </summary>
        /// <param name="sequenceGenerator">Sequence generator used to generate sequence number.</param>
        /// <param name="operation">Switch operation status.</param>
        /// <returns>The initialized <see cref="SwitchOperationStatus"/></returns>
        public static SwitchOperationStatus InitializeSwitchOperationStatus(SequenceGenerator sequenceGenerator, SwitchOperationStatus operation)
        {
            return new SwitchOperationStatus(sequenceGenerator, operation);
        }

        /// <summary>
        /// Initializes a new instance of of <see cref="SwitchOperationStatus"/>.
        /// </summary>
        /// <param name="sequenceGenerator">Sequence generator used to generate sequence number.</param>
        /// <param name="operation">Switch operation.</param>
        /// <returns>The initialized <see cref="SwitchOperationStatus"/></returns>
        public static SwitchOperationStatus InitializeSwitchOperationStatus(SequenceGenerator sequenceGenerator, SwitchOperation operation)
        {
            return new SwitchOperationStatus(sequenceGenerator, operation);
        }

        /// <summary>
        /// Initializes a new instance of of <see cref="AspectSwitch"/>.
        /// </summary>
        /// <returns>The initialized <see cref="AspectSwitch"/>.</returns>
        public static AspectSwitch InitializeAspectSwitch()
        {
            return new AspectSwitch();
        }
    }
}
