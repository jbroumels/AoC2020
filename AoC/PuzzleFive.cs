using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC
{
    public class PuzzleFive : Puzzle
    {
        public PuzzleFive() : base("05") { }

        public override object GetAnswerOne()
        {
            Seat[] seats = GetSeats().ToArray();
            return seats.Max(it => it.GetSeatId());
        }

        public override object GetAnswerTwo()
        {
            Seat[] seats = GetSeats().OrderBy(it => it.GetSeatId()).ToArray();

            foreach (var seat in seats)
            {
                if (seats.Any(it => it.GetSeatId() == seat.GetSeatId() + 2) && seats.All(it => it.GetSeatId() != seat.GetSeatId() + 1))
                {
                    return seat.GetSeatId() + 1;
                }
            }

            return base.GetAnswerTwo();
        }

        private IEnumerable<Seat> GetSeats()
        {
            return Lines.Select(it => new Seat(it));
        }

        public class Seat
        {
            public int Row { get; private set; }
            public int Col { get; private set; }

            public Seat(string seatData)
            {
                var rows = seatData.Take(7).ToArray();
                var columns = seatData.Skip(7).ToArray();

                Row = 0;
                for (int i = 6; i >=0 ; i--)
                {
                    Row += ((rows[i] == 'B') ? Convert.ToInt16(Math.Pow(2, 6 - i)) : 0);
                }


                Col = 0;
                for (int i = 2; i >= 0; i--)
                {
                    Col += ((columns[i] == 'R') ? Convert.ToInt16(Math.Pow(2, 2 - i)) : 0);
                }
            }

            public long GetSeatId()
            {
                return (Row * 8) + Col;
            }

            public override string ToString()
            {
                return GetSeatId().ToString();
            }
        }
    }
}