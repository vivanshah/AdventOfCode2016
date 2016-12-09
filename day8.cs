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
    public class Day8
    {
        private void rotateRow(bool[,] screen, int row, int positions)
        {
            var newRow = new bool[50];
            for(int x = 50-positions;x<50;x++)
            {
                newRow[(x+positions)%50] = screen[row,x];
            }
            for (int x = 0; x < 50-positions; x++)
            {
                newRow[positions+x] = screen[row,x];
            }
            for(int x = 0; x < 50; x++)
            {
                screen[row, x] = newRow[x];
               // PrintScreen(screen);
            }
        }

        private void rotateColumn(bool[,] screen, int column, int positions)
        {
            var newColumn = new bool[6];
            for (int x = 6 - positions; x < 6; x++)
            {
                newColumn[(x+ positions)%6] = screen[x,column];
            }
            for (int x = 0; x < 6-positions; x++)
            {
                newColumn[positions+x] = screen[x, column];
            }
            for (int x = 0; x < 6; x++)
            {
                screen[x,column] = newColumn[x];
               // PrintScreen(screen);

            }
        }

        private void rect(bool[,] screen, int height, int width)
        {
            for (int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    screen[x, y] = true;
                   // PrintScreen(screen);

                }
            }
        }
        public void calculate1()
        {
            var st = new Stopwatch();
            st.Start();
            IEnumerable<string> input = File.ReadAllLines("input\\day8.txt");
            var screen = new bool[6, 50];
            foreach(var line in input)
            {
                
                var instructions = line.Replace("by", "").Split(new char[] {' ', '=','x'}, StringSplitOptions.RemoveEmptyEntries);
                switch (instructions.First())
                {
                    case "rect":
                        rect(screen, Int32.Parse(instructions[1]), Int32.Parse(instructions[2]));
                        break;
                    case "rotate":
                        if(instructions[1] == "row")
                        {
                            rotateRow(screen, Int32.Parse(instructions[3]), Int32.Parse(instructions[4]));
                        }
                        else
                        {
                            rotateColumn(screen, Int32.Parse(instructions[2]), Int32.Parse(instructions[3]));
                        }
                        break;
                }

                PrintScreen(screen);
            }
            Console.WriteLine(PrintScreen(screen) + " lit pixels");
            st.Stop();
            Console.WriteLine(" in " + st.Elapsed.TotalMilliseconds);
        }
        private int PrintScreen(bool[,] screen)
        {
            Console.Clear();
            int litPixels = 0;
            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 50; x++)
                {
                    if (screen[y, x])
                    {
                        litPixels++;
                    }
                    Console.Write(screen[y, x] ? '#' : ' ');
                }
                Console.Write(Environment.NewLine);
            }
            return litPixels;
        }
        public void calculate2()
        {
            var st = new Stopwatch();
            st.Start();
            IEnumerable<string> input = File.ReadAllLines("input\\day7.txt");
            int sslIPs = 0;
            Parallel.ForEach(input, line => 
            {
                char[] queue = new char[3];
                int queuePtr = 0;
                bool inHypernet = false;
                var abaInHypernet = new HashSet<string>();
                var abaInSupernet = new HashSet<string>();
                string reverseAba = null;
                foreach (var c in line)
                {
                    if (c == '[')
                    {
                        inHypernet = true;
                        Array.Clear(queue, 0, 3);
                        queuePtr = 0;
                        continue;
                    }
                    else if (c == ']')
                    {
                        Array.Clear(queue, 0, 3);
                        queuePtr = 0;
                        inHypernet = false;
                        continue;
                    }
                    else
                    {
                        Enqueue(c, queue, ref queuePtr, 3);
                    }

                    var aba = GetABA(queue, queuePtr);

                    if ( aba != null)
                    {
                        reverseAba = ReverseABA(aba);
                        if (inHypernet)
                        {
                            abaInHypernet.Add(aba);
                            if (abaInSupernet.Contains(reverseAba))
                            {
                                Interlocked.Increment(ref sslIPs);
                                break;
                            }
                            
                        }
                        else //in supernet
                        {
                            abaInSupernet.Add(aba);
                            if (abaInHypernet.Contains(reverseAba))
                            {
                                Interlocked.Increment(ref sslIPs);
                                break;
                            }
                            
                        }
                    }

                }
            });
            st.Stop();
            Console.WriteLine(sslIPs + " in " + st.Elapsed.TotalMilliseconds);
        }

        private static string ReverseABA(string aba)
        {
            return new string(new char[] {aba[1], aba[0], aba[1]});
        }

        private static void Enqueue(char c, char[] queue, ref int queuePtr, int maxSize)
        {
            queue[queuePtr] = c;
            queuePtr++;
            if (queuePtr == maxSize)
            {
                queuePtr = 0;
            }
        }

        private static bool CheckAbba(char[] queue, int queuePtr)
        {
            switch(queuePtr)
            {
                case 0:
                    return queue[0] != queue[1] && queue[1] == queue[2] && queue[0] == queue[3];
                case 1:
                    return queue[1] != queue[2] && queue[2] == queue[3] && queue[1] == queue[0];
                case 2:
                    return queue[2] != queue[3] && queue[3] == queue[0] && queue[2] == queue[1];
                case 3:
                    return queue[3] != queue[0] && queue[0] == queue[1] && queue[3] == queue[2];
                default:
                    return false;
            }
        }

        private string GetABA( char[] queue, int queuePtr)
        {
            switch (queuePtr)
            {
                case 0:
                    if( queue[0] != queue[1] && queue[0] == queue[2])
                    {
                        return new string(queue);
                    }
                    break;
                case 1:
                    if(queue[1] != queue[2] && queue[1] == queue[0])
                    {
                        return new string(new char[] {queue[1], queue[2], queue[0]});
                    }
                    break;
                case 2:
                    if (queue[2] != queue[0] && queue[2] == queue[1])
                    {
                        return new string(new char[] { queue[2], queue[0], queue[1] });
                    }
                    break;
            }
            return null;
        }
    }
}
