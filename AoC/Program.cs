using System;
using System.Diagnostics;

namespace AoC
{
    class Program
    {
        static void Main(string[] args)
        {
            //Puzzle puzzle = new PuzzleOne();
            //Puzzle puzzle = new PuzzleTwo();
            //Puzzle puzzle = new PuzzleThree();
            //Puzzle puzzle = new PuzzleFour();
            //Puzzle puzzle = new PuzzleFive();
            //Puzzle puzzle = new PuzzleSix();
            //Puzzle puzzle = new PuzzleSeven();
            //Puzzle puzzle = new PuzzleEight();
            //Puzzle puzzle = new PuzzleNine();
            //Puzzle puzzle = new PuzzleTen();
            //Puzzle puzzle = new PuzzleEleven();
            //Puzzle puzzle = new PuzzleTwelve();
            //Puzzle puzzle = new PuzzleThirteen();
            Puzzle puzzle = new PuzzleFifteen();

            // warm up
            Console.WriteLine($"Running puzzle {puzzle.PuzzleNumber}");
            puzzle.GetAnswerOne();
            puzzle.GetAnswerTwo();

            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine($"1: {puzzle.GetAnswerOne()}");
            Console.WriteLine($"elapsed msec: {sw.ElapsedMilliseconds} - ticks: {sw.ElapsedTicks}");
            Console.WriteLine();
            
            sw = Stopwatch.StartNew();
            Console.WriteLine($"2: {puzzle.GetAnswerTwo()}");
            Console.WriteLine($"elapsed msec: {sw.ElapsedMilliseconds} - ticks: {sw.ElapsedTicks}");
            Console.WriteLine();


            Console.ReadKey();
        }
    }
}
