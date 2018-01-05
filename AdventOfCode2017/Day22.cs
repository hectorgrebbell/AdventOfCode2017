
using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2017
{
    /// <summary>
    /// http://adventofcode.com/2017/day/22
    /// </summary>
    internal class Day22 : IDay<int, string[]>
    {
        public string[] Input
        {
            get
            {
                return File.ReadAllLines("../../InputFiles/Day22.txt");
            }
        }

        public enum Dir : byte
        {
            Up = 0,
            Right = 1,
            Down = 2,
            Left = 3
        }

        public enum State : byte
        {
            Clean = 0,
            Weakened = 1,
            Infected = 2,
            Flagged = 3
        }

        public int Part1(string[] input)
        {
            var isInfected = new HashSet<Tuple<int, int>>();

            var centreY = (input.Length) / 2;
            var centreX = (input[0].Length) / 2;

            for (var y = 0; y < input.Length; ++y)
            {
                for (var x = 0; x < input[0].Length; ++x)
                {
                    if (input[input.Length - y - 1][x] == '#')
                    {
                        isInfected.Add(new Tuple<int, int>(y - centreY, x - centreX));
                    }
                }
            }

            var posY = 0;
            var posX = 0;
            var currDir = Dir.Up;

            var hasInfected = 0;

            for (var i = 0; i < 10000; ++i)
            {
                var key = new Tuple<int, int>(posY, posX);
                var nodeInfected = isInfected.Remove(key);

                if (nodeInfected)
                {
                    currDir = (Dir)(((int)currDir + 1) % 4);
                }
                else
                {
                    isInfected.Add(key);
                    ++hasInfected;
                    currDir = (Dir)(((int)currDir + 3) % 4);
                }

                switch (currDir)
                {
                    case Dir.Up:
                        ++posY;
                        break;
                    case Dir.Down:
                        --posY;
                        break;
                    case Dir.Left:
                        --posX;
                        break;
                    case Dir.Right:
                        ++posX;
                        break;
                }
            }

            return hasInfected;
        }

        public int Part2(string[] input)
        {
            // Initially used a dictionary to reduce space usage.
            // This seems really slow - almost certainly could be solved faster
            // just using a grid
            var isInfected = new Dictionary<Tuple<int, int>, State>();

            var centreY = (input.Length) / 2;
            var centreX = (input[0].Length) / 2;

            for (var y = 0; y < input.Length; ++y)
            {
                for (var x = 0; x < input[0].Length; ++x)
                {
                    if (input[input.Length - y - 1][x] == '#')
                    {
                        isInfected.Add(new Tuple<int, int>(y - centreY, x - centreX), State.Infected);
                    }
                }
            }

            var posY = 0;
            var posX = 0;
            var currDir = Dir.Up;

            var hasInfected = 0;

            for (var i = 0; i < 10000000; ++i)
            {
                var key = new Tuple<int, int>(posY, posX);
                var nodeState = State.Clean;

                isInfected.TryGetValue(key, out nodeState);
                var nextState = (State)(((byte)nodeState + 1) % 4);

                switch (nodeState)
                {
                    case State.Clean:
                        currDir = (Dir)(((byte)currDir + 3) % 4);
                        isInfected[key] = nextState;
                        break;
                    case State.Weakened:
                        isInfected[key] = nextState;
                        ++hasInfected;
                        break;
                    case State.Infected:
                        currDir = (Dir)(((byte)currDir + 1) % 4);
                        isInfected[key] = nextState;
                        break;
                    case State.Flagged:
                        currDir = (Dir)(((byte)currDir + 2) % 4);
                        isInfected.Remove(key);
                        break;
                }

                switch (currDir)
                {
                    case Dir.Up:
                        ++posY;
                        break;
                    case Dir.Down:
                        --posY;
                        break;
                    case Dir.Left:
                        --posX;
                        break;
                    case Dir.Right:
                        ++posX;
                        break;
                }
            }

            return hasInfected;
        }
    }
}
