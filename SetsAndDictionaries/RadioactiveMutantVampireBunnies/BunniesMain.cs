using System;
using System.Linq;

namespace RadioactiveMutantVampireBunnies
{
    public class BunniesMain
    {
        public static void Main()
        {
            //TODO 50/100 - fix solution

            string commands = Console.ReadLine();
            int[] matrixSize = commands.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int rowCount = matrixSize[0];
            int columnCount = matrixSize[1];

            var matrix = new char[rowCount, columnCount];
            FillMatrix(rowCount, columnCount, matrix);

            var playerPosition = FindPlayerStartingPosition(matrix);
            int playerRow = playerPosition[0];
            int playerCol = playerPosition[1];
            int previousPlayerRow = 0;
            int previousPlayerCol = 0;

            bool hasWon = false;
            bool hasDied = false;

            commands = Console.ReadLine();

            foreach (var command in commands)
            {
                previousPlayerRow = playerRow;
                previousPlayerCol = playerCol;

                if (command == 'R')
                {
                    playerCol++;
                    matrix[previousPlayerRow, previousPlayerCol] = '.';
                }
                else if (command == 'L')
                {
                    playerCol--;
                    matrix[previousPlayerRow, previousPlayerCol] = '.';
                }
                else if (command == 'U')
                {
                    playerRow--;
                    matrix[previousPlayerRow, previousPlayerCol] = '.';
                }
                else if (command == 'D')
                {
                    playerRow++;
                    matrix[previousPlayerRow, previousPlayerCol] = '.';
                }

                if (PlayerHasEscaped(matrix, playerRow, playerCol))
                {
                    hasWon = true;
                    SpreadNewBunnies(matrix);
                    FixBunnySymbols(matrix);

                    break;
                }

                SpreadNewBunnies(matrix);
                FixBunnySymbols(matrix);

                if (matrix[playerRow, playerCol] == 'B')
                {
                    hasDied = true;
                    break;
                }
            }

            PrintMatrix(matrix);
            PrintGameResult(hasWon, hasDied, playerRow, playerCol, previousPlayerRow, previousPlayerCol);
        }

        private static void FillMatrix(int rowCount, int columnCount, char[,] matrix)
        {
            for (int row = 0; row < rowCount; row++)
            {
                string input = Console.ReadLine();

                for (int col = 0; col < columnCount; col++)
                {
                    matrix[row, col] = input[col];
                }
            }
        }

        private static int[] FindPlayerStartingPosition(char[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    char cell = matrix[row, col];

                    if (cell == 'P')
                    {
                        return new int[] { row, col };
                    }
                }
            }

            return new int[] { 0, 0 };
        }

        private static bool PlayerHasEscaped(char[,] lair, int playerRow, int playerCol)
        {
            if (playerRow > lair.GetLength(0) - 1 || playerRow < 0)
            {
                return true;
            }
            if (playerCol > lair.GetLength(1) - 1 || playerCol < 0)
            {
                return true;
            }

            return false;
        }


        private static void SpreadNewBunnies(char[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    char cell = matrix[row, col];

                    if (cell == 'B')
                    {
                        if (row > 0)
                        {
                            matrix[row - 1, col] = 'N';
                        }
                        if (row < matrix.GetLength(0) - 1)
                        {
                            matrix[row + 1, col] = 'N';
                        }
                        if (col > 0)
                        {
                            matrix[row, col - 1] = 'N';
                        }
                        if (col < matrix.GetLength(1) - 1)
                        {
                            matrix[row, col + 1] = 'N';
                        }
                    }
                }
            }
        }

        private static void FixBunnySymbols(char[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    char cell = matrix[row, col];

                    if (cell == 'N')
                    {
                        matrix[row, col] = 'B';
                    }
                }
            }
        }


        private static char[,] CopyMatrix(char[,] matrix)
        {
            char[,] matrixCopy = new char[matrix.GetLength(0), matrix.GetLength(1)];

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrixCopy[row, col] = matrix[row, col];
                }
            }

            return matrixCopy;
        }

        private static void PrintMatrix(char[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write($"{matrix[row, col]}");
                }
                Console.WriteLine();
            }
        }

        private static void PrintGameResult(bool hasWon, bool hasDied, int playerRow, int playerCol, int previousPlayerRow, int previousPlayerCol)
        {
            if (hasWon)
            {
                Console.WriteLine($"won: {previousPlayerRow} {previousPlayerCol}");
            }
            if (hasDied)
            {
                Console.WriteLine($"dead: {playerRow} {playerCol}");
            }
        }
    }
}