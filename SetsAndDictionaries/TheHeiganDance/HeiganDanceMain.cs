using System;
using System.Linq;

namespace TheHeiganDance
{
    public class HeiganDanceMain
    {
        public static void Main()
        {
            double heiganHitPoints = 3000000000;
            int playerHitPoints = 18500;

            int plagueCloudDamage = 3500;
            int eruptionDamage = 6000;

            int playerRow = 7;
            int playerCol = 7;

            int chamberSize = 15;
            string[,] matrix = new string[chamberSize, chamberSize];


            bool playerIsDead = false;
            bool heiganIsDead = false;

            bool killedByPlagueCloud = false;
            bool killedByEruption = false;

            string input = Console.ReadLine();
            double playerDamage = double.Parse(input);

            while (playerIsDead == false && heiganIsDead == false)
            {
                heiganHitPoints -= playerDamage;
                if (heiganHitPoints <= 0)
                {
                    heiganIsDead = true;
                }

                input = Console.ReadLine();
                string[] command = input.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

                string spell = command[0];
                int spellRow = int.Parse(command[1]);
                int spellCol = int.Parse(command[2]);

                if (heiganIsDead == false)
                {
                    if (spell == "Cloud")
                    {
                        HitMatrixWithCloud(matrix, spellRow, spellCol);
                    }
                    else if (spell == "Eruption")
                    {
                        HitMatrixWithEruption(matrix, spellRow, spellCol);
                    }
                }

                if (PlayerIsInAreaOfDamage(matrix, playerRow, playerCol))
                {
                    int[] newPlayerPosition = MovePlayer(matrix, playerRow, playerCol);

                    if (PlayerCannotMove(playerRow, playerCol, newPlayerPosition))
                    {
                        string cell = matrix[playerRow, playerCol];

                        if (cell.Contains("e"))
                        {
                            playerHitPoints -= eruptionDamage;
                            if (playerHitPoints <= 0)
                            {
                                playerIsDead = true;
                                killedByEruption = true;
                            }
                        }
                        if (cell.Contains("p"))
                        {
                            playerHitPoints -= plagueCloudDamage;
                            if (playerHitPoints <= 0)
                            {
                                playerIsDead = true;
                                killedByPlagueCloud = true;
                            }
                        }
                    }

                    playerRow = newPlayerPosition[0];
                    playerCol = newPlayerPosition[1];
                }

                RemoveSpellEffects(matrix);
            }

            PrintGameResult(heiganIsDead, playerIsDead, killedByPlagueCloud, killedByEruption, heiganHitPoints, playerHitPoints, playerRow, playerCol);


        }

        private static void PrintGameResult(bool heiganIsDead, bool playerIsDead, bool killedByPlagueCloud, bool killedByEruption, double heiganHitPoints, int playerHitPoints, int playerRow, int playerCol)
        {
            if (heiganIsDead)
            {
                Console.WriteLine("Heigan: Defeated!");
            }
            else
            {
                Console.WriteLine("Heigan: {0:F2}", heiganHitPoints);
            }

            if (playerIsDead)
            {
                if (killedByEruption)
                {
                    Console.WriteLine("Player: Killed by Eruption");
                }
                else
                {
                    Console.WriteLine("Player: Killed by Plague Cloud");
                }
            }
            else
            {
                Console.WriteLine($"Player: {playerHitPoints}");
            }

            Console.WriteLine($"Final position: {playerRow}, {playerCol}");
        }


        private static void RemoveSpellEffects(string[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (matrix[row, col] == null)
                    {
                        matrix[row, col] = string.Empty;
                    }

                    if (matrix[row, col].Contains("e"))
                    {
                        matrix[row, col].Replace("e", "");
                    }
                    if (matrix[row, col].Contains("p1"))
                    {
                        matrix[row, col].Replace("p1", "p2");
                    }
                    else if (matrix[row, col].Contains("p2"))
                    {
                        matrix[row, col].Replace("p2", "");
                    }
                }
            }
        }

        private static bool PlayerCannotMove(int playerRow, int playerCol, int[] newPlayerPosition)
        {
            if (playerRow == newPlayerPosition[0] && playerCol == newPlayerPosition[1])
            {
                return true;
            }
            return false;
        }

        private static int[] MovePlayer(string[,] matrix, int playerRow, int playerCol)
        {
            if (IsInsideMatrix(matrix, playerRow - 1, playerCol))
            {
                if (string.IsNullOrEmpty(matrix[playerRow - 1, playerCol]))
                {
                    return new int[] {playerRow - 1, playerCol};
                }
            }
            else if (IsInsideMatrix(matrix, playerRow, playerCol + 1))
            {
                if (string.IsNullOrEmpty(matrix[playerRow, playerCol + 1]))
                {
                    return new int[] { playerRow, playerCol + 1 };
                }
            }
            else if (IsInsideMatrix(matrix, playerRow + 1, playerCol))
            {
                if (string.IsNullOrEmpty(matrix[playerRow + 1, playerCol]))
                {
                    return new int[] { playerRow + 1, playerCol };
                }
            }
            else if (IsInsideMatrix(matrix, playerRow, playerCol - 1))
            {
                if (string.IsNullOrEmpty(matrix[playerRow, playerCol - 1]))
                {
                    return new int[] { playerRow, playerCol - 1 };
                }
            }

            return new int[] { playerRow, playerCol };
        }

        private static bool PlayerIsInAreaOfDamage(string[,] matrix, int playerRow, int playerCol)
        {
            string playerCell = matrix[playerRow, playerCol];

            if (string.IsNullOrEmpty(playerCell))
            {
                return false;

            }
            return true;
        }

        private static void HitMatrixWithEruption(string[,] matrix, int spellRow, int spellCol)
        {
            for (int row = -1; row <= 1; row++)
            {
                for (int col = -1; col <= 1; col++)
                {
                    int rowToHit = spellRow + row;
                    int colToHit = spellCol + col;

                    if (IsInsideMatrix(matrix, rowToHit, colToHit))
                    {
                        if (matrix[rowToHit, colToHit] == null)
                        {
                            matrix[rowToHit, colToHit] = string.Empty;
                        }
                        matrix[rowToHit, colToHit] += "e";
                    }
                }
            }
        }

        private static void HitMatrixWithCloud(string[,] matrix, int spellRow, int spellCol)
        {
            for (int row = -1; row <= 1; row++)
            {
                for (int col = -1; col <= 1; col++)
                {
                    int rowToHit = spellRow + row;
                    int colToHit = spellCol + col;

                    if (IsInsideMatrix(matrix, rowToHit, colToHit))
                    {
                        if (matrix[rowToHit, colToHit] == null)
                        {
                            matrix[rowToHit, colToHit] = string.Empty;
                        }
                        matrix[rowToHit, colToHit] += "p1";
                    }
                }
            }
        }

        private static bool IsInsideMatrix(string[,] matrix, int row, int col)
        {
            if (row < 0 || col < 0)
            {
                return false;
            }
            if (row >= matrix.GetLength(0))
            {
                return false;
            }
            if (col >= matrix.GetLength(1))
            {
                return false;
            }

            return true;
        }
    }
}