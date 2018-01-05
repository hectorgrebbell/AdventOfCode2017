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
    internal class Day21 : IDay<int, string[]>
    {
        public string[] Input
        {
            get
            {
                return File.ReadAllLines("../../InputFiles/Day21.txt");
            }
        }

        public int Part1(string[] input)
        {
            return Simulate(input, 5);
        }

        public int Part2(string[] input)
        {
            return FastSolve(input, 18);
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

        public static int FastSolve(string[] input, int nIterations = 18)
        {
            if (nIterations % 3 > 0) return -1;
            // Each 3x3 grid from the rules is identified by a unique ascending baseID.
            // The id's allow arrays to be used instead of more costly lookup structures.

            // For 2x2 a mixture of a similar baseId (id2x2) and raw key (see below) are
            // used becuase the 4 bit ID is small enough that for the _id2ToId3 structure a
            // direct lookup without a translation layer is more performant.

            // Mapping of each rotation/flip of 3x3 grid to its base ID
            var transform3 = new int[512];

            var max3x3Id = -1;

            // Mapping of each 2x2 base ID to the underlying grid
            var _idToArr2 = new string[6];
            // Mapping of each 2x2 raw key to the 3x3 base id which will result
            // after 1 iteration.
            var _id2ToId3 = new int[16];
            // Mapping of each 3x3 base ID to the underlying grid
            var _idToArr3 = new string[128];
            // Mapping of each rotation/flip of 2x2 grid to its base ID
            var transform2 = new int[16];

            // Maps a 3x3 base ID to the 9 child 3x3 grids which will result
            // from 3 iterations.
            var _threeIterCache = new int[128][];
            int[] afterThree = new int[128], tmp = null;

            // inGrid maps the ids currently in the grid to the count of how many of them there are.
            var inGrid = new int[128];

            int i, k1;
            string line;

            {
                // Assumes rules are ordered by size. Processing in reverse lets us build
                // _id2ToId3 without an additional loop.
                i = input.Length - 1;
                // For each 3x3 rule
                int k2, k3, k4, k5, k6, k7, k8;
                for (; (line = input[i])[6] != '='; --i)
                {
                    _idToArr3[++max3x3Id] = line;

                    // '#' ends in a 1 bit. This means by bitshifting for every char
                    // and XORing we get a unique key.
                    // Each transformation can be hardcoded since we know the movements.
                    k1 = 511 & (line[0] ^ line[1] << 1 ^ line[2] << 2 ^ line[4] << 3 ^ line[5] << 4 ^ line[6] << 5 ^ line[8] << 6 ^ line[9] << 7 ^ line[10] << 8);
                    transform3[k1] = max3x3Id;
                    k2 = 511 & (line[8] ^ line[9] << 1 ^ line[10] << 2 ^ line[4] << 3 ^ line[5] << 4 ^ line[6] << 5 ^ line[0] << 6 ^ line[1] << 7 ^ line[2] << 8);
                    transform3[k2] = max3x3Id;
                    k3 = 511 & (line[2] ^ line[1] << 1 ^ line[0] << 2 ^ line[6] << 3 ^ line[5] << 4 ^ line[4] << 5 ^ line[10] << 6 ^ line[9] << 7 ^ line[8] << 8);
                    transform3[k3] = max3x3Id;
                    k4 = 511 & (line[8] ^ line[4] << 1 ^ line[0] << 2 ^ line[9] << 3 ^ line[5] << 4 ^ line[1] << 5 ^ line[10] << 6 ^ line[6] << 7 ^ line[2] << 8);
                    transform3[k4] = max3x3Id;
                    k5 = 511 & (line[10] ^ line[6] << 1 ^ line[2] << 2 ^ line[9] << 3 ^ line[5] << 4 ^ line[1] << 5 ^ line[8] << 6 ^ line[4] << 7 ^ line[0] << 8);
                    transform3[k5] = max3x3Id;
                    k6 = 511 & (line[0] ^ line[4] << 1 ^ line[8] << 2 ^ line[1] << 3 ^ line[5] << 4 ^ line[9] << 5 ^ line[2] << 6 ^ line[6] << 7 ^ line[10] << 8);
                    transform3[k6] = max3x3Id;
                    k7 = 511 & (line[10] ^ line[9] << 1 ^ line[8] << 2 ^ line[6] << 3 ^ line[5] << 4 ^ line[4] << 5 ^ line[2] << 6 ^ line[1] << 7 ^ line[0] << 8);
                    transform3[k7] = max3x3Id;
                    k8 = 511 & (line[2] ^ line[6] << 1 ^ line[10] << 2 ^ line[1] << 3 ^ line[5] << 4 ^ line[9] << 5 ^ line[0] << 6 ^ line[4] << 7 ^ line[8] << 8);
                    transform3[k8] = max3x3Id;
                }
                // BaseID for 2x2
                for (; i >= 0; --i)
                {
                    line = input[i];

                    _idToArr2[i] = line;

                    // Each key - calculated here because we need them twice.
                    k1 = (line[0] ^ line[1] << 1 ^ line[3] << 2 ^ line[4] << 3) & 15;
                    k2 = (line[3] ^ line[4] << 1 ^ line[0] << 2 ^ line[1] << 3) & 15;
                    k3 = (line[1] ^ line[0] << 1 ^ line[4] << 2 ^ line[3] << 3) & 15;
                    k4 = (line[3] ^ line[0] << 1 ^ line[4] << 2 ^ line[1] << 3) & 15;
                    k5 = (line[4] ^ line[1] << 1 ^ line[3] << 2 ^ line[0] << 3) & 15;
                    k6 = (line[4] ^ line[3] << 1 ^ line[1] << 2 ^ line[0] << 3) & 15;
                    k7 = (line[1] ^ line[4] << 1 ^ line[0] << 2 ^ line[3] << 3) & 15;

                    // This step caches the 2*2 raw key -> 3*3 baseId transformation in key form.
                    k8 = transform3[
                        511 & (line[9] ^ line[10] << 1 ^ line[11] << 2 ^
                        line[13] << 3 ^ line[14] << 4 ^ line[15] << 5 ^
                        line[17] << 6 ^ line[18] << 7 ^ line[19] << 8)];

                    // Transform map as before
                    transform2[k1] =
                        transform2[k2] = transform2[k3] =
                        transform2[k4] = transform2[k5] =
                        transform2[k6] = transform2[k7] =
                        i;

                    _id2ToId3[k1] =
                        _id2ToId3[k2] = _id2ToId3[k3] =
                        _id2ToId3[k4] = _id2ToId3[k5] =
                        _id2ToId3[k6] = _id2ToId3[k7] =
                        k8;
                }

            }
            // The initial sequence keys to 133. We need to look up which baseId this maps to.
            k1 = transform3[133];
            inGrid[k1] = 1;

            // 3 iterations at a time. Probably ought to add handling after this for
            // nIterations not divisible by 3.
            for (var iter = 0; iter < nIterations; iter += 3)
            {
                // afterThree contains the counts of IDs which will exist after
                // the 3 iterations
                for (i = 0; i <= max3x3Id; ++i)
                {
                    if (inGrid[i] == 0) continue;
                    // Look up the children for each present ID
                    tmp = _threeIterCache[i] ??
                        (_threeIterCache[i] = GenerateChildren(_idToArr3[i], _id2ToId3, _idToArr2, transform2));

                    k1 = inGrid[i];
                    inGrid[i] = 0;

                    afterThree[tmp[0]] += k1;
                    afterThree[tmp[1]] += k1;
                    afterThree[tmp[2]] += k1;
                    afterThree[tmp[3]] += k1;
                    afterThree[tmp[4]] += k1;
                    afterThree[tmp[5]] += k1;
                    afterThree[tmp[6]] += k1;
                    afterThree[tmp[7]] += k1;
                    afterThree[tmp[8]] += k1;
                }
                tmp = inGrid;
                inGrid = afterThree;
                afterThree = tmp;
            }

            k1 = 0;
            // Count costs for each square.
            for (i = 0; i <= max3x3Id; ++i)
            {
                if (inGrid[i] == 0) continue;
                // '#' ends in a 1 bit. This means by &'ing with 1 and summing we
                // get the total cost.
                line = _idToArr3[i];
                k1 += inGrid[i] * ((line[0] & 1) + (line[1] & 1) + (line[2] & 1) +
                    (line[4] & 1) + (line[5] & 1) + (line[6] & 1) +
                    (line[8] & 1) + (line[9] & 1) + (line[10] & 1));
            }

            return k1;
        }

        private static int[] GenerateChildren(
            string startArr,
            int[] id2ToId3,
            string[] idToArr2,
            int[] transform2)
        {
            // The 3*3 becomes 4 2*2 grids
            var replaceWith00 = idToArr2[transform2[
                15 & (startArr[15] ^ startArr[16] << 1 ^ startArr[20] << 2 ^ startArr[21] << 3)]];
            var replaceWith02 = idToArr2[transform2[
                15 & (startArr[17] ^ startArr[18] << 1 ^ startArr[22] << 2 ^ startArr[23] << 3)]];
            var replaceWith20 = idToArr2[transform2[
                15 & (startArr[25] ^ startArr[26] << 1 ^ startArr[30] << 2 ^ startArr[31] << 3)]];
            var replaceWith22 = idToArr2[transform2[
                15 & (startArr[27] ^ startArr[28] << 1 ^ startArr[32] << 2 ^ startArr[33] << 3)]];

            // The 4 3*3 grids are split into 9 2*2 grids. Instead of doing that
            // we just lift out the values required to generate the key for the 2*2 grid.
            // This can then be mapped to the 2*2 base ID and id2ToId3 is a cache of
            // the 3*3 grid this becomes one iteration later.
            return new int[]
            {
                id2ToId3[15 &(
                replaceWith00[9] ^ replaceWith00[10] << 1 ^
                replaceWith00[13] << 2 ^ replaceWith00[14] << 3)
                ],
                id2ToId3[15 &(
                replaceWith00[11] ^ replaceWith02[9] << 1 ^
                replaceWith00[15] << 2 ^ replaceWith02[13] << 3)
                ],
                id2ToId3[15 &(
                replaceWith02[10] ^ replaceWith02[11] << 1 ^
                replaceWith02[14] << 2 ^ replaceWith02[15] << 3)
                ],
                id2ToId3[15 &(
                replaceWith00[17] ^ replaceWith00[18] << 1 ^
                replaceWith20[9] << 2 ^ replaceWith20[10] << 3)
                ],
                id2ToId3[15 &(
                replaceWith00[19] ^ replaceWith02[17] << 1 ^
                replaceWith20[11] << 2 ^ replaceWith22[9] << 3)
                ],
                id2ToId3[15 &(
                replaceWith02[18] ^ replaceWith02[19] << 1 ^
                replaceWith22[10] << 2 ^ replaceWith22[11] << 3)
                ],
                id2ToId3[15 &(
                replaceWith20[13] ^ replaceWith20[14] << 1 ^
                replaceWith20[17] << 2 ^ replaceWith20[18] << 3)
                ],
                id2ToId3[15 &(
                replaceWith20[15] ^ replaceWith22[13] << 1 ^
                replaceWith20[19] << 2 ^ replaceWith22[17] << 3)
                ],
                id2ToId3[15 &(
                replaceWith22[14] ^ replaceWith22[15] << 1 ^
                replaceWith22[18] << 2 ^ replaceWith22[19] << 3)
                ]
            };
        }
    }
}
