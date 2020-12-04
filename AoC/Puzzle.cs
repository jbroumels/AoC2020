using System.IO;

namespace AoC
{
    public abstract class Puzzle
    {
        protected readonly string[] Lines;

        public string PuzzleNumber { get; private set; }

        public Puzzle(string puzzleNumber)
        {
            PuzzleNumber = puzzleNumber;
            Lines = File.ReadAllLines($"{puzzleNumber}.txt");
        }

        public virtual object GetAnswerOne()
        {
            return "?";
        }

        public virtual object GetAnswerTwo()
        {
            return "?";
        }
    }
}