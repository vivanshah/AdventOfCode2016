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
    public class Day10
    {
        public void calculate1()
        {
            var st = new Stopwatch();
            st.Start();
            var input = File.ReadLines("input\\day10.txt");
            //var input = File.ReadLines("input\\day10sample.txt");
            var bots = new Dictionary<int, Tuple<int?, int?>>();
            var chips = new Dictionary<int, int>();
            var botInstructions = new Dictionary<int, Queue<string>>();
            var skip = new Dictionary<int, bool>();
            int answerBot = -1;
            var searchHigh = 61;
            var searchLow = 17;
            var output = new Dictionary<int, int>();
            while (skip.Count != input.Count())
            {
                int x = 0;
                foreach (var line in input)
                {
                    if(skip.ContainsKey(x) && skip[x])
                    {
                        x++;
                        continue;
                    }
                    var tokens = line.Split(' ');
                    if (tokens[0] == "value")
                    {
                        var chip = int.Parse(tokens[1]);
                        var bot = int.Parse(tokens[5]);
                        if (!bots.ContainsKey(bot))
                        {
                            bots.Add(bot, new Tuple<int?, int?>(chip, null));
                        }
                        else
                        {
                            bots[bot] = new Tuple<int?, int?>(bots[bot].Item1, chip);
                        }
                        
                        skip.Add(x, true);
                        x++;
                    }
                    else
                    {
                        var bot = int.Parse(tokens[1]);
                        if (bots.ContainsKey(bot) && bots[bot].Item1.HasValue && bots[bot].Item2.HasValue)
                        {
                            var high = Math.Max(bots[bot].Item1.Value, bots[bot].Item2.Value);
                            var low = Math.Min(bots[bot].Item1.Value, bots[bot].Item2.Value);
                            if(high == searchHigh &&low == searchLow)
                            {
                                answerBot = bot;
                                //break;
                            }
                            if (tokens[5] == "bot")
                            {
                                var lowDestBot = int.Parse(tokens[6]);
                                if (!bots.ContainsKey(lowDestBot))
                                {
                                    bots.Add(lowDestBot, new Tuple<int?, int?>(low, null));
                                } else
                                {
                                    bots[lowDestBot] = new Tuple<int?, int?>(low, bots[lowDestBot].Item1);
                                }

                            } else if (tokens[5] == "output")
                            {
                                var o = int.Parse(tokens[6]);
                                if (!output.ContainsKey(o))
                                {
                                    output.Add(o, low);
                                }
                            }

                            if (tokens[10] == "bot") 
                            {

                                var highDestBot = int.Parse(tokens[11]);
                                if (!bots.ContainsKey(highDestBot))
                                {
                                    bots.Add(highDestBot, new Tuple<int?, int?>(high, null));
                                }
                                else
                                {
                                    bots[highDestBot] = new Tuple<int?, int?>(high, bots[highDestBot].Item1);
                                }
                            } else if (tokens[10] == "output")
                            {
                                var o = int.Parse(tokens[11]);
                                if (!output.ContainsKey(o))
                                {
                                    output.Add(o, high);
                                }
                            }
                            bots[bot] = null;
                            skip.Add(x, true);
                        }

                        x++;
                    }
                }
            }

            st.Stop();
            Console.WriteLine(answerBot + " in " + st.Elapsed.TotalMilliseconds);
            Console.WriteLine(output[0] * output[1] * output[2]);
        }

    
    }
}
