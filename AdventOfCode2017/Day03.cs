
using System;

namespace AdventOfCode2017
{
    /// <summary>
    /// http://adventofcode.com/2017/day/3
    /// </summary>
    internal class Day03 : IDay<int, int>
    {
        public int Input
        {
            get { return 277678; }
        }

        public int Part1(int input)
        {
            if (input == 1) { return 0; }

            // Find the size of the grid holding the input number
            var size = 1;
            while (size * size < input)
            {
                size += 1;
            }

            // Find out where in the line the middle is
            var posOfCentre = size / 2 + ((size % 2 > 0) ? 1 : 0);

            // Find out how far the input position is from a corner.
            var fromCorner = (size * size) - input;
            if (fromCorner >= (size - 1))
            {
                fromCorner -= (size - 1);
            }
            else if (size % 2 == 0)
            {
                fromCorner += 1;
            }

            // Manhatten distance.
            var manhatten =
                (size - posOfCentre) +
                Math.Abs(size - posOfCentre - fromCorner);

            return manhatten;
        }

        public int Part2(int input)
        {
            // Just fill in the grid. Assumes 100*100 is big enough for the input.
            var squares = new int[100, 100];
            squares[50, 50] = 1;

            var posX = 50;
            var posY = 50;
            var currSizeX = 1;
            var currSizeY = 1;

            while (true)
            {
                if (posY == 50 - currSizeY + 1)
                {
                    ++posX;
                    var val =
                        squares[posX - 1, posY] +
                        squares[posX - 1, posY + 1] +
                        squares[posX, posY + 1] +
                        squares[posX + 1, posY + 1];
                    squares[posX, posY] = val;

                    if (squares[posX, posY] > input)
                    {
                        break;
                    }
                    if (posX > 50 + currSizeX - 1)
                    {
                        ++currSizeX;
                    }
                    else { continue; }
                }
                if (posX == 50 + currSizeX - 1)
                {
                    ++posY;
                    var val =
                        squares[posX, posY - 1] +
                        squares[posX - 1, posY + 1] +
                        squares[posX - 1, posY] +
                        squares[posX - 1, posY - 1];
                    squares[posX, posY] = val;

                    if (squares[posX, posY] > input)
                    {
                        break;
                    }
                    if (posY > 50 + currSizeY - 1)
                    {
                        ++currSizeY;
                    }
                    else { continue; }
                }
                if (posY == 50 + currSizeY - 1)
                {
                    --posX;
                    var val =
                        squares[posX + 1, posY] +
                        squares[posX - 1, posY - 1] +
                        squares[posX, posY - 1] +
                        squares[posX + 1, posY - 1];
                    squares[posX, posY] = val;

                    if (squares[posX, posY] > input)
                    {
                        break;
                    }
                    if (posX > 50 - currSizeX + 1)
                    {
                        continue;
                    }
                }
                if (posX == 50 - currSizeX + 1)
                {
                    --posY;
                    var val =
                        squares[posX, posY + 1] +
                        squares[posX + 1, posY - 1] +
                        squares[posX + 1, posY] +
                        squares[posX + 1, posY + 1];
                    squares[posX, posY] = val;

                    if (squares[posX, posY] > input)
                    {
                        break;
                    }
                    if (posY > 50 - currSizeY + 1)
                    {
                        continue;
                    }
                }

            }

            return squares[posX, posY];
        }
    }
}
