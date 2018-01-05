
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017
{
    /// <summary>
    /// http://adventofcode.com/2017/day/24
    /// </summary>
    internal class Day24 : IDay<long, string[]>
    {
        public string[] Input
        {
            get
            {
                return File.ReadAllLines("../../InputFiles/Day24.txt");
            }
        }

        public long Part1(string[] input)
        {
            var workingSet = new List<Tuple<int, int>>();

            foreach (var line in input)
            {
                var parts = line.Split('/');
                workingSet.Add(new Tuple<int, int>(int.Parse(parts[0]), int.Parse(parts[1])));
            }

            workingSet = new List<Tuple<int, int>>(workingSet.OrderByDescending(i => Math.Max(i.Item1, i.Item2)));

            var maxLength = 0l;
            var scoreFor = 0l ;
            return (Best(workingSet, 0, 0, 0, ref maxLength, ref scoreFor));
        }

        public long Part2(string[] input)
        {
            var workingSet = new List<Tuple<int, int>>();

            foreach (var line in input)
            {
                var parts = line.Split('/');
                workingSet.Add(new Tuple<int, int>(int.Parse(parts[0]), int.Parse(parts[1])));
            }

            workingSet = new List<Tuple<int, int>>(workingSet.OrderByDescending(i => Math.Max(i.Item1, i.Item2)));

            var maxLength = 0l;
            var scoreFor = 0l;

            Best(workingSet, 0, 0, 0, ref maxLength, ref scoreFor);
            return scoreFor;
        }

        private static long Best(List<Tuple<int, int>> rem, int length,
            int socket, int currScore, ref long maxLength, ref long scoreFor)
        {
            long best = currScore;
            ++length;
            for (var i = 0; i < rem.Count; ++i)
            {
                var curr = rem[i];
                var toAdd = curr.Item1 + curr.Item2;
                if (curr.Item1 == socket)
                {
                    rem.RemoveAt(i);
                    var score = Best(rem, length, curr.Item2, currScore + toAdd,
                        ref maxLength, ref scoreFor);
                    rem.Insert(i, curr);
                    if (score > best)
                    {
                        best = score;
                        if (length >= maxLength)
                        {
                            if (length > maxLength || score > scoreFor)
                            {
                                scoreFor = score;
                            }
                            maxLength = length;
                        }
                    }
                }
                else if (curr.Item2 == socket)
                {
                    rem.RemoveAt(i);
                    var score = Best(rem, length, curr.Item1, currScore + toAdd,
                        ref maxLength, ref scoreFor);
                    rem.Insert(i, curr);
                    if (score > best)
                    {
                        best = score;
                        if (length >= maxLength)
                        {
                            if (length > maxLength || score > scoreFor)
                            {
                                scoreFor = score;
                            }
                            maxLength = length;
                        }
                    }
                }
            }
            return best;
        }
    }
}
