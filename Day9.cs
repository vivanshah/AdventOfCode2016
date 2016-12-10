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
    public class Day9
    {
        public void calculate1()
        {
            var st = new Stopwatch();
            st.Start();
            var input = File.ReadLines("input\\day9.txt").First();
           // var input = "X(8x2)(3x3)ABCY";
            int length = 0;
            bool inParen = false;

            for(int x = 0; x < input.Length;)
            { 
                if(input[x] == '(' && !inParen)
                {
                    inParen = true;
                }
                if (inParen)
                {
                    //calc length to add and jump to new position
                    int endParen = input.IndexOf(')', x);
                    var instruction = input.Substring(x+1, endParen-x-1).Split('x');
                    var dupeLength = int.Parse(instruction.First());
                    var dupeCount = int.Parse(instruction.Last());
                    length += dupeLength * dupeCount;
                    x = endParen + dupeLength+1;
                    inParen = false;
                }
                else
                {
                    length++;
                    x++;
                }
            }


            st.Stop();
            Console.WriteLine(length + " in " + st.Elapsed.TotalMilliseconds);
        }
     
    }
}
