using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    public class Day2
    {
        public string calculate1()
        {

            IEnumerable<string> input = File.ReadAllLines("input\\day2.txt");
            int x = 1;
            int codeNum;
            var result = string.Empty;
            foreach(var direction in input)
            {
                codeNum = 5;
                foreach (var move in direction)
                {
                    switch (move)
                    {
                        case 'L':
                            if(codeNum != 1 && codeNum != 4 && codeNum != 7)
                            {
                                codeNum--;
                            }
                            break;
                        case 'R':
                            if (codeNum != 3 && codeNum != 6 && codeNum != 9)
                            {
                                codeNum++;
                            }
                            break;
                        case 'U':
                            if (codeNum != 1 && codeNum != 2 && codeNum != 3)
                            {
                                codeNum-= 3;
                            }
                            break;
                        case 'D':
                            if (codeNum != 7 && codeNum != 8 && codeNum != 9)
                            {
                                codeNum += 3;
                            }
                            break;
                    }
                }
                result+= codeNum;

            }
            return result;
        }

        public string calculate2()
        {

            IEnumerable<string> input = File.ReadAllLines("input\\day2.txt");
            int x = 1;
            char codeNum;
            var result = string.Empty;
            foreach (var direction in input)
            {
                codeNum = '5';
                foreach (var move in direction)
                {
                    switch (move)
                    {
                        case 'L':
                            if (codeNum != '1' && codeNum != '2' && codeNum != '5' && codeNum !='A' && codeNum != 'D')
                            {
                                switch (codeNum)
                                {
                                    case '3': codeNum = '2'; break;
                                    case '4': codeNum = '3'; break;
                                    case '6': codeNum = '5'; break;
                                    case '7': codeNum = '6'; break;
                                    case '8': codeNum = '7'; break;
                                    case '9': codeNum = '8'; break;
                                    case 'B': codeNum = 'A'; break;
                                    case 'C': codeNum = 'B'; break;
                                }
                            }
                            break;
                        case 'R':
                            if (codeNum != '1' && codeNum != '4' && codeNum != '9' && codeNum != 'C' && codeNum != 'D')
                            {
                                switch (codeNum)
                                {
                                    case '2': codeNum = '3'; break;
                                    case '3': codeNum = '4'; break;
                                    case '5': codeNum = '6'; break;
                                    case '6': codeNum = '7'; break;
                                    case '7': codeNum = '8'; break;
                                    case '8': codeNum = '9'; break;
                                    case 'A': codeNum = 'B'; break;
                                    case 'B': codeNum = 'C'; break;
                                }
                            }
                            break;
                        case 'U':
                            if (codeNum != '5' && codeNum != '2' && codeNum != '1' && codeNum != '4' && codeNum != '9')
                            {
                                switch (codeNum)
                                {
                                    case 'D': codeNum = 'B'; break;
                                    case 'A': codeNum = '6'; break;
                                    case 'B': codeNum = '7'; break;
                                    case 'C': codeNum = '8'; break;
                                    case '6': codeNum = '2'; break;
                                    case '7': codeNum = '3'; break;
                                    case '8': codeNum = '4'; break;
                                    case '3': codeNum = '1'; break;
                                }
                            }
                            break;
                        case 'D':
                            if (codeNum != '5' && codeNum != 'A' && codeNum != 'D' && codeNum != 'C' && codeNum != '9')
                            {
                                switch (codeNum)
                                {
                                    case '1': codeNum = '3'; break;
                                    case '2': codeNum = '6'; break;
                                    case '3': codeNum = '7'; break;
                                    case '4': codeNum = '8'; break;
                                    case '6': codeNum = 'A'; break;
                                    case '7': codeNum = 'B'; break;
                                    case '8': codeNum = 'C'; break;
                                    case 'B': codeNum = 'D'; break;
                                }
                            }
                            break;
                    }
                }
                result += codeNum;

            }
            return result;
        }

    }
}
