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
            foreach (var line in input)
            {
                var checksum = line.Substring(line.IndexOf('[') + 1).TrimEnd(']');
                var sectorId = line.Substring(line.LastIndexOf('-') + 1, line.IndexOf('[') - 1 - line.LastIndexOf('-'));
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
                        result += Int32.Parse(sectorId);
                        validRooms.Add(new Tuple<string, string>(roomwithDashes, sectorId));
                    }
                    i++;

                }
            }

            Console.WriteLine("sector sum: " + result);
            foreach (var rs in validRooms)
            {
                var room = rs.Item1;
                var sector = Int32.Parse(rs.Item2);
                var chars = "abcdefghijklmnopqrstuvwxyz";

                var sb = new StringBuilder();
                foreach (var c in room)
                {
                    if (c == '-')
                    {
                        sb.Append(" ");
                        continue;
                    }
                    int i = chars.IndexOf(c);
                    i = (i + sector) % 26;
                    sb.Append(chars[i]);


                }
                var roomname = sb.ToString();
                if (roomname.Contains("orth"))
                {
                    Console.WriteLine(sb.ToString() + " " + sector);
                }
            }
        }
    }
}
