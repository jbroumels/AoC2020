using System.IO;

namespace AoC
{
    public abstract class Puzzle
    {
        protected readonly string[] Lines;

        public Puzzle(string puzzleNumber)
        {
            Lines = File.ReadAllLines($"{puzzleNumber}.txt");
        }

        public virtual string GetAnswerOne()
        {
            return "?";
        }

        public virtual string GetAnswerTwo()
        {
            return "?";
        }
    }
}