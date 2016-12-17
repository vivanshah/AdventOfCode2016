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
    public class Day16
    {

        public void calculate1()
        {
            Console.SetBufferSize(400, 10000);
            Console.SetWindowSize(140, 45);
            var st = new Stopwatch();
            st.Start();
            var input = "10011111011011001".Select(x => x == '1').ToList();
            var space = 35651584;
            var randomData = new List<bool>();
            randomData.AddRange(input);
            int l = input.Count;
            while(l < space)
            {
                randomData.AddRange(Flip(randomData));
                l = randomData.Count;
            }
            var data = randomData;
            var sb = new List<bool>();
            do
            {
                sb.Clear();
                for(int c = 0; c < data.Count && c < space; c += 2)
                {
                    sb.Add(data[c] == data[c + 1]);
                }
                data.Clear();
                data = new List<bool>(sb);

            } while (sb.Count % 2 == 0);



            st.Stop();
            Console.WriteLine(new string(sb.Select(x=>x ? '1' : '0').ToArray()) + " in " + st.Elapsed.TotalMilliseconds);
        }

        private List<bool> Flip(List<bool> v)
        {
            var result = new List<bool>(v.Count+1);
            result.Add(false);
            var count = v.Count - 1;
            for(int x = 0; x <count; x++)
            {
                result.Add(!v[count - x]);
            }
            return result;
        }
    }
}
