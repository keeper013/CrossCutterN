/**
* Description: Advice switch status
* Author: David Cui
*/

using System;

namespace CrossCutterN.Advice.Switch
{
    internal enum SwitchStatus
    {
        Switched,
        On,
        Off
    }

    internal sealed class SwitchOperation
    {
        private readonly SequenceGenerator _sequenceGenerator;
        public int Sequence { get; private set; }
        public SwitchStatus Status { get; private set; }

        public SwitchOperation(SequenceGenerator sequenceGenerator, SwitchStatus status)
        {
#if DEBUG
            if (sequenceGenerator == null)
            {
                throw new ArgumentNullException("sequenceGenerator");
            }
#endif
            _sequenceGenerator = sequenceGenerator;
            Sequence = sequenceGenerator.Next();
            Status = status;
        }

        public bool Switch(SwitchStatus status)
        {
            switch (status)
            {
                case SwitchStatus.On:
                case SwitchStatus.Off:
                    Sequence = _sequenceGenerator.Next();
                    Status = status;
                    break;
                case SwitchStatus.Switched:
                    switch (Status)
                    {
                        case SwitchStatus.On:
                            Sequence = _sequenceGenerator.Next();
                            Status = SwitchStatus.Off;
                            break;
                        case SwitchStatus.Off:
                            Sequence = _sequenceGenerator.Next();
                            Status = SwitchStatus.On;
                            break;
                        case SwitchStatus.Switched:
                            return true;
                    }
                    break;
            }
            return false;
        }

        public bool Switch(bool value)
        {
            switch (Status)
            {
                case SwitchStatus.Switched:
                    return !value;
                case SwitchStatus.On:
                    return true;
                case SwitchStatus.Off:
                    return false;
            }
            throw new Exception(string.Format("Invalid status {0} detected", Status));
        }
    }
}
