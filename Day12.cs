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
        public void calculate2()
        {
            int a, b, c, d;
            var sw = new Stopwatch();
            sw.Start();
            var lines = File.ReadAllLines("input\\day12.txt");
            a = b = d = c = 0;
            int x = 0;
            int cpyval, flag;
            int l = lines.Length;
            var dict = new string[l][];
            var firstChars = new char[l][];
            var instructions = new char[l];
            var jints = new int[l];
            var jflags = new int[l];
            var cints = new int[l];
            char[] args;
            char ch = new char { };

            for(;x<l;x++)
            {
                instructions[x] = lines[x][0];
                dict[x] = lines[x].Split(' ');
                firstChars[x] = dict[x].Skip(1).Select(v => v[0]).ToArray();
                ch = dict[x][1][0];
                if (instructions[x] == 'j')
                {
                    jints[x] = int.Parse(dict[x][2]);
                    if (ch < 'a')
                    {
                        jflags[x] = int.Parse(dict[x][1]);
                    }
                } else if (instructions[x] == 'c' && ch < 'a')
                {
                    cints[x] = int.Parse(dict[x][1]);
                }
            }


            foreach (int n in new int[]{0,1})
            {
                a = b = d = 0;
                c = n;
                x = 0;
                while (x < l)
                {
                    args = firstChars[x];
                    switch (instructions[x])
                    {
                        case 'c':
                            switch (args[0])
                            {
                                case 'a': cpyval = a; break;
                                case 'b': cpyval = b; break;
                                case 'c': cpyval = c; break;
                                case 'd': cpyval = d; break;
                                default:
                                    cpyval = cints[x];
                                    break;
                            }
                            switch (args[1])
                            {
                                case 'a': a = cpyval; break;
                                case 'b': b = cpyval; break;
                                case 'c': c = cpyval; break;
                                case 'd': d = cpyval; break;
                            }
                            x++;
                            break;
                        case 'i':
                            switch (args[0])
                            {
                                case 'a': a++; break;
                                case 'b': b++; break;
                                case 'c': c++; break;
                                case 'd': d++; break;

                            }
                            x++;
                            break;
                        case 'd':
                            switch (args[0])
                            {
                                case 'a': a--; break;
                                case 'b': b--; break;
                                case 'c': c--; break;
                                case 'd': d--; break;
                            }
                            x++;
                            break;
                        case 'j':
                            switch (args[0])
                            {
                                case 'a': flag = a; break;
                                case 'b': flag = b; break;
                                case 'c': flag = c; break;
                                case 'd': flag = d; break;
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
                            break;

                    }
                }
                Console.WriteLine("a: " + a + " in " + sw.Elapsed.TotalMilliseconds);
                sw.Reset();
                sw.Start();
            }
            sw.Stop();
        }
    }
}
