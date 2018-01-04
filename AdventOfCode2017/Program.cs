using System;
using System.Diagnostics;

namespace AdventOfCode2017
{
    class Program
    {
        static void Main(string[] args)
        {
            var challenge = new Day11();
            var nIterations = 10;

            var stopWatch = Stopwatch.StartNew();
            stopWatch.Stop(); stopWatch.Reset();

            var input = challenge.Input;

            long min = long.MaxValue, max = 0, avg = 0;
            var result = challenge.Part1(input);

            for (var i = 0; i < nIterations; ++i)
            {
                stopWatch.Start();
                result = challenge.Part1(input);
                stopWatch.Stop();

                if (stopWatch.ElapsedTicks < min)
                {
                    min = stopWatch.ElapsedTicks;
                }
                else if (stopWatch.ElapsedTicks > max)
                {
                    max = stopWatch.ElapsedTicks;
                }
                avg += stopWatch.ElapsedTicks;
                stopWatch.Reset();
            }

            var minNs = (min * (1000000 / TimeSpan.TicksPerMillisecond));
            var maxNs = (max * (1000000 / TimeSpan.TicksPerMillisecond));
            var avgNs = (avg * (1000000 / TimeSpan.TicksPerMillisecond)) / nIterations;

            Console.Out.WriteLine("Part 1. Result -> {0}", result);
            Console.Out.WriteLine("Min: {0}ns, Max: {1}ns, Avg: {1}ns", minNs, maxNs, avgNs);
            Console.Out.WriteLine();

            min = long.MaxValue; max = -1; avg = 0;
            result = challenge.Part2(input);

            for (var i = 0; i < nIterations; ++i)
            {
                stopWatch.Start();
                result = challenge.Part2(input);
                stopWatch.Stop();

                if (stopWatch.ElapsedTicks < min)
                {
                    min = stopWatch.ElapsedTicks;
                }
                else if (stopWatch.ElapsedTicks > max)
                {
                    max = stopWatch.ElapsedTicks;
                }
                avg += stopWatch.ElapsedTicks;
                stopWatch.Reset();
            }

            minNs = (min * (1000000 / TimeSpan.TicksPerMillisecond));
            maxNs = (max * (1000000 / TimeSpan.TicksPerMillisecond));
            avgNs = (avg * (1000000 / TimeSpan.TicksPerMillisecond)) / nIterations;

            Console.Out.WriteLine("Part 2. Result -> {0}", result);
            Console.Out.WriteLine("Min: {0}ns, Max: {1}ns, Avg: {1}ns", minNs, maxNs, avgNs);

            Console.In.ReadLine();
        }
    }
}
