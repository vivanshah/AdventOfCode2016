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
    public class Day12
    {
        int a, b, c, d;
        public void calculate1()
        {
            var lines = File.ReadAllLines("input\\day12.txt");
            a = b = c = d = 0;
            for (int x = 0; x < lines.Length;)
            {
                var bits = lines[x].Split(' ');
                switch (bits.First())
                {
                    case "cpy":
                        int cpyval = GetValue(bits[1]);
                        SetValue(bits[2], cpyval);
                        x++;
                        break;
                    case "inc":
                        Increment(bits[1]);
                        x++;
                        break;
                    case "dec":
                        Decrement(bits[1]);
                        x++;
                        break;
                    case "jnz":
                        int flag = GetValue(bits[1]);
                        if (flag != 0)
                        {
                            int jumpVal = int.Parse(bits[2]);
                            x += jumpVal;
                        }
                        else
                        {
                            x++;
                        }
                        break;

                }
            }

            Console.WriteLine("a: " + a + " b: " + b + " c: " + c + " d: " + d);
        }

        public void calculate2()
        {
            var lines = File.ReadAllLines("input\\day12.txt");
            a = b = c = d = 0;
            c = 1;
            for (int x = 0; x < lines.Length;)
            {
                var bits = lines[x].Split(' ');
                switch (bits.First())
                {
                    case "cpy":
                        int cpyval = GetValue(bits[1]);
                        SetValue(bits[2], cpyval);
                        x++;
                        break;
                    case "inc":
                        Increment(bits[1]);
                        x++;
                        break;
                    case "dec":
                        Decrement(bits[1]);
                        x++;
                        break;
                    case "jnz":
                        int flag = GetValue(bits[1]);
                        if (flag != 0)
                        {
                            int jumpVal = int.Parse(bits[2]);
                            x += jumpVal;
                        }
                        else
                        {
                            x++;
                        }
                        break;

                }
            }

            Console.WriteLine("a: " + a + " b: " + b + " c: " + c + " d: " + d);

        }


        void Decrement(string s)
        {
            if (s == "a")
            {
                a--;
            }
            if (s == "b")
            {
                b--;
            }
            if (s == "c")
            {
                c--;
            }
            if (s == "d")
            {
                d--;
            }
        }
        void Increment(string s)
        {
            if (s == "a")
            {
                a++;
            }
            if (s == "b")
            {
                b++;
            }
            if (s == "c")
            {
                c++;
            }
            if (s == "d")
            {
                d++;
            }
        }
        void SetValue(string s, int value)
        {
            if (s == "a")
            {
                a = value;
            }
            if (s == "b")
            {
                b = value;
            }
            if (s == "c")
            {
                c = value;
            }
            if (s == "d")
            {
                d = value;
            }
        }
        int GetValue(string s)
        {
            if (s == "a")
            {
                return a;
            }
            if (s == "b")
            {
                return b;
            }
            if (s == "c")
            {
                return c;
            }
            if (s == "d")
            {
                return d;
            }
            return int.Parse(s);
        }


    }
}
