using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    public class Day1
    {
        public int calculate1()
        {
            Orientation o = Orientation.North;
            int x=0, y = 0;


            var file = new StreamReader("input\\day1.txt");
            IEnumerable<string> input = file.ReadLine().Split(',');
            foreach(var direction in input)
            {
                TurnDirection d = direction[0] == 'L' ? TurnDirection.Left : TurnDirection.Right;
                int distance = Int32.Parse(direction.Substring(1));
                switch (o)
                {
                    case Orientation.North:
                        if (d == TurnDirection.Left)
                        {
                            x -= distance;
                            o = Orientation.West;
                        } else
                        {
                            x += distance;
                            o = Orientation.East;
                        }
                        break;
                    case Orientation.South:
                        if (d == TurnDirection.Left)
                        {
                            x += distance;
                            o = Orientation.East;
                        }
                        else
                        {
                            x -=distance;
                            o = Orientation.West;
                        }
                        break;
                    case Orientation.East:
                        if (d == TurnDirection.Left)
                        {
                            y += distance;
                            o = Orientation.North;
                        }
                        else
                        {
                            y -= distance;
                            o = Orientation.South;
                        }
                        break;

                    case Orientation.West:
                        if (d == TurnDirection.Left)
                        {
                            y -= distance;
                            o = Orientation.South;
                        }
                        else
                        {
                            y += distance;
                            o = Orientation.North;
                        }
                        break;
                }

            }
            return Math.Abs(x) + Math.Abs(y);
        }

        public int calculate2()
        {
            Orientation o = Orientation.North;
            int x = 0, y = 0;
            List<Tuple<int, int>> visited = new List<Tuple<int, int>>();
            visited.Add(new Tuple<int, int>(0, 0));
            var file = new StreamReader("input\\day1.txt");
            IEnumerable<string> input = file.ReadLine().Split(',');
            foreach (var direction in input)
            {
                TurnDirection d = direction[0] == 'L' ? TurnDirection.Left : TurnDirection.Right;
                int distance = Int32.Parse(direction.Substring(1));
                switch (o)
                {
                    case Orientation.North:
                        if (d == TurnDirection.Left)
                        {
                            for(int a = 0; a < distance; a++)
                            {
                                x--;
                                
                                if (visited.Any(t => t.Item1 == x && t.Item2 == y))
                                {
                                    return Math.Abs(x) + Math.Abs(y);
                                }
                                visited.Add(new Tuple<int, int>(x, y));
                            }
                            o = Orientation.West;
                        }
                        else
                        {
                            for (int a = 0; a < distance; a++)
                            {
                                x++;
                                if (visited.Any(t => t.Item1 == x && t.Item2 == y))
                                {
                                    return Math.Abs(x) + Math.Abs(y);
                                }
                                visited.Add(new Tuple<int, int>(x, y));

                            }
                            o = Orientation.East;
                        }
                        break;
                    case Orientation.South:
                        if (d == TurnDirection.Left)
                        {
                            for (int a = 0; a < distance; a++)
                            {
                                x++;
                                if (visited.Any(t => t.Item1 == x && t.Item2 == y))
                                {
                                    return Math.Abs(x) + Math.Abs(y);
                                }
                                visited.Add(new Tuple<int, int>(x, y));

                            }
                            o = Orientation.East;
                        }
                        else
                        {
                            for (int a = 0; a < distance; a++)
                            {
                                x--;
                                if (visited.Any(t => t.Item1 == x && t.Item2 == y))
                                {
                                    return Math.Abs(x) + Math.Abs(y);
                                }
                                visited.Add(new Tuple<int, int>(x, y));

                            }
                            o = Orientation.West;
                        }
                        break;
                    case Orientation.East:
                        if (d == TurnDirection.Left)
                        {
                            for (int a = 0; a < distance; a++)
                            {
                                y++;
                                if (visited.Any(t => t.Item1 == x && t.Item2 == y))
                                {
                                    return Math.Abs(x) + Math.Abs(y);
                                }
                                visited.Add(new Tuple<int, int>(x, y));

                            }
                            o = Orientation.North;
                        }
                        else
                        {
                            for (int a = 0; a < distance; a++)
                            {
                                y--;
                                if (visited.Any(t => t.Item1 == x && t.Item2 == y))
                                {
                                    return Math.Abs(x) + Math.Abs(y);
                                }
                                visited.Add(new Tuple<int, int>(x, y));

                            }
                            o = Orientation.South;
                        }
                        break;

                    case Orientation.West:
                        if (d == TurnDirection.Left)
                        {
                            for (int a = 0; a < distance; a++)
                            {
                                y--;
                                if (visited.Any(t => t.Item1 == x && t.Item2 == y))
                                {
                                    return Math.Abs(x) + Math.Abs(y);
                                }
                                visited.Add(new Tuple<int, int>(x, y));

                            }
                            o = Orientation.South;
                        }
                        else
                        {
                            for (int a = 0; a < distance; a++)
                            {
                                y++;
                                if (visited.Any(t => t.Item1 == x && t.Item2 == y))
                                {
                                    return Math.Abs(x) + Math.Abs(y);
                                }
                                visited.Add(new Tuple<int, int>(x, y));

                            }
                            o = Orientation.North;
                        }
                        break;
                }
            }
            return Math.Abs(x) + Math.Abs(y);
        }
        private enum TurnDirection
        {
            Left,Right
        }
        private enum Orientation
        {
            North,South,East,West
        }
    }
}
