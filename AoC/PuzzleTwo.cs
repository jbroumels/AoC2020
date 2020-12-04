using System;
using System.Linq;

namespace AoC
{
    public class PuzzleTwo : Puzzle
    {
        public PuzzleTwo() : base("02")
        {

        }

        public override object GetAnswerOne()
        {
            PasswordLine[] passwordLines = Lines.Select(it => new PasswordLine(it)).ToArray();

            return passwordLines.Count(it => it.IsValid());
        }

        public override object GetAnswerTwo()
        {
            PasswordLine[] passwordLines = Lines.Select(it => new PasswordLine(it)).ToArray();

            return passwordLines.Count(it => it.IsValid2());
        }

        public class PasswordLine
        {
            private int minOccur;
            private int maxOccur;
            private char letter;
            private string passwordValue;

            public PasswordLine(string line)
            {
                string[] split = line.Split(new[] {':'}).Select(it => it.Trim()).ToArray();

                string rule = split[0];

                string minMax = rule.Substring(0, rule.Length - 1);

                letter = rule.Last();

                minOccur = Convert.ToInt32(minMax.Split(new[] { '-' })[0]);
                maxOccur = Convert.ToInt32(minMax.Split(new[] { '-' })[1]);

                passwordValue = split[1];
            }

            public bool IsValid()
            {
                int occurancesOfLetter = passwordValue.Count(it => it == letter);

                return maxOccur >= occurancesOfLetter && minOccur <= occurancesOfLetter;
            }


            public bool IsValid2()
            {
                bool first = passwordValue.Length >= minOccur && passwordValue[minOccur - 1] == letter;
                bool second = passwordValue.Length >= maxOccur && passwordValue[maxOccur - 1] == letter;

                return first ^ second;
            }
        }
    }
}