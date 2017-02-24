using Mono.Cecil;
using Mono.Cecil.Cil;
using System;

namespace MonoMod
{
    public static class MonoModRules
    {
        public static void ReplaceString(MethodDefinition method, CustomAttribute attrib)
        {
            if (!method.HasBody)
                return;

            string from = (string)attrib.ConstructorArguments[0].Value;
            string to = (string)attrib.ConstructorArguments[1].Value;

            foreach (Instruction instr in method.Body.Instructions)
            {
                if (instr.OpCode == OpCodes.Ldstr && (string)instr.Operand == from)
                {
                    instr.Operand = to;
                }
            }
        }
    }
}
