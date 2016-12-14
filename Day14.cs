using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace AdventOfCode2016
{
    public class Day14
    {
        
        MD5 md5 = MD5.Create();
        private string GetString(string toHash)
        {
            var salt = "ahsbgdzn";
            //var salt = "abc";
            byte[] input = Encoding.ASCII.GetBytes(salt + toHash);
            byte[] hash = md5.ComputeHash(input);
            var hex = new StringBuilder();
            for (int h = 0; h < 2016; h++)
            {
                
                for (int i = 0; i < hash.Length; i++)
                {
                    hex.Append(hash[i].ToString("X2"));
                }
                hash = md5.ComputeHash(Encoding.ASCII.GetBytes(hex.ToString().ToLower()));
                hex.Clear();
            }
            for (int i = 0; i < hash.Length; i++)
            {
                hex.Append(hash[i].ToString("X2"));
            }
            return hex.ToString();
        }
        public void calculate1()
        {
            Console.SetBufferSize(400, 10000);
            Console.SetWindowSize(240, 72);
            var st = new Stopwatch();
            st.Start();

            int x = 0;
            int keyNum = 0;
            var keys = new List<Tuple<int,int, char>>();
            var findBy = new Dictionary<char, List<int>>();
            char? three;
            List<char> five;
            while (true)
            {

                var code = GetString(x.ToString("D"));
                three = HasTriple(code);
                five = HasFiveRepeated(code);
                if (three.HasValue)
                {

                       // Console.WriteLine("found sequence of 3 " + three.Value + " at " + x);
                        if (findBy.ContainsKey(three.Value))
                        {
                            findBy[three.Value].Add(x);
                        }
                        else
                        {
                            findBy.Add(three.Value, new List<int> { x });
                        }
                }
                
                if (five.Count > 0)
                {
                    foreach(var f in five)
                    {
                        if (findBy.ContainsKey(f))
                        {
                            var match = findBy[f].Where(t => t >= x - 1000 && t != x).ToList();
                            foreach (var m in match)
                            {
                                keys.Add(Tuple.Create(m, x, f));
                                findBy[f].Remove(m);
                                Console.WriteLine("found sequence of 5 " + f + " at index " + x + ". Previous set of 3 at " + m);
                                if (findBy[f].Count == 0)
                                {
                                    findBy.Remove(f);
                                }
                            }
                        }
                    }
                    if (keys.Count > 80) break;
                }
                x++;
            }
            keys = keys.OrderBy(t => t.Item1).ToList();
            for (int k=0;k < keys.Count;k++)
            {
                Console.WriteLine(k+1 + "\t" + keys[k].Item3 +  "\t" + keys[k].Item1 + "\t" + keys[k].Item2 + "\t3: " + GetString(keys[k].Item1.ToString("D")) + "\t5: " + GetString(keys[k].Item2.ToString("D")));
            }
            st.Stop();
            Console.WriteLine(x + " in " + st.Elapsed.TotalMilliseconds);
        }
        public char? HasTriple(string s)
        {
            var result = new List<char>();
            for (int x = 2; x < s.Length; x++)
            {
                if (s[x] == s[x - 1] && s[x - 1] == s[x - 2])
                {
                    return s[x];
                }
            }
            return null;
        }
        public List<char> HasFiveRepeated(string s)
        {
            var result = new List<char>();
            for (int x = 4; x < s.Length; x++)
            {
                if (s[x] == s[x - 1] && s[x - 1] == s[x - 2] && s[x - 2] == s[x - 3] && s[x - 3] == s[x - 4])
                {
                    result.Add(s[x]);
                }

            }
            return result;
        }





    }
}
