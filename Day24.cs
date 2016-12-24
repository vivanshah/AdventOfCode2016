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
    public class Day24
    {
        int solution = 0;
        int numLocations = 0;
        Queue<Tuple<int, int, int, int>> queue = new Queue<Tuple<int, int, int, int>>();
        bool[,] visited;
        string order = null;
        int visitedLocations = 1;
        public void calculate1()
        {
            var st = new Stopwatch();
            st.Start();
            var lines = File.ReadAllLines("input\\day24.txt").ToList();
            var grid = new char[lines.Count, lines[0].Length];
            visited = new bool[lines.Count, lines[0].Length];
            var locations = new List<Tuple<int, int, int>>();
            for(int row = 0;row < lines.Count;row++)
            {
                for(int c = 0;c<lines[row].Length;c++)
                {
                    grid[row, c] = lines[row][c];
                    if(grid[row,c] != '#' && grid[row,c] != '.')
                    {
                        numLocations++;
                        locations.Add(Tuple.Create(row, c, grid[row, c] - 48));
                    }
                }
            }
            locations = locations.OrderBy(x => x.Item3).ToList();
            var start = locations.First(x => x.Item3 == 0);
            
            
            visited[start.Item1, start.Item2] = true;
            grid[start.Item1, start.Item2] = '.';
            int count = 0;
            int minSolution = int.MaxValue;
            foreach (var perm in Extensions.QuickPerm(locations.Skip(1).Select(x => (char)(x.Item3+48))))
            {
                visitedLocations = 1;
                solution = 0;
                visited = new bool[lines.Count, lines[0].Length];
                visited[start.Item1, start.Item2] = true;
                grid[start.Item1, start.Item2] = '.';
                queue.Clear();
                queue.Enqueue(Tuple.Create(start.Item1, start.Item2, 1, 0));
                order = "0" + new String(perm.ToArray());
               // Console.WriteLine("checking " + order);
                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();
                    visited[current.Item1, current.Item2] = true;
                    if (count % 1000 == 0)
                    {
                         //Print(grid, visited);
                    }
                    count++;
                    if (current.Item3 == 2 && grid[current.Item1, current.Item2] == order[visitedLocations])
                    {
                        //Print(grid, visited);

                        visitedLocations++;
                        solution += current.Item4;
                        if(solution > minSolution)
                        {
                            break;
                        }
                        //Console.WriteLine("Found location " + grid[current.Item1, current.Item2] + " in " + current.Item4 + " moves, total " + solution + " moves, " + visitedLocations + "/" + numLocations);
                        if (visitedLocations == numLocations)
                        {
                            if(solution < minSolution)
                            {
                                minSolution = solution;
                                Console.WriteLine("Found better solution in " + minSolution + " moves");
                            }
                            break;
                        }
                        //grid[current.Item1, current.Item2] = '.';
                        visited = new bool[lines.Count, lines[0].Length];
                        visited[current.Item1, current.Item2] = true;
                        queue.Clear();
                        queue.Enqueue(Tuple.Create(current.Item1, current.Item2, 1, 0));
                        continue;
                    }
                    var moves = GetValidMoves(grid, current);
                    foreach (var move in moves)
                    {
                        queue.Enqueue(move);
                    }
                }
            }
            st.Stop();
            Console.WriteLine(minSolution + " in " + st.Elapsed.TotalMilliseconds);
        }

        public void calculate2()
        {
            var st = new Stopwatch();
            st.Start();
            var lines = File.ReadAllLines("input\\day24.txt").ToList();
            var grid = new char[lines.Count, lines[0].Length];
            visited = new bool[lines.Count, lines[0].Length];
            var locations = new List<Tuple<int, int, int>>();
            for (int row = 0; row < lines.Count; row++)
            {
                for (int c = 0; c < lines[row].Length; c++)
                {
                    grid[row, c] = lines[row][c];
                    if (grid[row, c] != '#' && grid[row, c] != '.')
                    {
                        numLocations++;
                        locations.Add(Tuple.Create(row, c, grid[row, c] - 48));
                    }
                }
            }
            locations = locations.OrderBy(x => x.Item3).ToList();
            var start = locations.First(x => x.Item3 == 0);


            visited[start.Item1, start.Item2] = true;
            grid[start.Item1, start.Item2] = '.';
            int count = 0;
            int minSolution = int.MaxValue;
            numLocations++;
            foreach (var perm in Extensions.QuickPerm(locations.Skip(1).Select(x => (char)(x.Item3 + 48))))
            {
                visitedLocations = 1;
                solution = 0;
                visited = new bool[lines.Count, lines[0].Length];
                visited[start.Item1, start.Item2] = true;
                queue.Clear();
                queue.Enqueue(Tuple.Create(start.Item1, start.Item2, 1, 0));
                order = "0" + new String(perm.ToArray()) + "0";
                //Console.WriteLine("checking " + order);
                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();
                    visited[current.Item1, current.Item2] = true;
                    if (count % 1000 == 0)
                    {
                        //Print(grid, visited);
                    }
                    count++;
                    if (current.Item3 == 2 && grid[current.Item1, current.Item2] == order[visitedLocations])
                    {
                        //Print(grid, visited);

                        visitedLocations++;
                        solution += current.Item4;
                        if (solution > minSolution)
                        {
                            break;
                        }
                        //Console.WriteLine("Found location " + grid[current.Item1, current.Item2] + " in " + current.Item4 + " moves, total " + solution + " moves, " + visitedLocations + "/" + numLocations);
                        if (visitedLocations == numLocations-1)
                        {
                            grid[start.Item1, start.Item2] = '0';
                        }
                        if (visitedLocations == numLocations)
                        {
                            if (solution < minSolution)
                            {
                                minSolution = solution;
                                Console.WriteLine("Found better solution in " + minSolution + " moves");
                            }
                            break;
                        }
                        //grid[current.Item1, current.Item2] = '.';
                        visited = new bool[lines.Count, lines[0].Length];
                        visited[current.Item1, current.Item2] = true;
                        queue.Clear();
                        queue.Enqueue(Tuple.Create(current.Item1, current.Item2, 1, 0));
                        continue;
                    }
                    var moves = GetValidMoves(grid, current);
                    foreach (var move in moves)
                    {
                        queue.Enqueue(move);
                    }
                }
            }
            st.Stop();
            Console.WriteLine(minSolution + " in " + st.Elapsed.TotalMilliseconds);
        }

        private void Print(char[,] grid, bool[,] visited)
        {
            
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for(int col = 0; col < grid.GetLength(1); col++)
                {
                    Console.SetCursorPosition(col, row);
                    Console.Write(visited[row, col] ? 'V' : grid[row, col]);
                }
                Console.Write(Environment.NewLine);
            }
        }
        private List<Tuple<int,int, int, int>> GetValidMoves(char[,] grid, Tuple<int, int,int, int> current)
        {
            var moves = new List<Tuple<int, int, int, int>>();
            int row = current.Item1;
            int col = current.Item2;
            if (grid[row, col + 1] != '#' && !visited[row, col + 1])
            {
                moves.Add(Tuple.Create(row, col + 1, grid[row, col + 1] == order[visitedLocations]? current.Item3 + 1 : current.Item3, current.Item4 + 1));
                visited[row, col + 1] = true;
            }
            if (grid[row, col - 1] != '#' && !visited[row,col-1])
            {
                bool v = visited[row, col - 1];
                moves.Add(Tuple.Create(row, col - 1, grid[row, col - 1] == order[visitedLocations]? current.Item3 + 1 : current.Item3, current.Item4 + 1));
                visited[row, col - 1] = true;
            }
            if (grid[row + 1, col] != '#' && !visited[row+1, col])
            {
                bool v = visited[row + 1, col];
                moves.Add(Tuple.Create(row+ 1, col, grid[row + 1, col] == order[visitedLocations] ? current.Item3 + 1 : current.Item3, current.Item4 + 1));
                visited[row+1, col] = true;
            }
            if (grid[row - 1, col] != '#' && !visited[row - 1, col])
            {
                bool v = visited[row - 1, col];
                moves.Add(Tuple.Create(row - 1, col, grid[row - 1, col] == order[visitedLocations] ? current.Item3 + 1 : current.Item3, current.Item4 + 1));
                visited[row - 1, col] = true;
            }
            return moves;
        }

    }
}
