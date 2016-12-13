using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Program
    {
        static void Main(string[] args)
        {
            //new Day12().calculate1();
            var d12 = new Day12();
            for (int x = 0; x < 20; x++)
            {
                d12.calculate2();
            }
            //Console.ReadLine();
        }
    }
}
