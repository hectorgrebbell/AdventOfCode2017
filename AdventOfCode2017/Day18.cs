using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2017
{
    /// <summary>
    /// http://adventofcode.com/2017/day/18
    /// </summary>
    internal class Day18 : IDay<long, IEnumerable<string>>
    {
        public IEnumerable<string> Input
        {
            get
            {
                return File.ReadAllLines("../../InputFiles/Day18.txt");
            }
        }

        public long Part1(IEnumerable<string> input)
        {

            var registers = new Dictionary<string, long>();

            long lastPlayed = -1;
            long lastRecovered = -1;
            var lines = input.ToArray();

            for (long line = 0; line < lines.Length; ++line)
            {
                var opts = lines[line].Split(' ');

                var op = opts[0];
                var reg = opts[1];

                long currVal = registers.ContainsKey(reg) ?
                    registers[reg] : 0;
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
                    case "snd":
                        lastPlayed = currVal;
                        break;
                    case "set":
                        registers[reg] = otherVal;
                        break;
                    case "add":
                        registers[reg] = currVal + otherVal;
                        break;
                    case "mul":
                        registers[reg] = currVal * otherVal;
                        break;
                    case "mod":
                        registers[reg] = currVal % otherVal;
                        break;
                    case "rcv":
                        if (currVal != 0)
                        {
                            lastRecovered = lastPlayed;
                            return (int)lastRecovered;
                        }
                        break;
                    case "jgz":
                        if (currVal > 0)
                        {
                            line = line + otherVal - 1;
                        }
                        break;
                    default:
                        throw new ArgumentException(opts[5]);
                }
            }
            return -1;
        }

        public long Part2(IEnumerable<string> input)
        {
            var lines = input.Select(l => l.Split(' ')).ToArray();

            var t0registers = new Dictionary<string, long>();
            t0registers["p"] = 0;
            var t1registers = new Dictionary<string, long>();
            t1registers["p"] = 1;


            var t0Queue = new Queue<long>();
            var t1Queue = new Queue<long>();

            long t1sent1 = 0, t0Pos = 0, t1Pos = 0;

            // bit flags for each PID
            uint waiting = 0;

            while (waiting <= 2)
            {
                // task 0
                while ((waiting & 1) == 0)
                {
                    RunIter(ref t0Pos, lines[t0Pos], t0Queue, t1Queue, t0registers, ref waiting, 0);
                }
                // task 1
                while ((waiting & 2) == 0)
                {
                    t1sent1 += RunIter(ref t1Pos, lines[t1Pos], t1Queue, t0Queue, t1registers, ref waiting, 1);
                }
            }


            return t1sent1;
        }

        public static long RunIter(
            ref long pos, 
            string[] options, 
            Queue<long> rcv, Queue<long> snd,
            Dictionary<string, long> registers,
            ref uint waiting, uint pid)
        {
            var op = options[0];
            var reg = options[1];

            long currVal = 0;
            if (!long.TryParse(reg, out currVal))
            {
                currVal = registers.ContainsKey(reg) ?
                    registers[reg] : 0;
            }

            long otherVal = 0;
            if (options.Length > 2)
            {
                if (!long.TryParse(options[2], out otherVal))
                {
                    otherVal = registers.ContainsKey(options[2]) ?
                        registers[options[2]] : 0;
                }
            }

            switch (op)
            {
                case "snd":
                    snd.Enqueue(currVal);
                    waiting &= ~(pid + 2) % 2;
                    ++pos;
                    return 1;
                case "set":
                    registers[reg] = otherVal;
                    break;
                case "add":
                    registers[reg] = currVal + otherVal;
                    break;
                case "mul":
                    registers[reg] = currVal * otherVal;
                    break;
                case "mod":
                    registers[reg] = currVal % otherVal;
                    break;
                case "rcv":
                    var retryCount = 0;
                    if (rcv.Count > 0)
                    {
                        var val = rcv.Dequeue();
                        registers[reg] = val;
                    }
                    else
                    {
                        --pos;
                        waiting |= (pid + 1);
                    }
                    break;
                case "jgz":
                    if (currVal > 0)
                    {
                        pos = pos + otherVal - 1;
                    }
                    break;
            }
            ++pos;
            return 0;
        }
    }
}
