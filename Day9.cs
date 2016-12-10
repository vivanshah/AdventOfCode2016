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

        public void calculate2()
        {
            var st = new Stopwatch();
            st.Start();
           var input = File.ReadLines("input\\day9.txt").First();
            //var input = "(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN";

            long length = GetLength(input);

            st.Stop();
            Console.WriteLine(length + " in " + st.Elapsed.TotalMilliseconds);
        }

        private long GetLength(string input)
        {
            long length = 0;
            bool inParen = false;

            for (int x = 0; x < input.Length;)
            {
                if (input[x] == '(' && !inParen)
                {
                    inParen = true;
                }
                if (inParen)
                {
                    //calc length to add and jump to new position
                    int endParen = input.IndexOf(')', x);
                    var instruction = input.Substring(x + 1, endParen - x - 1).Split('x');
                    var dupeLength = int.Parse(instruction.First());
                    var dupeCount = int.Parse(instruction.Last());
                    var substring = input.Substring(endParen + 1, dupeLength);
                    length += GetLength(substring) * dupeCount;
                    x = endParen + dupeLength + 1;
                    inParen = false;
                }
                else
                {
                    length++;
                    x++;
                }
            }
            return length;
        }

    }
}
