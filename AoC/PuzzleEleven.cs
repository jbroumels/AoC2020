using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AoC
{
    public class PuzzleEleven : Puzzle
    {
        public PuzzleEleven() : base("11")
        {

        }

        public override object GetAnswerOne()
        {
            var room = GetRoom();

            long takenValue;

            do
            {
                takenValue = room.TotalTakenSeatValue();

                var actionsToTake = room.GetActionsToTake();

                foreach (var actionToTake in actionsToTake)
                {
                    if (actionToTake.Item2 == SeatAction.Take)
                    {
                        actionToTake.Item1.IsTaken = true;
                    }
                    else if (actionToTake.Item2 == SeatAction.Leave)
                    {
                        actionToTake.Item1.IsTaken = false;
                    }
                }
            } while (room.TotalTakenSeatValue() != takenValue);

            return room.TakenSeatCount();
        }

        public override object GetAnswerTwo()
        {
            var room = GetRoom();

            long takenValue;

            do
            {
                takenValue = room.TotalTakenSeatValue();

                var actionsToTake = room.GetActionsToTake2();

                foreach (var actionToTake in actionsToTake)
                {
                    if (actionToTake.Item2 == SeatAction.Take)
                    {
                        actionToTake.Item1.IsTaken = true;
                    }
                    else if (actionToTake.Item2 == SeatAction.Leave)
                    {
                        actionToTake.Item1.IsTaken = false;
                    }
                }
            } while (room.TotalTakenSeatValue() != takenValue);

            return room.TakenSeatCount();
        }

        private Room GetRoom()
        {
            var room = new Room();
            for (int y = 0; y < Lines.Length; y++)
            {
                string line = Lines[y];
                for (int x = 0; x < line.Length; x++)
                {
                    if (line[x] == 'L')
                    {
                        room.AddSeat(x,y);
                    }
                }
            }
            
            return room;
        }

        public class Room
        {
            private readonly Dictionary<Location, Seat> seats = new Dictionary<Location, Seat>();

            public int MaxX { get; private set; } = -1;

            public int MaxY { get; private set; } = -1;

            public long TotalTakenSeatValue()
            {
                return seats.Sum(it =>
                {
                    if (it.Value.IsTaken)
                    {
                        return it.Key.X + (MaxX * it.Key.Y);
                    }

                    return 0;
                });
            }

            public void AddSeat(in int x, in int y)
            {
                var l = new Location(x, y);
                seats.Add(l, new Seat(this, l));

                if (x > MaxX)
                {
                    MaxX = x;
                }
                if (y > MaxY)
                {
                    MaxY = y;
                }
            }

            public int TakenSeatCount()
            {
                return seats.Count(it => it.Value.IsTaken);
            }

            public List<Tuple<Seat, SeatAction>> GetActionsToTake()
            {
                return seats.Select(it => new Tuple<Seat, SeatAction>(it.Value, it.Value.GetActionToTake())).ToList();
            }

            public List<Tuple<Seat, SeatAction>> GetActionsToTake2()
            {
                return seats.Select(it => new Tuple<Seat, SeatAction>(it.Value, it.Value.GetActionToTake2())).ToList();
            }

            public bool TryGetSeat(Location location, out Seat seat)
            {
                return seats.TryGetValue(location, out seat);
            }
        }

        public class Seat
        {
            private readonly Room room;

            public Seat(Room room, Location location)
            {
                this.room = room;
                Location = location;
            }

            public Location Location { get; }

            public bool IsTaken { get; set; }
            
            public SeatAction GetActionToTake()
            {
                var surrounding = GetSurroundingSeats();

                int surroundingTaken = surrounding.Count(it => it.IsTaken);

                if (!IsTaken && surroundingTaken == 0)
                {
                    return SeatAction.Take;
                }

                if (IsTaken && surroundingTaken >= 4)
                {
                    return SeatAction.Leave;
                }

                return SeatAction.Nothing;
            }


            public SeatAction GetActionToTake2()
            {
                var surrounding = GetSurroundingSeats2();

                int surroundingTaken = surrounding.Count(it => it.IsTaken);

                if (!IsTaken && surroundingTaken == 0)
                {
                    return SeatAction.Take;
                }

                if (IsTaken && surroundingTaken >= 5)
                {
                    return SeatAction.Leave;
                }

                return SeatAction.Nothing;
            }

            private List<Seat> surroundingSeats2 = null;
            private List<Seat> GetSurroundingSeats2()
            {
                if (surroundingSeats2 == null)
                {
                    surroundingSeats2 = new List<Seat>();

                    for (int shiftY = -1; shiftY <= 1; shiftY++)
                    {
                        for (int shiftX = -1; shiftX <= 1; shiftX++)
                        {
                            if (shiftX != 0 || shiftY != 0)
                            {
                                Location l = Location;
                                while (true)
                                {
                                    l = new Location(l.X + shiftX, l.Y + shiftY);

                                    // should be in bounds of the room
                                    if (l.X >= 0 && l.Y >= 0 && l.X <= room.MaxX && l.Y <= room.MaxY)
                                    {
                                        if (room.TryGetSeat(l, out Seat s))
                                        {
                                            surroundingSeats2.Add(s);
                                            break;
                                        }
                                        
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                return surroundingSeats2;
            }

            private List<Seat> surroundingSeats = null;
            private List<Seat> GetSurroundingSeats()
            {
                if (surroundingSeats == null)
                {
                    surroundingSeats = new List<Seat>();

                    for (int y = Math.Max(0, Location.Y - 1); y <= Math.Min(room.MaxY, Location.Y + 1); y++)
                    {
                        for (int x = Math.Max(0, Location.X - 1); x <= Math.Min(room.MaxX, Location.X + 1); x++)
                        {
                            if (room.TryGetSeat(new Location(x, y), out Seat seat) && seat != this)
                            {
                                surroundingSeats.Add(seat);
                            }
                        }
                    }
                }

                return surroundingSeats;
            }
        }

        public enum SeatAction
        {
            Take,
            Leave,
            Nothing
        }

        public struct Location
        {
            public Location(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; }
            public int Y { get; }
        }
    }
}