
using System;

namespace AdventOfCode2017
{
    /// <summary>
    /// http://adventofcode.com/2017/day/15
    /// </summary>
    internal class Day15 : IDay<int, Tuple<UInt64, UInt64>>
    {
        public Tuple<UInt64, UInt64> Input
        {
            get
            {
                return new Tuple<UInt64, UInt64>(634, 301);
            }
        }

        public int Part1(Tuple<UInt64, UInt64> input)
        {
            var genAStart = input.Item1;
            var genBStart = input.Item2;
            UInt64 prod;
            int count = 0;

            for (var i = 0; i < 40000000; ++i)
            {
                prod = genAStart * 16807ul;
                // Faster than %
                genAStart = (prod & 0x7fffffff) + (prod >> 31);
                genAStart = genAStart >> 31 > 0 ? genAStart - 0x7fffffff : genAStart;
                prod = genBStart * 48271;
                genBStart = (prod & 0x7fffffff) + (prod >> 31);
                genBStart = genBStart >> 31 > 0 ? genBStart - 0x7fffffff : genBStart;
                if ((genAStart & 0xFFFF) == (genBStart & 0xFFFF)) ++count;
            }

            return count;
        }

        public int Part2(Tuple<UInt64, UInt64> input)
        {
            var genAStart = input.Item1;
            var genBStart = input.Item2;
            UInt64 prod;
            int count = 0;

            for (var i = 0; i < 5000000; ++i)
            {
                do
                {
                    prod = genAStart * 16807ul;
                    genAStart = (prod & 0x7fffffff) + (prod >> 31);
                    genAStart = genAStart >> 31 > 0 ? genAStart - 0x7fffffff : genAStart;
                } while ((genAStart & 3) > 0);
                do
                {
                    prod = genBStart * 48271ul;
                    genBStart = (prod & 0x7fffffff) + (prod >> 31);
                    genBStart = genBStart >> 31 > 0 ? genBStart - 0x7fffffff : genBStart;
                } while ((genBStart & 7) > 0);
                if ((genAStart & 0xFFFF) == (genBStart & 0xFFFF)) ++count;
            }

            return count;
        }
    }
}
