using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    public class Day7
    {


        public void calculate1()
        {

            IEnumerable<string> input = File.ReadAllLines("input\\day7.txt");
       //     var input = new List<String>() { "aaaabba" };
            int abbaIPs = 0;
            
            bool inHypernet = false;
            int x = 0;
            foreach (var line in input)
            {
                char[] queue = new char[4];
                int queuePtr = 0;
                bool abbaInHypernet = false;
                bool hasABBA = false;
                foreach (var c in line)
                {
                    if(c == '[')
                    {
                        inHypernet = true;
                        Array.Clear(queue, 0, 4);
                        queuePtr = 0;
                        continue;
                    } else if (c == ']')
                    {
                        Array.Clear(queue, 0, 4);
                        queuePtr = 0;
                        inHypernet = false;
                        continue;
                    } else
                    {
                        Enqueue(c, queue, ref queuePtr, 4);
                        x++;
                    }

                    if (CheckAbba(queue, queuePtr))
                    {
                        if (inHypernet)
                        {
                            abbaInHypernet = true;
                        } else
                        {
                            hasABBA = true;
                        }
                    }

                }
                if(!abbaInHypernet && hasABBA)
                {
                    abbaIPs++;
                }
            }
            Console.WriteLine(abbaIPs);
       }

        public void calculate2()
        {

            IEnumerable<string> input = File.ReadAllLines("input\\day7.txt");
            int sslIPs = 0;
            int x = 0;
            bool inHypernet = false;
            var abaInHypernet = new List<string>();
            var abaInSupernet = new List<string>();
            foreach (var line in input)
            {
                inHypernet = false;
                x++;
                char[] queue = new char[3];
                int queuePtr = 0;
                abaInHypernet.Clear();
                abaInSupernet.Clear();
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
                                Console.WriteLine("aba: " + aba + "  bab: " + reverseAba + " inHyper: " + String.Join(",", abaInHypernet) + " inSuper: " + String.Join(",",abaInSupernet) + " line: " + line);
                                sslIPs++;
                                break;
                            }
                            
                        }
                        else //in supernet
                        {
                            abaInSupernet.Add(aba);
                            if (abaInHypernet.Contains(reverseAba))
                            {
                                Console.WriteLine("aba: " + aba + "  bab: " + reverseAba + " inHyper: " + String.Join(",", abaInHypernet) + " inSuper: " + String.Join(",", abaInSupernet) + " line: " + line);
                                sslIPs++;
                                break;
                            }
                            
                        }
                    }

                }
            }
            Console.WriteLine(sslIPs);
        }

        private static String ReverseABA(string aba)
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
