using System;
using System.Collections.Generic;
namespace Advent2020
{
    class Instruction
    {
        public string OpCode { get; set; }
        public int Argument { get; set; }
        public Instruction(string code, int arg)
        {
            OpCode = code;
            Argument = arg;
        }
    }
    public class GameConsole
    {
        private List<Instruction> instructions;
        private int pc;
        private int accumulator;
        private bool hasTerminated;
        private bool error;
        public GameConsole()
        {
            instructions = new List<Instruction>();
            Reset();
        }
        public void Reset()
        {
            pc = 0;
            accumulator = 0;
            hasTerminated = false;
            error = false;
        }
        public void ReadProgamFromFile(string filename)
        {
            foreach (string line in FileReader.ReadFileOfStrings(filename))
            {
                string[] ia = line.Split(' ');
                instructions.Add(new Instruction(ia[0], int.Parse(ia[1])));
            }
        }
        public void ExecuteInstruction()
        {
            Instruction instruction = instructions[pc];
            switch (instruction.OpCode)
            {
            case "nop":
                pc += 1;
                break;
            case "acc":
                accumulator += instruction.Argument;
                pc += 1;
                break;
            case "jmp":
                pc += instruction.Argument;
                break;
            }
        }
        public int RunUntilLoop()
        {
            HashSet<int> visited = new HashSet<int>();
            while (!visited.Contains(pc))
            {
                visited.Add(pc);
                ExecuteInstruction();
                if (pc == instructions.Count)
                {
                    hasTerminated = true;
                    return accumulator;
                }
                if (pc > instructions.Count)
                {
                    error = true;
                    return accumulator;
                }
            }
            return accumulator;
        }
        public bool Patch(int address)
        {
            switch (instructions[address].OpCode)
            {
            case "nop":
                instructions[address].OpCode = "jmp";
                return true;
            case "jmp":
                instructions[address].OpCode = "nop";
                return true;
            }
            return false;
        }
        public int RunUntilPatched()
        {
            for (int i = instructions.Count - 1; i >= 0; i--)
            {
                if (Patch(i))
                {
                    Reset();
                    int acc = RunUntilLoop();
                    if (hasTerminated)
                    {
                        return acc;
                    }
                    Patch(i);
                }
            }
            return 0;
        }
    }
}