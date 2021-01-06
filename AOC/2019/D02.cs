using System;
using System.IO;
using System.Linq;

namespace AOC._2019
{
    class D02
    {
        static readonly string input = File.ReadAllText("2019/D02.txt");

        enum Opcode
        {
            ADD = 1,
            MULT = 2,
            HALT = 99,
        }

        public static void Main()
        {
            var mem = input.Split(',').Select(x => Convert.ToInt32(x)).ToArray();

            // copy original for restoring
            var memCopy = new int[mem.Length];
            mem.CopyTo(memCopy, 0);
            void RestoreMem()
            {
                memCopy.CopyTo(mem, 0); ;
            }

            // part 1
            // restore 1202 state
            mem[1] = 12;
            mem[2] = 2;
            Execute(mem);
            Console.WriteLine($"Value at position 0 after execution halts (part 1) = {mem[0]}");

            // part 2
            int? noun = null;
            int? verb = null;
            for (int i = 0; i < 100; i++)
                for (int j = 0; j < 100; j++)
                {
                    RestoreMem();
                    mem[1] = i;
                    mem[2] = j;
                    Execute(mem);
                    if (mem[0] == 19690720)
                    {
                        noun = i;
                        verb = j;
                        goto LoopEnd;
                    }
                }
            LoopEnd:
            if (noun != null && verb != null)
                Console.WriteLine($"Result of 100 * noun + verb = {100 * noun + verb}");
            else
                Console.WriteLine($"Error: Not found");
        }

        static void Execute(int[] mem)
        {
            Opcode? opcode;
            var ip = 0;
            do
            {
                opcode = (Opcode)mem[ip];
                var in1Addr = mem[ip + 1];
                var in2Addr = mem[ip + 2];
                var outAddr = mem[ip + 3];
                switch (opcode)
                {
                    case Opcode.ADD:
                        mem[outAddr] = mem[in1Addr] + mem[in2Addr];
                        break;
                    case Opcode.MULT:
                        mem[outAddr] = mem[in1Addr] * mem[in2Addr];
                        break;
                    case Opcode.HALT:
                        continue;
                    default:
                        throw new Exception("invalid opcode");
                }
                ip += 4;
            } while (opcode != Opcode.HALT);
        }
    }
}
