using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace AdventOfCode2016
{
    public class Day17
    {
        MD5 md5 = MD5.Create();
        HashSet<string> visited = new HashSet<string>();
        Queue<Tuple<int, int, string>> queue = new Queue<Tuple<int, int, string>>();
        int destRow = 3, destColumn = 3;
        public void calculate1()
        {
            Console.SetBufferSize(400, 10000);
            Console.SetWindowSize(140, 45);
            var st = new Stopwatch();
            st.Start();
            int row, column = 0;
            var path = "kglvqrro";
            int count = 0;
            string solution = null;
            queue.Enqueue(Tuple.Create(0, 0, path));
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                count++;
                if (count % 10000 == 0)
                {
                    Console.WriteLine(String.Format("Checked {0} positions, current best solution is {1}", count, solution));
                }
                if (current.Item1 == destRow && current.Item2 == destColumn && current.Item3.Length < (solution == null ? int.MaxValue : solution.Length))
                {
                    solution = current.Item3;
                    break;
                }
                var moves = GetValidMoves(current);
                foreach (var move in moves.OrderByDescending(x=>x.Item2).ThenByDescending(x=>x.Item1))
                {
                    queue.Enqueue(move);
                }
            }


            st.Stop();
            Console.WriteLine( solution.TrimStart(path.ToArray()) ?? "no solution" + " in " + st.Elapsed.TotalMilliseconds);
        }

        public void calculate2()
        {
            Console.SetBufferSize(400, 10000);
            Console.SetWindowSize(140, 45);
            var st = new Stopwatch();
            st.Start();
            var path = "lpvhkcbi";
            int count = 0;
            string solution = null;
            queue.Enqueue(Tuple.Create(0, 0, path));
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                count++;
                if (count % 10000 == 0)
                {
                  //  Console.WriteLine(String.Format("Checked {0} positions, current best solution is {1}", count, solution.Length));
                }
                if (current.Item1 == destRow && current.Item2 == destColumn)
                {
                    if (current.Item3.Length > (solution == null ? int.MinValue : solution.Length - path.Length))
                    {
                        solution = current.Item3;
                        //Console.WriteLine(String.Format("new best solution length {0}", solution.Length - path.Length));
                    }
                }
                else
                {
                    var moves = GetValidMoves(current);
                    foreach (var move in moves.OrderBy(x => x.Item2).ThenBy(x => x.Item1))
                    {
                        queue.Enqueue(move);
                    }
                }
            }


            st.Stop();
            Console.WriteLine(solution.Length - path.Length  + " in " + st.Elapsed.TotalMilliseconds);
        }

        private List<Tuple<int, int, string>> GetValidMoves(Tuple<int, int, string> current)
        {
            var hash = HashFirstFour(current.Item3);
            var paths = new List<Tuple<int, int, string>>();
            var currentRow = current.Item1;
            var currentColumn = current.Item2;
            if (hash[0] > 'A' && currentRow > 0)
            {
                var upPath = String.Copy(current.Item3);
                upPath += "U";
                paths.Add(Tuple.Create(currentRow - 1, currentColumn, upPath));
            }
            if (hash[1] > 'A' && currentRow < 3)
            {
                var downPath = String.Copy(current.Item3);
                downPath += "D";
                paths.Add(Tuple.Create(currentRow + 1, currentColumn, downPath));
            }

            if (hash[2] > 'A' && currentColumn > 0)
            {
                var leftPath = String.Copy(current.Item3);
                leftPath += "L";
                paths.Add(Tuple.Create(currentRow, currentColumn - 1, leftPath));
            }
            if (hash[3] > 'A' && currentColumn < 3)
            {
                var rightPath = String.Copy(current.Item3);
                rightPath += "R";
                paths.Add(Tuple.Create(currentRow, currentColumn + 1, rightPath));
            }
            return paths;
        }

        private string HashFirstFour(string toHash)
        {
            byte[] input = Encoding.ASCII.GetBytes(toHash);
            byte[] hash = md5.ComputeHash(input);
            var hex = new StringBuilder();
            for (int i = 0; i < 2; i++)
            {
                hex.Append(hash[i].ToString("X2"));
            }
            return hex.ToString();
        }
    }
}
