using System;
using System.Collections.Generic;

namespace LogsAggregator
{
    public class LogsAggregatorMain
    {
        private static double WinMaterialAmount = 250;

        public static void Main()
        {
            var materials = new Dictionary<string, double>();
            var junk = new Dictionary<string, double>();

            bool wonGame = false;

            string input = Console.ReadLine();

            while (wonGame == false)
            {
                string[] elements = input.ToLower().Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < elements.Length; i+= 2)
                {
                    double quantity = double.Parse(elements[i]);
                    string material = elements[i + 1];

                    if (CheckForJunk(material))
                    {
                        AddJunk(material, quantity, junk);
                    }
                    else
                    {
                        AddMaterials(material, quantity, materials);
                        if (CheckForWinningMaterialAmount(material, materials))
                        {
                            PrintOutput(material, materials, junk);

                            wonGame = true;
                            break;
                        }
                    }
                }

                input = Console.ReadLine();
            }
        }

        private static bool CheckForJunk(string material)
        {
            if (material == "shards" || material == "fragments" || material == "motes")
            {
                return true;
            }

            return false;
        }

        private static void AddJunk(string material, double quantity, Dictionary<string, double> junk)
        {
            if (junk.ContainsKey(material))
            {
                junk[material] += quantity;
            }
            else
            {
                junk[material] = quantity;
            }
        }

        private static void AddMaterials(string material, double quantity, Dictionary<string, double> materials)
        {
            if (materials.ContainsKey(material))
            {
                materials[material] += quantity;
            }
            else
            {
                materials[material] = quantity;
            }
        }

        private static bool CheckForWinningMaterialAmount(string material, Dictionary<string, double> materials)
        {
            if (materials[material] >= WinMaterialAmount)
            {
                return true;
            }
            return false;
        }

        private static void PrintOutput(string material, Dictionary<string, double> materials, Dictionary<string, double> junk)
        {
            throw new NotImplementedException();
        }
    }
}