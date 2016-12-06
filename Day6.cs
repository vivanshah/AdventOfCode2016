using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    public class Day6
    {
        public void calculate()
        {

            IEnumerable<string> input = File.ReadAllLines("input\\day6.txt");
            var part1 = new StringBuilder();
            var part2 = new StringBuilder();
            var counts = new Dictionary<int, Dictionary<char, int>>();
            foreach (var line in input)
            {
                char c;
                for(int x = 0; x < line.Length; x++)
                {
                    c = line[x];
                    if (!counts.ContainsKey(x)) {
                        counts[x] = new Dictionary<char, int>();
                    }
                    if (!counts[x].ContainsKey(c))
                    {
                        counts[x][c] = 1;
                    }
                    else
                    {
                        counts[x][c]++;
                    }
                }
            }
            IEnumerable<KeyValuePair<char, int>> ordered;
            foreach(var p in counts.Keys)
            {
                ordered = counts[p].OrderByDescending(x => x.Value);
                part1.Append(ordered.First().Key);
                part2.Append(ordered.Last().Key);
            }
            Console.WriteLine(part1.ToString());
            Console.WriteLine(part2.ToString());
        }
    }
}
