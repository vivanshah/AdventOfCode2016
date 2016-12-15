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
    public class Day15
    {

        public void calculate1()
        {
            Console.SetBufferSize(400, 10000);
            Console.SetWindowSize(240, 72);
            var st = new Stopwatch();
            st.Start();
            IEnumerable<string> input = File.ReadAllLines("input\\day15.txt");
            List<Disc> discs = new List<Disc>();
            foreach(var line in input)
            {
                var bits = line.Split(' ');
                discs.Add(new Disc() { NumPositions = int.Parse(bits[3]), CurrentPosition = int.Parse(bits.Last().TrimEnd('.')) });
            }
            discs.Add(new Disc() { NumPositions = 11, CurrentPosition = 0});
            int t = 0;
            while (true)
            {
                if(AreLinedUp(t, discs))
                {
                    break;
                }
                t++;
            }

            st.Stop();
            Console.WriteLine(t + " in " + st.Elapsed.TotalMilliseconds);
        }

        private bool AreLinedUp(int seconds, List<Disc> discs)
        {
            var linedUp = true;
            int s = 1;
            foreach(var d in discs)
            { 
                if((d.CurrentPosition + seconds + s) % d.NumPositions != 0)
                {
                    linedUp = false;
                }
                s++;
            }
            return linedUp;
        }
        private class Disc
        {
            public int NumPositions { get; set; }
            public int CurrentPosition { get; set; }
        }
    }
}
