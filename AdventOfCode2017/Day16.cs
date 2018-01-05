using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017
{
    /// <summary>
    /// http://adventofcode.com/2017/day/16
    /// </summary>
    internal class Day16 : IDay<string, string>
    {
        public string Input
        {
            get
            {
                return File.ReadAllText("../../InputFiles/Day16.txt");
            }
        }

        public string Part1(string input)
        {
            var vals = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p' };

            Run(input, vals);

            return string.Join("", vals);
        }

        public string Part2(string input)
        {
            var baseVals = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p' };
            var vals = new List<char>(baseVals);
            var loop = 0;

            do
            {
                Run(input, vals);
                ++loop;
            } while (!baseVals.SequenceEqual(vals));

            var rem = 1000000000 % loop;
            for (var i = 0; i < rem; ++i)
            {
                Run(input, vals);
            }

            return string.Join("", vals);
        }

        public static void Run(string steps, List<char> vals)
        {
            int len, posA, posB;
            char tmp;

            for (var i = 0; i < steps.Length; ++i)
            {
                switch (steps[i])
                {
                    case 's':
                        len = (i + 2 < steps.Length) && (steps[i + 2] >= '0') && (steps[i + 2] <= '9') ?
                            2 : 1;
                        var jmp = int.Parse(steps.Substring(i + 1, len));

                        var front = vals.Skip(vals.Count - jmp).ToArray();
                        vals.RemoveRange(vals.Count - jmp, jmp);
                        vals.InsertRange(0, front);
                        i += len;
                        break;
                    case 'x':
                        len = (i + 2 < steps.Length) && (steps[i + 2] >= '0') && (steps[i + 2] <= '9') ?
                            2 : 1;
                        posA = int.Parse(steps.Substring(i + 1, len));
                        i += 1 + len;
                        len = (i + 2 < steps.Length) && (steps[i + 2] >= '0') && (steps[i + 2] <= '9') ?
                            2 : 1;
                        posB = int.Parse(steps.Substring(i + 1, len));
                        i += len;

                        tmp = vals[posA];
                        vals[posA] = vals[posB];
                        vals[posB] = tmp;
                        break;
                    case 'p':
                        for (posA = 0; vals[posA] != steps[i + 1]; ++posA) ;
                        for (posB = 0; vals[posB] != steps[i + 3]; ++posB) ;
                        i += 3;

                        tmp = vals[posA];
                        vals[posA] = vals[posB];
                        vals[posB] = tmp;
                        break;
                }
            }
        }
    }
}