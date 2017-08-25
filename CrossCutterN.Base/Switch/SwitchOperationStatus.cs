// <copyright file="SwitchOperationStatus.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Base.Switch
{
    using System;

    /// <summary>
    /// Status of an aspect switch.
    /// </summary>
    internal enum SwitchStatus
    {
        /// <summary>
        /// Default value according to configuration, can be On or Off.
        /// </summary>
        Default,

        /// <summary>
        /// Switched from default value, if default value is on, then Switched means Off, and vise versa.
        /// </summary>
        Switched,

        /// <summary>
        /// Switched on.
        /// </summary>
        On,

        /// <summary>
        /// Switched off.
        /// </summary>
        Off,
    }

    /// <summary>
    /// Switch operation type against <see cref="SwitchStatus"/>.
    /// </summary>
    internal enum SwitchOperation
    {
        /// <summary>
        /// Switch operation, change Default to Switched, Off to On and On to Off.
        /// </summary>
        Switch,

        /// <summary>
        /// Switch on.
        /// </summary>
        On,

        /// <summary>
        /// Switch off.
        /// </summary>
        Off,
    }

    /// <summary>
    /// Status of an aspect switch.
    /// </summary>
    internal sealed class SwitchOperationStatus
    {
        private readonly SequenceGenerator sequenceGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="SwitchOperationStatus"/> class.
        /// </summary>
        /// <param name="sequenceGenerator">Sequence number generator used to generate operation sequence number in case it is not provided.</param>
        /// <param name="operation">Operation</param>
        public SwitchOperationStatus(SequenceGenerator sequenceGenerator, SwitchOperation operation)
        {
            this.sequenceGenerator = sequenceGenerator ?? throw new ArgumentNullException("sequenceGenerator");
            Status = SwitchStatus.Default;
            Switch(operation);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SwitchOperationStatus"/> class.
        /// </summary>
        /// <param name="sequenceGenerator">Sequence number generator used to generate operation sequence number in case it is not provided.</param>
        /// <param name="operation">Operation</param>
        public SwitchOperationStatus(SequenceGenerator sequenceGenerator, SwitchOperationStatus operation)
        {
#if DEBUG
            if (operation == null)
            {
                throw new ArgumentNullException("operation");
            }
#endif
            this.sequenceGenerator = sequenceGenerator ?? throw new ArgumentNullException("sequenceGenerator");
            Sequence = operation.Sequence;
            Status = operation.Status;
        }

        /// <summary>
        /// Gets operation sequence.
        /// </summary>
        public int Sequence { get; private set; }

        /// <summary>
        /// Gets switch status.
        /// </summary>
        public SwitchStatus Status { get; private set; }

        /// <summary>
        /// Applies switch operation to this switch status.
        /// </summary>
        /// <param name="operation">Operation to be applied.</param>
        public void Switch(SwitchOperation operation)
        {
            Sequence = sequenceGenerator.Next;
            SwitchInternalStatus(operation);
        }

        /// <summary>
        /// Applies switch operation to this switch status.
        /// </summary>
        /// <param name="operation">Operation to be applied.</param>
        /// <param name="sequence">Operation sequence number.</param>
        public void Switch(SwitchOperation operation, int sequence)
        {
            Sequence = sequence;
            SwitchInternalStatus(operation);
        }

        /// <summary>
        /// Gets aspect switch status based on internal switch status record.
        /// </summary>
        /// <param name="value">Original switch value.</param>
        /// <returns>Switched switch value.</returns>
        public bool Switch(bool value)
        {
            switch (Status)
            {
                case SwitchStatus.Default:
                    return value;
                case SwitchStatus.Switched:
                    return !value;
                case SwitchStatus.On:
                    return true;
                case SwitchStatus.Off:
                    return false;
            }

            throw new Exception($"Invalid status {Status} detected");
        }

        /// <inheritdoc/>
        public override int GetHashCode() => Sequence;

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            var cast = obj as SwitchOperationStatus;
            if (cast == null)
            {
                return false;
            }

            return Sequence == cast.Sequence;
        }

        private static SwitchStatus Switch(SwitchStatus status, SwitchOperation operation)
        {
            switch (operation)
            {
                case SwitchOperation.On:
                    return SwitchStatus.On;
                case SwitchOperation.Off:
                    return SwitchStatus.Off;
                case SwitchOperation.Switch:
                    switch (status)
                    {
                        case SwitchStatus.On:
                            return SwitchStatus.Off;
                        case SwitchStatus.Off:
                            return SwitchStatus.On;
                        case SwitchStatus.Switched:
                            return SwitchStatus.Default;
                        case SwitchStatus.Default:
                            return SwitchStatus.Switched;
                    }

                    break;
            }

            throw new ArgumentException($"Invalid combination of switch status: {status} and switch operation {operation}");
        }

        private void SwitchInternalStatus(SwitchOperation operation) => Status = Switch(Status, operation);
    }
}
