using System;
using System.IO;

namespace AdventOfCode2017
{
    /// <summary>
    /// http://adventofcode.com/2017/day/11
    /// </summary>
    internal class Day11 : IDay<int, string>
    {
        public string Input
        {
            get
            {
                return File.ReadAllText("../../InputFiles/Day11.txt").Trim();
            }
        }

        public int Part1(string input)
        {
            var north = 0;
            var east = 0;

            var steps = input.Split(',');

            foreach (var step in steps)
            {
                switch (step)
                {
                    case "n":
                        north += 2;
                        break;
                    case "ne":
                        north += 1;
                        east += 1;
                        break;
                    case "se":
                        north -= 1;
                        east += 1;
                        break;
                    case "s":
                        north -= 2;
                        break;
                    case "sw":
                        north -= 1;
                        east -= 1;
                        break;
                    case "nw":
                        north += 1;
                        east -= 1;
                        break;
                    default:
                        throw new ArgumentException(step);
                }
            }

            var ret = (Math.Abs(north) + Math.Abs(east)) / 2;
            return ret;
        }

        public int Part2(string input)
        {
            var north = 0;
            var east = 0;

            var steps = input.Split(',');
            var max = 0;

            foreach (var step in steps)
            {
                switch (step)
                {
                    case "n":
                        north += 2;
                        break;
                    case "ne":
                        north += 1;
                        east += 1;
                        break;
                    case "se":
                        north -= 1;
                        east += 1;
                        break;
                    case "s":
                        north -= 2;
                        break;
                    case "sw":
                        north -= 1;
                        east -= 1;
                        break;
                    case "nw":
                        north += 1;
                        east -= 1;
                        break;
                    default:
                        throw new ArgumentException(step);
                }
                var ret = (Math.Abs(north) + Math.Abs(east)) / 2;
                if (ret > max)
                {
                    max = ret;
                }
            }

            return max;
        }
    }
}
