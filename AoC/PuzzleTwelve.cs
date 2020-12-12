using System;

namespace AoC
{
    public class PuzzleTwelve : Puzzle
    {
        public PuzzleTwelve() : base("12"){}

        public override object GetAnswerOne()
        {
            var startLocation = new Location(0, 0);
            var startDirection = Direction.East;
            var ship = new Ship(startLocation, startDirection);

            foreach (var line in Lines)
            {
                var moveType = line[0];
                var moveValue = Convert.ToInt32(line.Substring(1));
                switch (moveType)
                {
                    case 'N':
                    {
                        ship.MoveY(-moveValue);
                        break;
                    }
                    case 'E':
                    {
                        ship.MoveX(moveValue);
                        break;
                    }
                    case 'S':
                    {
                        ship.MoveY(moveValue);
                        break;
                    }
                    case 'W':
                    {
                        ship.MoveX(-moveValue);
                        break;
                    }
                    case 'L':
                    {
                        ship.Turn(-moveValue);
                        break;
                        }
                    case 'R':
                    {
                        ship.Turn(moveValue);
                        break;
                        }
                    case 'F':
                    {
                        ship.Forward(moveValue);
                        break;
                    }
                    default:
                    {
                        throw new NotImplementedException();
                    }
                }
            }

            var distance = Math.Abs(startLocation.X - ship.Location.X) + Math.Abs(startLocation.Y - ship.Location.Y);
            return distance;
        }

        public override object GetAnswerTwo()
        {
            var startLocation = new Location(0, 0);
            var wayPointLocation = new Location(10, -1);
            var ship = new Ship(startLocation, Direction.East);
            var wayPoint = new Ship(wayPointLocation, Direction.East);

            foreach (var line in Lines)
            {
                var moveType = line[0];
                var moveValue = Convert.ToInt32(line.Substring(1));
                switch (moveType)
                {
                    case 'N':
                    {
                        wayPoint.MoveY(-moveValue);
                        break;
                    }
                    case 'E':
                    {
                        wayPoint.MoveX(moveValue);
                        break;
                    }
                    case 'S':
                    {
                        wayPoint.MoveY(moveValue);
                        break;
                    }
                    case 'W':
                    {
                        wayPoint.MoveX(-moveValue);
                        break;
                    }
                    case 'L':
                    {
                        wayPoint.Turn2(-moveValue);
                        break;
                    }
                    case 'R':
                    {
                        wayPoint.Turn2(moveValue);
                        break;
                    }
                    case 'F':
                    {
                        for (int x = 0; x < moveValue; x++)
                        {
                            ship.MoveX(wayPoint.Location.X);
                            ship.MoveY(wayPoint.Location.Y);
                        }

                        break;
                    }
                    default:
                    {
                        throw new NotImplementedException();
                    }
                }
            }

            var distance = Math.Abs(startLocation.X - ship.Location.X) + Math.Abs(startLocation.Y - ship.Location.Y);
            return distance;
        }
        
        public class Ship
        {
            public Location Location { get; private set; }

            public Direction Direction { get; set; }

            public Ship(Location location, Direction direction)
            {
                Location = location;
                Direction = direction;
            }

            public void MoveY(int moveY)
            {
                Location = new Location(Location.X, Location.Y + moveY);
            }
            public void MoveX(int moveX)
            {
                Location = new Location(Location.X + moveX, Location.Y);
            }

            public void Turn(int turn)
            {
                int d = (int)Direction;
                switch (turn)
                {
                    case 90:
                    case -270:
                    {
                        d+=1;
                        break;
                    }
                    case 180:
                    case -180:
                    {
                        d += 2;
                        break;
                    }
                    case 270:
                    case -90:
                    {
                        d += 3;
                        break;
                    }
                    default:
                    {
                        throw new NotImplementedException();
                    }
                }

                if (d > 4)
                {
                    d -= 4;
                }

                Direction = (Direction)(d);
            }

            public void Forward(int forward)
            {
                switch (Direction)
                {
                    case Direction.North:
                    {
                        Location = new Location(Location.X, Location.Y - forward);
                        break;

                    }
                    case Direction.East:
                    {
                        Location = new Location(Location.X + forward, Location.Y);
                        break;
                    }
                    case Direction.South:
                    {
                        Location = new Location(Location.X, Location.Y + forward);
                        break;
                    }
                    case Direction.West:
                    {
                        Location = new Location(Location.X - forward, Location.Y);
                        break;
                    }
                    default:
                    {
                        throw new NotImplementedException();
                    }
                }
            }

            public void Turn2(int turn)
            {
                int t = 0;
                switch (turn)
                {
                    case 90:
                    case -270:
                    {
                        t = 1;
                        
                        break;
                    }
                    case 180:
                    case -180:
                    {
                        t = 2;
                        break;
                    }
                    case 270:
                    case -90:
                    {
                        t = 3;
                        break;
                    }
                    default:
                    {
                        throw new NotImplementedException();
                    }
                }

                for (int x = 0; x < t; x++)
                {
                    Location = new Location(-Location.Y, Location.X);
                }
            }
        }

        public enum Direction
        {
            North = 1,
            East = 2,
            South = 3,
            West = 4
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