using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace AoC
{
    public class PuzzleTen : Puzzle
    {
        public PuzzleTen() : base("10"){}

        public override object GetAnswerOne()
        {
            List<int> adapters = Lines.Select(it => Convert.ToInt32(it)).OrderByDescending(x => x).ToList();
            
            // find high
            int high = adapters.Max();

            // add device
            adapters.Insert(0, high + 3);

            // add to zero
            adapters.Add(0);

            Dictionary<int, int> diffCounter = new Dictionary<int, int>();

            int current = high + 3;
            foreach(var a in adapters)
            {
                int diff = current - a;

                if (!diffCounter.ContainsKey(diff))
                {
                    diffCounter[diff] = 0;
                }

                diffCounter[diff]++;

                current = a;
            }

            return diffCounter[1] * diffCounter[3];
        }

        public override object GetAnswerTwo()
        {
            List<int> adapters = Lines.Select(it => Convert.ToInt32(it))
                .OrderByDescending(x => x).ToList();

            // find high
            int high = adapters.Max();

            // add device
            adapters.Insert(0, high + 3);

            // add to zero
            adapters.Add(0);

            // find arrangements

            ConcurrentDictionary<int, long> d = new ConcurrentDictionary<int, long>();
            // insert the known first one as a dummy
            d.TryAdd(0, 1);
            
            long arrangements = FindArrangements(d, adapters, high + 3);

            return arrangements;
        }

        private long FindArrangements(ConcurrentDictionary<int, long> cache, List<int> adapters, int high)
        {
            if (cache.TryGetValue(high, out long l))
            {
                return l;
            }

            var possible = adapters.Where(it => it < high && it >= high - 3).ToList();

            var r = possible.Sum(it => FindArrangements(cache, adapters, it));

            cache.TryAdd(high, r);

            return r;
        }
    }

}