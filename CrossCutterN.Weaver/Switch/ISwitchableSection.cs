// <copyright file="ISwitchableSection.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Switch
{
    using System.Collections.Generic;
    using CrossCutterN.Weaver.Utilities;
    using Mono.Cecil.Cil;

    /// <summary>
    /// Switchable section record for weaving process.
    /// Introducing this approach to apply switching off some local variable initializations which may take too much time.
    /// Like <see cref="CrossCutterN.Base.Metadata.IExecution"/>, <see cref="CrossCutterN.Base.Metadata.IExecutionContext"/>, <see cref="CrossCutterN.Base.Metadata.IReturn"/>
    /// </summary>
    internal interface ISwitchableSection : IResetable
    {
        /// <summary>
        /// Gets a value indicating whether the switchable section has set start and end instructions.
        /// </summary>
        bool HasSetStartEndInstruction { get; }

        /// <summary>
        /// Sets index of start instruction for this section in an instruction list.
        /// </summary>
        int StartIndex { set; }

        /// <summary>
        /// Sets index of end instruction for this section in an instruction list.
        /// </summary>
        int EndIndex { set; }

        /// <summary>
        /// Gets the value of the section length, which matters when deciding to use br_s or br
        /// </summary>
        int SectionLength { get; }

        /// <summary>
        /// Gets start instruction of this switchable section.
        /// </summary>
        Instruction StartInstruction { get; }

        /// <summary>
        /// Gets end instruction of this switchable section.
        /// </summary>
        Instruction EndInstruction { get; }

        /// <summary>
        /// Sets start and end instructions for this section according to instruction list and default end instruction.
        /// </summary>
        /// <param name="instructions">The instruction list that includes instructions of this section.</param>
        /// <param name="defaultEndInstruction">default ending instruction in case the ending instruction is not included in the instruction list.</param>
        void SetInstructions(IList<Instruction> instructions, Instruction defaultEndInstruction);

        /// <summary>
        /// Adjusts end instruction to a new one, if the current ending instruction is the same with originalEnd.
        /// </summary>
        /// <param name="originalEnd">Original ending instruction</param>
        /// <param name="newEnd">new ending instruction.</param>
        void AdjustEndInstruction(Instruction originalEnd, Instruction newEnd);
    }
}
