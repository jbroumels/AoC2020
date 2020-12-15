using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AoC
{
    public class PuzzleFifteen : Puzzle
    {
        public PuzzleFifteen():base("15"){}

        public override object GetAnswerOne()
        {
            long[] init = Lines[0].Split(new[] {','}).Select(it => Convert.ToInt64(it)).ToArray();

            Dictionary<long, List<long>> ht = new Dictionary<long, List<long>>();

            long prevNumber = -1;

            for (int x = 1; x <= init.Length; x++)
            {
                long v = init[x - 1];
                if (!ht.ContainsKey(v))
                {
                    ht[v] = new List<long>();
                }
                ht[v].Add(x);

                prevNumber = v;
            }

            for (int x = init.Length + 1; x <= 2020; x++)
            {
                long nextUp;
                if (ht[prevNumber].Count == 1)
                {
                    // first time
                    nextUp = 0;
                }
                else
                {
                    // get diff
                    nextUp = ht[prevNumber].Last() - ht[prevNumber][ht[prevNumber].Count - 2];
                }

                if (!ht.ContainsKey(nextUp))
                {
                    ht[nextUp] = new List<long>();
                }
                ht[nextUp].Add(x);

                prevNumber = nextUp;
            }

            return prevNumber;
        }

        public override object GetAnswerTwo()
        {
            long[] init = Lines[0].Split(new[] { ',' }).Select(it => Convert.ToInt64(it)).ToArray();

            Dictionary<long, List<long>> ht = new Dictionary<long, List<long>>();

            long prevNumber = -1;

            for (int x = 1; x <= init.Length; x++)
            {
                long v = init[x - 1];
                if (!ht.ContainsKey(v))
                {
                    ht[v] = new List<long>();
                }
                ht[v].Add(x);

                prevNumber = v;
            }

            for (int x = init.Length + 1; x <= 30000000; x++)
            {
                long nextUp;
                if (ht[prevNumber].Count == 1)
                {
                    // first time
                    nextUp = 0;
                }
                else
                {
                    // get diff
                    nextUp = ht[prevNumber].Last() - ht[prevNumber][ht[prevNumber].Count - 2];
                }

                if (!ht.ContainsKey(nextUp))
                {
                    ht[nextUp] = new List<long>();
                }
                ht[nextUp].Add(x);

                prevNumber = nextUp;
            }

            return prevNumber;
        }
    }
}