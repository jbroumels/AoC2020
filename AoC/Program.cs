using System;

namespace AoC
{
    class Program
    {
        static void Main(string[] args)
        {
            //Puzzle puzzle = new PuzzleOne();
            //Puzzle puzzle = new PuzzleTwo();
            //Puzzle puzzle = new PuzzleThree();
            Puzzle puzzle = new PuzzleFour();

            Console.WriteLine("1: " + puzzle.GetAnswerOne());
            Console.WriteLine("2: " + puzzle.GetAnswerTwo());

            Console.ReadKey();
        }
    }
}
