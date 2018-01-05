using System;
using System.Linq;

namespace AdventOfCode2017
{
    /// <summary>
    /// http://adventofcode.com/2017/day/14
    /// </summary>
    internal class Day14 : IDay<int, string>
    {
        public string Input
        {
            get
            {
                return "nbysizxe";
            }
        }

        private static Day10 _Day10 = new Day10();

        public int Part1(string input)
        {
            input += '-';

            var count = 0;
            for (var i = 0; i < 128; ++i)
            {
                var inVal = input + i.ToString();
                var hash = _Day10.Part2(inVal);

                for (var pos = 0; pos < hash.Length; pos += 8)
                {
                    var part = hash.Substring(pos, 8);
                    var partAsInt = int.Parse(part, System.Globalization.NumberStyles.HexNumber);
                    count += NumberOfSetBits(partAsInt);
                }
            }
            return count;
        }

        public int Part2(string input)
        {
            input += '-';

            var grid = new byte[128][];
            for (var i = 0; i < 128; ++i) { grid[i] = new byte[128]; }

            for (var i = 0; i < 128; ++i)
            {
                var inVal = input + i.ToString();
                var hash = _Day10.Part2(inVal);

                for (var pos = 0; pos < hash.Length; pos += 8)
                {
                    var part = hash.Substring(pos, 8);
                    var partAsInt = uint.Parse(part, System.Globalization.NumberStyles.HexNumber);

                    var asBools = Convert.ToString(partAsInt, 2).PadLeft(32, '0').Select(s => (byte)(s - '0')).ToArray();
                    Array.Copy(asBools, 0, grid[i], pos * 4, asBools.Length);
                }
            }

            var count = 0;

            for (var i = 0; i < 128; ++i)
            {
                for (var j = 0; j < 128; ++j)
                {
                    if (grid[i][j] == 1)
                    {
                        ++count;
                        MarkRegion(grid, i, j);
                    }
                }
            }

            return count;
        }

        private static void MarkRegion(byte[][] grid, int i, int j)
        {
            grid[i][j] = 2;
            if (i > 0 && grid[i - 1][j] == 1)
            {
                MarkRegion(grid, i - 1, j);
            }
            if (j > 0 && grid[i][j - 1] == 1)
            {
                MarkRegion(grid, i, j - 1);
            }
            if (i < (grid.Length - 1) && grid[i + 1][j] == 1)
            {
                MarkRegion(grid, i + 1, j);
            }
            if (j < (grid[0].Length - 1) && grid[i][j + 1] == 1)
            {
                MarkRegion(grid, i, j + 1);
            }
        }

        private static int NumberOfSetBits(int i)
        {
            i = i - ((i >> 1) & 0x55555555);
            i = (i & 0x33333333) + ((i >> 2) & 0x33333333);
            return (((i + (i >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
        }
    }
}
