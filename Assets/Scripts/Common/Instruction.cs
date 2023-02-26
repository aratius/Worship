using System.Collections.Generic;

namespace Worship.Common
{

    public enum Instruction
    {
        HandsUp,
        HandsDown,
        StandUp,
        SitDown,
        Tutorial,
        Finish,
        Stop
    }

    public static class InstructionUtils
    {

        public static Dictionary<Instruction, float> instructionDurationTable
        {
            get
            {
                Dictionary<Instruction, float> table = new Dictionary<Instruction, float>();
                table.Add(Instruction.HandsUp, 1f);
                table.Add(Instruction.HandsDown, 1f);
                table.Add(Instruction.StandUp, 1f);
                table.Add(Instruction.SitDown, 1f);
                table.Add(Instruction.Tutorial, 120f);
                table.Add(Instruction.Finish, 20f);
                table.Add(Instruction.Stop, 1f);
                return table;
            }
        }
    }

}