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
    public class Day19
    {
        public class Node
        {
            public int value;
            public Node next;
            public Node previous;
        }
        public void calculate1()
        {
            Console.SetBufferSize(400, 10000);
            Console.SetWindowSize(140, 45);
            var st = new Stopwatch();
            st.Start();
            int s = 3018458;
            Node n = new Node() { value = 1 };
            var start = n;
            for (int x = 2; x <= s; x++)
            {
                n.next = new Node() { value = x };
                n = n.next;
            }
            n.next = start;
            while (start.value != start.next.value)
            {
                start.next = start.next.next;
               // Console.WriteLine("current " + start.value + " next: " + start.next.value);
                start = start.next;
                
            }
            st.Stop();
            Console.WriteLine(start.value + " in " + st.Elapsed.TotalMilliseconds);
        }


        public void calculate2()
        {
            Console.SetBufferSize(400, 10000);
            Console.SetWindowSize(140, 45);
            var st = new Stopwatch();
            st.Start();
            int s = 3018458;
            Node n = new Node() { value = 1 };
            var start = n;
            Node nodeToOrphan = null;
            for (int x = 2; x <= s; x++)
            {
                n.next = new Node() { value = x, previous = n };
                n = n.next;
                if (x == (s / 2)+1)
                {
                    nodeToOrphan = n;
                }
            }
            n.next = start;
            start.previous = n;
            while (start.value != start.next.value)
            {
                var oldOrphan = nodeToOrphan;
                Orphan(nodeToOrphan);
                if(s%2 == 0)
                {
                    nodeToOrphan = oldOrphan.next;
                } else
                {
                    nodeToOrphan = oldOrphan.next.next;
                }
                
                s--;
                start = start.next;

            }
            st.Stop();
            Console.WriteLine(start.value + " in " + st.Elapsed.TotalMilliseconds);
        }


        private void Orphan(Node n)
        {
            var oldPrev = n.previous;
            n.previous.next = n.next;
            n.next.previous = oldPrev;
        }
    }
}
