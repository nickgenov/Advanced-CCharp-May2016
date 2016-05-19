using System;
using System.Collections.Generic;
using System.Linq;

namespace LegendaryFarming
{
    public class LegendaryFarmingMain
    {
        private static double WinMaterialAmount = 250;

        public static void Main()
        {
            var materials = new Dictionary<string, double>();
            var junk = new Dictionary<string, double>();

            materials["shards"] = 0;
            materials["fragments"] = 0;
            materials["motes"] = 0;

            bool wonGame = false;

            string input = Console.ReadLine();

            while (true)
            {
                string[] elements = input.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < elements.Length; i += 2)
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
                            materials[material] -= WinMaterialAmount;
                            PrintOutput(material, materials, junk);

                            wonGame = true;
                            break;
                        }
                    }
                }

                if (wonGame)
                {
                    break;
                }

                input = Console.ReadLine();
            }
        }

        private static bool CheckForJunk(string material)
        {
            if (material == "shards" || material == "fragments" || material == "motes")
            {
                return false;
            }

            return true;
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
            var legendaryItem = ReturnLegendaryItemName(material);

            Console.WriteLine("{0} obtained!", legendaryItem);

            var sortedMaterials = materials
                .OrderByDescending(m => m.Value)
                .ThenBy(m => m.Key);

            foreach (var sortedMaterial in sortedMaterials)
            {
                Console.WriteLine("{0}: {1}", sortedMaterial.Key, sortedMaterial.Value);
            }

            var sortedJunk = junk.OrderBy(j => j.Key);

            foreach (var junkItem in sortedJunk)
            {
                Console.WriteLine("{0}: {1}", junkItem.Key, junkItem.Value);
            }
        }

        private static string ReturnLegendaryItemName(string material)
        {
            string legendaryItem = string.Empty;

            switch (material)
            {
                case "shards":
                    legendaryItem = "Shadowmourne";
                    break;
                case "fragments":
                    legendaryItem = "Valanyr";
                    break;
                case "motes":
                    legendaryItem = "Dragonwrath";
                    break;
                    ;
                default:
                    legendaryItem = string.Empty;
                    break;
            }

            return legendaryItem;
        }
    }
}