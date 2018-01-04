using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017
{
    /// <summary>
    /// http://adventofcode.com/2017/day/7
    /// </summary>
    internal class Day07 : IDay<string, IEnumerable<string>>
    {
        public IEnumerable<string> Input
        {
            get
            {
                return File.ReadAllLines("../../InputFiles/Day07.txt");
            }
        }

        public string Part1(IEnumerable<string> input)
        {
            var refs = new HashSet<string>();

            var words = new HashSet<string>(input.Select(l =>
            {
                var opts = l.Split(' ');
                foreach (var opt in opts.Skip(3))
                {
                    refs.Add(opt.Trim(','));
                }
                return opts[0];
            }));

            return words.Where(w => !refs.Contains(w)).First();
        }

        public string Part2(IEnumerable<string> input)
        {
            var words = input.Select(l =>
            {
                var opts = l.Split(' ');
                return new Tuple<string, Tuple<int, HashSet<string>>>(
                    opts[0],
                    new Tuple<int, HashSet<string>>(
                        int.Parse(opts[1].Trim(new char[] { '(', ')' })),
                        new HashSet<string>(opts.Skip(3).Select(o => o.Trim(',')))));
            }).ToDictionary(t => t.Item1, t => t.Item2);


            var cache = new Dictionary<string, int>();
            foreach (var word in words.Where(w => w.Value.Item2.Count > 1))
            {
                var costs = word.Value.Item2.Select(w => new Tuple<string, int>(w, Cost(w, words, cache)));
                var baseVal = costs.First().Item2;
                var others = costs.Where(c => c.Item2 != baseVal);

                if (others.Any())
                {
                    if (others.Skip(1).Any())
                    {
                        return (words[costs.First().Item1].Item1 - baseVal + others.First().Item2).ToString();
                    }
                    else
                    {
                        return (words[others.First().Item1].Item1 - others.First().Item2 + baseVal).ToString();
                    }
                }
            }
            return "-1";
        }

        private static int Cost(string word, Dictionary<string, Tuple<int, HashSet<string>>> lookup, Dictionary<string, int> cache)
        {
            if (cache.ContainsKey(word))
            {
                return cache[word];
            }

            var val = lookup[word];
            var ret = val.Item1 + val.Item2.Select(w => Cost(w, lookup, cache)).Sum();
            cache[word] = ret;
            return ret;
        }
    }
}
