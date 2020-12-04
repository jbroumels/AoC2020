using System;
using System.Linq;

namespace AoC
{
    public class PuzzleOne : Puzzle
    {
        public PuzzleOne() : base("01")
        {

        }
        

        public override object GetAnswerOne()
        {
            long[] values = Lines.Select(it => Convert.ToInt64(it)).ToArray();

            long valueToFind = 2020;

            for (int x = 0; x < values.Length; x++)
            {
                long val1 = values[x];

                long val2 = valueToFind - val1;

                if (values.Any(it => it != val1 && it == val2))
                {
                    return (val1 * val2);
                }
            }

            return base.GetAnswerOne();
        }

        public override object GetAnswerTwo()
        {
            long[] values = Lines.Select(it => Convert.ToInt64(it)).ToArray();

            long valueToFind = 2020;

            for (int x = 0; x < values.Length; x++)
            {
                long val1 = values[x];

                long[] values2 = values.Where(it => it != val1).ToArray();

                for (int y = 0; y < values2.Length; y++)
                {
                    long val2 = values2[y];

                    long val3 = valueToFind - val1 - val2;

                    if (values.Any(it => it != val1 && it != val2 && it == val3))
                    {
                        return (val1 * val2 * val3);
                    }
                }
            }

            return base.GetAnswerTwo();
        }
    }
}