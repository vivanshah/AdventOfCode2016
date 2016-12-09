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
                        rect(screen, int.Parse(instructions[1]), int.Parse(instructions[2]));
                        break;
                    case "rotate":
                        if(instructions[1] == "row")
                        {
                            rotateRow(screen, int.Parse(instructions[3]), int.Parse(instructions[4]));
                        }
                        else
                        {
                            rotateColumn(screen, int.Parse(instructions[2]), int.Parse(instructions[3]));
                        }
                        break;
                }
                //PrintScreen(screen);
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
    }
}
