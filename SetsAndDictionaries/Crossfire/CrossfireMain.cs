using System;
using System.Collections.Generic;
using System.Linq;

namespace Crossfire
{
    public class CrossfireMain
    {
        public static void Main()
        {
            //TODO FIX IT, 60/100

            string input = Console.ReadLine();
            int[] dimensions = input.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            int rows = dimensions[0];
            int cols = dimensions[1];

            var matrix = new int[rows, cols];
            FillMatrix(rows, cols, matrix);

            input = Console.ReadLine();
            while (input != "Nuke it from orbit")
            {
                int[] commands = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                int targetRow = commands[0];
                int targetCol = commands[1];
                int targetRadius = commands[2];

                if (IsInsideMatrix(matrix, targetRow, targetCol))
                {
                    FireAtMatrix(matrix, targetRow, targetCol, targetRadius);
                }
                //row outside, col inside matrix
                else if (targetCol >= 0 && targetCol < matrix.GetLength(1))
                {
                    DestroyMatrixCol(matrix, targetRow, targetCol, targetRadius);
                }
                //row inside, col outside matrix
                else if (targetRow >= 0 && targetRow < matrix.GetLength(0))
                {
                    DestroyMatrixRow(matrix, targetRow, targetCol, targetRadius);
                }

                ReorderMatrix(matrix);

                input = Console.ReadLine();
            }

            PrintMatrix(matrix);
        }

        private static void DestroyMatrixRow(int[,] matrix, int targetRow, int targetCol, int targetRadius)
        {
            if (targetCol < 0)
            {
                int startCol = targetCol + targetRadius;
                for (int col = startCol; col >= 0; col--)
                {
                    if (col >= 0 && col < matrix.GetLength(1))
                    {
                        matrix[targetRow, col] = 0;
                    }
                }
            }
            if (targetCol >= matrix.GetLength(1))
            {
                int startCol = targetCol - targetRadius;
                for (int col = startCol; col < matrix.GetLength(1); col++)
                {
                    if (col >= 0 && col < matrix.GetLength(1))
                    {
                        matrix[targetRow, col] = 0;
                    }
                }
            }
        }

        private static void DestroyMatrixCol(int[,] matrix, int targetRow, int targetCol, int targetRadius)
        {
            if (targetRow < 0)
            {
                int startRow = targetRow + targetRadius;
                for (int row = startRow; row >= 0; row--)
                {
                    if (row >=0 && row < matrix.GetLength(0))
                    {
                        matrix[row, targetCol] = 0;
                    }
                }
            }
            if (targetRow >= matrix.GetLength(0))
            {
                int startRow = targetRow - targetRadius;
                for (int row = startRow; row < matrix.GetLength(0); row++)
                {
                    if (row >= 0 && row < matrix.GetLength(0))
                    {
                        matrix[row, targetCol] = 0;
                    }
                }
            }
        }

        private static void ReorderMatrix(int[,] matrix)
        {
            var elements = new Queue<int>();

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    int element = matrix[row, col];
                    if (element != 0)
                    {
                        elements.Enqueue(element);
                    }
                }

                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (elements.Count > 0)
                    {
                        matrix[row, col] = elements.Dequeue();
                    }
                    else
                    {
                        matrix[row, col] = 0;
                    }
                }

                elements.Clear();
            }
        }

        private static void FireAtMatrix(int[,] matrix, int targetRow, int targetCol, int targetRadius)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (row == targetRow && col == targetCol)
                    {
                        DestroyCells(matrix, targetRadius, row, col);
                    }
                }
            }
        }

        private static void DestroyCells(int[,] matrix, int targetRadius, int row, int col)
        {
            //center
            matrix[row, col] = 0;
            //up
            for (int i = 1; i <= targetRadius; i++)
            {
                int tRow = row - i;
                int tCol = col;

                if (IsInsideMatrix(matrix, tRow, tCol))
                {
                    matrix[tRow, tCol] = 0;
                }
                else
                {
                    break;
                }
            }
            //down
            for (int i = 1; i <= targetRadius; i++)
            {
                int tRow = row + i;
                int tCol = col;

                if (IsInsideMatrix(matrix, tRow, tCol))
                {
                    matrix[tRow, tCol] = 0;
                }
                else
                {
                    break;
                }
            }
            //left
            for (int i = 1; i <= targetRadius; i++)
            {
                int tRow = row;
                int tCol = col - i;

                if (IsInsideMatrix(matrix, tRow, tCol))
                {
                    matrix[tRow, tCol] = 0;
                }
                else
                {
                    break;
                }
            }
            //right
            for (int i = 1; i <= targetRadius; i++)
            {
                int tRow = row;
                int tCol = col + i;

                if (IsInsideMatrix(matrix, tRow, tCol))
                {
                    matrix[tRow, tCol] = 0;
                }
                else
                {
                    break;
                }
            }
        }

        private static bool IsInsideMatrix(int[,] matrix, int row, int col)
        {
            if (row < 0 || col < 0)
            {
                return false;
            }
            if (row > matrix.GetLength(0) - 1)
            {
                return false;
            }
            if (col > matrix.GetLength(1) - 1)
            {
                return false;
            }

            return true;
        }

        private static void FillMatrix(int rows, int cols, int[,] matrix)
        {
            int element = 1;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    matrix[row, col] = element;
                    element++;
                }
            }
        }

        private static void PrintMatrix(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    int element = matrix[row, col];
                    if (element != 0)
                    {
                        Console.Write($"{matrix[row, col]} ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}