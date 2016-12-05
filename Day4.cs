using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    public class Day4
    {
        public void calculate1()
        {

            IEnumerable<string> input = File.ReadAllLines("input\\day4.txt");
            var validRooms = new List<Tuple<string, string>>();
            int result = 0;
            var chars = "abcdefghijklmnopqrstuvwxyz";
            foreach (var line in input)
            {
                var checksum = line.Substring(line.IndexOf('[') + 1).TrimEnd(']');
                var sectorId = Int32.Parse(line.Substring(line.LastIndexOf('-') + 1, line.IndexOf('[') - 1 - line.LastIndexOf('-')));
                var roomwithDashes = line.Substring(0, line.LastIndexOf('-'));
                var room = roomwithDashes.Replace("-", "");
                var letters = room.ToList();
                var letterCounts = letters.GroupBy(c => c).ToDictionary(x => x.Key, x => x.Count());
                var orderedCounts = new List<KeyValuePair<char, int>>(letterCounts).OrderByDescending(x => x.Value).ThenBy(x => x.Key);
                int i = 0;
                foreach (var c in checksum)
                {
                    if (orderedCounts.ElementAt(i).Key != c)
                    {
                        break;
                    }
                    if (i == checksum.Length - 1)
                    {
                        result += sectorId;
                        var sb = new StringBuilder();
                        foreach (var l in roomwithDashes)
                        {
                            if (l == '-')
                            {
                                sb.Append(" ");
                                continue;
                            }
                            int s = chars.IndexOf(l);
                            s = (s + sectorId) % 26;
                            sb.Append(chars[s]);
                        }
                        var roomname = sb.ToString();
                        if (roomname.Contains("north"))
                        {
                            Console.WriteLine(sb.ToString() + " " + sectorId);
                        }
                    }
                    i++;

                }
            }
            Console.WriteLine("sector sum: " + result);
        }
    }
}
