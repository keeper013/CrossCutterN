/**
* Description: Advice switch status
* Author: David Cui
*/

using System;

namespace CrossCutterN.Advice.Switch
{
    internal enum SwitchStatus
    {
        Default,
        Switched,
        On,
        Off
    }

    internal enum SwitchOperation
    {
        Switch,
        On,
        Off
    }

    internal sealed class SwitchOperationStatus
    {
        private readonly SequenceGenerator _sequenceGenerator;
        public int Sequence { get; private set; }
        public SwitchStatus Status { get; private set; }

        public SwitchOperationStatus(SequenceGenerator sequenceGenerator, SwitchOperation operation)
        {
#if DEBUG
            if (sequenceGenerator == null)
            {
                throw new ArgumentNullException("sequenceGenerator");
            }
#endif
            _sequenceGenerator = sequenceGenerator;
            Status = SwitchStatus.Default;
            Switch(operation);
        }

        public SwitchOperationStatus(SequenceGenerator sequenceGenerator, SwitchOperationStatus operation)
        {
#if DEBUG
            if (operation == null)
            {
                throw new ArgumentNullException("operation");
            }
            if (sequenceGenerator == null)
            {
                throw new ArgumentNullException("sequenceGenerator");
            }
#endif
            _sequenceGenerator = sequenceGenerator;
            Sequence = operation.Sequence;
            Status = operation.Status;
        }

        public void Switch(SwitchOperation operation)
        {
            Sequence = _sequenceGenerator.Next();
            SwitchInternalStatus(operation);
        }

        public void Switch(SwitchOperation operation, int sequence)
        {
            Sequence = sequence;
            SwitchInternalStatus(operation);
        }

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
            throw new Exception(string.Format("Invalid status {0} detected", Status));
        }

        public override int GetHashCode()
        {
            return Sequence;
        }

        public override bool Equals(object obj)
        {
            var cast = obj as SwitchOperationStatus;
            if (cast == null)
            {
                return false;
            }
            return Sequence == cast.Sequence;
        }

        private void SwitchInternalStatus(SwitchOperation operation)
        {
            Status = Switch(Status, operation);
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
            throw new ArgumentException(
                string.Format("Invalid combination of switch status: {0} and switch operation {1}", status, operation));
        }
    }
}
