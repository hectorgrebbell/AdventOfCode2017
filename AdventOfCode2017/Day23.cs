
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017
{
    /// <summary>
    /// http://adventofcode.com/2017/day/23
    /// </summary>
    internal class Day23 : IDay<long, string[]>
    {
        public string[] Input
        {
            get
            {
                return File.ReadAllLines("../../InputFiles/Day23.txt");
            }
        }

        public long Part1(string[] input)
        {
            var registers = new Dictionary<string, long>();

            return Run1(input, registers);
        }

        public long Part2(string[] input)
        {
            // Looking at the input the problem is basically just how many
            // prime numbers are there when b is incremented by 17 1000 times
            var b = int.Parse(input[0].Split().Last()) * 
                int.Parse(input[4].Split().Last()) -
                int.Parse(input[5].Split().Last());

            var inc = int.Parse(input[input.Length - 2].Split().Last());

            long ret = 0;

            for (var i = 0; i <= 1000; ++i)
            {
                ret += isPrime(b);
                b += 17;
            }
            return ret;
        }

        public static long Run1(string[] lines, Dictionary<string, long> registers)
        {
            var mul = 0;
            for (long line = 0; line < lines.Length; ++line)
            {
                var opts = lines[line].Split(' ');

                var op = opts[0];
                var reg = opts[1];

                long currVal = 0;
                if (!long.TryParse(reg, out currVal))
                {
                    currVal = registers.ContainsKey(reg) ?
                        registers[reg] : 0;
                }

                long otherVal = 0;
                if (opts.Length > 2)
                {
                    if (!long.TryParse(opts[2], out otherVal))
                    {
                        otherVal = registers.ContainsKey(opts[2]) ?
                            registers[opts[2]] : 0;
                    }
                }

                switch (op)
                {
                    case "set":
                        registers[reg] = otherVal;
                        break;
                    case "sub":
                        registers[reg] = currVal - otherVal;
                        break;
                    case "mul":
                        registers[reg] = currVal * otherVal;
                        ++mul;
                        break;
                    case "jnz":
                        if (currVal != 0)
                        {
                            line = line + otherVal - 1;
                        }
                        break;
                    default:
                        throw new ArgumentException(opts[5]);
                }
            }
            return mul;
        }

        static long isPrime(int number)
        {
            if (number == 1) return 1;
            if (number == 2) return 0;

            for (int i = 2; i <= Math.Ceiling(Math.Sqrt(number)); ++i)
            {
                if (number % i == 0) return 1;
            }

            return 0;

        }
    }
}
