
namespace AdventOfCode2017
{
    /// <summary>
    /// Represents the challenge for a single day
    /// </summary>
    /// <typeparam name="TO">Output type</typeparam>
    /// <typeparam name="TI">Input Type</typeparam>
    internal interface IDay<TO,TI>
    {
        /// <summary>
        /// Input to supply to function. This is called once
        /// and passed through to the function on each iteration.
        /// </summary>
        TI Input { get; }

        /// <summary>
        /// Solution for part 1
        /// </summary>
        /// <param name="input">Input</param>
        /// <returns></returns>
        TO Part1(TI input);

        /// <summary>
        /// Solution for part 2
        /// </summary>
        /// <param name="input">Input</param>
        /// <returns></returns>
        TO Part2(TI input);
    }
}
