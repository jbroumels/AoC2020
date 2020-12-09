using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AoC
{
    public class PuzzleNine : Puzzle
    {
        public PuzzleNine() : base("09")
        {
        }

        public override object GetAnswerOne()
        {
            Span<long> vals = Lines.Select(it => Convert.ToInt64(it)).ToArray().AsSpan(0);
            for (int x = 25; x < Lines.Length; x++)
            {
                long current = vals[x];

                // must be in range of 25 before
                bool inRange = CheckInRange(current, vals.Slice(x - 25, 25));

                if (!inRange)
                {
                    return current;
                }
            }
            return base.GetAnswerOne();
        }

        public override object GetAnswerTwo()
        {
            long find = 1639024365;
            long[] vals = Lines.Select(it => Convert.ToInt64(it)).ToArray();
            for (int x = 0; x < vals.Length - 2; x++)
            {
                bool sequenceFound = TryFindSequence(find, vals, x, out long[] sequence);

                if (sequenceFound)
                {
                    return sequence.Min() + sequence.Max();
                }
            }

            return base.GetAnswerTwo();
        }

        private bool TryFindSequence(long find, long[] vals, int pointer, [NotNullWhen(true)]out long[] result)
        {
            for (int x = 2; x < vals.Length - pointer; x++)
            {
                long[] take = vals.Skip(pointer).Take(x).ToArray();
                long sum = take.Sum();
                if (sum == find)
                {
                    result = take;
                    return true;
                }
                if (sum > find)
                {

                    result = null;
                    return false;
                }
            }

            result = null;
            return false;
        }

        private bool CheckInRange(long current, Span<long> slice)
        {
            for (int x = 0; x < slice.Length; x++)
            {
                long pos = slice[x];

                long remainder = current - pos;

                if (slice.Contains(remainder))
                {
                    return true;
                }
            }

            return false;
        }
    }
}