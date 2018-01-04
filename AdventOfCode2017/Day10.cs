using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017
{
    /// <summary>
    /// http://adventofcode.com/2017/day/10
    /// </summary>
    internal class Day10 : IDay<string, string>
    {
        public string Input
        {
            get
            {
                return "97,167,54,178,2,11,209,174,119,248,254,0,255,1,64,190";
            }
        }


        public string Part1(string input)
        {
            var asInts = input.Split(',').Select(i => int.Parse(i));

            var skip = 0;
            var pos = 0;
            var list = new int[256];
            for (var i = 0; i < 255; ++i) { list[i] = i; }

            foreach (var i in asInts)
            {
                for (var p = 0; p < i / 2; ++p)
                {
                    var p1 = (pos + p) % list.Length;
                    var p2 = (pos + i - p - 1) % list.Length;

                    var temp = list[p1];
                    list[p1] = list[p2];
                    list[p2] = temp;
                }
                pos += (i + skip++) % list.Length;
            }

            return (list[0] * list[1]).ToString();
        }

        public string Part2(string input)
        {
            IEnumerable<char> rInput = (input as IEnumerable<char>).Concat(new char[] { (char)17, (char)31, (char)73, (char)47, (char)23 });

            var skip = 0;
            var pos = 0;
            var list = new byte[256];
            for (var i = 0; i < list.Length; ++i) { list[i] = (byte)i; }

            for (var r = 0; r < 64; ++r)
            {
                foreach (var i in rInput)
                {
                    for (var p = 0; p < i / 2; ++p)
                    {
                        var p1 = (pos + p) % list.Length;
                        var p2 = (pos + i - p - 1) % list.Length;

                        var temp = list[p1];
                        list[p1] = list[p2];
                        list[p2] = temp;
                    }
                    pos += (i + skip++) % list.Length;
                    skip %= list.Length;
                }
            }

            var test = list.Take(16);
            var hash = 0;
            foreach (var el in test) { hash ^= el; }

            var ret = new byte[list.Length / 16];
            var pR = 0;
            byte cur = 0;
            for (var i = 0; i < list.Length; ++i)
            {
                if (i / 16 > pR)
                {
                    ret[pR++] = cur;
                    cur = 0;
                }
                cur ^= list[i];
            }
            ret[(list.Length - 1) / 16] = cur;

            return BitConverter.ToString(ret).Replace("-", "");
        }
    }
}
