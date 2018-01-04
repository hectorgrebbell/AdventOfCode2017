using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace AdventOfCode2017
{
    /// <summary>
    /// http://adventofcode.com/2017/day/12
    /// </summary>
    internal class Day12 : IDay<int, IEnumerable<string>>
    {
        public IEnumerable<string> Input
        {
            get
            {
                return File.ReadAllLines("../../InputFiles/Day12.txt");
            }
        }

        public int Part1(IEnumerable<string> input)
        {
            // Build connections lookup
            var nodes = input.Select(l =>
            {
                var tokens = l.Split(' ');
                return new Tuple<int, IEnumerable<int>>(
                    int.Parse(tokens[0].Trim(',')),
                    tokens.Skip(2).Select(c => int.Parse(c.Trim(','))));
            }).GroupBy(l => l.Item1)
            .ToDictionary(g => g.Key, g => new HashSet<int>(g.SelectMany(v => v.Item2)));

            // Available nodes to explore
            var workingSet = new Queue<int>(nodes[0]);
            nodes.Remove(0);

            // Nodes reachable from node 0
            var inSet = new HashSet<int>(workingSet);
            inSet.Add(0);

            while (workingSet.Count > 0)
            {
                var next = workingSet.Dequeue();
                if (!nodes.ContainsKey(next))
                {
                    continue;
                }

                var temp = nodes[next];
                nodes.Remove(next);

                foreach (var node in temp)
                {
                    if (nodes.ContainsKey(node))
                    {
                        workingSet.Enqueue(node);
                        inSet.Add(node);
                    }
                }
            }

            return inSet.Count;
        }

        public int Part2(IEnumerable<string> input)
        {
            // Build connections lookup
            var nodes = input.Select(l =>
            {
                var tokens = l.Split(' ');
                return new Tuple<int, IEnumerable<int>>(
                    int.Parse(tokens[0].Trim(',')),
                    tokens.Skip(2).Select(c => int.Parse(c.Trim(','))));
            }).GroupBy(l => l.Item1)
            .ToDictionary(g => g.Key, g => new HashSet<int>(g.SelectMany(v => v.Item2)));

            var groups = 0;

            while (nodes.Count > 0)
            {
                ++groups;
                var id = nodes.Keys.First();
                var workingSet = new Queue<int>(nodes[id]);
                nodes.Remove(id);

                while (workingSet.Count > 0)
                {
                    var next = workingSet.Dequeue();
                    if (!nodes.ContainsKey(next))
                    {
                        continue;
                    }

                    var temp = nodes[next];
                    nodes.Remove(next);

                    foreach (var node in temp)
                    {
                        if (nodes.ContainsKey(node))
                        {
                            workingSet.Enqueue(node);
                        }
                    }
                }
            }

            return groups;
        }
    }
}
