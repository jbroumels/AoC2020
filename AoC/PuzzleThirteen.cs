using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace AoC
{
    public class PuzzleThirteen : Puzzle
    {
        public PuzzleThirteen() : base("13"){}

        public override object GetAnswerOne()
        {
            long arrival = Convert.ToInt64(Lines[0]);

            long[] validBusses = Lines[1].Split(new[] {','}).Where(it => it != "x").Select(it => Convert.ToInt64(it)).ToArray();

            var bestBus = validBusses.Select(it =>
            {
                long fullRounds = arrival % it != 0 ? arrival / it + 1 : it;
                return new
                {
                    BusNumber = it,
                    Time = fullRounds * it
                };
            }).OrderBy(it => it.Time).First();

            return (bestBus.Time - arrival) * bestBus.BusNumber;
        }

        public override object GetAnswerTwo()
        {
            string[] busInput = Lines[1].Split(new[] { ',' }).ToArray();

            List<Bus> busses = new List<Bus>();
            for (int x = 0; x < busInput.Length; x++)
            {
                if (long.TryParse(busInput[x], out long busNumber))
                {
                    busses.Add(new Bus(busNumber, x));
                }
            }

            long time = 0;
            long step = 1;

            foreach (var bus in busses)
            {
                // increase with 1 to skip the initial (zero always matches, and you need the next)
                while ((time + bus.Offset + 1) % bus.Number > 0)
                {
                    time += step;
                }

                step *= bus.Number;
            }

            // add one because we compared with a +1
            return time + 1;
        }

        public class Bus
        {
            public long Number { get; }
            public long Offset { get; }
            
            public Bus(long number, long offset)
            {
                Number = number;
                Offset = offset;
            }

        }
    }
}