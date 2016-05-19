using System;
using System.Collections.Generic;

namespace PopulationCounter
{
    public class PopulationCounterMain
    {
        public static void Main()
        {
            var populationData = new Dictionary<string, Dictionary<string, int>>();

            string input = Console.ReadLine();

            while (input != "report")
            {
                string[] data = input.Split('|');

                string country = data[1];
                string city = data[0];
                int population = int.Parse(data[2]);

                if (populationData.ContainsKey(country) == false)
                {
                    populationData[country] = new Dictionary<string, int>();
                }

                populationData[country][city] = population;

                input = Console.ReadLine();
            }



        }
    }
}