using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolverBacktracking
{
    class Program
    {
        static void Main(string[] args)
        {
            string strField =
            "       8 w" +
            "     4  2w" +
            "429  86  w" +
            "   2 5 37w" +
            "  1   5  w" +
            "95 4 7   w" +
            "  21  965w" +
            "5  7     w" +
            " 1       ";

            int[,] field = LoadField(strField);

            Console.WriteLine("puzzle valid: " + PuzzleValid(field));

            DrawField(field);

            Console.WriteLine("solution: ");

            SolvePuzzle((int[,])field.Clone(), ref field);

            // draw the solution
            DrawField(field);
            Console.WriteLine("puzzle valid: " + PuzzleValid(field));

            Console.ReadLine();
        }

        /// <summary>
        /// draw the _field to the console
        /// </summary>
        /// <param name="_field"></param>
        public static void DrawField(int[,] _field)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("+-------+-------+-------+");

            for (int y = 0; y < 9; y++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("| ");
                Console.ForegroundColor = ConsoleColor.White;

                for (int x = 0; x < 9; x++)
                {
                    if (_field[x, y] == -1)
                        Console.Write(" ");
                    else
                        Console.Write(_field[x, y]);

                    if (x % 3 == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(" | ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                        Console.Write(" ");
                }
                Console.WriteLine();

                if (y % 3 == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("+-------+-------+-------+");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        /// <summary>
        /// load the string into the array
        /// </summary>
        public static int[,] LoadField(string _strPuzzle)
        {
            int[,] _field = new int[9, 9];
            string[] colums = _strPuzzle.Split('w');

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    _field[x, y] = colums[y][x] - '0';

                    if (colums[y][x] == ' ')
                        _field[x, y] = -1;
                }
            }

            return _field;
        }

        public static void SolvePuzzle(int[,] _field, ref int[,] _solvedField)
        {
            SudokuAlgorithm((int[,])_field.Clone(), 0, 0, ref _solvedField);
        }

        public static bool SudokuAlgorithm(int[,] _field, int posX, int posY, ref int[,] _solvedField)
        {
            // puzzle solved!
            if (posX == 0 && posY == 9)
            {
                _solvedField = _field;
                return true;
            }

            // already filled?
            if (_field[posX, posY] != -1)
            {
                // go to the next box
                int nextX = posX != 8 ? posX + 1 : 0;
                int nextY = (posX == 8) ? posY + 1 : posY;

                if (SudokuAlgorithm((int[,])_field.Clone(), nextX, nextY, ref _solvedField))
                    return true;
            }
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    _field[posX, posY] = (i + 1);
                    if (PuzzleValid(_field))
                    {
                        int nextX = posX != 8 ? posX + 1 : 0;
                        int nextY = (posX == 8) ? posY + 1 : posY;

                        if (SudokuAlgorithm((int[,])_field.Clone(), nextX, nextY, ref _solvedField))
                            return true;
                    }
                }
            }

            return false;
        }

        public static bool PuzzleValid(int[,] _field)
        {
            // check each row
            for (int y = 0; y < 9; y++)
            {
                List<int> usedNumbers = new List<int>();
                for (int x = 0; x < 9; x++)
                {
                    if (usedNumbers.Contains(_field[x, y]) && _field[x, y] != -1)
                        return false;
                    else
                        usedNumbers.Add(_field[x, y]);
                }
            }

            // check each column
            for (int x = 0; x < 9; x++)
            {
                List<int> usedNumbers = new List<int>();
                for (int y = 0; y < 9; y++)
                {
                    if (usedNumbers.Contains(_field[x, y]) && _field[x, y] != -1)
                        return false;
                    else
                        usedNumbers.Add(_field[x, y]);
                }
            }

            // check each block
            for (int iy = 0; iy < 3; iy++)
            {
                for (int ix = 0; ix < 3; ix++)
                {
                    List<int> usedNumbers = new List<int>();

                    for (int y = 0; y < 3; y++)
                    {
                        for (int x = 0; x < 3; x++)
                        {
                            if (usedNumbers.Contains(_field[ix * 3 + x, iy * 3 + y]) && _field[ix * 3 + x, iy * 3 + y] != -1)
                                return false;
                            else
                                usedNumbers.Add(_field[ix * 3 + x, iy * 3 + y]);
                        }
                    }
                }
            }

            return true;
        }
    }
}
