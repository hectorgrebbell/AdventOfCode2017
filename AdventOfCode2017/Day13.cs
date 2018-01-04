using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017
{
    /// <summary>
    /// http://adventofcode.com/2017/day/13
    /// </summary>
    internal class Day13 : IDay<int, IEnumerable<string>>
    {
        public IEnumerable<string> Input
        {
            get
            {
                return File.ReadAllLines("../../InputFiles/Day13.txt");
            }
        }

        public int Part1(IEnumerable<string> input)
        {
            var depths = input.Select(l =>
            {
                var tokens = l.Split(':');
                return new Tuple<int, int>(int.Parse(tokens[0]), int.Parse(tokens[1]));
            });

            // If there is a collision (then we need to include the severity.
            return depths.Where(l => l.Item1 % ((l.Item2 - 1) * 2) == 0).Select(l => l.Item1 * l.Item2).Sum();
        }

        public int Part2(IEnumerable<string> input)
        {
            var depths = input.Select(l =>
            {
                var tokens = l.Split(':');
                return new Tuple<int, int>(int.Parse(tokens[0]), int.Parse(tokens[1]));
            }).ToArray();

            // Linq is too slow here (12s vs 50ms)
            // Keep incrementing the delay until no collision occurs.
            var delay = -1;
            int i = 0;
            while (i < depths.Length)
            {
                ++delay;
                for (i = 0; i < depths.Length; ++i)
                {
                    var layer = depths[i];
                    if ((delay + layer.Item1) % ((layer.Item2 - 1) * 2) == 0)
                    {
                        break;
                    }
                }
            }
            return delay;
        }
    }
}
