using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    public class Day5
    {
        public void calculate1()
        {
            var doorId= "wtnhxymk";
            var sb = new StringBuilder();
            int x = 0;
            var md5 = System.Security.Cryptography.MD5.Create();
            while (true)
            {
                var stringToHash = doorId + x.ToString();
                byte[] input = System.Text.Encoding.ASCII.GetBytes(stringToHash);
                byte[] hash = md5.ComputeHash(input);
                var hex = new StringBuilder();
                for(int i = 0; i < hash.Length; i++)
                {
                    hex.Append(hash[i].ToString("X2"));
                }
                var code = hex.ToString();
                if (code.StartsWith("00000"))
                {
                    sb.Append(code[5]);
                    if(sb.Length == 8)
                    {
                        break;
                    }
                }
                x++;
            }
            Console.WriteLine(sb.ToString());
        }

        public void calculate2()
        {
            var doorId = "wtnhxymk";
            var passCode = new char[8] { '_', '_', '_', '_', '_', '_', '_', '_' };
            int x = 0;
            int y = 0;
            var md5 = System.Security.Cryptography.MD5.Create();
            while (true)
            {
                var stringToHash = doorId + x.ToString();
                byte[] input = System.Text.Encoding.ASCII.GetBytes(stringToHash);
                byte[] hash = md5.ComputeHash(input);
                var hex = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    hex.Append(hash[i].ToString("X2"));
                }
                var code = hex.ToString();
                x++;
                if (code.StartsWith("00000"))
                {
                    var position = (int)char.GetNumericValue(code[5]);
                    var element = code[6];
                    if(position < 0 || position > 7)
                    {
                        continue;
                    }
                    if(passCode[position] == '_')
                    {
                        y++;
                        passCode[position] = element;
                        Console.WriteLine(passCode);
                    }

                    if(y == 8)
                    {
                        Console.WriteLine();
                        break;
                    }

                }
                
            }
            Console.WriteLine(passCode);
        }

    }
}
