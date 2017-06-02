/**
 * Description: Switchable section
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Switch
{
    using System;
    using System.Collections.Generic;
    using Mono.Cecil.Cil;

    internal sealed class SwitchableSection : ISwitchableSection
    {
        public int StartIndex { set; private get; }
        public int EndIndex { set; private get; }
        public Instruction StartInstruction { get; private set; }
        public Instruction EndInstruction { get; private set; }

        public bool CanSetInstructions
        {
            get { return StartIndex >= 0 && EndIndex >= 0; }
        }

        public bool HasContent
        {
            get { return StartInstruction != null && EndInstruction != null; }
        }

        public void Reset()
        {
            StartIndex = -1;
            EndIndex = -1;
            StartInstruction = null;
            EndInstruction = null;
        }

        public void SetInstructions(IList<Instruction> instructions, Instruction defaultInstruction)
        {
            if (StartIndex == -1)
            {
                return;
            }
            if (instructions == null)
            {
                throw new ArgumentNullException("instructions");
            }
            if (defaultInstruction == null)
            {
                throw new ArgumentNullException("defaultInstruction");
            }
            if (EndIndex == -1)
            {
                throw new InvalidOperationException("End index not set yet");
            }
            if (EndIndex <= StartIndex)
            {
                throw new InvalidOperationException("End index should be above start index");
            }
            if (StartIndex >= instructions.Count)
            {
                throw new ArgumentException("Instruction list not correctly populated");
            }
            StartInstruction = instructions[StartIndex];
            EndInstruction = EndIndex >= instructions.Count ? defaultInstruction : instructions[EndIndex];
            StartIndex = -1;
            EndIndex = -1;
        }

        public void AdjustEndInstruction(Instruction originalEnd, Instruction newEnd)
        {
            if(newEnd == null)
            {
                throw new ArgumentNullException("newEnd");
            }
            if(EndInstruction != null && EndInstruction == originalEnd)
            {
                EndInstruction = newEnd;
            }
        }
    }
}
