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
            var sw = new Stopwatch();
            sw.Start();
            var lines = File.ReadAllLines("input\\day12.txt");
            a = b = c = d = 0;
            int cpyval;
            int flag;
            int jumpVal;
            for (int x = 0; x < lines.Length;)
            {
                var bits = lines[x].Split(' ');
                switch (bits.First())
                {
                    case "cpy":
                        cpyval = GetValue(bits[1]);
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
                        flag = GetValue(bits[1]);
                        if (flag != 0)
                        {
                            jumpVal = int.Parse(bits[2]);
                            x += jumpVal;
                        }
                        else
                        {
                            x++;
                        }
                        break;
                }
            }
            sw.Stop();
            Console.WriteLine("a: " + a + " b: " + b + " c: " + c + " d: " + d + " in " + sw.Elapsed.TotalMilliseconds);
        }

        public void calculate2()
        {
            var sw = new Stopwatch();
            sw.Start();
            var lines = File.ReadAllLines("input\\day12.txt");
            a = b = d = 0;
            c = 1;
            int x = 0;
            int cpyval, flag;
            int l = lines.Length;
            var dict = new string[l][];
            var firstChars = new byte[l][];
            var instructions = new byte[l];
            var jints = new int[l];
            var jflags = new int[l];
            var cints = new int[l];
            byte[] bits;
            char ch = new char { };

            for(;x<l;x++)
            {
                instructions[x] = (byte)lines[x][0];
                dict[x] = lines[x].Split(' ');
                firstChars[x] = dict[x].Skip(1).Select(v => (byte)v[0]).ToArray();
                ch = dict[x][1][0];
                if (lines[x][0] == 'j')
                {
                    jints[x] = int.Parse(dict[x][2]);
                    if (ch < 'a')
                    {
                        jflags[x] = int.Parse(dict[x][1]);
                    }
                } else if (lines[x][0] == 'c' && ch < 'a')
                {
                    cints[x] = int.Parse(dict[x][1]);
                }
            }
            
            x = 0;
            while (x < l)
            {
                bits = firstChars[x];
                switch (instructions[x])
                {
                    case 99:
                        switch (bits[0])
                        {
                            case 97: cpyval= a; break;
                            case 98: cpyval = b; break;
                            case 99: cpyval = c; break;
                            case 100: cpyval = d; break;
                            default:
                                cpyval = cints[x];
                                break;
                        }
                        switch (bits[1])
                        {
                            case 97: a = cpyval; break;
                            case 98: b = cpyval; break;
                            case 99: c = cpyval; break;
                            case 100: d = cpyval; break;
                        }
                        x++;
                        continue;
                    case 105:
                        switch (bits[0])
                        {
                            case 97: a++; break;
                            case 98: b++; break;
                            case 99: c++; break;
                            case 100: d++; break;

                        }
                        x++;
                        continue;
                    case 100:
                        switch (bits[0])
                        {
                            case 97: a--; break;
                            case 98: b--; break;
                            case 99: c--; break;
                            case 100: d--; break;
                        }
                        x++;
                        continue;
                    case 106:
                        switch (bits[0])
                        {
                            case 97: flag = a; break;
                            case 98: flag = b; break;
                            case 99: flag = c; break;
                            case 100: flag = d; break;
                            default:
                                flag = jflags[x];
                                break;
                        }
                        if (flag == 0)
                        {
                            x++;
                        }
                        else
                        {
                            x += jints[x];
                        }
                        continue;

                }
            }
            sw.Stop();
            Console.WriteLine("a: " + a + " in " + sw.Elapsed.TotalMilliseconds);
           // Console.WriteLine("Executed " + i + " instructions");

        }


        void Decrement(string s)
        {
            if (s == "a")
            {
                a--;
                return;
            }
            if (s == "b")
            {
                b--;
                return;
            }
            if (s == "c")
            {
                c--;
                return;
            }
            if (s == "d")
            {
                d--;
                return;
            }
        }
        void Increment(string s)
        {
            if (s == "a")
            {
                a++;
                return;
            }
            if (s == "b")
            {
                b++;
                return;
            }
            if (s == "c")
            {
                c++;
                return;
            }
            if (s == "d")
            {
                d++;
                return;
            }
        }
        void SetValue(string s, int value)
        {
            if (s == "a")
            {
                a = value;
                return;
            }
            if (s == "b")
            {
                b = value;
                return;
            }
            if (s == "c")
            {
                c = value;
                return;
            }
            if (s == "d")
            {
                d = value;
                return;
            }
        }


        int GetValue(string s)
        {
            switch (s[0])
            {
                case 'a': return a;
                case 'b': return b;
                case 'c': return c;
                case 'd': return d;
                default:
                    return int.Parse(s);
            }

            
        }


    }
}
