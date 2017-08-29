// <copyright file="SwitchableSection.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Switch
{
    using System;
    using System.Collections.Generic;
    using Mono.Cecil.Cil;

    /// <summary>
    /// Switchable section implementation.
    /// </summary>
    internal sealed class SwitchableSection : ISwitchableSection
    {
        /// <inheritdoc/>
        public int StartIndex { private get; set; }

        /// <inheritdoc/>
        public int EndIndex { private get; set; }

        /// <inheritdoc/>
        public int SectionLength { get; private set; }

        /// <inheritdoc/>
        public Instruction StartInstruction { get; private set; }

        /// <inheritdoc/>
        public Instruction EndInstruction { get; private set; }

        /// <inheritdoc/>
        public bool HasSetStartEndInstruction => StartInstruction != null && EndInstruction != null;

        /// <inheritdoc/>
        public void SetInstructions(IList<Instruction> instructions, Instruction defaultInstruction)
        {
            if (StartIndex == -1)
            {
                return;
            }
#if DEBUG
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
#endif
            StartInstruction = instructions[StartIndex];
            EndInstruction = EndIndex >= instructions.Count ? defaultInstruction : instructions[EndIndex];
            SectionLength = EndIndex - StartIndex;
            StartIndex = -1;
            EndIndex = -1;
        }

        /// <inheritdoc/>
        public void Reset()
        {
            StartIndex = -1;
            EndIndex = -1;
            StartInstruction = null;
            EndInstruction = null;
        }

        /// <inheritdoc/>
        public void AdjustEndInstruction(Instruction originalEnd, Instruction newEnd)
        {
#if DEBUG
            if (newEnd == null)
            {
                throw new ArgumentNullException("newEnd");
            }
#endif
            if (EndInstruction != null && EndInstruction == originalEnd)
            {
                EndInstruction = newEnd;
            }
        }
    }
}
