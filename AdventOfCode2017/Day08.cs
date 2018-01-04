
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017
{
    /// <summary>
    /// http://adventofcode.com/2017/day/8
    /// </summary>
    internal class Day08 : IDay<int, IEnumerable<string>>
    {
        public IEnumerable<string> Input
        {
            get
            {
                return File.ReadAllLines("../../InputFiles/Day08.txt");
            }
        }

        public int Part1(IEnumerable<string> input)
        {
            var registers = new Dictionary<string, int>();

            foreach (var line in input)
            {
                var opts = line.Split(' ');
                var reg = opts[0];
                var inc = opts[1] == "inc";
                var amt = int.Parse(opts[2]);

                var val1 = registers.ContainsKey(opts[4]) ?
                    registers[opts[4]] : 0;
                var val2 = registers.ContainsKey(reg) ?
                    registers[reg] : 0;

                switch (opts[5])
                {
                    case ">":
                        if (val1 > int.Parse(opts[6]))
                        {
                            registers[reg] = inc ?
                                val2 + amt : val2 - amt;
                        }
                        break;
                    case "<":
                        if (val1 < int.Parse(opts[6]))
                        {
                            registers[reg] = inc ?
                                val2 + amt : val2 - amt;
                        }
                        break;
                    case "<=":
                        if (val1 <= int.Parse(opts[6]))
                        {
                            registers[reg] = inc ?
                                val2 + amt : val2 - amt;
                        }
                        break;
                    case ">=":
                        if (val1 >= int.Parse(opts[6]))
                        {
                            registers[reg] = inc ?
                                val2 + amt : val2 - amt;
                        }
                        break;
                    case "==":
                        if (val1 == int.Parse(opts[6]))
                        {
                            registers[reg] = inc ?
                                val2 + amt : val2 - amt;
                        }
                        break;
                    case "!=":
                        if (val1 != int.Parse(opts[6]))
                        {
                            registers[reg] = inc ?
                                val2 + amt : val2 - amt;
                        }
                        break;
                    default:
                        throw new ArgumentException(opts[5]);
                }
            }
            return registers.Values.Max();
        }

        public int Part2(IEnumerable<string> input)
        {
            var registers = new Dictionary<string, int>();
            var max = 0;

            foreach (var line in input)
            {
                var opts = line.Split(' ');
                var reg = opts[0];
                var inc = opts[1] == "inc";
                var amt = int.Parse(opts[2]);

                var val1 = registers.ContainsKey(opts[4]) ?
                    registers[opts[4]] : 0;
                var val2 = registers.ContainsKey(reg) ?
                    registers[reg] : 0;

                switch (opts[5])
                {
                    case ">":
                        if (val1 > int.Parse(opts[6]))
                        {
                            registers[reg] = inc ?
                                val2 + amt : val2 - amt;
                            if (registers[reg] > max)
                            {
                                max = registers[reg];
                            }
                        }
                        break;
                    case "<":
                        if (val1 < int.Parse(opts[6]))
                        {
                            registers[reg] = inc ?
                                val2 + amt : val2 - amt;
                            if (registers[reg] > max)
                            {
                                max = registers[reg];
                            }
                        }
                        break;
                    case "<=":
                        if (val1 <= int.Parse(opts[6]))
                        {
                            registers[reg] = inc ?
                                val2 + amt : val2 - amt;
                            if (registers[reg] > max)
                            {
                                max = registers[reg];
                            }
                        }
                        break;
                    case ">=":
                        if (val1 >= int.Parse(opts[6]))
                        {
                            registers[reg] = inc ?
                                val2 + amt : val2 - amt;
                            if (registers[reg] > max)
                            {
                                max = registers[reg];
                            }
                        }
                        break;
                    case "==":
                        if (val1 == int.Parse(opts[6]))
                        {
                            registers[reg] = inc ?
                                val2 + amt : val2 - amt;
                            if (registers[reg] > max)
                            {
                                max = registers[reg];
                            }
                        }
                        break;
                    case "!=":
                        if (val1 != int.Parse(opts[6]))
                        {
                            registers[reg] = inc ?
                                val2 + amt : val2 - amt;
                            if (registers[reg] > max)
                            {
                                max = registers[reg];
                            }
                        }
                        break;
                    default:
                        throw new ArgumentException(opts[5]);
                }
            }
            return max;
        }
    }
}