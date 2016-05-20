using System;
using System.Collections.Generic;
using System.Linq;

namespace DragonArmy
{
    public class DragonArmyMain
    {
        public static void Main()
        {
            var dragonsData = new Dictionary<string, SortedDictionary<string, Dictionary<string, int?>>>();

            string input = Console.ReadLine();
            int rowCount = int.Parse(input);

            for (int count = 0; count < rowCount; count++)
            {
                input = Console.ReadLine();
                string[] rowData = input.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries).ToArray();

                string type = rowData[0];
                string name = rowData[1];
                int? damage = ParseNullableInt(rowData[2]);
                int? health = ParseNullableInt(rowData[3]);
                int? armor = ParseNullableInt(rowData[4]);

                if (damage == null)
                {
                    damage = 45;
                }
                if (health == null)
                {
                    health = 250;
                }
                if (armor == null)
                {
                    armor = 10;
                }

                StoreDragonsData(dragonsData, type, name, damage, health, armor);
            }

            PrintOutput(dragonsData);
        }

        private static void PrintOutput(Dictionary<string, SortedDictionary<string, Dictionary<string, int?>>> dragonsData)
        {
            var averageStats = new List<string>();

            foreach (var dragonType in dragonsData)
            {
                var damageList = new List<int?>();
                var healthList = new List<int?>();
                var armorList = new List<int?>();

                var dragonsOfCurrentType = dragonType.Value;
                foreach (var dragon in dragonsOfCurrentType)
                {
                    var dragonStats = dragon.Value;
                    int? damage = dragonStats["damage"];
                    int? health = dragonStats["health"];
                    int? armor = dragonStats["armor"];

                    damageList.Add(damage);
                    healthList.Add(health);
                    armorList.Add(armor);
                }

                string stats = string.Format("{0}::({1:F2}/{2:F2}/{3:F2})", dragonType.Key, damageList.Average(), healthList.Average(), armorList.Average());

                averageStats.Add(stats);
            }

            int counter = 0;

            foreach (var dragonType in dragonsData)
            {
                Console.WriteLine(averageStats[counter]);

                var dragonsOfCurrentType = dragonType.Value;
                foreach (var dragon in dragonsOfCurrentType)
                {
                    var dragonStats = dragon.Value;

                    string name = dragon.Key;
                    int? damage = dragonStats["damage"];
                    int? health = dragonStats["health"];
                    int? armor = dragonStats["armor"];

                    string output = String.Format("-{0} -> damage: {1}, health: {2}, armor: {3}", name, damage, health, armor);
                    Console.WriteLine(output);
                }

                counter++;
            }
        }

        private static void StoreDragonsData(Dictionary<string, SortedDictionary<string, Dictionary<string, int?>>> dragonsData, string type, string name, int? damage, int? health, int? armor)
        {
            if (dragonsData.ContainsKey(type))
            {
                if (dragonsData[type].ContainsKey(name))
                {
                    dragonsData[type][name]["damage"] = damage;
                    dragonsData[type][name]["health"] = health;
                    dragonsData[type][name]["armor"] = armor;
                }
                else
                {
                    dragonsData[type].Add(name, new Dictionary<string, int?>());
                    dragonsData[type][name].Add("damage", damage);
                    dragonsData[type][name].Add("health", health);
                    dragonsData[type][name].Add("armor", armor);
                }
            }
            else
            {
                dragonsData.Add(type, new SortedDictionary<string, Dictionary<string, int?>>());

                dragonsData[type].Add(name, new Dictionary<string, int?>());
                dragonsData[type][name].Add("damage", damage);
                dragonsData[type][name].Add("health", health);
                dragonsData[type][name].Add("armor", armor);
            }
        }

        public static int? ParseNullableInt(string input)
        {
            int num;
            if (int.TryParse(input, out num))
            {
                return num;
            }

            return null;
        }
    }
}