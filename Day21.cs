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
    public class Day21
    {
        public void calculate1()
        {
            var sw = new Stopwatch();
            sw.Start();
            var input = "abcdefgh".ToArray();
            var instr = File.ReadAllLines("input\\day21.txt");
            foreach(var i in instr)
            {
                var tokens = i.Split(' ');
                if(tokens[0] == "rotate" && (tokens[1] == "right" || tokens[1] == "left"))
                {
                    Rotate(ref input, int.Parse(tokens[2]), tokens[1] == "left");
                }
                
                if(tokens[0] == "swap" && tokens[1] == "position")
                {
                    Swap(input, int.Parse(tokens[2]), int.Parse(tokens[5]));
                }
                if (tokens[0] == "swap" && tokens[1] == "letter")
                {
                    Swap(input, tokens[2], tokens[5]);
                }
                if (tokens[0] == "rotate" && tokens[1] == "based")
                {
                    var spaces = Array.IndexOf(input, char.Parse(tokens[6])) + 1;
                    if (spaces > 4)
                    {
                        spaces++;
                    }
                    Rotate(ref input, spaces, false);
                }
                if(tokens[0] == "reverse")
                {
                    Reverse(ref input, int.Parse(tokens[2]), int.Parse(tokens[4]));
                }
                if(tokens[0] == "move")
                {
                    Move(input, int.Parse(tokens[2]), int.Parse(tokens[5]));
                }

            }

            sw.Stop();
            Console.WriteLine(new string(input) + " in " + sw.Elapsed.TotalMilliseconds);
        }


        public void calculate2()
        {
            var sw = new Stopwatch();
            sw.Start();
            var input = "fbgdceah".ToArray();
            var instr = File.ReadAllLines("input\\day21.txt").Reverse();
            foreach (var i in instr)
            {
                var tokens = i.Split(' ');
                if (tokens[0] == "rotate" && (tokens[1] == "right" || tokens[1] == "left"))
                {
                    Rotate(ref input, int.Parse(tokens[2]), tokens[1] != "left");
                }

                if (tokens[0] == "swap" && tokens[1] == "position")
                {
                    Swap(input, int.Parse(tokens[2]), int.Parse(tokens[5]));
                }
                if (tokens[0] == "swap" && tokens[1] == "letter")
                {
                    Swap(input, tokens[2], tokens[5]);
                }
                if (tokens[0] == "rotate" && tokens[1] == "based")
                {
                    for(int x = 1; x < input.Length; x++)
                    {
                        var test = new char[input.Length];
                        Array.Copy(input, test, input.Length);
                        Rotate(ref test, x, true);
                        var testSpaces = Array.IndexOf(test, char.Parse(tokens[6])) + 1;
                        if (testSpaces > 4)
                        {
                            testSpaces++;
                        }
                        Rotate(ref test, testSpaces, false);
                        if (test.SequenceEqual(input))
                        {
                            Rotate(ref test, x, true);
                            input = test;
                            break;
                        }
                    }
                }
                if (tokens[0] == "reverse")
                {
                    Reverse(ref input, int.Parse(tokens[2]), int.Parse(tokens[4]));
                }
                if (tokens[0] == "move")
                {
                    Move(input, int.Parse(tokens[5]), int.Parse(tokens[2]));
                }

            }

            sw.Stop();
            Console.WriteLine(new string(input) + " in " + sw.Elapsed.TotalMilliseconds);
        }
        private void Move(char[] input, int v1, int v2)
        {
            char c = input[v1];
            if (v2 > v1)
            {
                for (int x = v1; x < v2; x++)
                {
                    input[x] = input[x + 1];
                }
            } else
            {
                for(int x = v1;x > v2; x--)
                {
                    input[x] = input[x - 1];
                }
            }
            input[v2] = c;
        }

        private void Reverse(ref char[] input, int i1, int i2)
        {
            //int y = 0;
            //for(int x = i1; (x <= (i2- i1) / 2 +1); x++)
            //{
            //    char c = input[x];
            //    input[x] = input[i2 - y];
            //    input[i2 - y] = c;
            //    y++;
            //}

            Array.Reverse(input, i1, 1+i2 - i1);
        }

        private void Swap(char[] input, string c1, string c2)
        {
            var i = new String(input);
            var i1 = i.IndexOf(c1);
            var i2 = i.IndexOf(c2);
            Swap(input, i1, i2);
        }

        private void Swap(char[] input, int index1, int index2)
        {
            char c = input[index2];
            input[index2] = input[index1];
            input[index1] = c;
        }

        private void Rotate(ref char[] input, int spaces, bool left)
        {
            if(spaces == input.Length)
            {
                return;
            }
            spaces = spaces % input.Length;
            char[] result = new char[input.Length];
            if (left)
            {
                Array.Copy(input, spaces, result, 0, input.Length - spaces);
                Array.Copy(input, 0, result, input.Length - spaces, spaces);
            }
            else
            {
                Array.Copy(input, input.Length - spaces, result, 0, spaces);
                Array.Copy(input, 0, result, spaces, input.Length - spaces);
            }

            input = result;
            return;



        }
    }
}
