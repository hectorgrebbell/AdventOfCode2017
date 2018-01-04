using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017
{
    /// <summary>
    /// http://adventofcode.com/2017/day/6
    /// </summary>
    internal class Day06 : IDay<int, int[]>
    {
        public int[] Input
        {
            get
            {
                return new int[] { 10, 3, 15, 10, 5, 15, 5, 15, 9, 2, 5, 8, 5, 2, 3, 6 };
            }
        }

        // Each pattern can be represented as a ulong.
        // Each number is assumed to be under 4 bits.
        public static ulong ToKey(IEnumerable<int> vals)
        {
            ulong ret = 0;
            var shift = 0;
            foreach (ulong val in vals)
            {
                ret ^= val << shift;
                shift += 4;
            }
            return ret;
        }


        public int Part1(int[] input)
        {
            input = (int[])input.Clone();

            var patterns = new HashSet<Tuple<ulong, ulong>>();
            var currPat = new Tuple<ulong, ulong>(
                ToKey(input.Take(input.Length / 2)),
                ToKey(input.Skip(input.Length / 2)));
            var steps = 0;

            while (!patterns.Contains(currPat))
            {
                patterns.Add(currPat);
                ++steps;
                int maxIdx = 0;
                for (var i = 0; i < input.Length; ++i)
                {
                    if (input[i] > input[maxIdx])
                    {
                        maxIdx = i;
                    }
                }

                var maxVal = input[maxIdx];
                input[maxIdx] = 0;
                while (maxVal-- > 0)
                {
                    maxIdx = (maxIdx + 1) % input.Length;
                    ++input[maxIdx];
                }
                currPat = new Tuple<ulong, ulong>(
                     ToKey(input.Take(input.Length / 2)),
                     ToKey(input.Skip(input.Length / 2)));
            }

            return steps;
        }

        public int Part2(int[] input)
        {
            input = (int[])input.Clone();

            var patterns = new HashSet<Tuple<ulong, ulong>>();
            var currPat = new Tuple<ulong, ulong>(
                ToKey(input.Take(input.Length / 2)),
                ToKey(input.Skip(input.Length / 2)));

            while (!patterns.Contains(currPat))
            {
                patterns.Add(currPat);
                int maxIdx = 0;
                for (var i = 0; i < input.Length; ++i)
                {
                    if (input[i] > input[maxIdx])
                    {
                        maxIdx = i;
                    }
                }

                var maxVal = input[maxIdx];
                input[maxIdx] = 0;
                while (maxVal-- > 0)
                {
                    maxIdx = (maxIdx + 1) % input.Length;
                    ++input[maxIdx];
                }
                currPat = new Tuple<ulong, ulong>(
                    ToKey(input.Take(input.Length / 2)),
                    ToKey(input.Skip(input.Length / 2)));
            }

            var matchPat = currPat;

            var steps = 0;
            do
            {
                ++steps;
                int maxIdx = 0;
                for (var i = 0; i < input.Length; ++i)
                {
                    if (input[i] > input[maxIdx])
                    {
                        maxIdx = i;
                    }
                }

                var maxVal = input[maxIdx];
                input[maxIdx] = 0;
                while (maxVal-- > 0)
                {
                    maxIdx = (maxIdx + 1) % input.Length;
                    ++input[maxIdx];
                }
                currPat = new Tuple<ulong, ulong>(
                    ToKey(input.Take(input.Length / 2)),
                    ToKey(input.Skip(input.Length / 2)));
            } while (currPat.Item1 != matchPat.Item1 || currPat.Item2 != matchPat.Item2);

            return steps;
        }
    }
}
