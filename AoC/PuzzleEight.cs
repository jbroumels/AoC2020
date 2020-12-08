using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC
{
    public class PuzzleEight : Puzzle
    {
        public PuzzleEight() : base("08")
        {

        }

        public override object GetAnswerOne()
        {
            Tuple<bool, int> result = Run(Lines);

            return result.Item2;
        }

        public override object GetAnswerTwo()
        {
            for (int x = 0; x < Lines.Length; x++)
            {
                //change the line
                string[] adjustedLines = Lines.Select(it => it).ToArray();

                if (adjustedLines[x].StartsWith("nop"))
                {
                    adjustedLines[x] = adjustedLines[x].Replace("nop", "jmp");
                }
                else if (adjustedLines[x].StartsWith("jmp"))
                {
                    adjustedLines[x] = adjustedLines[x].Replace("jmp", "nop");
                }
                Tuple<bool, int> result = Run(adjustedLines);

                if (result.Item1 == false)
                {
                    return result.Item2;
                }
            }

            return base.GetAnswerTwo();
        }

        private static Tuple<bool, int> Run(string[] lines)
        {
            HashSet<int> visitedPos = new HashSet<int>();
            int pos = 0;
            int acc = 0;

            do
            {
                visitedPos.Add(pos);

                string currentLine = lines[pos];

                string instruction = currentLine.Substring(0, 3);
                string p = currentLine.Substring(4);
                if (instruction == "nop")
                {
                    pos++;
                }
                else if (instruction == "acc")
                {
                    acc += Convert.ToInt32(p);

                    pos++;
                }
                else if (instruction == "jmp")
                {
                    pos += Convert.ToInt32(p);
                }

                if (pos == lines.Length)
                {
                    return new Tuple<bool, int>(false, acc);

                }
            } while (!visitedPos.Contains(pos));

            return new Tuple<bool, int>(true, acc);

        }
    }
}