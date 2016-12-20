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
    public class Day20
    {
        public void calculate1()
        {
            var sw = new Stopwatch();
            sw.Start();
            var lines = File.ReadAllLines("input\\day20.txt");

            var exclusions = lines.Select(x => Tuple.Create(UInt32.Parse(x.Split('-').First()), UInt32.Parse(x.Split('-').Last()))).ToList();
            exclusions = exclusions.OrderBy(x => x.Item1).ThenBy(x => x.Item2).ToList();

            uint minValue = 0;
            int i = 0;
            Tuple<uint, uint> last = exclusions[0];

            while (i < exclusions.Count)
            {
                var current = Tuple.Create(exclusions[i].Item1, Math.Max(exclusions[i].Item2, last.Item2));
                if (current.Item1 > last.Item2 + 1)
                {
                    minValue = last.Item2 + 1;
                    break;
                }
                if (current.Item1 <= minValue && current.Item2 >= minValue)
                {
                    minValue = current.Item2 + 1;
                }

                last = current;
                i++;
            }


            sw.Stop();
            Console.WriteLine("First IP: " + minValue + " in " + sw.Elapsed.TotalMilliseconds);
        }

        public void calculate2()
        {
            var sw = new Stopwatch();
            sw.Start();

            var lines = File.ReadAllLines("input\\day20.txt");
            var exclusions = lines.Select(x => Tuple.Create(UInt32.Parse(x.Split('-').First()), UInt32.Parse(x.Split('-').Last()))).ToList();
            exclusions = exclusions.OrderBy(x => x.Item1).ThenBy(x => x.Item2).ToList();

            int i = 0;
            uint ipCount = 0;
            Tuple<uint, uint> last = exclusions[0];

            while (i < exclusions.Count)
            {
                var current = Tuple.Create(exclusions[i].Item1, Math.Max(exclusions[i].Item2, last.Item2));

                if (current.Item1 > last.Item2 + 1)
                {
                    ipCount += current.Item1 - (last.Item2 + 1);
                }

                if (current.Item2 == UInt32.MaxValue)
                {
                    break;
                }
                last = current;
                i++;
            }

            sw.Stop();
            Console.WriteLine("Total IPs: " + ipCount + " in " + sw.Elapsed.TotalMilliseconds);
        }
    }
}
