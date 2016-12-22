using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    public class Day22
    {
        HashSet<string> visited = new HashSet<string>();
        public void calculate1()
        {
            var sw = new Stopwatch();
            sw.Start();
            var input = File.ReadAllLines("input\\day22.txt").Skip(2);
            List<Node> nodes = new List<Node>();
            int i = 0;
            foreach(var node in input)
            {
                //if(i>10) { break; }
                var tokens = node.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                nodes.Add(new Node()
                {
                    x = int.Parse(new String(tokens[0].Split('-').ElementAt(1).Skip(1).ToArray())),
                    y = int.Parse(new String(tokens[0].Split('-').ElementAt(2).Skip(1).ToArray())),
                    size = int.Parse(tokens[1].TrimEnd('T')),
                    used = int.Parse(tokens[2].TrimEnd('T')),
                    free = int.Parse(tokens[3].TrimEnd('T')),
                    usePct = int.Parse(tokens[4].TrimEnd('%'))
                });
                i++;

            }
            var pairs = nodes.SelectMany(x => nodes, (x, y) => Tuple.Create(x, y)).ToList();
            var uniquePairs = pairs.Where(tuple => tuple.Item1.x != tuple.Item2.x || tuple.Item1.y != tuple.Item2.y).ToList();

            sw.Stop();
            Console.WriteLine(pairs.Count(x=>((x.Item1.free >= x.Item2.used && x.Item2.used > 0))) + " in " + sw.Elapsed.TotalMilliseconds);
        }

        int goalX = 2;
        int goalY = 0;
        int maxX = 0;
        int maxY = 0;
        int solution = int.MaxValue;
        public void calculate2()
        {
            var sw = new Stopwatch();
            sw.Start();
            var input = File.ReadAllLines("input\\day22.txt").Skip(2);
            List<Node> nodes = new List<Node>();

            foreach (var node in input)
            {
                var tokens = node.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                nodes.Add(new Node()
                {
                    x = int.Parse(new String(tokens[0].Split('-').ElementAt(1).Skip(1).ToArray())),
                    y = int.Parse(new String(tokens[0].Split('-').ElementAt(2).Skip(1).ToArray())),
                    size = int.Parse(tokens[1].TrimEnd('T')),
                    used = int.Parse(tokens[2].TrimEnd('T')),
                    free = int.Parse(tokens[3].TrimEnd('T')),
                    usePct = int.Parse(tokens[4].TrimEnd('%'))
                });
                if(nodes.Last().x > maxX)
                {
                    maxX = nodes.Last().x;
                }
                if (nodes.Last().x > maxY)
                {
                    maxY = nodes.Last().y;
                }
            }
            maxX++;
            maxY++;
            var grid = new Node[maxY, maxX];
            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    grid[y, x] = nodes.First(n => n.x == x && n.y == y);
                }
            }

            //print and solve by hand :)
            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    Console.Write(GetChar(grid[y, x]));
                }
                Console.WriteLine();
            }

            //var queue = new Queue<Grid>();
            //queue.Enqueue(new Grid(nodes) { steps = 0 });
            //int count = 0;
            //while (queue.Count > 0)
            //{
            //    var current = queue.Dequeue();
            //    //if (visited.Contains(current.State()))
            //    //{
            //    //    continue;
            //    //}
            //    //visited.Add(current.State());
            //    count++;
            //    if (IsComplete(current))
            //    {
            //        if (solution > current.steps)
            //        {
            //            solution = current.steps;
            //            Console.WriteLine("SOLUTION FOUND: " + solution);
            //            break;
            //        }
            //    }
            //    var moves = GetValidMoves(current);
            //    foreach (var move in moves)
            //    {
            //        queue.Enqueue(move);
            //    }
            //}


            sw.Stop();
            Console.WriteLine( " in " + sw.Elapsed.TotalMilliseconds);
        }

        private string GetChar(Node node)
        {
            if(node.y == 0 && node.x == maxX-1)
            {
                return "G";
            }
            if(node.usePct > 95 && node.size > 15)
            {
                return "#";
            }
            return node.free == node.size ? "E" : "_";
        }

        private List<Grid> GetValidMoves(Grid current)
        {
            var moves = new List<Grid>();
            var nodes = current.Nodes.Cast<Node>().ToList();
            var pairs = nodes.SelectMany(x => nodes, (x, y) => Tuple.Create(x, y)).ToList();
            var uniquePairs = pairs.Where(tuple => tuple.Item1.x != tuple.Item2.x || tuple.Item1.y != tuple.Item2.y).ToList();
            var validPairs = pairs.Where(x => ((x.Item1.free >= x.Item2.used && x.Item2.used > 0))).ToList();

            foreach(var pair in validPairs)
            {
                moves.Add(MoveData(nodes, pair, current.steps + 1));
            }

            return null;

        }

        private Grid MoveData(List<Node> nodes, Tuple<Node, Node> pair, int step)
        {
            var clone = new Node[nodes.Count];
            nodes.CopyTo(clone);

            var grid = new Grid(clone.ToList(), maxX, maxY);
            return grid;
        }

        private bool IsComplete(Grid current)
        {
            return current.Nodes[0,0].dataX == goalX && current.Nodes[0,0].dataY == 0;
        }

        public class Grid
        {
            public Grid(List<Node> nodes, int maxX, int maxY)
            {
                Nodes = new Node[maxY, maxX];
                for (int x = 0; x < maxX; x++)
                {
                    for (int y = 0; y < maxY; y++)
                    {
                        Nodes[y, x] = nodes.First(n => n.x == x && n.y == y);
                    }
                }
            }
            public Node[,] Nodes;
            public int steps;
        }

        public class Node
        {
            public int x;
            public int y;
            public int size;
            public int used;
            public int free;
            public int usePct;
            public int dataX;
            public int dataY;
        }

        
    }
}
