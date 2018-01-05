using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2017
{
    /// <summary>
    /// http://adventofcode.com/2017/day/19
    /// </summary>
    internal class Day19 : IDay<string, IEnumerable<string>>
    {
        public IEnumerable<string> Input
        {
            get
            {
                return File.ReadAllLines("../../InputFiles/Day19.txt");
            }
        }

        enum currDir
        {
            Down,
            Right,
            Up,
            Left
        }

        public string Part1(IEnumerable<string> input)
        {
            var lines = input.ToArray();

            var posY = 0;
            var posX = 0;
            var currDirection = currDir.Down;
            while (lines[0][posX] != '|') ++posX;

            var ret = new StringBuilder();

            while (true)
            {
                switch (currDirection)
                {
                    case currDir.Down:
                        if ((posY + 1) < lines.Length && lines[posY + 1][posX] != ' ')
                        { ++posY; }
                        else if ((posX + 1) < lines[posY].Length && lines[posY][posX + 1] != ' ')
                        { currDirection = currDir.Right; ++posX; }
                        else if (posX > 0 && lines[posY][posX - 1] != ' ')
                        { currDirection = currDir.Left; --posX; }
                        else { return ret.ToString(); }
                        break;
                    case currDir.Right:
                        if ((posX + 1) < lines[posY].Length && lines[posY][posX + 1] != ' ')
                        { ++posX; }
                        else if ((posY + 1) < lines.Length && lines[posY + 1][posX] != ' ')
                        { currDirection = currDir.Down; ++posY; }
                        else if (posY > 0 && lines[posY - 1][posX] != ' ')
                        { currDirection = currDir.Up; --posY; }
                        else { return ret.ToString(); }
                        break;
                    case currDir.Up:
                        if (posY > 0 && lines[posY - 1][posX] != ' ')
                        { --posY; }
                        else if ((posX + 1) < lines[posY].Length && lines[posY][posX + 1] != ' ')
                        { currDirection = currDir.Right; ++posX; }
                        else if (posX > 0 && lines[posY][posX - 1] != ' ')
                        { currDirection = currDir.Left; --posX; }
                        else { return ret.ToString(); }
                        break;
                    case currDir.Left:
                        if (posX > 0 && lines[posY][posX - 1] != ' ')
                        { currDirection = currDir.Left; --posX; }
                        else if ((posY + 1) < lines.Length && lines[posY + 1][posX] != ' ')
                        { currDirection = currDir.Down; ++posY; }
                        else if (posY > 0 && lines[posY - 1][posX] != ' ')
                        { currDirection = currDir.Up; --posY; }
                        else { return ret.ToString(); }
                        break;
                }
                if (char.IsLetter(lines[posY][posX]))
                {
                    ret.Append(lines[posY][posX]);
                }
            }
        }

        public string Part2(IEnumerable<string> input)
        {
            var lines = input.ToArray();

            var posY = 0;
            var posX = 0;
            var currDirection = currDir.Down;
            while (lines[0][posX] != '|') ++posX;

            var steps = 1;

            while (true)
            {
                switch (currDirection)
                {
                    case currDir.Down:
                        if ((posY + 1) < lines.Length && lines[posY + 1][posX] != ' ')
                        { ++posY; }
                        else if ((posX + 1) < lines[posY].Length && lines[posY][posX + 1] != ' ')
                        { currDirection = currDir.Right; ++posX; }
                        else if (posX > 0 && lines[posY][posX - 1] != ' ')
                        { currDirection = currDir.Left; --posX; }
                        else { return steps.ToString(); }
                        break;
                    case currDir.Right:
                        if ((posX + 1) < lines[posY].Length && lines[posY][posX + 1] != ' ')
                        { ++posX; }
                        else if ((posY + 1) < lines.Length && lines[posY + 1][posX] != ' ')
                        { currDirection = currDir.Down; ++posY; }
                        else if (posY > 0 && lines[posY - 1][posX] != ' ')
                        { currDirection = currDir.Up; --posY; }
                        else { return steps.ToString(); }
                        break;
                    case currDir.Up:
                        if (posY > 0 && lines[posY - 1][posX] != ' ')
                        { --posY; }
                        else if ((posX + 1) < lines[posY].Length && lines[posY][posX + 1] != ' ')
                        { currDirection = currDir.Right; ++posX; }
                        else if (posX > 0 && lines[posY][posX - 1] != ' ')
                        { currDirection = currDir.Left; --posX; }
                        else { return steps.ToString(); }
                        break;
                    case currDir.Left:
                        if (posX > 0 && lines[posY][posX - 1] != ' ')
                        { currDirection = currDir.Left; --posX; }
                        else if ((posY + 1) < lines.Length && lines[posY + 1][posX] != ' ')
                        { currDirection = currDir.Down; ++posY; }
                        else if (posY > 0 && lines[posY - 1][posX] != ' ')
                        { currDirection = currDir.Up; --posY; }
                        else { return steps.ToString(); }
                        break;
                }
                ++steps;
            }
        }
    }
}
