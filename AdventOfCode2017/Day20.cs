using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017
{
    /// <summary>
    /// http://adventofcode.com/2017/day/20
    /// </summary>
    internal class Day20 : IDay<int, IEnumerable<string>>
    {
        public IEnumerable<string> Input
        {
            get
            {
                return File.ReadAllLines("../../InputFiles/Day20.txt");
            }
        }

        public int Part1(IEnumerable<string> input)
        {
            var point = 0;
            return input.Select(l =>
                new Tuple<double, int>(
                    // Minimum acceleration vector. So we take the acceleration,
                    // Sqrt(x^2+y^2+z^2) and find the point for the min value

                    // If there were a tie we would need to additionally compare 
                    // initial velocity and position
                    Math.Sqrt(
                        l.Split('<').Last().Trim(new char[] { ' ', '>' }).Split(',')
                         .Select(s => { var sx = double.Parse(s); return sx * sx; })
                         .Sum()),
                    point++))
                .Min().Item2;            
        }

        public int Part2(IEnumerable<string> input)
        {
            // Current positions of all points
            var currPos = new Dictionary<Tuple<int, int, int>, int>();
            // Particles detected to have collided by not yet removed
            var collisions = new HashSet<int>();

            var points = new Dictionary<int,
                Tuple<int, int, int>[]>();

            var point = 0;

            foreach (var line in input)
            {
                var parts = line.Split('<').Skip(1)
                    .Select(p => p.Trim(new char[] { ' ', ',', '>', '=', 'v', 'a' }))
                    .Select(p =>
                    {
                        var sub = p.Split(',').ToArray();
                        return new Tuple<int, int, int>(
                            int.Parse(sub[0]), int.Parse(sub[1]), int.Parse(sub[2]));
                    }).ToArray();

                if (currPos.ContainsKey(parts[0]))
                {
                    collisions.Add(currPos[parts[0]]);
                }
                else
                {
                    currPos.Add(parts[0], point);
                    points.Add(point,
                        new Tuple<int, int, int>[] {
                            parts[0], parts[1], parts[2] });
                }

                ++point;
            }

            // Technically not a complete solution but it works
            // for this dataset and this is Advent Of Code..
            var noCollision = 0;
            while (noCollision < 50)
            {
                if (collisions.Count == 0)
                {
                    ++noCollision;
                }
                else
                {
                    foreach (var coll in collisions)
                    {
                        points.Remove(coll);
                    }
                }

                currPos = new Dictionary<Tuple<int, int, int>, int>();
                collisions = new HashSet<int>();

                foreach (var p in points)
                {

                    p.Value[1] = new Tuple<int, int, int>(
                        p.Value[1].Item1 + p.Value[2].Item1,
                        p.Value[1].Item2 + p.Value[2].Item2,
                        p.Value[1].Item3 + p.Value[2].Item3);

                    p.Value[0] = new Tuple<int, int, int>(
                        p.Value[0].Item1 + p.Value[1].Item1,
                        p.Value[0].Item2 + p.Value[1].Item2,
                        p.Value[0].Item3 + p.Value[1].Item3);

                    if (currPos.ContainsKey(p.Value[0]))
                    {
                        collisions.Add(p.Key);
                        collisions.Add(currPos[p.Value[0]]);
                    }
                    else
                    {
                        currPos.Add(p.Value[0], p.Key);
                    }
                }
            }

            return points.Count;
        }
    }
}
