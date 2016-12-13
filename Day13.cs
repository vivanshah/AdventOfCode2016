using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    public class Day13
    {
        HashSet<string> visited = new HashSet<string>();
        int solution = int.MaxValue;
        Queue<Tuple<int,int,int>> queue = new Queue<Tuple<int, int,int>>();
        public void calculate1()
        {
            var st = new Stopwatch();
            st.Start();
            queue.Enqueue(Tuple.Create(1,1,0));
            int count = 0;
            while (queue.Count> 0)
            {
                var current = queue.Dequeue();
                if (visited.Contains(current.Item1.ToString() + ',' + current.Item2.ToString()))
                {
                    continue;
                }
                visited.Add(current.Item1.ToString() + ',' + current.Item2.ToString());
                count++;
                if (current.Item1 == 31 && current.Item2 == 39)
                {
                    solution = current.Item3;
                    break;
                }
                var moves = GetValidMoves(current);
                foreach(var move in moves)
                {
                    queue.Enqueue(move);
                }
            }
            st.Stop();
            Console.WriteLine(solution + " in " + st.Elapsed.TotalMilliseconds);
        }

        public void calculate2()
        {
            var st = new Stopwatch();
            st.Start();
            queue.Enqueue(Tuple.Create(1, 1, 0));
            int count = 0;
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (visited.Contains(current.Item1.ToString() + ',' + current.Item2.ToString()))
                {
                    continue;
                }
                visited.Add(current.Item1.ToString() + ',' + current.Item2.ToString());
                count++;
                if (current.Item3 == 50) continue;
                var moves = GetValidMoves(current);
                foreach (var move in moves)
                {
                    queue.Enqueue(move);
                }
            }
            st.Stop();
            Console.WriteLine(count + " in " + st.Elapsed.TotalMilliseconds);
        }

        private List<Tuple<int,int, int>> GetValidMoves(Tuple<int, int,int> current)
        {
            var moves = new List<Tuple<int, int, int>>();
            int x = current.Item1;
            int y = current.Item2;
            if (CheckLocation(x + 1, y))
            {
                moves.Add(Tuple.Create(x + 1, y,current.Item3+1));
            }
            if (CheckLocation(x, y+1))
            {
                moves.Add(Tuple.Create(x , y+1, current.Item3 + 1));
            }
            if (CheckLocation(x -1, y))
            {
                moves.Add(Tuple.Create(x - 1, y, current.Item3 + 1));
            }
            if (CheckLocation(x, y-1))
            {
                moves.Add(Tuple.Create(x, y-1, current.Item3 + 1));
            }



            return moves;
        }

        private bool CheckLocation(int x, int y)
        {
            if (x < 0 || y < 0) return false;
            int i = x * x + 3 * x + 2 * x * y + y + y * y;
            i += 1352;
            if(CountBits(i)%2 == 0)
            {
                return true;
            }
            return false;

        }
        private int CountBits(int value)
        {
            int count = 0;
            while (value != 0)
            {
                count++;
                value &= value - 1;
            }
            return count;
        }
    }
}
