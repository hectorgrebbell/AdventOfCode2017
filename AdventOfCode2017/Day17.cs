using System;
using System.Collections.Generic;

namespace AdventOfCode2017
{
    /// <summary>
    /// http://adventofcode.com/2017/day/17
    /// </summary>
    internal class Day17 : IDay<int, int>
    {
        public int Input
        {
            get { return 367; }
        }

        public int Part1(int input)
        {
            var pos = 0;
            var step = input;

            var buffer = new List<int> { 0 };

            for (var i = 1; i <= 2017; ++i)
            {
                pos = (pos + step) % buffer.Count;
                buffer.Insert(++pos, i);
            }

            return buffer[(pos + 1) % 2018];
        }

        public int Part2(int input)
        {
            var pos = 0;
            var step = input;

            var valAfter = 1;

            for (var i = 1; i <= 50000000; ++i)
            {
                pos = ((pos + step) % i) + 1;
                if (pos == 1) { valAfter = i; }
            }
            return valAfter;
        }
    }
}
