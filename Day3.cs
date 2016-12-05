using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    public class Day3
    {
        public void calculate()
        {
            int result = 0;
            string line;
            int[] side = new int[3];
            int smallest;
            int middle;
            int largest;
            var file = new StreamReader("input\\day3.txt");
            while((line = file.ReadLine()) != null)
            {
                side = line.Trim().Split((char[])null, StringSplitOptions.RemoveEmptyEntries).Select(x=>int.Parse(x)).OrderBy(x=>x).ToArray();
                if(side[0] + side[1] > side[2])
                {
                    result++;
                }
            }
            Console.WriteLine(result);
        }


        public void calculate2()
        {
            int result = 0;
            string line;
            int[] col1 = new int[3];
            int[] col2 = new int[3];
            int[] col3 = new int[3];

            int[] cols = new int[3];

            var file = new StreamReader("input\\day3.txt");
            int i = 0;
            while ((line = file.ReadLine()) != null)
            {

                cols = line.Trim().Split((char[])null, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
                col1[i] = cols[0];
                col2[i] = cols[1];
                col3[i] = cols[2];
                i++;
                if (i == 3)
                {
                    Console.WriteLine("col1: " + col1[0] + " " + col1[1] + " " + col1[2]);
                    Console.WriteLine("col2: " + col2[0] + " " + col2[1] + " " + col2[2]);
                    Console.WriteLine("col3: " + col3[0] + " " + col3[1] + " " + col3[2]);
                    //colX will be full, sort and check each one
                    col1 = col1.OrderBy(x => x).ToArray();
                    col2 = col2.OrderBy(x => x).ToArray();
                    col3 = col3.OrderBy(x => x).ToArray();

                    if (col1[0] + col1[1] > col1[2])
                    {
                        result++;
                    }
                    if (col2[0] + col2[1] > col2[2])
                    {
                        result++;
                    }
                    if (col3[0] + col3[1] > col3[2])
                    {
                        result++;
                    }
                    i = 0;
                }

            }
            Console.WriteLine(result);
        }
    }
}
