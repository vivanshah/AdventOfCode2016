﻿using System;
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
            Console.SetBufferSize(400, 10000);
            Console.SetWindowSize(180, 45);
            for (int x = 0; x < 1; x++)
            {
                new Day25().calculate1();
               // new Day25().calculate2();
            }

            Console.ReadLine();
        }
    }
}
