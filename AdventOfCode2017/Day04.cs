using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017
{
    /// <summary>
    /// http://adventofcode.com/2017/day/1
    /// </summary>
    internal class Day04 : IDay<int, IEnumerable<string[]>>
    {
        public IEnumerable<string[]> Input
        {
            get
            {
                return File.ReadAllLines("../../InputFiles/Day04.txt").Select(l => l.Split());
            }
        }

        public int Part1(IEnumerable<string[]> input)
        {
            return input.Where(pp =>
            {
                var words = new HashSet<string>();
                // If we're already encountered the word then its a fail
                return !pp.Where(w => !words.Add(w)).Any();
            }).Count();
        }

        public int Part2(IEnumerable<string[]> input)
        {
            return input.Where(pp =>
            {
                var words = new HashSet<string>();
                // Same as Part 1 but sort the letters first.
                return !pp.Where(w => !words.Add(String.Concat(w.OrderBy(c => c)))).Any();
            }).Count();
        }
    }
}
