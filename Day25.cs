using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    public class Day25
    {
        public void calculate1()
        {
            int a, b, c, d;
            var sw = new Stopwatch();
            sw.Start();
            var lines = File.ReadAllLines("input\\day25.txt");
            a = b = d = c = 0;
            int x = 0;
            int cpyval, flag;
            int l = lines.Length;
            var dict = new string[l][];
            var firstChars = new char[l][];
            var instructions = new char[l];
            var jints = new int[l];
            var jflags = new int[l];
            var tflags = new int[l];
            var oflags = new int[l];
            var cints = new int[l];
            char[] args;
            char ch = new char { };

            for (; x < l; x++)
            {
                instructions[x] = lines[x][0];
                dict[x] = lines[x].Split(' ');
                firstChars[x] = dict[x].Skip(1).Select(v => v[0]).ToArray();
                ch = dict[x][1][0];
                if (instructions[x] == 'j')
                {
                    if (dict[x][2][0] < 'a')
                    {
                        jints[x] = int.Parse(dict[x][2]);
                    }
                    if (ch < 'a')
                    {
                        jflags[x] = int.Parse(dict[x][1]);
                    }
                }
                else if (instructions[x] == 'c' && ch < 'a')
                {
                    cints[x] = int.Parse(dict[x][1]);
                }
                else if (instructions[x] == 't')
                {
                    if (ch < 'a')
                    {
                        tflags[x] = int.Parse(dict[x][1]);
                    }
                }
                else if (instructions[x] == 't')
                {
                    if (ch < 'a')
                    {
                        oflags[x] = int.Parse(dict[x][1]);
                    }
                }
            }


            int u = 1;
            for (; u < int.MaxValue; u++)
            {
                x = 0;
                a = u;
                bool output = true;
                int threshold =30;
                int count = 0;
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
                            //lookahead and find all incs
                            int add = 1;
                            if (instructions[x + 1] == 'j')
                            {
                                add = 1;
                            }
                            switch (args[0])
                            {
                                case 'a': a += add; break;
                                case 'b': b += add; break;
                                case 'c': c += add; break;
                                case 'd': d += add; break;

                            }
                            x += add;
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
                            var flagIsRegister = true;
                            switch (args[0])
                            {
                                case 'a': flag = a; break;
                                case 'b': flag = b; break;
                                case 'c': flag = c; break;
                                case 'd': flag = d; break;
                                default:
                                    flag = jflags[x];
                                    flagIsRegister = false;
                                    break;
                            }
                            int value = 0;
                            switch (args[1])
                            {
                                case 'a': value = a; break;
                                case 'b': value = b; break;
                                case 'c': value = c; break;
                                case 'd': value = d; break;
                                default:
                                    value = jints[x];
                                    break;
                            }
                            if (flag == 0)
                            {
                                x++;
                                continue;
                            }

                            /*flag is one so we are jumping.  lets look for simple loops (-2 jump with 2 preceding instructions either inc or dec, 
                              and one of the arguments of the inc or dec is the flag parameter of this jump */
                            if (value == -2 && flagIsRegister)
                            {
                                char flagRegister = args[0];
                                char multRegister = (char)0;
                                bool inc = false;
                                if ((instructions[x - 1] == 'i' || instructions[x - 1] == 'd') && firstChars[x - 1][0] == flagRegister)
                                {
                                    //previous instruction updates flag, 2 back is mult register
                                    if ((instructions[x - 2] == 'i' || instructions[x - 2] == 'd') && firstChars[x - 2][0] >= 'a')
                                    {
                                        multRegister = firstChars[x - 2][0];
                                        inc = instructions[x - 2] == 'i';
                                    }
                                }
                                else if ((instructions[x - 2] == 'i' || instructions[x - 2] == 'd') && firstChars[x - 2][0] == flagRegister)
                                {
                                    if ((instructions[x - 1] == 'i' || instructions[x - 1] == 'd') && firstChars[x - 1][0] >= 'a')
                                    {
                                        multRegister = firstChars[x - 1][0];
                                        inc = instructions[x - 1] == 'i';
                                    }
                                }
                                int flagRegisterValue = 0;
                                switch (flagRegister)
                                {
                                    case 'a':
                                        flagRegisterValue = a; break;
                                    case 'b':
                                        flagRegisterValue = b; break;
                                    case 'c':
                                        flagRegisterValue = c; break;
                                    case 'd':
                                        flagRegisterValue = d; break;
                                }
                                switch (multRegister)
                                {
                                    case 'a':
                                        a += (inc ? 1 : -1) * Math.Abs(flagRegisterValue); x++; continue;
                                    case 'b':
                                        b += (inc ? 1 : -1) * Math.Abs(flagRegisterValue); x++; continue;
                                    case 'c':
                                        c += (inc ? 1 : -1) * Math.Abs(flagRegisterValue); x++; continue;
                                    case 'd':
                                        d += (inc ? 1 : -1) * Math.Abs(flagRegisterValue); x++; continue;
                                }
                            }
                            else
                            {
                                x += value;
                            }
                            break;
                        case 't':
                            int target = 0;
                            switch (args[0])
                            {
                                case 'a': target = a; break;
                                case 'b': target = b; break;
                                case 'c': target = c; break;
                                case 'd': target = d; break;
                                default:
                                    target = tflags[x];
                                    break;
                            }
                            if (target == 0)
                            {
                                instructions[x] = 'i';
                            }
                            else
                            {
                                if (x + target > l - 1) { x++; continue; }
                                var orig = instructions[x + target];
                                switch (orig)
                                {
                                    case 'j':
                                        instructions[x + target] = 'c';
                                        cints[x + target] = jflags[x + target]; break;
                                    case 'c':
                                        instructions[x + target] = 'j';
                                        jflags[x + target] = cints[x + target];
                                        jints[x + target] = args[0];
                                        break;
                                    case 'i': instructions[x + target] = 'd'; break;
                                    case 'd':
                                    case 't': instructions[x + target] = 'i'; break;

                                }
                            }
                            x++;
                            break;
                        case 'o':
                            int o = 0;
                            switch (args[0])
                            {
                                case 'a': o = a; break;
                                case 'b': o = b; break;
                                case 'c': o = c; break;
                                case 'd': o = d; break;
                                default:
                                    o = oflags[x];
                                    break;
                            }
                            if(o != (output ? 0 : 1))
                            {
                                x = int.MaxValue - 1;
                            } else
                            {
                                output = !output;
                                count++;
                                if(count == threshold)
                                {
                                    break;
                                }
                            }
                            x++;
                            break;

                    }
                }
                if(count == threshold)
                {
                    break;
                }
            }
            Console.WriteLine(u + " in " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();
            sw.Start();
            sw.Stop();
        }

    }
}
