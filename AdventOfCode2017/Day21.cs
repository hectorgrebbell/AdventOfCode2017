using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2017
{
    /// <summary>
    /// http://adventofcode.com/2017/day/21
    /// </summary>
    internal class Day21 : IDay<int, IEnumerable<string>>
    {
        public IEnumerable<string> Input
        {
            get
            {
                return File.ReadAllLines("../../InputFiles/Day21.txt");
            }
        }

        public int Part1(IEnumerable<string> input)
        {
            return Simulate(input, 5);
        }

        public int Part2(IEnumerable<string> input)
        {
            return Simulate(input, 18);
        }

        // Maps each 2x2/3x3 grid to an int. Uses that '#' is odd and '.' isn't -
        // meaning bitshifting can result in unique keys
        private int ToKey(string[] key, int yPos, int xPos, int keySize)
        {
            var ret = key[yPos][xPos] ^ key[yPos][xPos + 1] << 1 ^
                      key[yPos + 1][xPos] << 2 ^ key[yPos + 1][xPos + 1] << 3;
            if (keySize == 3)
            {
                ret = ret ^ key[yPos][xPos + 2] << 4 ^ key[yPos + 1][xPos + 2] << 5 ^
                    key[yPos + 2][xPos] << 6 ^ key[yPos + 2][xPos + 1] << 7 ^ key[yPos + 2][xPos + 2] << 8;
            }
            return ret;
        }

        // Maps all rotations / flips for each key
        private IEnumerable<Tuple<int, string[]>> Translate(string key, string[] value)
        {
            if (key.Length == 5)
            {
                return new Tuple<int, string[]>[]
                {
                    new Tuple<int,string[]>(key[0] ^ key[1] << 1 ^ key[3] << 2 ^ key[4] << 3, value),
                    new Tuple<int,string[]>(key[3] ^ key[4] << 1 ^ key[0] << 2 ^ key[1] << 3, value),
                    new Tuple<int,string[]>(key[1] ^ key[0] << 1 ^ key[4] << 2 ^ key[3] << 3, value),
                    new Tuple<int,string[]>(key[3] ^ key[0] << 1 ^ key[4] << 2 ^ key[1] << 3, value),
                    new Tuple<int,string[]>(key[4] ^ key[1] << 1 ^ key[3] << 2 ^ key[0] << 3, value),
                    new Tuple<int,string[]>(key[4] ^ key[3] << 1 ^ key[1] << 2 ^ key[0] << 3, value),
                    new Tuple<int,string[]>(key[1] ^ key[4] << 1 ^ key[0] << 2 ^ key[3] << 3, value)
            };
            }
            return new Tuple<int, string[]>[]
            {
                new Tuple<int,string[]>(key[0] ^ key[1] << 1 ^ key[2] << 4 ^ key[4] << 2 ^ key[5] << 3 ^ key[6] << 5 ^ key[8] << 6 ^ key[9] << 7 ^ key[10] << 8, value),
                new Tuple<int,string[]>(key[8] ^ key[9] << 1 ^ key[10] << 4 ^ key[4] << 2 ^ key[5] << 3 ^ key[6] << 5 ^ key[0] << 6 ^ key[1] << 7 ^ key[2] << 8, value),
                new Tuple<int,string[]>(key[2] ^ key[1] << 1 ^ key[0] << 4 ^ key[6] << 2 ^ key[5] << 3 ^ key[4] << 5 ^ key[10] << 6 ^ key[9] << 7 ^ key[8] << 8, value),
                new Tuple<int,string[]>(key[8] ^ key[4] << 1 ^ key[0] << 4 ^ key[9] << 2 ^ key[5] << 3 ^ key[1] << 5 ^ key[10] << 6 ^ key[6] << 7 ^ key[2] << 8, value),
                new Tuple<int,string[]>(key[10] ^ key[6] << 1 ^ key[2] << 4 ^ key[9] << 2 ^ key[5] << 3 ^ key[1] << 5 ^ key[8] << 6 ^ key[4] << 7 ^ key[0] << 8, value),
                new Tuple<int,string[]>(key[0] ^ key[4] << 1 ^ key[8] << 4 ^ key[1] << 2 ^ key[5] << 3 ^ key[9] << 5 ^ key[2] << 6 ^ key[6] << 7 ^ key[10] << 8, value),
                new Tuple<int,string[]>(key[10] ^ key[9] << 1 ^ key[8] << 4 ^ key[6] << 2 ^ key[5] << 3 ^ key[4] << 5 ^ key[2] << 6 ^ key[1] << 7 ^ key[0] << 8, value),
                new Tuple<int,string[]>(key[2] ^ key[6] << 1 ^ key[10] << 4 ^ key[1] << 2 ^ key[5] << 3 ^ key[9] << 5 ^ key[0] << 6 ^ key[4] << 7 ^ key[8] << 8, value),
            };
        }

        private int Simulate(IEnumerable<string> input, int nIterations)
        {
            // Rules are a map of the key grid (keyed as an int - see ToKey) to the
            // new grid to replace with.
            var rules = input.SelectMany(l =>
                Translate(
                    l.Substring(0, l.IndexOf(' ')),
                    l.Substring(l.IndexOf('>') + 2).Trim().Split('/')))
                .GroupBy(kv => kv.Item1)
                .ToDictionary(kv => kv.Key, kv => kv.First().Item2);

            var currGrid = new string[]
            {
                ".#.",
                "..#",
                "###"
            };

            while (--nIterations >= 0)
            {
                var currentSplit = (currGrid.Length % 2 == 0) ? 2 : 3;
                var nextSplit = currentSplit + 1;
                var nextSize = (currGrid.Length / currentSplit) * nextSplit;

                var nextGrid = new StringBuilder[nextSize];

                var nextY = 0;
                for (var currY = 0; currY < currGrid.Length; 
                    currY += currentSplit, nextY += nextSplit)
                {
                    for (var i = 0; i < nextSplit; ++i) {
                        nextGrid[nextY + i] = new StringBuilder();
                    }

                    var nextX = 0;
                    for (var currX = 0; currX < currGrid.Length;
                        currX += currentSplit, nextX += nextSplit)
                    {
                        var replaceWith = rules[ToKey(currGrid, currY, currX, currentSplit)];

                        for (var i = 0; i < nextSplit; ++i)
                        {
                            nextGrid[nextY + i].Append(replaceWith[i]);
                        }
                    }
                }

                currGrid = new string[nextSize];
                for (var i = 0; i < nextSize; ++i)
                {
                    currGrid[i] = nextGrid[i].ToString();
                }
            }

            return currGrid.Select(r => r.Sum(c => c == '#' ? 1 : 0)).Sum();
        }
    }
}
