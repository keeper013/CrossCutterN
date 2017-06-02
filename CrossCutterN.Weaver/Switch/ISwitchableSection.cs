/**
 * Description: Switchable section interface
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Switch
{
    using System.Collections.Generic;
    using Mono.Cecil.Cil;
    using Utilities;

    internal interface ISwitchableSection : ICanReset
    {
        bool CanSetInstructions { get; }
        bool HasContent { get; }
        int StartIndex { set; }
        int EndIndex { set; }

        Instruction StartInstruction { get; }
        Instruction EndInstruction { get; }

        void SetInstructions(IList<Instruction> instructions, Instruction defaultInstruction);
        void AdjustEndInstruction(Instruction originalEnd, Instruction newEnd);
    }
}
