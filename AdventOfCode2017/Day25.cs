
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017
{
    /// <summary>
    /// http://adventofcode.com/2017/day/24
    /// </summary>
    internal class Day25 : IDay<long, string[]>
    {
        public string[] Input
        {
            get
            {
                return null;
            }
        }

        public enum State
        {
            a, b, c, d, e, f
        }

        public long Part1(string[] input)
        {
            // Hard code for input. Todo: Interpret the input
            long checkAfter = 12172063;
            var state = State.a;
            long pos = 0;

            var tape = new HashSet<long>();
            bool posVal;

            for (long i = 0; i < checkAfter; ++i)
            {
                switch (state)
                {
                    case State.a:
                        posVal = tape.Contains(pos);
                        if (!posVal)
                        {
                            tape.Add(pos);
                            ++pos;
                            state = State.b;
                        }
                        else
                        {
                            tape.Remove(pos);
                            --pos;
                            state = State.c;
                        }
                        break;
                    case State.b:
                        posVal = tape.Contains(pos);
                        if (!posVal)
                        {
                            tape.Add(pos);
                            --pos;
                            state = State.a;
                        }
                        else
                        {
                            --pos;
                            state = State.d;
                        }
                        break;
                    case State.c:
                        posVal = tape.Contains(pos);
                        if (!posVal)
                        {
                            tape.Add(pos);
                            ++pos;
                            state = State.d;
                        }
                        else
                        {
                            tape.Remove(pos);
                            ++pos;
                        }
                        break;
                    case State.d:
                        posVal = tape.Contains(pos);
                        if (!posVal)
                        {
                            --pos;
                            state = State.b;
                        }
                        else
                        {
                            tape.Remove(pos);
                            ++pos;
                            state = State.e;
                        }
                        break;
                    case State.e:
                        posVal = tape.Contains(pos);
                        if (!posVal)
                        {
                            tape.Add(pos);
                            ++pos;
                            state = State.c;
                        }
                        else
                        {
                            --pos;
                            state = State.f;
                        }
                        break;
                    case State.f:
                        posVal = tape.Contains(pos);
                        if (!posVal)
                        {
                            tape.Add(pos);
                            --pos;
                            state = State.e;
                        }
                        else
                        {
                            ++pos;
                            state = State.a;
                        }
                        break;
                }
            }

            return tape.Count;
        }

        public long Part2(string[] input)
        {
            return Part1(input);
        }
    }
}
